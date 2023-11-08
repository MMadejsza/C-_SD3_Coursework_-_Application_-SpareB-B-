public class Landlord : Person_base
{
    private int landlord_ID;
    //DO IT IN SINGLETON CONTAINING ALL THE PROPERTIES AND THAN YOU CAN PROCES/SORT THEM GETTING THE SAME SOLUTION REULT LIKE FROM TRYING TO STORE THE LIST WITHIN THE LANDLORD CLASS
    //private List<Property> Properties;

    public Landlord (string firstName, string lastName, string e_mail, string phone, bool premium_member, bool red_flag, int ID) : base(firstName, lastName, e_mail, phone, premium_member, red_flag)
    {
        this.landlord_ID = ID;
    }
    
    //----------Getters:-----------------------------
    public int getID()
    {
        return this.landlord_ID;
    }      

    //public void getFormattedProperties()
    //{
    //    foreach (Property prop in this.Properties)
    //    {
    //        return $"Property: {prop.getID}\nAddress: {}\nType: {}\nOwwner: {}"
    //    }
    //}

    //----------Setters:-----------------------------
    public void setID(int ID)
    {
        this.landlord_ID = ID;
    }
    
    //public void addProperty(Property property)
    //{
    //    this.Properties.Add(property);
    //}

}