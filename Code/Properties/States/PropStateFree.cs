// Base class for properties, following IPropState interface 
class PropStateFree : IPropState
{
    // Declaring state's property for access
    private readonly PropBase property;

    // Constructor
    // When this state is instantiated, it assigns proper property for access
    public PropStateFree(PropBase property)
    {
        this.property = property;
    }

    // Tenant final booking-handler - at this state -> calling propertys SetState to change to "booked" + user message
    public void Book(Tenant tenant)
    {        
        this.property.SetState(new PropStateBooked(this.property));
        Console.WriteLine($"\n{this.property.GetType()} (ID: {this.property.GetID()}) booked! Enjoy your stay {tenant.GetFormattedName()}!");
        this.property.SetTenant(tenant);
    }

    // Tenant final Release-handler - at this state no action -> error message
    public void Release(PersonBase person) => Console.WriteLine($"\nThis {this.property.GetType()} (ID: {this.property.GetID()}) is ALREADY free! Nothing to release.");

    // Property GetBookingInfo() - handler -> at this state -> describes the states pulling out the property and its ID
    public void GetBookingInfo() => Console.WriteLine($"\nThis {this.property.GetType()} (ID: {this.property.GetID()}) is FREE!");

    // Landlord final Restore-handler - at this state no action -> error message
    public void Restore() => Console.WriteLine($"\nThis {this.property.GetType()} is live - NOTHING to restore.");

}