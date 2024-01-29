using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10257103
// Student Name : Lim Ze Yu
// Partner Name : Tan Yew Wren
//==========================================================
namespace PRG2_Assignment
{
    class Order
    {
        private int id;
        public int Id { get; set; }

        private DateTime timereceived;
        public DateTime TimeReceived { get; set; }

        private DateTime? timefulfilled;
        public DateTime? TimeFulfilled { get; set; }

        private List<IceCream> iceCreamList;
        public List<IceCream> IceCreamList { get; set; } = new List<IceCream>();

        //Constructors
        public Order() { }
        public Order(int i, DateTime tr)
        {
            Id = i;
            TimeReceived = tr;
            TimeFulfilled = null;
        }

        //Methods
        public void ModifyIceCream(int i)
        {
            //IceCream iceCream = IceCreamList[i - 1];
            //Flavours
            List<string> flavours = new List<string>();

            //Premium flavours
            List<string> premium_flavours = new List<string>();

            //Toppings list
            List<string> toppings_list = new List<string>();

            //Premium flavours
            //List<string> premium_flavours = new List<string> { "durian", "ube", "sea salt" };

            //Toppings list
            //List<string> toppings_list = new List<string> {"sprinkles","mochi","sago","orea"};

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


            //Utility Methods

            bool IsPremium(string temp) //Checks if the flavour is premium or not
            {
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

            string IsToppings(string temp)
            {
                if(toppings_list.Contains(temp))
                {
                    foreach(string t in toppings_list)
                    {
                        if(t.ToLower() == temp)
                        {
                            return t;
                        }
                    }
                }
                return "ERROR";
            }
            try
            {
                while (true)
                {
                    IceCream iceCream = IceCreamList[i - 1];
                    Console.WriteLine("[1] Option");
                    Console.WriteLine("[2] Scoops");
                    Console.WriteLine("[3] Flavours");
                    Console.WriteLine("[4] Toppings");
                    if (iceCream is Cone)
                    {
                        Console.WriteLine("[5] Dipped Cone");
                    }
                    else if (iceCream is Waffle)
                    {
                        Console.WriteLine("[5] Waffle Flavour");
                    }

                    Console.Write("What would you like to modify? (0 to exit) : ");
                    string choice = Console.ReadLine();
                    Console.WriteLine();

                    if (choice == "0")
                    {
                        break;
                    }
                    //Options
                    else if (choice == "1")
                    {
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("These are the available options:" + "\n1\tCup" + "\n2\tCone" + "\n3\tWaffle" );

                                Console.Write("Please enter the index for the options you want (0 to exit): ");
                                string option = Console.ReadLine();
                                string temp = option.ToLower();
                                if (option == "0")
                                {
                                    break;
                                }
                                else if(option == "1" && iceCream is not Cup)
                                {
                                    Console.WriteLine("Changed to Cup");
                                    Console.WriteLine();
                                    iceCream.Option = "Cup";
                                    Cup iceCream2 = new Cup(iceCream.Option,iceCream.Scoops,iceCream.Flavours,iceCream.Toppings);
                                    IceCreamList[i - 1] = iceCream2;
                                    break;
                                }
                                else if(option == "2" && iceCream is not Cone)
                                {
                                    Console.WriteLine("Changed to Cone");
                                    Console.WriteLine();
                                    

                                    Cone iceCream2 = new Cone(iceCream.Option, iceCream.Scoops, iceCream.Flavours, iceCream.Toppings,false); //Default set dipped cone to false
                                    IceCreamList[i-1] = iceCream2;

                                    break;
                                }
                                else if(option == "3" && iceCream is not Waffle)
                                {
                                    Console.WriteLine("Changed to Waffle");
                                    Console.WriteLine();
                                    iceCream.Option = "Waffle";
                                    Waffle iceCream2 = new Waffle(iceCream.Option, iceCream.Scoops, iceCream.Flavours, iceCream.Toppings,"Original");// Default set to original
                                    IceCreamList[i - 1] = iceCream2;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Option or you already have that option");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid Input");
                            }
                        }
                    }
                    //Toppings
                    else if (choice == "2")
                    {
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("[0] Exit");
                                Console.WriteLine("[1] Add Scoop");
                                Console.WriteLine("[2] Remove Scoop");
                                Console.WriteLine();
                                Console.WriteLine("The flavours available are:");
                                Console.WriteLine("Vanilla\nChocolate\nStrawberry\nDurain\nUbe\nSea salt\n");
                                Console.Write("Please enter option: ");
                                int option = Convert.ToInt32(Console.ReadLine());

                                if(option == 0 )
                                {
                                    break;
                                }
                                if(option == 1)
                                {
                                    if(iceCream.Scoops == 3)
                                    {
                                        Console.WriteLine("You have the maximum amount of scoops in a icecream");
                                        Console.WriteLine();
                                        break;
                                    }
                                    //Adding scoops
                                    bool premium = false; //Default set premium to false
                                    int count = 1;
                                    Console.WriteLine("These are the flavours available");
                                    foreach(string s in flavours)
                                    {
                                        Console.WriteLine("{0}\t{1}", count, s);
                                        count++;
                                    }

                                    while(true)
                                    {
                                        try
                                        {
                                            bool key = false;
                                            Console.WriteLine();
                                            Console.Write("Please enter a index[0 to exit]: ");
                                            
                                            int index = Convert.ToInt32(Console.ReadLine());
                                            if(index == 0)
                                            {
                                                break;
                                            }
                                            Flavour flavour = new Flavour(flavours[index - 1], IsPremium(flavours[index - 1]), 1);
                                            foreach (Flavour f in iceCream.Flavours)
                                            {
                                                if (f.Type == flavour.Type)
                                                {
                                                    f.Quantity += 1;
                                                    key = true;
                                                    break;
                                                }
                                            }
                                            if (!key)
                                            {
                                                iceCream.Flavours.Add(flavour);
                                                
                                            }
                                            iceCream.Scoops++;
                                            Console.WriteLine("Flavour Added");
                                            Console.WriteLine();   
                                            break;
                                        }
                                        catch(ArgumentOutOfRangeException)
                                        {
                                            Console.WriteLine("Index out of range");
                                        }
                                        catch(FormatException)
                                        {
                                            Console.WriteLine("Invalid input");
                                        }

                                    }
                                }
                                else if(option == 2)
                                {
                                    
                                    int count = 1;
                                    //Removing Flavour
                                    Console.WriteLine("These are the flavours you currently have.");
                                    Console.WriteLine("{0,-10}{1,-15}{2,-15}", "Index", "Flavour", "Quantity");
                                    foreach (Flavour f in iceCream.Flavours)
                                    {
                                        Console.WriteLine("{0,-10}{1,-15}{2,-15}", count, f.Type, f.Quantity);
                                        count++;
                                    }
                                    Console.WriteLine();
                                    while (true)
                                    {
                                        bool success = false;
                                        try
                                        {
                                            Console.Write("Please enter the index[0 to exit]: ");
                                            int index = Convert.ToInt32(Console.ReadLine());
                                            if(index == 0)
                                            {
                                                break;
                                            }
                                            Flavour flavour_remove = new Flavour(iceCream.Flavours[index - 1].Type, IsPremium(iceCream.Flavours[index - 1].Type), 1);
                                            foreach (Flavour f in iceCream.Flavours)
                                            {
                                                if (f.Type == flavour_remove.Type)
                                                {
                                                    Console.WriteLine("Scoops removed");
                                                    if (f.Quantity > 1)
                                                    {
                                                        f.Quantity -= 1;
                                                        iceCream.Scoops--;
                                                        success = true;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        iceCream.Scoops--;
                                                        iceCream.Flavours.Remove(f);
                                                        success = true;
                                                        break;
                                                    }

                                                }
                                            }
                                            if (success)
                                            {
                                                break;
                                            }
                                            
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
                                                                   
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                    Console.WriteLine();
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid Input");
                            }
                        }
                    }
                    //Flavours
                    else if (choice == "3")
                    {
                        while (true)
                        {
                            try
                            {
                                
                                bool premium = false; //Default to false
                                bool key = false;
                                Console.WriteLine();
                                
                                
                                int count = 1;
                                Console.WriteLine("These are your current Flavours.");
                                Console.WriteLine("{0,-10}{1,-15}{2,-15}", "Index", "Flavour", "Quantity");
                                foreach (Flavour f in iceCream.Flavours)
                                {
                                    Console.WriteLine("{0,-10}{1,-15}{2,-15}", count, f.Type, f.Quantity);
                                    count++;
                                }
                                Console.WriteLine();
                                Console.Write("Enter the index [0 to exit]: ");
                                int index = Convert.ToInt32(Console.ReadLine());
                                if (index == 0)
                                {
                                    Console.WriteLine("Exiting");
                                    Console.WriteLine();
                                    break;
                                }
                                Flavour flavour_to_change = iceCream.Flavours[index-1];
                                count = 1;
                                Console.WriteLine();
                                Console.WriteLine("These are the available flavours to change to");
                                foreach(string s in flavours)
                                {
                                    Console.WriteLine("{0,-10}{1,-15}",count,s);
                                    count++;
                                }
                                Console.WriteLine();
                                Console.Write("Please enter the index for the flavour you want to change to: ");
                                index = Convert.ToInt32(Console.ReadLine());
                                Flavour flavour_to_change_to = new Flavour(flavours[index-1], IsPremium(flavours[index-1]),1);

                                foreach(Flavour f in iceCream.Flavours)
                                {
                                    if(f.Type == flavour_to_change.Type)
                                    {
                                        if (f.Quantity > 1)
                                        {
                                            //What if they change the flavour to the same flavour they change to 
                                            f.Quantity--;
                                            
                                        }
                                        else
                                        {
                                            iceCream.Flavours.Remove(flavour_to_change);
                                            break;
                                        }

                                    }
                                        
                                }
                                bool key_add = true;
                                foreach (Flavour f1 in iceCream.Flavours)
                                {
                                    if (f1.Type == flavour_to_change_to.Type) //Checking if the flavour to change to exist in the flavour list alrdy
                                    {
                                        f1.Quantity++;
                                        key_add = false;
                                        break;
                                    }
                                    
                                }
                                if(key_add)
                                {
                                    iceCream.Flavours.Add(flavour_to_change_to);
                                    break;
                                }

                                Console.WriteLine();
                                break;
                                
                            }
                            catch(ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("Index out of range");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid Input");
                            }
                        }
                    }
                    //Toppings
                    else if (choice == "4")
                    {

                        try
                        {
                            Console.WriteLine("Toppings you currently have:");
                            foreach (Topping t in iceCream.Toppings)
                            {
                                Console.WriteLine(t.Type);
                            }
                            Console.WriteLine();
                            
                            while (true)
                            {

                                Console.WriteLine("[0] Exit");
                                Console.WriteLine("[1] Add Toppings");
                                Console.WriteLine("[2] Remove Toppings");
                                Console.Write("Enter Choice: ");
                                string option = Console.ReadLine();
                                if (option == "0")
                                {
                                    break;
                                }
                                else if (option == "1")
                                {

                                    while (true)
                                    {
                                        try
                                        {
                                            bool key = false;
                                            int count = 1;
                                            Console.WriteLine("These are the flavours available");
                                            foreach (string s in toppings_list)
                                            {
                                                Console.WriteLine("{0}\t{1}", count, s);
                                                count++;
                                            }
                                            Console.Write("Please enter the number of the topping you want [0 to exit]: ");
                                            int index = Convert.ToInt32(Console.ReadLine());

                                            if (index == 0)
                                            {
                                                Console.WriteLine("Exiting");
                                                Console.WriteLine();
                                                break;
                                            }
                                            string topping_type = toppings_list[index - 1];
                                            Topping topping = new Topping(topping_type);


                                            if (index > toppings_list.Count)
                                            {
                                                Console.WriteLine("Index out of range");
                                            }
                                            foreach (Topping t in iceCream.Toppings)
                                            {
                                                if (t.Type == topping.Type)
                                                {
                                                    key = true;
                                                }
                                            }
                                            if (key)
                                            {
                                                Console.WriteLine("Cannot add duplicate toppings");
                                                Console.WriteLine();
                                                continue;
                                            }
                                            if (!key)
                                            {
                                                Console.WriteLine("Topping added");
                                                iceCream.Toppings.Add(topping);
                                                break;
                                            }
                                        }
                                        catch(ArgumentOutOfRangeException)
                                        {
                                            Console.WriteLine("Index out of range");
                                        }
                                        catch(FormatException)
                                        {
                                            Console.WriteLine("Input is invalid");
                                        }
                                    }
                                }
                                else if (option == "2")
                                {
                                    int count = 1;
                                    Console.WriteLine("Toppings you currently have:");
                                    foreach (Topping t in iceCream.Toppings)
                                    {
                                        Console.WriteLine("{0}\t{1}", count, t.Type);
                                        count++;
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            bool key = false;
                                            Topping topping = null;
                                            Console.Write("Please enter the index of the toppings you want to remove [0 to exit]: ");
                                            int index = Convert.ToInt32(Console.ReadLine());
                                            if (index == 0)
                                            {
                                                Console.WriteLine("Exiting");
                                                break;
                                            }

                                            iceCream.Toppings.Remove(iceCream.Toppings[index - 1]);
                                            Console.WriteLine("Removed");
                                            Console.WriteLine();
                                            break;
                                        }
                                        catch (ArgumentOutOfRangeException)
                                        {
                                            Console.WriteLine("Index out of range");
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Input is in a incorrect format, please enter integers");
                                        }


                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option");
                                }
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid Input");
                        }
                        
                    }

                    else if(choice == "5")
                    {

                        if (iceCream is Cone)
                        {
                            Console.WriteLine("[0] Exit");
                            Console.WriteLine("[1] Add dipped cone");
                            Console.WriteLine("[2] Remove dipped cone");
                            while (true)
                            {
                                try
                                {
                                    Cone iceCream2 = (Cone)iceCream;
                                    Console.Write("Please enter your option: ");
                                    string option = Console.ReadLine();
                                    if(option == "0")
                                    {
                                        Console.WriteLine("Exiting");
                                        Console.WriteLine();
                                        break;
                                    }
                                    if (option == "1")
                                    {
                                        iceCream2.Dipped = true;
                                        break;
                                    }
                                    else if (option == "2")
                                    {
                                        iceCream2.Dipped = false;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid option");
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invalid Input");
                                }
                            }
                        }
                        else if(iceCream is Waffle)
                        {
                            Console.WriteLine("These are the available waffle flavours");
                            Console.WriteLine("[1] Original" + "\n[2] Red Velvet" + "\n[3] Characoal" + "\n[4] Pandan");
                            while (true)
                            {
                                try
                                {
                                    Waffle iceCream2 = (Waffle)iceCream;
                                    Console.Write("Please enter the index for your waffle flavour [0] to exit : ");
                                    string index = Console.ReadLine();
                                    if (index == "0")
                                    {
                                        break;
                                    }
                                    if (index == "1")
                                    {
                                        iceCream2.WaffleFlavour = "Original";
                                        break;
                                    }
                                    else if (index == "2")
                                    {
                                        iceCream2.WaffleFlavour = "Red Velvet";
                                        break;
                                    }
                                    else if (index == "3")
                                    {
                                        iceCream2.WaffleFlavour = "Characoal";
                                        break;
                                    }
                                    else if (index == "4")
                                    {
                                        iceCream2.WaffleFlavour = "Pandan";
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Option");
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invalid input");
                                }
                            }
                        }
       
                        
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        Console.WriteLine();
                    }


                }
            }
            catch(FormatException)
            {
                Console.WriteLine("Invalid Input");
            }
        }

        public void AddIceCream(IceCream i)
        {
            IceCreamList.Add(i);
        }

        public void DeleteIceCream(int i)
        {
            IceCreamList.Remove(IceCreamList[i - 1]); //i-1 because int i is option of the icecream chosen from the icecreams displayed in the order starting by 1
        }

        public double CalculateTotal()
        {
            double total = 0;

            foreach (IceCream i in IceCreamList)
            {
                total += i.CalculatePrice();
            }

            return total;
        }

        public override string ToString()
        {
            string str = null;
            foreach (IceCream i in IceCreamList)
            {
                str += i.ToString() + "\n";
            }
            return "Id: " + Id + " TimeReceived: " + TimeReceived + " TimeFulfilled: " + TimeFulfilled + "\nIceCreams: " + str;
        }
    }
}
