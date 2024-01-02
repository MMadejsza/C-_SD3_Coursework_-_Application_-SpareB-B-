// Room property class extending ProBase class
class Room : PropBase
{
    // Set of exclusive properties not inherited from the base class
    public bool smokeAlarm;
    public bool separateEntrance;
    public string type;
    public bool privateKey;
    public string hostLanguage;

    // Constructor
    public Room(
        Landlord owner,  
        int ID, 
        int luxury_level, 
        int price, 
        bool smoke_alarm, 
        bool separate_entrance, 
        string type, 
        bool private_key, 
        string host_language) 
        : base(owner, ID, luxury_level, price)
    {
        this.smokeAlarm = smoke_alarm;
        this.separateEntrance = separate_entrance;
        this.type = type;
        this.privateKey = private_key;
        this.hostLanguage = host_language;         
    }
    public override string GetFullDetails() =>
            $"------------------{this.GetType()} {this.GetID()}-----------------------\n" +
            $"ID: {this.GetID()}\n" +
            $"Owner: {this.GetOwner().GetFormattedName()}\n" +
            $"Luxury Lvl: {this.luxuryLevel}\n" +
            $"Price: {this.GetPrice()}\n" +
            $"Type: {this.type}\n" +
            $"Host language: {this.hostLanguage}\n" +
            $"Smoke alarm: {(this.smokeAlarm == true ? "Yes" : "No")}\n" +
            $"Separate entrance: {(this.separateEntrance == true ? "Yes" : "No")}\n" +
            $"Private key: {(this.privateKey == true ? "Yes" : "No")}\n" +            
            $"State: {this.GetBookingInfo()}\n";
}
