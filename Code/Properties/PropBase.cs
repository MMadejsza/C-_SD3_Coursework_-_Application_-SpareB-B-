// Base class for properties, following DatabaseObjInterface interface (including people and properties). Abstract because never separately instantiated
abstract class PropBase : IDatabaseObject
{
    // Declaring set of properties held by Landlords and Tenants created based on this base
    protected Landlord owner;    
    protected Tenant? propTenant;
    protected int price;
    protected int ID;
    // Publicly accessible:
    public int luxuryLevel;

    // Declaring the state of each property to insatiate in the constructor. State type its interface
    protected IPropState propState;

    // Constructor:
    public PropBase(Landlord owner, int ID, int luxury_level, int price )
    {
        this.owner = owner;
        this.luxuryLevel = luxury_level;
        this.price = price;
        this.ID = ID;
        // Assign default state as free
        this.propState = new PropStateFree(this);
}

    // Tenant booking-handler method - calling state method to handle booking (validation in the state class)
    public void Book(Tenant tenant) => this.propState.Book(tenant);

    // Tenant releasing-handler method - calling state method to handle releasing (validation in the state class)
    public void Release(Tenant tenant) => this.propState.Release(tenant);

    // Landlord suspending-handler method - setting state to suspended (property off the market)
    public void Suspend(Landlord landlord)
    {
        // If called by property owner / proper landlord
        if (landlord.GetID() == owner.GetID()) {
            this.SetState(new PropStateSuspended(this));
        }
        else
        // Error message
        {
            Console.WriteLine($"CANNOT suspend - you are NOT the landlord.\n");
        }
    }
    // Landlord restoring-handler method - setting state to free (property back on the market)
        // Calling state method to handle restoring
    public void Restore(Landlord landlord)
    {
        // If called by property owner / proper landlord
        if (landlord.GetID() == owner.GetID())
        {
            this.propState.Restore();            
        }
        else
        // Error message
        {
            Console.WriteLine($"CANNOT restore - you are NOT the landlord.\n");
        }
    }

    //--------------Getters:--------------------------------------
    //DatabaseObjInterface interface method for giving access to property instance in database singleton
    public PropBase GetPropInstance() => this;

    // Artificial method to fulfill DatabaseObjInterface interface - null in property-case
    public PersonBase? GetPersonInstance() => null;

    // Method for DatabaseObjInterface interface - giving a descriptive name for properties as the search result
    public string GetFormattedName() => $"{this.GetType()} ID {this.GetID()}";

    // Method returning owner of the property
    public PersonBase GetOwner() => this.owner;

    // Method eventual tenant of the property or null
    public Tenant? GetTenant() => this.propTenant;

    // Method calling the owner to return his full details
    public string GetOwnerDetails() => this.owner.GetFullDetails();

    // Method calling the owner to return his formated name with ID
    public string GetOwnerName() => $"{this.owner.GetFormattedName()} (ID {this.owner.GetID()})";

    // Method returning property price
    public int GetPrice() => this.price;

    // Method returning property ID
    public int GetID() => this.ID;

    // Method returning property current state
    public IPropState GetState() => this.propState;

    // Method calling the current state of the property to get information about eventual booking
    public void GetBookingInfo() => this.propState.GetBookingInfo();

    //--------------Setters:--------------------------------------
    // Method to set the price of the property if needed
    public void SetPrice(int price) => this.price = price;

    // Method to set the ID of the property if needed
    public void SetID(int ID) => this.ID = ID;

    // Method to set a tenant of the property or release it (set tenant to null) -> handled by ternary conditional operator
    public void SetTenant(Tenant? propTenant) => this.propTenant = propTenant == null ? null : propTenant;

    // Method to set the current state of the property
    public void SetState(IPropState newState) => this.propState = newState;
}