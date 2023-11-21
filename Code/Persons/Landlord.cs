// : notation -> inhering from PersonBase
class Landlord : PersonBase
{    
    //Constructor (no any new properties so all are fetched from base class)
    public Landlord(string firstName, string lastName, string e_mail, string phone, bool premium_member, bool red_flag, string ID) : base(firstName, lastName, e_mail, phone, premium_member, red_flag, ID)
    { }

    // Landlord-only suspension feature changing state of the property to "suspended"
        // call suspend method from the property (its the base class) giving landlord instance for validation
    public void SuspendProperty(PropBase property) => property.Suspend(this);

    // Landlord-only restoring feature changing state of the property from "suspended" to "free"
        // call restore method from the property (its base class) giving landlord instance for validation
    public void RestoreProperty(PropBase property) => property.Restore(this);

    // Landlord-only adding feature, adding the property to the database collection of properties
    public void AddProperty(PropBase property)
    {
        // Valid only for the owner of the property:
        if (property.GetOwner().GetID() == this.GetID())
        {
            database.Add(property);
        }
        // Error message when foreign landlord:
        else
        {
            Console.WriteLine($"You CANNOT add someone else's property!\n");
        }
    }
    // Landlord-only removing feature, removing property from the database collection of properties
    public void RemoveProperty(PropBase property)
    {
        // Valid only for the owner of the property:
        if (property.GetOwner().GetID() == this.GetID())
        {
            database.Remove(property);
        }
        // Error message when foreign landlord:
        else
        {
            Console.WriteLine($"\nYou CANNOT remove someone else's property!");
        }
    }

    //----------Getters:-----------------------------

    // Method for easier access for Landlords - listing their own properties only. (Admin user have full searching options)
        // Calling database (singleton) search method
    public void GetProperties() => database.Search("properties", "person", this);
}