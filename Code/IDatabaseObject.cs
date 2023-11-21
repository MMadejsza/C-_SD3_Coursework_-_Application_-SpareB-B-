// Interface for all objects occurring in the database making sure that these methods are present
interface IDatabaseObject
{
    public string GetFormattedName();
    public PropBase GetPropInstance();
    public PersonBase GetPersonInstance();
}