
using PRG2_Assignment;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
//==========================================================
// Student Number : S10257103
// Student Name : Lim Ze Yu
// Partner Name : Tan Yew Wren
//==========================================================

//Flavours
List<string> flavours = new List<string>();

//Premium flavours
List<string> premium_flavours = new List<string> ();

//Toppings list
List<string> toppings_list = new List<string> ();

//Gold queue
Queue<Order> goldQueue = new Queue<Order>();

//Regular queue
Queue<Order> regularQueue = new Queue<Order>();

//Utility Methods

bool IsPremium(string temp) //Checks if the flavour is premium or not
{

    foreach(string  s in premium_flavours)
    {
        if(s.ToLower() == temp.ToLower())
        {
            return true;
        }
    }
    return false;
}

bool IsFlavour(string temp)
{
    temp = temp;
    if (flavours.Contains(temp))
    {
        return true;
    }
    else
    {
        return false;
    }

}

bool IsToppings(string temp)
{
    if (toppings_list.Contains(temp))
    {
        return true;
    }
    else
    {
        return false;
    }
}
List<string[]> ReadingFile(string file)
{
    List<string[]> info = new List<string[]>();
    string[] array;

    using (StreamReader sr = new StreamReader(file))
    {
        string? s = sr.ReadLine();
        if (s != null)
        {
            string[] header = s.Split(",");
        }
        else
        {
            return null;
        }
        while ((s = sr.ReadLine()) != null)
        {
            //Contents of the Order File
            array = s.Split(",");
            
            info.Add(array);

        }
        
        
    }
    return info;
}

int CalculatePoints(double cost)
{
    //Covnersion rate  = 72%
    return Convert.ToInt32(Math.Floor(cost * 0.72));
}


//Adding dynamically of flavours and premium flavours

List<string[]> flavour_info = ReadingFile("flavours.csv");

foreach (string[] s in flavour_info)
{
    flavours.Add(s[0]);
    if (Convert.ToInt32(s[1]) > 0)
    {
        premium_flavours.Add(s[0]);
    }
}



//Adding dynamically of toppings
List<string[]> toppings_info = ReadingFile("toppings.csv");

foreach (string[] s in toppings_info)
{
    toppings_list.Add(s[0]);
}


List<string[]> order_info = ReadingFile("orders.csv");

List<string[]> customer_info = ReadingFile("customers.csv");


List<Order> order_list = new List<Order>();

List<Customer> customer_list = new List<Customer>();

//Adding of customers
for (int i = 0; i < customer_info.Count; i++)
{

    //Customer
    string name = customer_info[i][0];
    int memberid = Convert.ToInt32(customer_info[i][1]);
    DateTime dob = Convert.ToDateTime(customer_info[i][2]);

    //PointCard
    int points = Convert.ToInt32(customer_info[i][4]);
    int punchcard = Convert.ToInt32(customer_info[i][5]);
    string tier = customer_info[i][3];
    PointCard pointcard = new PointCard(points, punchcard);
    Customer customer = new Customer(name, memberid, dob);
    customer.Reward = pointcard;
    customer_list.Add(customer);
}

//Adding of orders to the customer

for (int i = 0; i<order_info.Count; i++)
{
    List<string> flavour_list = new List<string>();
    List<string> topping_list = new List<string>();
    List<Flavour> class_flavour_list = new List<Flavour>();
    List<Topping> class_topping_list = new List<Topping>();

    int id = Convert.ToInt32(order_info[i][0]);
    int memberId = Convert.ToInt32(order_info[i][1]);
    DateTime timeRecieved = Convert.ToDateTime(order_info[i][2]);
    DateTime timeFulfilled = Convert.ToDateTime(order_info[i][3]);
    string option = order_info[i][4];
    int scoops = Convert.ToInt32(order_info[i][5]);

    //CHECKING FOR CONE BOOLEAN (DIPPED)
    bool dipped = false; //DEFAULT TO FALSE
    string temp;
    if (!String.IsNullOrEmpty(temp = order_info[i][6]))
    {
        if(temp != "")
        {
            if(temp == "TRUE")
            {
                dipped = true;
            }
            /*else if(temp == "FALSE")
            {
                dipped = false;
            }*/
        }
    }
    string waffleFlavour = order_info[i][7];
    string flavour1 = order_info[i][8];
    flavour_list.Add(flavour1);
    Flavour flav = new Flavour(flavour1,IsPremium(flavour1),1);
    class_flavour_list.Add(flav);
    string flavour2;
    //Flavour flav2 = new Flavour() ;
    string flavour3;
    if((flavour2 = order_info[i][9]) != "") //Flavour 2
    {
        if(flavour_list.Contains(flavour2))
        {
            flav.Quantity++;
        }
        else
        {
            flavour_list.Add(flavour2);
            Flavour flav2 = new Flavour(flavour2, IsPremium(flavour2), 1);
            class_flavour_list.Add(flav2);
        }
        
    }
    if ((flavour3 = order_info[i][10]) != "") //Flavour 3
    {
        
        bool key = true;
        foreach(Flavour f in class_flavour_list)
        {
            if(f.Type == flavour3)
            {
                f.Quantity++;
                key = false;
                break;
            }
        }
        if(key)
        {
            Flavour flav3 = new Flavour(flavour3,IsPremium(flavour3),1);
        }

    }
    string t1;
    string t2;
    string t3;
    string t4;
    
    t1 = order_info[i][11];
    Topping topping1 = new Topping(t1);
    class_topping_list.Add(topping1);
    //topping_list.Add(topping1); //Topping 1
    if((t2 = order_info[i][12]) != "") //Topping 2
    {
        Topping topping2 = new Topping(t2);
        class_topping_list.Add(topping2);
    }
    if((t3 = order_info[i][13]) != "") //Topping 3
    {
        Topping topping3 = new Topping(t3);
        class_topping_list.Add(topping3);
    }
    if((t4 = order_info[i][14]) != "") //Topping 4
    {
        Topping topping4 = new Topping(t4);
        class_topping_list.Add((topping4));
    }


    Order order = new Order(id, timeRecieved);
    order.TimeFulfilled = timeFulfilled;

    IceCream iceCream;
    if(option == "Cup")
    {
        iceCream = new Cup(option, scoops, class_flavour_list, class_topping_list);
    }
    else if(option == "Cone")
    {
        iceCream = new Cone(option, scoops, class_flavour_list, class_topping_list, dipped);
    }
    else
    {
        //option == waffle
        iceCream = new Waffle(option, scoops, class_flavour_list, class_topping_list, waffleFlavour);
    }


    foreach(Customer c in customer_list)
    {
        if (c.MemberId == memberId)
        {
            order.AddIceCream(iceCream);
            c.OrderHistory.Add(order);
            break;
        }
     
    }
    order_list.Add(order);
}


//Option 1 Listing of customer
void DisplayCustomer()
{
    string CurrentOrder = null;
    string s = null;
    Console.WriteLine("{0,-7}{1,-15}{2,-15}{3,-15}{4,-15}", "Index","Name","MemberID","Dob","Rewards");
    int count = 1;
    foreach(Customer c in customer_list)
    {
        if(c.CurrentOrder == null)
        {
            Console.WriteLine("{0,-7}{1,-15}{2,-15}{3,-15}{4,-15}",count, c.Name, c.MemberId, c.Dob.ToString("dd/MM/yyyy"), c.Reward);
        }
        else
        {
            Console.WriteLine("{0,-7}{1,-15}{2,-15}{3,-15}{4,-15}", count,c.Name, c.MemberId, c.Dob.ToString("dd/MM/yyyy"), c.Reward);
        }
        count++;
    }
    Console.WriteLine();
}


//Other options methods
//YW DO ONE
//option 2
void DisplayCurrentOrders(Queue<Order> regularQueue, Queue<Order> goldQueue)
{
    // Display orders in the Gold Queue
    Console.WriteLine("Gold Queue Orders");
    Console.WriteLine("--------------------");

    // Iterate through each order in the Gold Queue
    foreach (Order order in goldQueue)
    {
        // Display the order information using ToString method
        Console.WriteLine(order.ToString());
    }

    // Display orders in the Regular Queue
    Console.WriteLine("Regular Queue Orders");
    Console.WriteLine("--------------------");

    // Iterate through each order in the Regular Queue
    foreach (Order order in regularQueue)
    {
        // Display the order information using ToString method
        Console.WriteLine(order.ToString());
    }
}

//option 3
void RegNewCustomer(List<Customer> customer_list)
{
    while (true)
    {
        try
        {
            int id = 0;

            // Get customer name from user input
            Console.Write("Name: ");
            string name = Console.ReadLine();

            // Validate that the name is not empty
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please enter a valid name");
                continue;
            }

            // Ensure a unique ID is entered
            while (true)
            {
                // Get a unique customer ID from user input
                Console.Write("Enter a unique ID Number: ");
                id = Convert.ToInt32(Console.ReadLine());

                // Check if the entered ID already exists
                if (customer_list.Any(c => c.MemberId == id))
                {
                    Console.WriteLine("This Member ID already exists. Please enter a new ID.");
                    continue;
                }
                else
                {
                    break; // Break out of the loop if the ID is unique
                }
            }

            // Get customer date of birth from user input
            Console.Write("Enter Date of Birth (dd/mm/yyyy): ");
            DateTime dob = Convert.ToDateTime(Console.ReadLine());
            Customer newCustomer = new Customer(name, id, dob); // Create a new Customer object
            DateOnly formattedDob = DateOnly.FromDateTime(newCustomer.Dob); // Format to display date only

            // Create a new PointCard and set the tier to "Ordinary"
            PointCard newPointCard = new PointCard(0, 0);
            newPointCard.Tier = "Ordinary";
            newCustomer.Reward = newPointCard;

            customer_list.Add(newCustomer);  // Add the new customer to the customer list

            using (StreamWriter sw = new StreamWriter("customers.csv", true))
            {
                sw.WriteLine("{0},{1},{2},{3},{4},{5}", newCustomer.Name, newCustomer.MemberId, formattedDob, newPointCard.Tier, newPointCard.Points, newPointCard.PunchCard);
            }

            // Display registration success message and break out of the loop
            Console.WriteLine("Registration Successful!\n");
            break;
        }
        catch (FormatException)
        {
            // Handle format exception (e.g., invalid input)
            Console.WriteLine("Format Error.\nRegistration Unsuccessful.\n");
            continue;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"An Error Occurred: {ex.Message}\nRegistration Unsuccessful.\n");
            continue;
        }
    }
}


//option 4
void CreateCustomerOrder(List<string> flavour_list, List<string> topping_list, Queue<Order> goldQueue, Queue<Order> regularQueue, List<Customer> customer_list)
{
    Order currentOrder = null;
    while (true)
    {
        // Initialising variables
        string addAnother = null;
        int NumOfToppings = 0;
        int indexInput = 0;
        int scoops = 0;
        string option = null;
        int customerId = 0;
        bool customerExists = false; // to check if the customer exists 
        bool dippedCone = false; // to check if the cone is dipped
        string[] options = { "Cup", "Cone", "Waffle" }; // Array of options
        string[] waffleflavours = { "Red Velvet", "Pandan", "Charcoal" }; // Array of waffle flavours
        List<Topping> selectedToppings = new List<Topping>(); // list of toppings that the user has selected
        List<Flavour> selectedFlavours = new List<Flavour>(); // list of flavours that the user has selected
        bool premium = false; // to check if the flavour is premium
        bool fexists = false; // to check if the flavour already exists in the selectedFlavours
        bool texists = false; // to check if the topping is already inside
        // Display existing customers
        DisplayCustomer();
        while (true)
        {
            try
            {
                Console.Write("Select a customer by index: ");
                indexInput = Convert.ToInt32(Console.ReadLine());

                // Check if the selected customer exists   
                foreach (Customer c in customer_list)
                {
                    if (c.Name == customer_list[indexInput - 1].Name)
                    {
                        customerExists = true;
                        customerId = c.MemberId; // get customer id to create new order object
                    }
                }
                break;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Please enter a valid index.\n");
                continue;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter an integer.\n");
                continue;
            }
        }
        if(currentOrder == null)
        {
            currentOrder = new Order(Convert.ToInt32(order_list[order_list.Count() - 1].Id) + 1, DateTime.Now); // create new order object
        }
        
        Console.WriteLine("Ice Cream Order" + "\n----------------");

        while (true)
        {
            try
            {
                // Prompt user to select an ice cream option (Cup, Cone, Waffle)
                Console.WriteLine("Options:\n" + "[1] Cup\n" + "[2] Cone\n" + "[3] Waffle");
                Console.Write("Select an option using 1, 2 or 3: ");
                int optionInput = Convert.ToInt32(Console.ReadLine());
                option = options[optionInput - 1];
                break;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please enter an integer from 1 to 3.\n");
                continue;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter an integer.\n");
                continue;
            }
        }
        while (true)
        {
            try
            {
                // Prompt user to enter the number of scoops (maximum: 3)
                Console.Write("Please enter the number of scoops [max: 3]: ");
                scoops = Convert.ToInt32(Console.ReadLine());
                if (scoops > 3 || scoops <= 0)
                {
                    Console.WriteLine("Please select up to 3 scoops.\n");
                    continue;
                }
                else
                {
                    break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter an integer.\n");
            }

        }


        // Prompt user to choose flavours
        while (true)
        {
            try
            {
                Console.WriteLine("Flavours:\n" + "[1] Vanilla\n" + "[2] Chocolate\n" + "[3] Strawberry\n" + "[4] *Durian\n" + "[5] *Ube\n" + "[6] *Sea Salt\n" + "(* flavours are premium)");
                for (int i = 1; i <= scoops; i++)
                {
                    Console.Write("Please select the flavour: ");
                    int Flavourinput = Convert.ToInt32(Console.ReadLine());

                    string ChosenFlavour = flavour_list[Flavourinput - 1];

                    // If the selected flavour is premium, set premium to true          
                    if (Flavourinput > 3 && Flavourinput < 7)
                    {
                        premium = true;
                    }

                    Flavour flavour = new Flavour(ChosenFlavour, premium, 1);


                    // Check if the flavour already exists in the selectedFlavours list
                    foreach (Flavour f in selectedFlavours)
                    {
                        if (f.Type == flavour.Type)
                        {
                            flavour.Quantity++;
                            fexists = true;
                        }
                    }
                    if (!fexists)
                    {
                        selectedFlavours.Add(flavour);
                    }
                }
                break;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Please nter a number from 1 to 6.\n");
                continue;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter an integer.\n");
                continue;
            }

        }

        while (true)
        {
            try
            {
                // Prompt user to choose toppings
                Console.WriteLine("Toppings:\n" + "[1] Sprinkles\n" + "[2] Mochi\n" + "[3] Sago\n" + "[4] Oreos");
                Console.Write("Enter the number of toppings: ");
                NumOfToppings = Convert.ToInt32(Console.ReadLine());

                if (NumOfToppings > 4 || NumOfToppings <= 0)
                {
                    Console.WriteLine("Please select up to 4 toppings.\n");
                    continue;
                }
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter an integer.\n");
                continue;
            }
        }
        while (true)
        {
            try
            {
                // Prompt user to choose toppings
                for (int i = 1; i <= NumOfToppings; i++)
                {
                    Console.Write("Please select the index of the topping: ");
                    int ToppingInput = Convert.ToInt32(Console.ReadLine());
                    string topping = toppings_list[ToppingInput - 1];
                    Topping toppings = new Topping(topping);
                    selectedToppings.Add(toppings);
                    texists = true;
                }
                break;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Please enter an integer from 1 to 4.\n");
                continue;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter an integer.\n");
                continue;
            }
        }


        // Create a new instance of IceCream based on the selected option
        if (option == "Cup")
        {
            IceCream newIceCream = new Cup(option, scoops, selectedFlavours, selectedToppings);
            currentOrder.IceCreamList.Add(newIceCream); // add ice cream to current order
        }
        else if (option == "Cone")
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Would you like to dip your cone?\n [1] Yes\n [2] No");
                    int dippedInput = Convert.ToInt32(Console.ReadLine());
                    if (dippedInput == 1)
                    {
                        dippedCone = true;

                    }
                    else if (dippedInput == 2)
                    {
                        dippedCone = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                    IceCream newIceCream = new Cone(option, scoops, selectedFlavours, selectedToppings, dippedCone);
                    currentOrder.IceCreamList.Add(newIceCream);  // add ice cream to current order
                    break;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Please enter an integer from 1 to 3.\n");
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter an integer.\n");
                    continue;
                }
            }
        }
        else if (option == "Waffle")
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Waffle Flavours:\n [1] Red Velvet\n [2] Pandan\n [3] Charcoal");
                    Console.Write("Select a waffle flavour: ");
                    int wfoption = Convert.ToInt32(Console.ReadLine());
                    string wf = waffleflavours[wfoption - 1];
                    IceCream newIceCream = new Waffle(option, scoops, selectedFlavours, selectedToppings, wf);
                    currentOrder.IceCreamList.Add(newIceCream);  // add ice cream to current order
                    break;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Please enter an integer from 1 to 3.\n");
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter an integer.\n");
                    continue;
                }
            }
        }

        // Create a new instance of IceCream based on the selected option
        while (true)
        {
            Console.Write("Do you wish to add another ice cream?[Y/N] ");
            addAnother = Console.ReadLine().ToLower();
            if (addAnother == "y" || addAnother == "n")
            {
                break;
            }
            else
            {
                Console.WriteLine("Please enter [Y/N].");
                continue;
            }
        }


        // loop again to continue adding ice cream
        if (addAnother == "y")
        {
            /*if (customer_list[indexInput - 1].Reward.Tier == "Gold")
            {
                goldQueue.Enqueue(currentOrder);
            }
            else
            {
                regularQueue.Enqueue(currentOrder);

            }*/
            continue;
        }
        // Process the order and exit the loop if the user doesn't want to add another ice cream
        else if (addAnother == "n")
        {
            if (customer_list[indexInput - 1].Reward.Tier == "Gold")
            {
                goldQueue.Enqueue(currentOrder);
            }
            else
            {
                regularQueue.Enqueue(currentOrder);

            }
            //customer_list[indexInput - 1].CurrentOrder = currentOrder;
            customer_list[indexInput - 1].CurrentOrder = currentOrder;
            order_list.Add(currentOrder);
            // orders.Add(currentOrder);
            Console.WriteLine("Your order has been placed successfully!\n");
            break;
        }
    }
}

//YW DO ONE


//Display Menu

void DisplayMenu() //Needs to be added more
{
    while (true)
    {








        try
        {

            Console.WriteLine("[0] Exit");
            Console.WriteLine("[1] List all customers");
            Console.WriteLine("[2] List all current orders");
            Console.WriteLine("[3] Register a new customer");
            Console.WriteLine("[4] Create a customer's order");
            Console.WriteLine("[5] Display order details of a customer");
            Console.WriteLine("[6] Modify order details");
            Console.WriteLine("[7] Process an order and checkout");
            Console.Write("Please enter options: ");
            string option = Console.ReadLine();

            if (option == "0")
            {
                Console.WriteLine("Thank you");
                break;
            }

            else if (option == "1")
            {
                DisplayCustomer();

            }
            //Add other options here

            else if (option == "2")
            {
                DisplayCurrentOrders(regularQueue, goldQueue);
            }

            else if (option == "3")
            {

                RegNewCustomer(customer_list);
            }
            else if (option == "4")
            {
                CreateCustomerOrder(flavours, toppings_list, goldQueue, regularQueue, customer_list);
            }


            else if (option == "5")
            {
                string flavour = "";
                string toppings = "";
                DisplayCustomer();
                Console.Write("Please select a customer using the index: ");
                int index = Convert.ToInt32(Console.ReadLine());

                Customer c = customer_list[index - 1];
                //Header 
                Console.WriteLine("Current Order");
                if (c.CurrentOrder != null)
                {
                    Console.WriteLine("{0,-25}{1,-25}", "TimeRecieved", "TimeFulfilled");
                    Console.WriteLine("{0,-25}{1,-25}", c.CurrentOrder.TimeReceived, c.CurrentOrder.TimeFulfilled);
                    Console.WriteLine("IceCream Details");
                    foreach (IceCream i in c.CurrentOrder.IceCreamList)
                    {
                        foreach (Flavour f in i.Flavours)
                        {
                            flavour += f.Type + "|";
                        }
                        foreach (Topping t in i.Toppings)
                        {
                            toppings += t.Type + "|";
                        }

                        Console.WriteLine("OrderId: {0,-10}Option: {1,-15} Scoops: {2,-15} Flavours: |{3,-15} Toppings: |{4,-15}", c.CurrentOrder.Id, i.Option, i.Scoops, flavour, toppings);
                        Console.WriteLine();
                        flavour = "";
                        toppings = "";
                    }
                }
                else
                {
                    Console.WriteLine("{0} has no current order", c.Name);
                }

                Console.WriteLine();
                Console.WriteLine("Order History");
                Console.WriteLine();

                foreach (Order o in c.OrderHistory)
                {
                    Console.WriteLine("{0,-25}{1,-25}", "TimeRecieved", "TimeFulfilled");
                    Console.WriteLine("{0,-25}{1,-25}", o.TimeReceived, o.TimeFulfilled);
                    Console.WriteLine("IceCream Details");
                    foreach (IceCream i in o.IceCreamList)
                    {
                        foreach (Flavour f in i.Flavours)
                        {
                            flavour += f.Type + "|";
                        }
                        foreach (Topping t in i.Toppings)
                        {
                            toppings += t.Type + "|";
                        }

                        Console.WriteLine("OrderId: {0,-10}Option: {1,-15} Scoops: {2,-15} Flavours: |{3,-15} Toppings: |{4,-15}", o.Id, i.Option, i.Scoops, flavour, toppings);
                        Console.WriteLine();
                        flavour = "";
                        toppings = "";
                    }

                }
            }
            else if (option == "6") //HAVE NOT DONE option 2 and 3 only did option 1
            {
                while (true)
                {


                    try
                    {
                        DisplayCustomer();
                        Console.Write("Please select a customer using the index: ");
                        int index_customer = Convert.ToInt32(Console.ReadLine());
                        Customer customer = customer_list[index_customer - 1];
                        if (customer.CurrentOrder == null)
                        {
                            Console.WriteLine("You have no current orders");
                            Console.WriteLine();
                            break;
                        }
                        int count = 1;
                        Console.WriteLine("These are your current Ice Creams");
                        foreach (IceCream i in customer.CurrentOrder.IceCreamList)
                        {
                            Console.WriteLine("Index: {0}\t{1}", count, i.ToString());
                            count++;
                        }
                        Console.WriteLine();
                        Console.Write("Plesae choose a icecream to edit: ");
                        int index_icecream = Convert.ToInt32(Console.ReadLine());
                        
                        customer.CurrentOrder.ModifyIceCream(index_icecream);
                        break;


                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Index out of range");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input");
                    }


                    

                }
            }

            else if (option == "7")
            {
                while (true)
                {

                    try
                    {

                        if(regularQueue.Count() == 0 && goldQueue.Count() == 0)
                        {
                            Console.WriteLine("There is no current orders in the queue");
                            Console.WriteLine();
                            break;
                        }

                        
                        double cost = 0;
                        Customer customer = null;
                        Order first_order = null;
                        //Declaring the first order out of the queue
                        if (goldQueue.Count == 0)
                        {
                            first_order = regularQueue.First();
                            regularQueue.Dequeue();
                        }
                        else
                        {
                            first_order = goldQueue.First();
                            goldQueue.Dequeue();
                        }
                            

                        //Searching for the customer
                        foreach(Customer c  in customer_list)
                        {
                            if( c.CurrentOrder != null && c.CurrentOrder.Id == first_order.Id )
                            {
                                customer = c;
                                break;
                            }
                        }
                        List<IceCream> dupe_icecreamlist = first_order.IceCreamList.ToList();
                        bool special = false;
                        if(customer.Reward.PunchCard == 10)
                        {
                            customer.Reward.Punch();
                            special = true;
                        }
                        //Displaying all ice cream in that order
                        foreach (IceCream i in first_order.IceCreamList)
                        {
                            Console.WriteLine(i.ToString());
                            cost += i.CalculatePrice();
                            if (customer.Reward.PunchCard < 10)
                            {
                                customer.Reward.Punch();
                            }
                        }
                        Console.WriteLine("COST: " + cost);

                        //Check if it is customer's bday
                        double most_ex = 0;
                        if (customer.IsBirthday())
                        {
                            foreach (IceCream i in first_order.IceCreamList)
                            {
                                double current_price = i.CalculatePrice();
                                if (current_price > most_ex)
                                {
                                    most_ex = current_price;
                                    dupe_icecreamlist.Remove(i);

                                }
                            }
                            cost -= most_ex;
                        }
                        //Checking for punch card
                        if (customer.Reward.PunchCard > 10 || special)
                        {
                            cost -= dupe_icecreamlist[0].CalculatePrice();
                            if(special)
                            {
                                customer.Reward.Punch();
                            }
                            

                        }

                            

                        //Checking eligibility of redeeming points
                        if (customer.Reward.Tier != "Ordinary")
                        {
                            while (true)
                            {
                                try
                                {
                                    if (cost == 0)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("The bill is already 0 dollars");
                                        Console.WriteLine();
                                        break;
                                    }
                                    Console.WriteLine("Available Points: {0}", customer.Reward.Points);
                                    Console.Write("How much points do you want to redeem: ");
                                    int points_redeem = Convert.ToInt32(Console.ReadLine());
                                    //Offsetting the price, Conversion rate, 1pt = $0.02
                                    double offset_amount = points_redeem * 0.02;

                                    if (points_redeem < 0)
                                    {
                                        Console.WriteLine("Please enter a integer that is >0");
                                        continue;
                                    }

                                   
                                    if(points_redeem > customer.Reward.Points)
                                    {
                                        Console.WriteLine("Insufficient points");
                                        continue;
                                    }

                                    else if ((cost - offset_amount) < 0)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Cannot redeem this amount, the bill will become negative.");
                                        Console.WriteLine();
                                        continue;
                                    }

                                    customer.Reward.RedeemPoints(points_redeem);
                                    cost -= offset_amount;

                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Please enter a integer");
                                }

                            }

                        }


                        //Displaying total bill
                        Console.WriteLine("FINAL COST: $" + cost);


                        //Displaying membership status and points
                        Console.WriteLine("Membership Status: {0}\tPoints: {1}", customer.Reward.Tier, customer.Reward.Points);

                        //Prompting user to press any key to complete payment
                        Console.WriteLine("Please press any key to complete payment");
                        string key = Console.ReadLine();

                        //Adding of Points and punchcard
                        customer.Reward.AddPoints(CalculatePoints(cost));

                        customer.CurrentOrder.TimeFulfilled = DateTime.Now;
                        customer.OrderHistory.Add(customer.CurrentOrder);
                        Order order_append = first_order;

                        customer.CurrentOrder = null;
                        //Appending to the the files (ORDER)
                        using (StreamWriter sw = new StreamWriter ("orders.csv",true))
                        {
                            //Header

                            foreach(IceCream i in order_append.IceCreamList)
                            {
                                List<string> flavour_list = new List<string>();

                                int id = 0;
                                DateTime timerecieved;
                                DateTime timefulfilled;
                                string options;
                                int scoops;
                                string dipped = "";
                                string waffleflavour = "";
                                string flav1 = "";
                                string flav2 = "";
                                string flav3 = "";
                                string topping1 = "";
                                string topping2 = "";
                                string topping3 = "";
                                string topping4 = "";
                                id = order_append.Id;
                                int memberid = customer.MemberId;
                                timerecieved = order_append.TimeReceived;
                                timefulfilled = Convert.ToDateTime(order_append.TimeFulfilled);

                                if (i.Option == "Cone")
                                {
                                    Cone cone = (Cone)i;
                                    if(cone.Dipped)
                                    {
                                        dipped = "TRUE";
                                    }
                                    else
                                    {
                                        dipped = "FALSE";
                                    }
                                }
                                if(i.Option == "Waffle")
                                {
                                    Waffle waffle = (Waffle)i;
                                    waffleflavour = waffle.WaffleFlavour;
                                }
                                foreach(Flavour f in i.Flavours)
                                {
                                    flavour_list.Add(f.Type);
                                }
                                flav1 = flavour_list[0];
                                
                                foreach(string s in flavour_list)
                                {
                                    try
                                    {
                                        flav1 = flavour_list[0];
                                        flav2 = flavour_list[1];
                                        flav3 = flavour_list[2];
                                        
                                    }
                                    catch(ArgumentOutOfRangeException)
                                    {
                                        break;
                                    }
                                }

                                
                                foreach(Topping t in i.Toppings)
                                {
                                    try
                                    {
                                        topping1 = i.Toppings[0].Type;
                                        topping2 = i.Toppings[1].Type;
                                        topping3 = i.Toppings[2].Type;
                                        topping4 = i.Toppings[3].Type;
                                    }
                                    catch(ArgumentOutOfRangeException)
                                    {
                                        break;
                                    }
                                }
                                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", id, memberid, timerecieved, timefulfilled, i.Option, i.Scoops, dipped, waffleflavour,flav1,flav2,flav3,topping1,topping2,topping3,topping4) ;
                            }


                        }

                        //Appending to customer
                        using (StreamWriter sw = new StreamWriter("customers.csv",false))
                        {
                            sw.WriteLine("Name,MemberId,DOB,MembershipStatus,MembershipPoints,PunchCard");
                            foreach(Customer c in customer_list)
                            {
                                sw.WriteLine("{0},{1},{2},{3},{4},{5}", c.Name,c.MemberId,c.Dob,c.Reward.Tier,c.Reward.Points,c.Reward.PunchCard);
                            }
                        }



                        break;

                       
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Index out of range");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
            }
                
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Input");
        }
    }
}


DisplayMenu();
