public class House : Property_base
{
    public int number_of_rooms;
    public bool self_check_in;
    public bool disable_friendly;
    public bool garden;

    public House(Landlord owner, 
        bool engagement, 
        int luxury_level, 
        int price, 
        int ID, 
        int number_of_rooms, 
        bool self_check_in, 
        bool disable_friendly, 
        bool garden) 
        : base(owner, engagement,luxury_level, price, ID)
    {
        this.number_of_rooms = number_of_rooms;
        this.self_check_in = self_check_in;
        this.disable_friendly = disable_friendly;
        this.garden = garden;  
    }     
}