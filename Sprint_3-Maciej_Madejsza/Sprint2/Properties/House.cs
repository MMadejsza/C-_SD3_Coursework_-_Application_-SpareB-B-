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
    public override string GetFullDetails() =>
            $"------------------{this.GetType()} {this.GetID()}-----------------------\n" +
            $"ID: {this.GetID()}\n" +
            $"Owner: {this.GetOwner().GetFormattedName()}\n" +
            $"Luxury Lvl: {this.luxuryLevel}\n" +
            $"Price: {this.GetPrice()}\n" +
            $"Rooms number: {this.numberOfRooms}\n" +
            $"Self Check-In: {(this.selfCheckIn == true ? "Yes" : "No")}\n" +
            $"Disabled friendly: {(this.disableFriendly == true ? "Yes" : "No")}\n" +
            $"Garden: {(this.garden == true ? "Yes" : "No")}\n" +
            $"State: {this.GetBookingInfo()}\n";
}