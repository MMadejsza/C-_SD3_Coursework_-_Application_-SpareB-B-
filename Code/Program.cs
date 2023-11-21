//Student: Maciej Mateusz Madejsza
//ID: MAD21541198
//Date: 19/11/2023
//Project: Coursework 2 - Sprint 2
//Target: Higher marks - enhanced version


/* Description:
    -This Sprint 2 covers all basic requirements
    -For higher marks:
        - User - there are 3 types of users with different privileges : Admin, Landlord and Tenant
        - Design patterns used:
          State pattern:to achieve smooth state instances of the properties and to be able to flexibly implement booking and suspension features (extra). 
                        Booking, releasing, and suspending change property state
          Singleton:    to achieve 1 common database and monitor the occurrence of objects. That allowed to implement listing of particular landlord or tenant properties, 
                        filter already instantiated object and validation based on for example repetition across the program
        - Various validation
    -Sections can be collapsed for convenient screening
*/
class Program
{
    static void Main()
    {
        //Singleton "database" instantiation 
        Database database = Database.getInstance();

        //Lanlords instantiation:
        Landlord lis = new Landlord(
            "Lisa",
            "Haskel",
            "Haskel@gmail.com",
            "07784882823",
            true,
            false,
            "01");
        Landlord matt = new Landlord(
           "Maciej",
           "Madejsza",
           "maciej.madejsza@gmail.com",
           "07784882823",
           false,
           false,
           "02");
        Landlord mom = new Landlord(
           "Momina",
           "Shaheen",
           "Momina@gmail.com",
           "07784882823",
           false,
           true,
           "03");
        Landlord jonIn = new Landlord(
           "Jon",
           "Inactive",
           "Jon@gmail.com",
           "07784882823",
           false,
           true,
           "04");
        Landlord ramIn = new Landlord(
          "Ram",
          "Inactive",
          "Ram@gmail.com",
          "07784882823",
          false,
          true,
          "06");
        Landlord elaIn = new Landlord(
           "Ela",
           "Inactive",
           "Ela@gmail.com",
           "07784882823",
           false,
           true,
           "05");
        //END Lanlords instatioation
        //Tenants instantiation:
        Tenant adrian = new Tenant(
            "Adrian",
            "Kowalski",
            "Kowalski@gmail.com",
            "07784882823",
            true,
            false,
            "04");

        Tenant iza = new Tenant(
           "Iza",
           "Nowak",
           "Nowak@gmail.com",
           "07784882823",
           false,
           false,
           "05");
        Tenant dominika = new Tenant(
           "Dominika",
           "Wygoda",
           "Wygoda@gmail.com",
           "07784882823",
           false,
           true,
           "06");
        Tenant marko = new Tenant(
           "Marko",
           "Inactive",
           "int1@gmail.com",
           "07784882823",
           false,
           true,
           "07");
        Tenant arek = new Tenant(
           "Arek",
           "Inactive",
           "int2@gmail.com",
           "07784882823",
           false,
           true,
           "08");
        //END Tenants instatioation
        //Properties instantiation:
        Room room34 = new Room(matt, 34, 3, 275,  true, true, "Office", true, "Polish");
        Room room35 = new Room(matt, 35, 5, 385, true, true, "Standard", true, "Polish");
        Room room36 = new Room(lis, 36, 5, 495, true, true, "Standard", true, "Polish");
        Flat flat01 = new Flat(matt, 01, 5, 3, 590,  true, false, false);
        Flat flat02 = new Flat(lis, 02, 5, 3, 690, true, false, false);
        Flat flat03 = new Flat(mom, 03, 5, 3, 790,  true, false, false);
        House house11 = new House(lis, 11, 6, 5, 1190,  true, true, true);
        House house12 = new House(lis, 12, 6, 5, 1290, true, true, true);
        House house13 = new House(mom, 13, 6, 5, 1390, true, true, true);

        //END Properties instatioation
        //------------Admin------------------
        Console.WriteLine($"------------Admin actions:------------------");
        //Adding landlord to database:
        Console.WriteLine($"***Adding landlords***");
        database.Add(mom);      
        database.Add(matt);                         // adding landlord
        database.Add(matt);                         // the same user validation
        database.Add(lis);
        database.Add(jonIn);
        database.Add(elaIn);
        database.Add(ramIn);
        Console.WriteLine($"***Listing after adding landlords***");
        database.Search("landlords", "all");        // listing all landlords from database after adding set

        Console.WriteLine($"***Removing landlords***");
        //Removing landlord from database:
        database.Remove(matt);                      // removing landlord when not active (no engagement)
        database.Remove(elaIn);
        Console.WriteLine($"***Listing after removing landlords + adding back Maciej***");
        database.Search("landlords", "all");        // listing all landlords from database after removing set
        database.Add(matt);                         

        Console.WriteLine($"***Adding tenants***");
        //Adding tenants to database:
        database.Add(adrian);
        database.Add(iza);
        database.Add(dominika);
        database.Add(marko);
        database.Add(arek);
        Console.WriteLine($"***Listing after adding tenants***");
        database.Search("tenants", "all");           // listing all tenants from database after adding set
        Console.WriteLine($"***Removing tenants***");
        database.Remove(iza);
        Console.WriteLine($"***Listing after removing tenants***");
        database.Search("tenants", "all");           // listing all tenants from database after removing set

        //Adding/removing properties to database:
        Console.WriteLine($"***Actions for searching***");
        database.Add(room34);                         
        dominika.Book(room34);                       // ahead tenant booking for admin features showcase

        //Advanced searching:
            //Admin can search:
                //Property - All,Free,Booked, belonging to particular landlord or hosting particular tenant
                //Landlords - All,Active,Premium, Flagged
                //Tenants - All,Active,Premium, Flagged
        Console.WriteLine($"***Searching***");
        database.Search("properties", "person", matt);
        database.Search("properties", "person", dominika);
        database.Search("tenants", "active");
        database.Search("tenants", "all");
        database.Search("properties", "booked");
        database.Search("properties", "free");
        Console.WriteLine($"***Removing tenants with validation ***");
        database.Remove(dominika);                  //validation - removing active tenant/user

        //-----------ADMIN END-------------------

        //----------Landlord-----------------
        Console.WriteLine($"------------Landlord actions:------------------");
        //Adding porperties:
        Console.WriteLine($"***Adding properties***");
        matt.AddProperty(room35);
        matt.AddProperty(room34);                   // validation - property exists
        matt.AddProperty(flat01);
        lis.AddProperty(room36);
        lis.AddProperty(flat02);
        lis.AddProperty(house11);
        lis.AddProperty(house12);
        mom.AddProperty(room36);                    // validation - can't add foreign property (matt's)
        mom.AddProperty(flat03);
        mom.AddProperty(house13);

        Console.WriteLine($"***Removing properties***");
        //Removing porperties:
        lis.RemoveProperty(flat02);
        mom.RemoveProperty(house12);                // validation - can't add foreign property (lisa's)

        database.Search("properties", "free");

        Console.WriteLine($"***Suspending and restoring properties***");
        //Suspending and restoring porperties:
        matt.SuspendProperty(room34);               // disables property from any action + removes tenants
            //to confirm previous action:
            Console.WriteLine($"Tenant: {(room34.GetTenant() == null ? "empty." : room34.GetTenant().GetFormattedName())}");
            database.Search("properties", "person", dominika);  //to confirm previous action:
            dominika.Book(room34);
        matt.RestoreProperty(room34);
        matt.RestoreProperty(room35);               // validation - property is live
        dominika.Book(room34);

        Console.WriteLine($"***Listing properties by landlords***");
        //Listing porperties:
        matt.GetProperties();
        lis.GetProperties();
        mom.GetProperties();
        jonIn.GetProperties();

        //---------Landlord END-------------

        //-----------Tenant------------------
        Console.WriteLine($"------------Tenant actions:------------------");
        Console.WriteLine($"***Bookings***");
        //Book property
        adrian.Book(flat03);
        adrian.Book(house12);
        dominika.Book(room35);
        Console.WriteLine($"***Realeasing***");
        //Release property
        adrian.Release(house12);
        Console.WriteLine($"***Listing bookings by tenants***");
        //Get all bookings
        adrian.GetBookings();
        dominika.GetBookings();

        //-----------Tenant END--------------

        //----------Landlord further validation examples-----------------
        Console.WriteLine($"------------Landlord actions:------------------");
        database.Remove(adrian);
        database.Remove(flat03);

        //----------Estates properties access examples-----------------
        Console.WriteLine($"----------Estates properties access examples-----------------");

        // Maybe used in the future for further filtering features on validation.

        ///*
        Console.WriteLine($"\nOwner:\n{room34.GetOwnerDetails()}");
        Console.WriteLine($"Room ID: {room34.GetID()}");
        Console.WriteLine($"Luxury level: {room34.luxuryLevel}");
        Console.WriteLine($"Price: £{room34.GetPrice()}");
        Console.WriteLine($"Has smoke alarm?: {room34.smokeAlarm}");
        Console.WriteLine($"Has separate entrance?: {room34.separateEntrance}");
        Console.WriteLine($"Will you get a  private key?: {room34.privateKey}");
        Console.WriteLine($"Host language: {room34.hostLanguage}\n");
        //*/
        ///*
        Console.WriteLine($"\nOwner:\n{flat01.GetOwnerDetails()}");
        Console.WriteLine($"Flat ID: {flat01.GetID()}");
        Console.WriteLine($"Luxury level: {flat01.luxuryLevel}");
        Console.WriteLine($"Price: £{flat01.GetPrice()}");
        Console.WriteLine($"Number of rooms: {flat01.numberOfRooms}");
        Console.WriteLine($"Self check_in?: {flat01.selfCheckIn}");
        Console.WriteLine($"Disable friendly: {flat01.disableFriendly}");
        Console.WriteLine($"Separate entrance: {flat01.separateEntrance}\n");
        //*/
        ///*
        Console.WriteLine($"\nOwner:\n{house11.GetOwnerDetails()}");
        Console.WriteLine($"House ID: {house11.GetID()}");
        Console.WriteLine($"Luxury level: {house11.luxuryLevel}");
        Console.WriteLine($"Price: £{house11.GetPrice()}");
        Console.WriteLine($"Number of rooms: {house11.numberOfRooms}");
        Console.WriteLine($"Self check_in?: {house11.selfCheckIn}");
        Console.WriteLine($"Disable friendly: {house11.disableFriendly}");
        Console.WriteLine($"Garden?: {house11.garden}\n");
        //*/
    }
}