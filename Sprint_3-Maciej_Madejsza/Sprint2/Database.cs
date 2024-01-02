using System;
using System.Collections.Generic;
using System.Linq;

class Database
{
    // Instatiate lists storing accordingly all landlords, tenants, and properties in the program. That allows us to control numbers, list entities, and enhance validation options.
    // IDatabaseObject interface to allow 1 method of adding, removing, and searching for all types of entities
    private List<IDatabaseObject> allLandlords = new List<IDatabaseObject>();
    private List<IDatabaseObject> allTenants = new List<IDatabaseObject>();
    private List<IDatabaseObject> allProperties = new List<IDatabaseObject>();

    // Declaring instance of database singleton as static - type accessed
    private static Database? databaseInstance = null;

    // Private empty constructor for the safety of having only 1 instance in the program
    private Database(){}

    // Public method-constructor for the safety of having only 1 instance in the program. 
    public static Database getInstance()
    {
        // If not instatiated yet - do it
        if (databaseInstance == null)
        {
            databaseInstance = new Database();
        }
        // If already exists - just return the instance calling type (static)
        return Database.databaseInstance;
    }

    // Method listing entire content of pointed collection of entities (without triggering search)
    public List<IDatabaseObject> ListContent(List <IDatabaseObject> list)
    {
        //List<IDatabaseObject> resultlist = new List<IDatabaseObject>();
        //foreach (IDatabaseObject obj in list)
        //{
        //         // listing via calling common .GetFormattedName() method
        //        Console.WriteLine($"{obj.GetFormattedName()} ");
        //        resultlist.Add($"{obj.GetFormattedName()} ");
        //}
        //Console.WriteLine($"\n ");
        return list;
    }

    // Method for adding the entity to the proper collection based on its type
    public void Add(IDatabaseObject obj) 
    {
        // Each condition has validation for attempt of adding the same entity + confiramtion message
        // When adding a landlord -> to landlords list
        if (obj.GetType() == typeof(Landlord) && !allLandlords.Contains(obj)) {
            // We have to specify the type under the interface given
            allLandlords.Add((Landlord)obj);
            Console.WriteLine($"{obj.GetFormattedName()} added!");
        }
        // When adding a Tenant -> to Tenants list
        else if (obj.GetType() == typeof(Tenant) && !allTenants.Contains(obj)) {
            allTenants.Add((Tenant)obj);
            Console.WriteLine($"{obj.GetFormattedName()} added!");
        }
        // When adding any kind of property -> to properties list. To achieve that we check if the given entity is a subclass of the property base class.
        else if (obj.GetType().IsSubclassOf(typeof(PropBase)) && !allProperties.Contains(obj)) {
            allProperties.Add((PropBase)obj);
            Console.WriteLine($"{obj.GetFormattedName()} added!");
        }
        // When failed validation -> error message
        else
        {
            Console.WriteLine($"\nEntity '{obj.GetFormattedName()}' already exists.\n");
        }      
    }

    // Method for removing the entity from the proper collection based on its type
    public string Remove(IDatabaseObject obj)
    {
        // Utility function to validate removal
        Boolean validateRemoval(string instance, string? ifProperty = null)
        {
            switch (ifProperty) 
            {
                // If we are validating personToListBy:
                case null:
                    // Chceck by calling further search method if this personToListBy has booking or property = is active
                    if (this.Search("properties", "personToListBy", (PersonBase)obj).Any())
                    {
                        // Print error message
                        Console.WriteLine($"\n{instance} is ACTIVE - cannot remove.");
                        // return false = validation failed
                        return false;
                    }
                    else
                    {
                        // return true = validation passed
                        return true;
                    }
                // If we are validating obj as a property:
                default:
                    // If it's booked / holds tenant info:
                    if (obj.GetPropInstance().GetTenant() != null)
                    {
                        // Error message:
                        Console.WriteLine($"\n{instance} is ACTIVE - cannot remove.");
                        // return false = validation failed
                        return false;
                    }
                    else
                    {
                        // return true = validation passed
                        return true;
                    }
            }            
        }
        if (obj == null) return "Choose target.";
        // When we want to remove a landlord 
        if (obj.GetType() == typeof(Landlord))
        {
            // When validation passed
            if (validateRemoval("Landlord"))
            {
                // Remove + print confiramtion message
                allLandlords.Remove((Landlord)obj);
                Console.WriteLine($"{obj.GetType()} {obj.GetFormattedName()} removed!");
                return $"{obj.GetType()} {obj.GetFormattedName()} removed!\nList database agian to check the reult.";
            }
            else
            {
                return $"Landlord validation failed!";
            }            
        }
        // When we want to remove a tenant 
        else if (obj.GetType() == typeof(Tenant))
        {
            if (validateRemoval("Tenant"))
            {
                allTenants.Remove((Tenant)obj);
                Console.WriteLine($"{obj.GetType()} {obj.GetFormattedName()} removed!");
                return $"{obj.GetType()} {obj.GetFormattedName()} removed!";
            }
            else
            {
                return $"Tenant validation failed!";
            }
        }
        // When we want to remove a property 
        else if (obj.GetType().IsSubclassOf(typeof(PropBase)))
        {
            if (validateRemoval("Property", "prop"))
            {
                allProperties.Remove((PropBase)obj);
                Console.WriteLine($"{obj.GetFormattedName()} removed!");
                return $"{obj.GetFormattedName()} removed!";
            }
            else
            {
                return $"property validation failed!";
            }
        }
        else
        // When wrong entity -> error message
        {
            Console.WriteLine($"\nEntity not recognized.");
            return $"Entity not recognized.";
        }
    }

    // Method for advanced searching by admin. Other users may call it only in limited way
    // Returns boolean utility validation function in removing method for landlord
    // personToListBy is nullable because we don't always consider an entity in search as a factor
    public List<IDatabaseObject>? Search(string category, string factor, PersonBase? person = null)
    {
        // Utility function to produce output (list eventually many records)
        List<IDatabaseObject>? searchOutput(List<IDatabaseObject> queryToSearch, string message)
        {
            // Declare utility list for results
            List<IDatabaseObject> filteredArray = new List<IDatabaseObject> ();            

            // If later on, given collection from query is empty and it's about personToListBy engagement:
            if(!queryToSearch.Any() && person!=null)
            {
                Console.WriteLine($"\n{message}\n{person.GetType()} {person.GetFormattedName()} - no any engagement.");
                // return false = not engaged
                return null;
            }
            // If later on, given collection from query is empty and it's about property:
            else if (!queryToSearch.Any())
            {
                Console.WriteLine($"\n{message}\nNO FREE properties\n");
                // return false = not engaged
                return null;
            }
            // If later on, given collection from query is full:
            else
            {
                // Print searching message
                Console.WriteLine($"\n{message}:");
                // List all the elements
                foreach (IDatabaseObject prop in queryToSearch)
                {
                    // If it wasn't listed yet (no repetitions)
                    if (!filteredArray.Contains(prop))
                    {
                        filteredArray.Add(prop);
                        Console.WriteLine($"{prop}");
                    }
                }
                // return true = engagement found
                return filteredArray;
            }
        }

        // Declare list for query results
        IEnumerable <string> queryToSearch = new List<string>();
        // Declare list for query results
        List<IDatabaseObject> results = new List<IDatabaseObject>();
        results.Clear();    
        // Pick and check the category for conditions:
        switch (category.ToUpper())
        {
            case "PROPERTIES":
                // when category : properties -> pick and check factor
                switch (factor.ToUpper())
                {
                    // when factor == all -> list all the properties calling existing method on proper collection
                    case "ALL":
                        Console.WriteLine($"\nSearched All properties:");
                        return ListContent(this.allProperties);
                    // when free -> create a query filtering by the state
                    case "FREE":
                        foreach (PropBase prop in this.allProperties)
                        {
                            if(prop.GetPropInstance().GetState().GetType() == typeof(PropStateFree))
                            {
                                results.Add(prop.GetPropInstance());
                            }
                        };

                        //queryToSearch = from prop in allProperties
                        //                    // we call method GetPropInstance() for further access
                        //                where prop.GetPropInstance().GetState().GetType() == typeof(PropStateFree)
                        //                select prop.GetFormattedName();

                        //// call utility function with custom message and query results
                        //return searchOutput(queryToSearch, "\nSearched FREE properties:");

                        return results;

                    // when booked -> create a query filtering by the state
                    case "BOOKED":

                        foreach (PropBase prop in this.allProperties)
                        {
                            if (prop.GetPropInstance().GetState().GetType() == typeof(PropStateBooked))
                            {
                                results.Add(prop.GetPropInstance());
                            }
                        };

                        //queryToSearch = from prop in allProperties
                        //                where prop.GetPropInstance().GetState().GetType() == typeof(PropStateBooked)
                        //                select prop.GetFormattedName();
                        //return searchOutput(queryToSearch, "\nSearched BOOKED properties:");

                        return results;
                    case "NOTSUSPENDED":

                        foreach (PropBase prop in this.allProperties)
                        {
                            if (prop.GetPropInstance().GetState().GetType() != typeof(PropStateSuspended))
                            {
                                results.Add(prop.GetPropInstance());
                            }
                        };
                        return results;
                    case "SUSPENDED":

                        foreach (PropBase prop in this.allProperties)
                        {
                            if (prop.GetPropInstance().GetState().GetType() == typeof(PropStateSuspended))
                            {
                                results.Add(prop.GetPropInstance());
                            }
                        };
                        return results;
                    // when extra factor is given "personToListBy"
                    case "PERSON":
                        // If it's about the landlord
                        if (person.GetType() == typeof(Landlord))
                        {
                            foreach (PropBase prop in this.allProperties)
                            {
                                if (prop.GetPropInstance().GetOwner().GetID() == person.GetID())
                                {
                                    results.Add(prop.GetPropInstance());
                                }
                            };

                            //queryToSearch = from prop in allProperties
                            //                where prop.GetPropInstance().GetOwner().GetID() == personToListBy.GetID()
                            //                select prop.GetFormattedName();
                            //return searchOutput(queryToSearch, $"Searched LANDLORD {personToListBy.GetFormattedName()}'s properties:");
                            
                            return results;
                        }
                        // If it's about the tenant
                        else if (person.GetType() == typeof(Tenant))
                        {

                            foreach (PropBase prop in this.allProperties)
                            {
                                if ((prop.GetPropInstance().GetTenant() != null ? prop.GetPropInstance().GetTenant().GetID() : "") == person.GetID())
                                {
                                    results.Add(prop.GetPropInstance());
                                }
                            };

                            //queryToSearch = from prop in allProperties
                            //                where (prop.GetPropInstance().GetTenant() != null? prop.GetPropInstance().GetTenant().GetID() : "" ) == personToListBy.GetID()
                            //                select prop.GetFormattedName();
                            //return searchOutput(queryToSearch, $"\nSearched TENANT {personToListBy.GetFormattedName()}'s properties:");

                            return results;

                        }
                        //Error message
                        else
                        {
                            Console.WriteLine($"\nPerson type NOT recognized");
                            // return false = not engaged
                            return null;
                        }
                    default:
                        Console.WriteLine($"\nFactor NOT recognized");
                        // return false = not engaged
                        return null;
                }                
            case "LANDLORDS":
                // when category : landlords -> pick and check factor
                switch (factor.ToUpper())
                {
                    // calling method on allLandlords
                    case "ALL":
                        Console.WriteLine($"\nSearched All landlords:");                        
                        return ListContent(this.allLandlords);
                    // filtering allProperties if Landlord has the one
                    case "ACTIVE":
                        foreach (PropBase prop in this.allProperties)
                        {
                            if (!results.Contains(prop.GetOwner()))
                            {
                                results.Add(prop.GetOwner());
                            }                    
                        };

                        //queryToSearch = from prop in allProperties

                        //                select prop.GetPropInstance().GetOwner().GetFormattedName();

                        //return searchOutput(queryToSearch, "\nSearched ACTIVE landlords:");
                        return results;

                    // filtering allLendlords if Landlord is premium. GetPersonInstance() for access
                    case "PREMIUM":

                        foreach (PersonBase landlord in this.allLandlords)
                        {
                            if (landlord.GetPersonInstance().IsPremium() == true)
                            {
                                results.Add(landlord.GetPersonInstance());
                            }
                        };

                        //queryToSearch = from landlord in allLandlords
                        //                where landlord.GetPersonInstance().IsPremium() == true
                        //                select landlord.GetFormattedName();
                        //return searchOutput(queryToSearch, "\nSearched PREMIUM landlords:");

                        return results;

                    case "FLAGGED":

                        foreach (PersonBase landlord in this.allLandlords)
                        {
                            if (landlord.GetPersonInstance().IsRedFlagged() == true)
                            {
                                results.Add(landlord.GetPersonInstance());
                            }
                        };

                        //queryToSearch = from landlord in allLandlords
                        //                where landlord.GetPersonInstance().IsRedFlagged() == true
                        //                select landlord.GetFormattedName();
                        //return searchOutput(queryToSearch, "\nSearched FLAGGED landlords:");

                        return results;

                    default:
                    // Error message
                        Console.WriteLine($"\nFactor NOT recognized");
                        // return false = not engaged
                        return null;
                }
            case "TENANTS":
                // when category : tenants -> pick and check factor
                switch (factor.ToUpper())
                {
                    // calling method on allTenants
                    case "ALL":
                        Console.WriteLine($"\nSearched All tenants:");
                        
                        return ListContent(this.allTenants);
                    // filtering allProperties if Tenants is in any of them
                    case "ACTIVE":

                        foreach (PropBase prop in this.allProperties)
                        {
                            if (prop.GetPropInstance().GetTenant() != null)
                            {
                                if (!results.Contains(prop.GetTenant()))
                                {
                                    results.Add(prop.GetTenant());
                                }
                            }                            
                        };

                        //queryToSearch = from prop in allProperties
                        //                where prop.GetPropInstance().GetTenant() != null 
                        //                select prop.GetPropInstance().GetTenant().GetFormattedName();
                        //return searchOutput(queryToSearch, "\nSearched ACTIVE tenants:");

                        return results;

                    // filtering allTenants
                    case "PREMIUM":

                        foreach (PersonBase t in this.allTenants)
                        {
                            if (t.GetPersonInstance().IsPremium() == true)
                            {
                                results.Add(t.GetPersonInstance());
                            }
                        };

                        //queryToSearch = from t in allTenants
                        //                where t.GetPersonInstance().IsPremium() == true
                        //                select t.GetFormattedName();
                        //return searchOutput(queryToSearch, "\nSearched PREMIUM tenants:");

                        return results;
                    
                    case "FLAGGED":

                        foreach (PersonBase t in this.allTenants)
                        {
                            if (t.GetPersonInstance().IsRedFlagged() == true)
                            {
                                results.Add(t.GetPersonInstance());
                            }
                        };

                        //queryToSearch = from t in allTenants
                        //                where t.GetPersonInstance().IsRedFlagged() == true
                        //                select t.GetFormattedName();
                        //return searchOutput(queryToSearch, "\nSearched FLAGGED tenants:");

                        return results;

                    // Error message
                    default:
                        Console.WriteLine($"\nFactor NOT recognized");
                        // return false = not engaged
                        return null;
                }
            // Main error message
            default:
                Console.WriteLine($"\nDatabase category NOT recognized");
                // return false = not engaged
                return null;               
        }
    }
}