using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sprint_3_Maciej_Madejsza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Singleton "database" instantiation 
        Database database = Database.getInstance(); 

        // id counter for simply unique ID
        int ID = 0;

// Persons input block---------------------------------------------------------------------------------------------------------------------------
        // Declaring variable storing selection 
        ComboBoxItem personToAddSelection;

        // Assigning selection of type of personToListBy to add to the database
        private void AddingPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            personToAddSelection = (ComboBoxItem)addingPerson.SelectedItem;
        }

        // Handling addition of the personToListBy to the database
        private void Add_Person_Button_Click(object sender, RoutedEventArgs e)
        {
            // validating if inputs are not empty
            if (!string.IsNullOrWhiteSpace(PersonFName.Text) && !actionLog.Items.Contains(PersonFName.Text) 
                || !string.IsNullOrWhiteSpace(PersonSName.Text) && !actionLog.Items.Contains(PersonSName.Text)
                || !string.IsNullOrWhiteSpace(PersonEmail.Text) && !actionLog.Items.Contains(PersonEmail.Text)
                )
            {
                // Initial log clearing
                actionLog.Items.Clear();

                // Catching inputs values
                string id = ID.ToString();
                string fName = PersonFName.Text;
                string sName = PersonSName.Text;
                string email = PersonEmail.Text;
                bool premium_member = (bool)PersonMembership.IsChecked;
                bool red_flag = (bool)PersonFlag.IsChecked;

                // Declaring variable to choose type of personToListBy later
                PersonBase inputPerson;

                // Creating landlord or Tenant conditionally
                if (personToAddSelection.Content.ToString() == "Landlord")
                {
                    // Clear droplist in property input section
                    assignLandlordDroplist.Items.Clear();

                    // Instatiate proper personToListBy
                    inputPerson = new Landlord(fName, sName, email, "Phone protected", premium_member, red_flag, id);

                    // Adding personToListBy to database
                    database.Add(inputPerson);

                    // Populate "choose landlord" in property input section
                    foreach (PersonBase landlord in database.Search("landlords", "all"))
                    {
                        // assign entity to value
                        assignLandlordDroplist.Items.Add(new {value=landlord.GetPersonInstance(), displayValue= $"{landlord.GetFormattedName()} (ID:{landlord.GetID()})"});
                    }
                    assignLandlordDroplist.SelectedValuePath = "value";
                    assignLandlordDroplist.DisplayMemberPath = "displayValue";
                }
                else
                {
                    // Instatiate proper personToListBy - tenant
                    inputPerson = new Tenant(fName, sName, email, "Phone protected", premium_member, red_flag, id);

                    // Adding personToListBy to database
                    database.Add(inputPerson);

                    // No need to populate anything with tenants
                }

                // Clearing inputs
                PersonFName.Clear();
                PersonSName.Clear();
                PersonEmail.Clear();
                PersonMembership.IsChecked = false;
                PersonFlag.IsChecked = false;

                // Displaying added personToListBy in log
                actionLog.Items.Add($"Added:\n{inputPerson.GetFullDetails()}");
            }
            // Error message in log
            else
            {
                actionLog.Items.Clear();
                actionLog.Items.Add($"Fill up the details first.");
            }
            // Increase counter
            ID++;
        }

// Property input block---------------------------------------------------------------------------------------------------------------------------
       
        // Declaring variable storing selection
        IDatabaseObject landlordToAssignSelection;

        // Assigning selection landlord as the future owner
        private void AssignLandlord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (assignLandlordDroplist.SelectedValue != null)
            {
                landlordToAssignSelection = (IDatabaseObject)assignLandlordDroplist.SelectedValue;
            }
        }

        // Declaring variable storing selection 
        ComboBoxItem propertyTypeToAddSelection;

        // Assigning selection of the property type wich will be added
        private void AddingProperty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            propertyTypeToAddSelection = (ComboBoxItem)addingProperty.SelectedItem;
            string selectedType = propertyTypeToAddSelection.Content.ToString();
            // If property type is Flat or House which have rooms to choose
            if (selectedType != "Room")
            {
                rooms.Items.Clear();
                rooms.Items.Add(new { value = 1, displayValue = "1" });
                rooms.Items.Add(new { value = 2, displayValue = "2" });
                rooms.Items.Add(new { value = 3, displayValue = "3" });
                rooms.Items.Add(new { value = 4, displayValue = "4" });
                rooms.Items.Add(new { value = 4, displayValue = "5" });
                // Declare pattern to find value
                rooms.SelectedValuePath = "value";
                rooms.DisplayMemberPath = "displayValue";
            }
            // Print proper instruction:
            switch (selectedType)
            {
                case "Room":
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"{selectedType} addition in progress - only following checkboxes are efevtive:" +
                        $"\n▢ Self check-in" +
                        $"\n▢ Separate entrance" +
                        $"\n▢ Smoke alarm" +
                        $"\n▢ Private key");
                    break;
                case "Flat":
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"{selectedType} addition in progress - only following checkboxes are efevtive:" +
                        $"\n▢ Self check-in" +
                        $"\n▢ Separate entrance" +
                        $"\n▢ Disabled friendly");
                    break;
                case "House":
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"{selectedType} addition in progress - only following checkboxes are efevtive:" +
                        $"\n▢ Self check-in" +
                        $"\n▢ Disabled friendly\"" +
                        $"\n▢ Garden");
                    break;
                default: 
                    break; 
            }
            //selCheckIn.Background = "Red";
        }

        // Clearing rooms option for Room type validation
        private void AddingProperty_DropDownOpened(object sender, EventArgs e)
        {
            rooms.Items.Clear();
        }

        // Declaring variable storing selection 
        string roomsToAssignSelection;

        // Assign rooms if type requires
        private void AssignRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rooms.SelectedValue != null)
            {
                roomsToAssignSelection = rooms.SelectedValue.ToString();
            }
        }

        // Declaring variable storing selection 
        ComboBoxItem luxToAssignSelection;

        // Assign luxury lvl
        private void Lux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            luxToAssignSelection = (ComboBoxItem)lux.SelectedItem;
        }

        // Handling addition of the property to the database
        private void Add_Property_Button_Click(object sender, RoutedEventArgs e)
        {
            // Validating if inputs are not empty
            if (!string.IsNullOrWhiteSpace(price.Text) && !actionLog.Items.Contains(price.Text)
                || !string.IsNullOrWhiteSpace(lux.Text) && !actionLog.Items.Contains(lux.Text)
                )
            {
                // Initial log clearing
                actionLog.Items.Clear();

                // Catching inputs values and validation
                Landlord landlord = (Landlord)landlordToAssignSelection;

                // If no landlord cought
                if (landlord == null)
                {
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"Choose landlord!");
                    return; 
                }

                // Validate and parse input to get number-like value only
                int result;
                bool parsedSuccessfully = int.TryParse(price.Text, out result);
                if (!parsedSuccessfully)
                {
                    actionLog.Items.Add($"Set the price!");
                    return;
                }

                // Assign the rest of inputs
                int propPrice = Int32.Parse(price.Text);
                int roomsNo;
                int luxLvl = Int32.Parse(luxToAssignSelection.Content.ToString());
                bool selfCheckIn = (bool)selCheckIn.IsChecked;
                bool isdisableFriendly = (bool)disableFriendly.IsChecked;
                bool hasSeparateEntrance = (bool)separateEntrance.IsChecked;
                bool hasGarden = (bool)garden.IsChecked;
                bool hasSmokeAlarm = (bool)smokeAlarm.IsChecked;
                bool hasPrivateKey = (bool)privateKey.IsChecked;

                // Declaring variable fro property to choose type of later
                IDatabaseObject inputProperty;

                // Creating property conditionally
                if (propertyTypeToAddSelection.Content.ToString() == "Room")
                {
                    inputProperty = new Room(landlord, ID, luxLvl, propPrice, hasSmokeAlarm, hasSeparateEntrance, "To live", hasPrivateKey, "English");
                }

                // Flat or House
                else
                {
                    // Validate rooms input - required field for these property types
                    int resultRooms;
                    bool parsedSuccessfullyRooms = int.TryParse(roomsToAssignSelection, out resultRooms);
                    if (!parsedSuccessfullyRooms)
                    {
                        actionLog.Items.Add($"Set rooms number!");
                        return;
                    }
                    roomsNo = Int32.Parse(roomsToAssignSelection);

                    // Creating property conditionally
                    if (propertyTypeToAddSelection.Content.ToString() == "Flat")
                    {
                        inputProperty = new Flat(landlord, ID, luxLvl, propPrice, roomsNo, selfCheckIn, isdisableFriendly, hasSeparateEntrance);
                    }
                    // House
                    else
                    {                        
                        inputProperty = new House(landlord, ID, luxLvl, propPrice, roomsNo, selfCheckIn, isdisableFriendly, hasGarden);
                    }
                }
                
                // Adding property to database
                database.Add(inputProperty);

                // Clearing inputs
                price.Clear();
                selCheckIn.IsChecked = false;
                disableFriendly.IsChecked = false;
                separateEntrance.IsChecked = false;
                garden.IsChecked = false;
                smokeAlarm.IsChecked = false;
                privateKey.IsChecked = false;

                // Displaying added property in log
                actionLog.Items.Add($"Added:\n{inputProperty.GetFullDetails()}");

                // Increase counter
                ID++;
            }
            // Error message in log
            else
            {
                actionLog.Items.Clear();
                actionLog.Items.Add($"IF conditions not met");
            }
        }

// Database listing block---------------------------------------------------------------------------------------------------------------------------

        // Declaring variable storing selection of the entity to search by
        ComboBoxItem entityToListBySelection;

        // Assign entity to search by
        private void EntityDropList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            entityToListBySelection = (ComboBoxItem)entityDropList.SelectedItem;
           
            // Populate feature droplist conditionally based on entity
            if (entityToListBySelection.Content.ToString() == "Properties")
            {
                featureDropList.Items.Clear();
                featureDropList.Items.Add(new ComboBoxItem { Content = "All".ToString()});
                featureDropList.Items.Add(new ComboBoxItem { Content = "Free".ToString() });
                featureDropList.Items.Add(new ComboBoxItem { Content = "Booked".ToString() });
                featureDropList.Items.Add(new ComboBoxItem { Content = "Suspended".ToString() });
                featureDropList.Items.Add(new ComboBoxItem { Content = "Not suspended".ToString() });
                featureDropList.Items.Add(new ComboBoxItem { Content = "Person".ToString() });
            }
            else
            {
                featureDropList.Items.Clear();
                featureDropList.Items.Add(new ComboBoxItem { Content = "All".ToString() });
                featureDropList.Items.Add(new ComboBoxItem { Content = "Active".ToString() });
                featureDropList.Items.Add(new ComboBoxItem { Content = "Premium".ToString() });
                featureDropList.Items.Add(new ComboBoxItem { Content = "Flagged".ToString() });
            }     
        }

        // Declaring variable storing selection 
        ComboBoxItem featureToListBySelection;

        // Assign feature to search by
        private void FeatureDropList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // reset personDropList options
            personDropList.SelectedIndex = -1;
            personDropList.Items.Clear();

            featureToListBySelection = (ComboBoxItem)featureDropList.SelectedItem;            
            
            // If listing by personToListBy -> populate personToListBy droplist with all persons from database which are active
            if (featureToListBySelection != null && featureToListBySelection.Content.ToString() == "Person")
            {
                List<IDatabaseObject> landlordList = database.Search("landlords", "active");
                // If not empty
                if (landlordList.Any())
                {
                    // populate each personToListBy
                    foreach (PersonBase landlord in landlordList)
                    {
                        personDropList.Items.Add(new { value = landlord.GetPersonInstance(), displayValue = $"(ID:{landlord.GetID()}) {landlord.GetFormattedName()}"});
                    }
                }
                List<IDatabaseObject> tenantList = database.Search("tenants", "active");
                if (tenantList.Any())
                {
                    foreach (PersonBase t in tenantList)
                    {
                        personDropList.Items.Add(new { value = t.GetPersonInstance(), displayValue = $"(ID:{t.GetID()}) {t.GetFormattedName()}"});
                    }
                }
                personDropList.SelectedValuePath = "value";
                personDropList.DisplayMemberPath = "displayValue";
            }
        }

        // Declaring variable storing selection 
        PersonBase personToListBy;

        // Assign selected person to list by
        private void PersonDropList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((PersonBase)personDropList.SelectedValue != null)
            {                
                personToListBy = (PersonBase)personDropList.SelectedValue;                
            }
        }


        // Handling listing (database search) based on all selections
        private void ListButton_Click(object sender, RoutedEventArgs e)
        {
            // Validation - if feature not captured
            if (featureToListBySelection == null || featureToListBySelection.Content.ToString() == "")
            {
                actionLog.Items.Add("Feature not selected - reset it");
                return;
            }

            List<IDatabaseObject>? list = null;
            string? entity = entityToListBySelection.Content.ToString();
            string? feature = featureToListBySelection.Content.ToString();

            // If feature wasn't to list by person search for proper set of entities
            if (featureToListBySelection.Content.ToString() != "Person")
            {
                list = database.Search(entity, feature);
            }
            // Otherwise
            else
            {
                list = database.Search(entity, feature, personToListBy);
            }

            // If searched set isn't empty (found something)
            actionLog.Items.Clear();
            if (list != null && list.Any())
            {
                // List fount set content
                    foreach (IDatabaseObject ent in list)
                    {
                        actionLog.Items.Add($"{ent.GetFullDetails()}");
                    };
            }
                // If nothing found
            else
            {
                    actionLog.Items.Add("No records.");
            }            
        }

        // Clearing features and persons options when changing entity
        private void EntityDropList_DropDownOpened(object sender, EventArgs e)
        {
            featureDropList.SelectedIndex = -1;
            personDropList.SelectedIndex = -1;
            personDropList.Items.Clear();
        }

        // When changing person
        private void PersonDropList_DropDownOpened(object sender, EventArgs e)
        {
        }        

// Actions block---------------------------------------------------------------------------------------------------------------------------

        // Declaring variable storing selection 
        ComboBoxItem PersonTypeForActionSelection;
        
        // Assigning selected person type to perform action on and populate conditionally actions droplist
        private void PersonTypeActionDropList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PersonTypeForActionSelection = (ComboBoxItem)personTypeActionDropList.SelectedItem;

            string option = PersonTypeForActionSelection.Content.ToString();
            List<IDatabaseObject>? list;

            // populating action droplist with actions based on personToListBy type
            actionsDroplist.Items.Clear();
            if (option == "Landlords")
            {
                actionsDroplist.Items.Add(new ComboBoxItem { Content = "Remove".ToString() });
                actionsDroplist.Items.Add(new ComboBoxItem { Content = "Suspend".ToString() });
                actionsDroplist.Items.Add(new ComboBoxItem { Content = "Restore".ToString() });
                actionsDroplist.Items.Add(new ComboBoxItem { Content = "Show properties".ToString() });
            }
            // Tenants:
            else
            {
                actionsDroplist.Items.Add(new ComboBoxItem { Content = "Book".ToString() });
                actionsDroplist.Items.Add(new ComboBoxItem { Content = "Release".ToString() });
                actionsDroplist.Items.Add(new ComboBoxItem { Content = "Show bookings".ToString() });
            }

            personForActionDroplist.Items.Clear();

            // All tenants or landlords
            list = database.Search(option, "all");

            // Populating person droplist with persons to choose based on PersonTypeForActionSelection
            foreach (IDatabaseObject person in list)
            {
                personForActionDroplist.Items.Add(new { value = person.GetPersonInstance(), displayValue = $"(ID:{person.GetID()}) {person.GetFormattedName()}"});
            }
            personForActionDroplist.SelectedValuePath = "value";
            personForActionDroplist.DisplayMemberPath = "displayValue";

        }

        // Clear person and actions when changing person type
        private void PersonTypeActionDropList_DropDownOpened(object sender, EventArgs e)
        {
            personForActionDroplist.SelectedIndex = -1;
            personForActionDroplist.Items.Clear();
            actionsDroplist.SelectedIndex = -1;
            actionsDroplist.Items.Clear();
        }

        // Declaring variable storing selection 
        PersonBase personForActionSelection;

        // Assign particular person to perform action by
        private void PersonForActionDropList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (personForActionDroplist.SelectedValue != null)
            {
                personForActionSelection = (PersonBase)personForActionDroplist.SelectedValue;
            }
            actionTargetDroplist.Items.Clear();
            actionsDroplist.SelectedIndex = -1;
        }

        // Declaring variable storing selection 
        ComboBoxItem actionSelection;

        // Assigning particular action and populating proper (validated) target entities. User cannot chose wrong ones.
        private void ActionDropList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actionSelection = (ComboBoxItem)actionsDroplist.SelectedItem;

            // Catching chosen personForActionSelection
            PersonBase person = personForActionSelection;            
            if (person == null)
            {
                actionLog.Items.Clear();
                actionLog.Items.Add($"Person not found - error!");
                return;
            }

            // Populating targets to choose from, conditionally - validation
            string? action = actionSelection == null ? "" : actionSelection.Content.ToString();
            
            switch (action)
            {
                case "Remove":
                    // Further to choose will be only properties which are free and belong to this particular personToListBy performing an action
                    actionTargetDroplist.Items.Clear();
                    foreach (PropBase prop in database.Search("properties", "free"))
                    {
                        if (prop.GetOwner().GetID() == person.GetID())
                        {
                            actionTargetDroplist.Items.Add(new {value = prop.GetPropInstance(), displayValue = $"(ID:{prop.GetID()}) {prop.GetFormattedName()}" });
                        }
                    }
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"Showing only these properties which are FREE and belong to this person!");
                    break;
                case "Suspend":
                    // Further to choose will be only properties which are not suspended and and belong to this particular personToListBy performing an action
                    actionTargetDroplist.Items.Clear();
                    foreach (PropBase prop in database.Search("properties", "NOTSUSPENDED"))
                    {
                        if (prop.GetOwner().GetID() == person.GetID())
                        {
                            actionTargetDroplist.Items.Add(new { value = prop.GetPropInstance(), displayValue = $"(ID:{prop.GetID()}) {prop.GetFormattedName()}" });
                        }
                    }
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"Showing only these properties which are NOT SUSPENDED and belong to this person!");
                    break;
                case "Restore":
                    // Further to choose will be only properties which are not suspended and and belong to this particular personToListBy performing an action
                    actionTargetDroplist.Items.Clear();
                    foreach (PropBase prop in database.Search("properties", "SUSPENDED"))
                    {
                        if (prop.GetOwner().GetID() == person.GetID())
                        {
                            actionTargetDroplist.Items.Add(new { value = prop.GetPropInstance(), displayValue = $"(ID:{prop.GetID()}) {prop.GetFormattedName()}" });
                        }
                    }
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"Showing only these properties which are SUSPENDED and belong to this person!");
                    break;
                case "Show properties":
                    // List all owner properties later on. "All" to indicate to user
                    actionTargetDroplist.Items.Add(new { value = personForActionSelection.GetPersonInstance(), displayValue = $"All" });
                    actionTargetDroplist.SelectedValue = personForActionSelection.GetPersonInstance();
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"Searching limited - Showing only all properties which belong to this person!");
                    break;
                case "Book":
                    // Further to choose will be only properties which are FREE
                    actionTargetDroplist.Items.Clear();
                    foreach (PropBase prop in database.Search("properties", "free"))
                    {                        
                        actionTargetDroplist.Items.Add(new { value = prop.GetPropInstance(), displayValue = $"(ID:{prop.GetID()}) {prop.GetFormattedName()}" });
                    }
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"Showing only these properties which are FREE = bookable!");
                    break;
                case "Release":
                    // Further to choose will be only properties which are BOOKED
                    actionTargetDroplist.Items.Clear();
                    foreach (PropBase prop in database.Search("properties", "booked"))
                    {
                        if (prop.GetTenant().GetID() == person.GetID())
                        {
                            actionTargetDroplist.Items.Add(new { value = prop.GetPropInstance(), displayValue = $"(ID:{prop.GetID()}) {prop.GetFormattedName()}" });
                        }
                    }
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"Showing only these properties which are BOOKED by THIS tenant!");
                    break;
                case "Show bookings":
                    actionTargetDroplist.Items.Add(new { value = personForActionSelection.GetPersonInstance(), displayValue = $"All" });
                    actionTargetDroplist.SelectedValue = personForActionSelection.GetPersonInstance();
                    actionLog.Items.Clear();
                    actionLog.Items.Add($"Searching limited - Showing only all bookings which belong to this person!");
                    break;
                default:
                    actionLog.Items.Add($"Choose action.");
                    break;
            }
            actionTargetDroplist.SelectedValuePath = "value";
            actionTargetDroplist.DisplayMemberPath = "displayValue";
        }

        // Declaring variable storing selection 
        IDatabaseObject ActionTargetSelection;

        // Assign selected target
        private void ActionTargetDropList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (actionTargetDroplist.SelectedValue != null)
            { 
                ActionTargetSelection = actionTargetDroplist.SelectedValue as dynamic;
            }
        }

        // Handle selected actions combination
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            actionLog.Items.Clear();

            switch (actionSelection == null ? "" : actionSelection.Content.ToString())
            {
                case "Remove":
                    actionLog.Items.Add($"{database.Remove(ActionTargetSelection)}");
                    break;
                case "Suspend":                    
                    actionLog.Items.Add($"{ActionTargetSelection.Suspend((Landlord)personForActionSelection)}");             
                    break;
                case "Restore":                    
                    actionLog.Items.Add($"{ActionTargetSelection.Restore((Landlord)personForActionSelection)}");
                    break;
                case "Show properties":
                    List <IDatabaseObject> list = database.Search("properties", "person", (Landlord)personForActionSelection);
                    // If not empty
                    if(list.Any())
                    {
                        foreach (PropBase p in list)
                        {
                            actionLog.Items.Add($"{p.GetFullDetails()}");
                        };
                    }
                    else
                    {
                        actionLog.Items.Add($"No records.");
                    }
                    break;
                case "Book":
                    actionLog.Items.Add($"{ActionTargetSelection.Book((Tenant)personForActionSelection)}");
                    break;
                case "Release":
                    actionLog.Items.Add($"{ActionTargetSelection.Release((Tenant)personForActionSelection)}");
                    break;
                case "Show bookings":
                    List<IDatabaseObject> list2 = database.Search("properties", "person", (Tenant)personForActionSelection);
                    if (list2.Any())
                    {
                        foreach (PropBase p in list2)
                        {
                            actionLog.Items.Add($"{p.GetFullDetails()}");
                        };
                    }
                    else
                    {
                        actionLog.Items.Add($"No records.");
                    }
                    break;
                default:
                    break;
            }

            actionTargetDroplist.Items.Clear();
            actionsDroplist.SelectedIndex=-1;
        }        
    }    
}
