// House property class extending ProBase class
class House : PropBase
{
    // Set of exclusive properties not inherited from the base class
    public int numberOfRooms;
    public bool selfCheckIn;
    public bool disableFriendly;
    public bool garden;

    // Constructor
    public House(Landlord owner,
        int ID, 
        int luxury_level, 
        int price, 
        int number_of_rooms, 
        bool self_check_in, 
        bool disable_friendly, 
        bool garden) 
        : base(owner, ID, luxury_level, price)
    {
        this.numberOfRooms = number_of_rooms;
        this.selfCheckIn = self_check_in;
        this.disableFriendly = disable_friendly;
        this.garden = garden;  
    }     
}