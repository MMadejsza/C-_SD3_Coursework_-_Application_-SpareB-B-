public class Room : Property_base
{
    public bool smoke_alarm;
    public bool separate_entrance;
    public string type;
    public bool private_key;
    public string host_language;

    public Room(Landlord owner, 
        bool engagement, 
        int luxury_level, 
        int price, 
        int ID, 
        bool smoke_alarm, 
        bool separate_entrance, 
        string type, 
        bool private_key, 
        string host_language) 
        : base(owner, engagement,luxury_level, price, ID)
    {
        this.smoke_alarm = smoke_alarm;
        this.separate_entrance = separate_entrance;
        this.type = type;
        this.private_key = private_key;
        this.host_language = host_language;         
    }     
}