// Base class for properties, following IPropState interface 
class PropStateSuspended : IPropState
{
    // Declaring state's property for access
    private readonly PropBase property;

    // Constructor
    // When this state is instantiated, it assigns proper property and calls the reset method
    public PropStateSuspended(PropBase property)
    {
        this.property = property;
        this.Reset();
    }

    // Tenant final booking-handler - at this state no action -> error message
    public void Book(Tenant tenant) => Console.WriteLine($"\nThis {this.property.GetType()} (ID: {this.property.GetID()}) is OFF the market - try to book again later.");
    
    // Tenant final Release-handler - at this state no action -> error message
    public void Release(PersonBase person) => Console.WriteLine($"\nThis {this.property.GetType()} (ID: {this.property.GetID()}) is OFF the market - try to release when booked.");
    
    // Property GetBookingInfo() - handler -> at this state no action -> error message
    public void GetBookingInfo() => Console.WriteLine($"\nThis {this.property.GetType()} (ID: {this.property.GetID()}) has been suspended by landlord and is UNAVAILABLE.");

    // Landlord final Restore-handler - at this state -> calling propertys SetState to change back to "free" + user message
    public void Restore()
    {
        this.property.SetState(new PropStateFree(this.property));
        Console.WriteLine($"\nThis {this.property.GetType()} (ID: {this.property.GetID()}) is BACK on the market.");
    }

    // State specific method to remove tenant from the property and returning action message
    public void Reset()
    {
        this.property.SetTenant(null);
        Console.WriteLine($"\n{this.property.GetType()} (ID: {this.property.GetID()}) has been suspended by landlord - all tenants has been removed.");
    }
}