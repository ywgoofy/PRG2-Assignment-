//Reading the files

using PRG2_Assignment;
using System.ComponentModel.Design;
using System.Security.Authentication;

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

//Option 1 Listing of customer
void DisplayCustomer()
{
    string CurrentOrder = null;
    string s = null;
    Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}", "Name","MemberID","Dob","CurrentOrder","OrderHistory","Rewards"); 
    
    foreach(Customer c in customer_list)
    {
        foreach(Order o in c.OrderHistory)
        {
            s = s + o.ToString();
        }
        Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}",c.Name,c.MemberId,c.Dob.ToString("dd/MM/yyyy"),c.CurrentOrder,s,c.Reward);
    }
    Console.WriteLine();
}



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
            
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Input");
        }
    }
}


DisplayMenu();

