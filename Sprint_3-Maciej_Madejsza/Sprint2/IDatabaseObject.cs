// Interface for all objects occurring in the database making sure that these methods are present
interface IDatabaseObject
{
    public string GetFormattedName();
    public PropBase GetPropInstance();
    public PersonBase GetPersonInstance();
    public string GetID();
    public string GetFullDetails();
    string Suspend(Landlord personForActionSelection);
    string Restore(Landlord personForActionSelection);
    string Book(Tenant personForActionSelection);
    string Release(Tenant personForActionSelection);
}