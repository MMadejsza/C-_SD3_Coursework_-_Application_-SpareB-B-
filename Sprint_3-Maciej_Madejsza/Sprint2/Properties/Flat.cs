// Flat property class extending ProBase class
class Flat : PropBase
{
    // Set of exclusive properties not inherited from the base class
    public int numberOfRooms;
    public bool selfCheckIn;
    public bool disableFriendly;
    public bool separateEntrance;

    // Constructor
    public Flat(Landlord owner, 
        int ID, 
        int luxury_level, 
        int price, 
        int number_of_rooms, 
        bool self_check_in, 
        bool disable_friendly, 
        bool separate_entrance) 
        : base(owner, ID, luxury_level, price)
    {
        this.numberOfRooms = number_of_rooms;
        this.selfCheckIn = self_check_in;
        this.disableFriendly = disable_friendly;
        this.separateEntrance = separate_entrance;  
    }
    public override string GetFullDetails() =>
            $"------------------{this.GetType()} {this.GetID()}-----------------------\n" +
            $"ID: {this.GetID()}\n" +
            $"Owner: {this.GetOwner().GetFormattedName()}\n" +
            $"Luxury Lvl: {this.luxuryLevel}\n" +
            $"Price: {this.GetPrice()}\n" +
            $"Rooms number: {this.numberOfRooms}\n" +
            $"Self Check-In: {(this.selfCheckIn==true?"Yes":"No")}\n" +
            $"Disabled friendly: {(this.disableFriendly == true ? "Yes" : "No")}\n" +
            $"Separate entrance: {(this.separateEntrance == true ? "Yes" : "No")}\n" +
            $"State: {this.GetBookingInfo()}\n";
}