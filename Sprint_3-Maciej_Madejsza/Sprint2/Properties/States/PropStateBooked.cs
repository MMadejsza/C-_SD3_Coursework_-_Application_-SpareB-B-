// Base class for properties, following IPropState interface 
using System;

class PropStateBooked : IPropState
{
    // Declaring state's property for access
    private readonly PropBase property;

    // Constructor
    // When this state is instantiated, it assigns proper property for access
    public PropStateBooked(PropBase property)
    {
        this.property = property;
    }

    // Tenant final booking-handler - at this state no action -> error message
    public string Book(Tenant tenant) => $"This {this.property.GetType()} (ID: {this.property.GetID()}) is ALREADY booked - try again later.";

    // Tenant final Release-handler - at this state -> calling propertys SetState to change to "free" + user message
    public string Release(PersonBase person)
    {
        // Validation that only booked tenant can release the property
        if (person.GetID() != this.property.GetTenant().GetID())
        {
            Console.WriteLine($"\nCANNOT release - you are NOT the tenant.\n");
            return $"CANNOT release - you are NOT the tenant.";
        }
        // If passed
        else
        {
            Console.WriteLine($"\nYou released the {this.property.GetType()} (ID {this.property.GetID()}, Landlord: {this.property.GetOwnerName()})\nSee you later!");
            this.property.SetState(new PropStateFree(this.property));
            this.property.SetTenant(null);
            return $"You released the {this.property.GetType()} (ID {this.property.GetID()}, Landlord: {this.property.GetOwnerName()})\nSee you later!";

        }
    }
    // Property GetBookingInfo() - handler -> at this state -> describes the states pulling out the property, its ID and tenant data
    public string GetBookingInfo()
    {
        Console.WriteLine($"\nThis {this.property.GetType()} (ID: {this.property.GetID()}) is BOOKED by {this.property.GetTenant().GetFormattedName()}!");
        return $"This {this.property.GetType()} (ID: {this.property.GetID()}) is BOOKED by {this.property.GetTenant().GetFormattedName()}!";
    }
    // Landlord final Restore-handler - at this state no action -> error message
    public string Restore() => $"This {this.property.GetType()} is live - NOTHING to restore.";
}