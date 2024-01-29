
using PRG2_Assignment;
using System.ComponentModel.Design;
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

//Utility Methods

bool IsPremium(string temp) //Checks if the flavour is premium or not
{
    temp = temp.ToLower();
    if (premium_flavours.Contains(temp))
    {
        return true;
    }
    else
    {
        return false;
    }
}

bool IsFlavour(string temp)
{
    temp = temp.ToLower();
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

//TESTING FOR OPTION 6
Order temp_order = new Order(6, DateTime.Now);
List<Flavour> temp_flavour_list = new List<Flavour>();
List<Topping> temp_toppings_list = new List<Topping>();
temp_flavour_list.Add(new Flavour("Strawberry", false, 1));
temp_flavour_list.Add(new Flavour("Chocolate", false, 1));
temp_toppings_list.Add(new Topping("Oreos"));
temp_toppings_list.Add(new Topping("Mochi"));
temp_order.IceCreamList.Add(new Cone("Cone", 2, temp_flavour_list, temp_toppings_list,false));
//TESTING FOR OPTION 6

//Adding dynamically of flavours and premium flavours

List<string[]> flavour_info = ReadingFile("flavours.csv");

foreach (string[] s in flavour_info)
{
    flavours.Add(s[0].ToLower());
    if (Convert.ToInt32(s[1]) > 0)
    {
        premium_flavours.Add(s[0].ToLower());
    }
}

//Adding dynamically of toppings
List<string[]> toppings_info = ReadingFile("toppings.csv");

foreach (string[] s in toppings_info)
{
    toppings_list.Add(s[0].ToLower());
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
    Flavour flav = new Flavour(flavour1,IsPremium(flavour1.ToLower()),1);
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














            else if(option == "5")
            {
                string flavour = "";
                string toppings = "";
                DisplayCustomer();
                Console.Write("Please select a customer using the index: ");
                int index = Convert.ToInt32(Console.ReadLine());

                Customer c = customer_list[index - 1];
                //Header 
                //Console.WriteLine("Current Order");
                //Console.WriteLine("{0,-25}{1,-25}",c.CurrentOrder.TimeReceived,c.CurrentOrder.TimeFulfilled);
                Console.WriteLine();
                Console.WriteLine("Order History");
                Console.WriteLine();
                
                foreach (Order o in c.OrderHistory)
                {
                    Console.WriteLine("{0,-25}{1,-25}", "TimeRecieved", "TimeFulfilled");
                    Console.WriteLine("{0,-25}{1,-25}",o.TimeReceived,o.TimeFulfilled);
                    Console.WriteLine("IceCream Details");
                    foreach (IceCream i in o.IceCreamList)
                    {
                        foreach(Flavour f in i.Flavours)
                        {
                            flavour += f.Type + "|";
                        }
                        foreach (Topping t in i.Toppings)
                        {
                            toppings += t.Type + "|";
                        }

                        Console.WriteLine("Option: {0,-15} Scoops: {1,-15} Flavours: |{2,-15} Toppings: |{3,-15}", i.Option,i.Scoops,flavour,toppings);
                        Console.WriteLine();
                        flavour = "";
                        toppings = "";
                    }
                    
                }
            }
            else if(option == "6")
            {
                /*DisplayCustomer();
                Console.WriteLine("Please select a customer using the index: ");
                int index = Convert.ToInt32(Console.ReadLine());*/

                
                temp_order.ModifyIceCream(1);

               

                
                

                foreach(IceCream i in temp_order.IceCreamList)
                {
                    Console.WriteLine(i.ToString());
                    
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
