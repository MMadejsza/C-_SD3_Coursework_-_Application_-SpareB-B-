// Base class for properties, following DatabaseObjInterface interface (including people and properties). Abstract because never separately instantiated
abstract class PersonBase : IDatabaseObject
{
    // Declaring set of properties held by Landlords and Tenants created based on this base
    protected string ID;
    protected string firstName;
    protected string lastName;
    protected string phone;
    protected bool premiumMember;
    protected bool redFlag;
    // Publicly accessible:
    public string e_mail;

    // Passing access to instance of singleton database (only 1 exists)
    protected Database database = Database.getInstance();

    // Constructor:
    public PersonBase(string firstName, string lastName, string e_mail, string phone, bool premium_member, bool red_flag, string ID)
    {
        this.ID = ID;
        this.firstName = firstName;
        this.lastName = lastName;
        this.e_mail = e_mail;
        this.phone = phone;
        this.premiumMember = premium_member;
        this.redFlag = red_flag;
    }

    //--------------Getters:--------------------------------------
    // Method providing all person details formatted in a block of information
    public virtual string GetFullDetails()
    {
        return
            $"-------------------------------------------\n" +
            $"ID: {this.GetID()}\n" +
            $"Name: {this.GetFormattedName()}\n" +
            $"Phone: {this.GetPhoneString()}\n" +
            $"E-mail: {this.e_mail}\n" +
            $"Premium?: {this.IsPremium()}\n" +
            $"Flagged?: {this.IsRedFlagged()}\n" +
            $"-------------------------------------------";
    }
    // Method (using expression body) providing more specifically full name
    public virtual string GetFormattedName() => $"{this.firstName} {this.lastName}";
    // Method providing more specifically phone number
    public virtual string GetPhoneString() => this.phone;
    // Method providing more specifically user ID 
    public virtual string GetID() => this.ID;
    // Method providing more specifically if user is premium
    public virtual bool IsPremium() => this.premiumMember;
    // Method providing more specifically if user is flagged
    public virtual bool IsRedFlagged() => this.redFlag;
    // Method to give access to it's instance - used through database objects interface in database search method for example
    public PersonBase GetPersonInstance() => this;
    // Artificial method to fulfill interface requirement (we have 1 interface for properties and persons in database)
    public PropBase? GetPropInstance() => null;

    //--------------Setters:--------------------------------------
    // Method to change the number
    public virtual void SetPhoneNumber(string phoneNumber) => this.phone = phoneNumber;
    // Method to change ID if needed
    public void SetID(string ID) => this.ID = ID;
    // Method to change user status - combination of flag and membership
    public virtual void SetStatus(bool membership, bool flagged = false)
    {
        this.premiumMember = membership;
        this.redFlag = flagged;
    }
}