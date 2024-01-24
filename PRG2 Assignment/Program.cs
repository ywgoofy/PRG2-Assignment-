//Reading the files

using PRG2_Assignment;
using System.ComponentModel.Design;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;


//Premium flavours
List<string> premium_flavours = new List<string> { "durian", "ube", "sea salt" };

//Toppings list
List<string> toppings_list = new List<string> { "sprinkles", "mochi", "sago", "orea" };

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
    if (temp == "vanilla" || temp == "chocolate" || temp == "strawberry" || temp == "durian" || temp == "ube" || temp == "sea salt")
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
List<string> flavour_list = new List<string>();
List<string> topping_list = new List<string>();
/*
for (int i = 0; i<order_info.Count; i++)
{
    int id = Convert.ToInt32(order_info[i][0]);
    int memberId = Convert.ToInt32(order_info[i][1]);
    DateTime timeRecieved = Convert.ToDateTime(order_info[i][2]);
    DateTime timeFulfilled = Convert.ToDateTime(order_info[i][3]);
    string option = order_info[i][4];
    int scoops = Convert.ToInt32(order_info[i][5]);
    bool dipped = Convert.ToBoolean(order_info[i][6]);
    string waffleFlavour = order_info[i][7];
    string flavour1 = order_info[i][8];
    flavour_list.Add(flavour1);
    Flavour flav = new Flavour(flavour1,IsPremium(flavour1.ToLower()),1);
    string flavour2;
    Flavour flav2 = new Flavour() ;
    string flavour3;
    if(string.IsNullOrEmpty(flavour2 = order_info[i][9])) //Flavour 2
    {
        flavour2 = order_info[i][9];
        if(flavour_list.Contains(flavour2))
        {
            flav.Quantity++;
        }
        else
        {
            flavour_list.Add(flavour2);
            flav2 = new Flavour(flavour2, IsPremium(flavour2), 1);
        }
        
    }
    if (string.IsNullOrEmpty(flavour3 = order_info[i][10])) //Flavour 3
    {
        flavour3 = order_info[i][10];
        if (flavour_list.Contains(flavour3))
        {
            foreach (string s in flavour_list)
            {
                if(s == flavour3)
                {
                    if(flav.Type == s)
                    {
                        flav.Quantity++;
                    }
                    flav2.Type = flavour2;
                    flav2.Premium = IsPremium(flavour2);
                    flav2.Quantity = 1;
                }
            }
        }
        else
        {
            flavour_list.Add(flavour3);
            Flavour flav3 = new Flavour(flavour3, IsPremium(flavour3), 1);
        }

    }
    string topping1;
    string topping2;
    string topping3;
    string topping4;
    
    topping1 = order_info[i][11];
    topping_list.Add(topping1); //Topping 1
    if(string.IsNullOrEmpty(topping2 = order_info[i][12])) //Topping 2
    {
        topping2 = order_info[i][12];
        if(toppings_list.Contains(topping2))
        {

        }
        topping_list.Add(topping2);
    }
    if(string.IsNullOrEmpty(topping3 = order_info[i][13])) //Topping 3
    {
        topping3 = order_info[i][13];
        topping_list.Add(topping3);
    }
    if(string.IsNullOrEmpty(topping4 = order_info[i][14])) //Topping 4
    {
        topping4 = order_info[i][14];
        topping_list.Add(topping4);
    }


    Order order = new Order(id, timeRecieved);

}
*/


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





//Option 5



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














            else if(option == "5") //NOT FINISHED
            {
                DisplayCustomer();
                Console.Write("Please select a customer using the index");
                int index = Convert.ToInt32(Console.ReadLine());
                Customer c = customer_list[index - 1];
                Console.WriteLine(c.ToString());
            }
            else if(option == "6")
            {
                DisplayCustomer();
                Console.WriteLine("Please select a customer using the index");
                int index = Convert.ToInt32(Console.ReadLine());
                
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Input");
        }
    }
}


DisplayMenu();
