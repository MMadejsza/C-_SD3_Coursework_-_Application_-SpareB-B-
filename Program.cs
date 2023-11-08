using System;
class Program
{
    static void Main()
    {
        Landlord matt = new Landlord(
            "Maciej",
            "Madejsza",
            "maciej.madejsza@gmail.com",
            "07784882823",
            true,
            false,
            01);

        /*
        Console.WriteLine(matt.getFromattedName());
        Console.WriteLine(matt.getPhoneString());
        Console.WriteLine(matt.isPremium());
        Console.WriteLine(matt.isRedFlagged());

        matt.setPhoneNumber("different number");
        matt.setStatus(false, true);

        Console.WriteLine(matt.getFromattedName());
        Console.WriteLine(matt.getPhoneString());
        Console.WriteLine(matt.isPremium());
        Console.WriteLine(matt.isRedFlagged());
        //*/

        Room room = new Room(matt, false, 3, 275, 34, true, true, "Office", true, "Polish");
        ///*
        Console.WriteLine($"Owner:\n{room.getOwner()}");
        Console.WriteLine($"Room ID: {room.getID()}");
        Console.WriteLine($"Luxury level: {room.luxury_level}");
        Console.WriteLine($"Price: £{room.getPrice()}");
        Console.WriteLine($"Engagaged?: {room.engagement}");
        Console.WriteLine($"Has smoke alarm?: {room.smoke_alarm}");
        Console.WriteLine($"Has separate entrance?: {room.separate_entrance}");
        Console.WriteLine($"Will you get a  private key?: {room.private_key}");
        Console.WriteLine($"Host language: {room.host_language}\n");
        //*/


        Flat flat = new Flat(matt, false, 5, 590, 01, 3, true, false, false);
        ///*
        Console.WriteLine($"Owner:\n{flat.getOwner()}");
        Console.WriteLine($"Flat ID: {flat.getID()}");
        Console.WriteLine($"Luxury level: {flat.luxury_level}");
        Console.WriteLine($"Price: £{flat.getPrice()}");
        Console.WriteLine($"Engagaged?: {flat.engagement}");
        Console.WriteLine($"Number of rooms: {flat.number_of_rooms}");
        Console.WriteLine($"Self check_in?: {flat.self_check_in}");
        Console.WriteLine($"Disable friendly: {flat.disable_friendly}");
        Console.WriteLine($"Separate entrance: {flat.separate_entrance}\n");
        //*/

        House house = new House(matt, false, 5, 1090, 11, 6, true, true,true);
        ///*
        Console.WriteLine($"Owner:\n{house.getOwner()}");
        Console.WriteLine($"House ID: {house.getID()}");
        Console.WriteLine($"Luxury level: {house.luxury_level}");
        Console.WriteLine($"Price: £{house.getPrice()}");
        Console.WriteLine($"Engagaged?: {house.engagement}");
        Console.WriteLine($"Number of rooms: {house.number_of_rooms}");
        Console.WriteLine($"Self check_in?: {house.self_check_in}");
        Console.WriteLine($"Disable friendly: {house.disable_friendly}");
        Console.WriteLine($"Garden?: {house.garden}\n");
        //*/
    }
}