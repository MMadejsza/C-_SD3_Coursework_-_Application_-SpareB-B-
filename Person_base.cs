abstract public class Person_base
{
    protected string firstName;
    protected string lastName;
    //protected int ID;
    //protected DateTime DoB; // DO iT AS STRING
    protected string phone;
    protected bool premium_member;
    protected bool red_flag;
    public string e_mail;


    // MAKE CLASSES FOR IT - SEPARATE INSTANCES DEOSNT BOTHER US
    //protected struct BankDetails
    //{
    //        protected int account_number;
    //        protected int sort_code;
    //
    //        public BankDetails(int account_number, int sort_code)
    //        {
    //            account_number=account_number;
    //            sort_code=sort_code;    
    //        }
    //};

    //protected struct Address
    //{
    //        protected int number;
    //        protected string city;
    //        protected string state;
    //        protected string post_code;
    //        protected string country;
    //
    //        public Address (int number , string city, string state, string post_code, string country)
    //        {
    //            number=number;
    //            city=city;
    //            state=state;
    //            post_code=post_code;
    //            country=country;            
    //        }
    //};

    public Person_base(string firstName, string lastName, string e_mail, /*int ID,*/ string phone, bool premium_member, bool red_flag)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.e_mail=e_mail;
        //this.ID=ID;
        //this.DoB;
        this.phone=phone;
        this.premium_member=premium_member;
        this.red_flag=red_flag;
}
    
    //--------------Getters:--------------------------------------
    public virtual string getFromattedName()
    {
        return $"{this.firstName} {this.lastName}";
    }

    public virtual string getPhoneString()
    {    
        return this.phone;
    }

    //public virtual int getID()
    //{
    //    return this.ID;
    //}

    public virtual bool isPremium()
    {
        return this.premium_member;
    }

    public virtual bool isRedFlagged()
    {
        return this.red_flag;
    }

    //--------------Setters:--------------------------------------
    public virtual void setPhoneNumber(string phoneNumber)
    {
        this.phone = phoneNumber;
    }
    //public void setID(int ID)
    //{
    //    this.ID = ID;
    //}
    public virtual void setStatus(bool membership, bool flagged = false)
    {
        this.premium_member = membership;
        this.red_flag = flagged;
    }

}