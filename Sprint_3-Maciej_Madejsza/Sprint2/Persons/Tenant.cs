// : notation -> inhering from PersonBase
class Tenant : PersonBase
{
    // Constructor (no any new properties so all are fetched from the base class)
    public Tenant(string firstName, string lastName, string e_mail, string phone, bool premium_member, bool red_flag, string ID) : base(firstName, lastName, e_mail, phone, premium_member, red_flag, ID)
    {}

    // Tenant-only booking feature changing state of the property to "booked"
    // call booking method from the property (its the base class) giving tenant instance for validation
    public string Book(PropBase property) => property.Book(this);

    // Tenant-only releasing feature changing state of the property to "free"
    // call releasing method from the property (its base class) giving tenant instance for validation
    public string Release(PropBase property) => property.Release(this);

    //----------Getters:-----------------------------
    //Method for easier access for tenants - listing their own bookings only. (Admin user have full searching options)
        // Calling database (singleton) search method
    public void GetBookings() => database.Search("properties", "personToListBy", this);
}