abstract public class Property_base
{
    protected Landlord owner;    
    protected int price;
    protected int ID;
    //private List<Tenant> tenants;
    public bool engagement;
    public int luxury_level;

    public Property_base(Landlord owner, bool engagement, int luxury_level, int price, int ID /*List<Tenant> tenants*/)
    {
        this.owner = owner;
        this.engagement = engagement;
        this.luxury_level = luxury_level;
        this.price = price;
        this.ID = ID;
        //this.tenants = tenants;
    }
    
    //--------------Getters:--------------------------------------
    public virtual string getOwner()
    {
        return
            $"-------------------------------------------\n" +
            $"ID: {this.owner.getID()}\n" +
            $"Name: {this.owner.getFromattedName()}\n" +
            $"Phone: {this.owner.getPhoneString()}\n" +
            $"E-mail: {this.owner.e_mail}\n" +
            $"Premium?: {this.owner.isPremium()}\n" +
            $"Flagged?: {this.owner.isRedFlagged()}\n" +
            $"-------------------------------------------";
    }

    public virtual int getPrice()
    {    
        return this.price;
    }

    public virtual int getID()
    {
        return this.ID;
    }
     //From singleton
    //public void getTenants()
    //{
    //    foreach (Tenant t in this.tenants)
    //    {
    //        return $"{index+1} Tenant: {t.getID}\nName: {}\n"
    //    }
    //}

    //--------------Setters:--------------------------------------
    public virtual void setPrice(int price)
    {
        this.price = price;
    }
    public void setID(int ID)
    {
        this.ID = ID;
    }
    //To  singleton? maybe fetch from singleton searched on property id? but in property object - not property base
    //public void addProperty(Tenant tenant)
    //{
    //    this.tenants.Add(tenant);
    //}
}