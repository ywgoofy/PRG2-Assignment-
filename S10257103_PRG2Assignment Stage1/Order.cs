using System;
using System.Collections.Generic;
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
        public List<IceCream> IceCreamsList { get; set; } = new List<IceCream>();

        //Constructors
        public Order() { }
        public Order(int i, DateTime tr)
        {
            Id = i;
            TimeReceived = tr;
            TimeFulfilled = null;
        }

        //Methods
        public void ModifyIceCream(int i) //Nah this one i discuss with that pookie, shit extra confusing
        {
            IceCream iceCream = iceCreamList[i - 1];
            //Premium flavours
            List<string> premium_flavours = new List<string> { "durian", "ube", "sea salt" };

            //Toppings list
            List<string> toppings_list = new List<string> {"sprinkles","mochi","sago","orea"};

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
                if(toppings_list.Contains(temp))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            try
            {
                while (true)
                {
                    Console.WriteLine("[1] Option");
                    Console.WriteLine("[2] Scoops");
                    Console.WriteLine("[3] Flavours");
                    Console.WriteLine("[4] Toppings");
                    if (iceCream.Option == "Cone")
                    {
                        Console.WriteLine("[5] Dipped Cone");
                    }
                    else if (iceCream.Option == "Waffle")
                    {
                        Console.WriteLine("[5]Waffle Flavour");
                    }

                    Console.Write("What would you like to modify? (0 to exit) : ");
                    string choice = Console.ReadLine();

                    if (choice == "0")
                    {
                        break;
                    }
                    else if (choice == "1")
                    {
                        while (true)
                        {
                            try
                            {


                                Console.WriteLine("What options would you like? (0 to exit): ");
                                string option = Console.ReadLine();
                                string temp = option.ToLower();
                                if (option == "0")
                                {
                                    break;
                                }
                                else if (temp == "cup" || temp == "cone" || temp == "waffle")
                                {
                                    iceCream.Option = option;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Option");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid Input");
                            }
                        }
                    }  else if (choice == "2")
                    {
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("[0] Exit");
                                Console.WriteLine("[1] Add Scoop");
                                Console.WriteLine("[2] Remove Scoop");
                                Console.WriteLine("The flavours available are:");
                                Console.WriteLine("Vanilla\nChocolate\nStrawberry\nDurain\nUbe\nSea salt\n");
                                Console.WriteLine("Please enter option: ");

                                int option = Convert.ToInt32(Console.ReadLine());
                                if (option == 0)
                                {
                                    break;
                                }
                                string temp = null;
                                bool premium = false;
                                while (true)
                                {
                                    Console.WriteLine("Please enter flavour for Scoop: ");
                                    temp = Console.ReadLine(); //Flavour
                                    temp = temp.ToLower();

                                    if (IsFlavour(temp))
                                    {
                                        premium = IsPremium(temp);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid flavour");
                                    }
                                }
                                Flavour flavour = new Flavour(temp, premium, 1);
                                if (option == 1)
                                {
                                    bool key = true;
                                    foreach (Flavour f in iceCream.Flavours)
                                    {
                                        if (f.Type == flavour.Type)
                                        {
                                            f.Quantity++;
                                            key = false;
                                            break;
                                        }
                                        
                                    }
                                    if (key)
                                    {
                                        iceCream.Flavours.Add(flavour);
                                        

                                    }
                                    iceCream.Scoops++;


                                }
                                else if (option == 2)
                                {
                                    for (int j = 0; j < iceCream.Flavours.Count; j++)
                                    {
                                        if (iceCream.Flavours[j].Type.ToLower() == temp)
                                        {
                                            if ((iceCream.Flavours[j].Quantity - 1) == 0)
                                            {
                                                iceCream.Flavours.RemoveAt(j);
                                            }
                                            else
                                            {
                                                iceCream.Flavours[j].Quantity -= 1;
                                            }
                                            iceCream.Scoops -= 1;
                                        }
                                        else
                                        {
                                            Console.WriteLine("You currently don't have this flavour.");
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

                    else if (choice == "3")
                    {
                        while (true)
                        {
                            try
                            {
                                int count = 1;
                                bool premium;
                                bool key = false;
                                Console.WriteLine("These are your current Flavours.");
                                foreach (Flavour f in iceCream.Flavours)
                                {
                                    Console.WriteLine(f.ToString());
                                }


                                Console.WriteLine();
                                string flavour_to_change;
                                string flavour_to_replace;
                                while (true)
                                {
                                    Console.Write("Which flavours would you like to change: ");
                                    flavour_to_change = Console.ReadLine();
                                    
                                    foreach (Flavour f in iceCream.Flavours)
                                    {
                                        if (flavour_to_change.ToLower() == f.Type.ToLower())
                                        {
                                            key = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("You have no such flavour");
                                            key = false;
                                        }
                                    }
                                    if (key)
                                    {
                                        break;
                                    }
                                }

                                while (true)
                                {
                                    Console.Write("What flavour would you like to replace it with: ");
                                    flavour_to_replace = Console.ReadLine();
                                    if (IsFlavour(flavour_to_replace))
                                    {
                                        premium = IsPremium(flavour_to_replace);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid flavour");
                                    }
                                }
                                Flavour flavour_replace = new Flavour(flavour_to_replace, premium, 1);

                                bool pass = true;
                                foreach (Flavour f in iceCream.Flavours)
                                {
                                    key = false;
                                    if (f.Type.ToLower() == flavour_to_change.ToLower() && f.Quantity > 1)
                                    {
                                        pass = false;
                                        f.Quantity -= 1;

                                        foreach (Flavour f1 in iceCream.Flavours)
                                        {
                                            if (f1.Type.ToLower() == flavour_to_replace.ToLower())
                                            {
                                                f1.Quantity += 1;
                                                key = true;
                                                break;
                                            }

                                        }
                                        if (!key)
                                        {
                                            iceCream.Flavours.Add(flavour_replace);
                                        }
                                        break;
                                    
                                    }
                                    
                                }
                                if (pass)
                                {
                                    foreach (Flavour f in iceCream.Flavours)
                                    {
                                        if (f.Type.ToLower() == flavour_to_change.ToLower() && f.Quantity <= 1)
                                        {
                                            key = false;
                                            iceCream.Flavours.Remove(f);
                                            foreach (Flavour f1 in iceCream.Flavours)
                                            {
                                                if (f1.Type == flavour_to_replace)
                                                {
                                                    f1.Quantity += 1;
                                                    key = true;
                                                    break;
                                                }

                                            }
                                            if (!key)
                                            {
                                                iceCream.Flavours.Add(flavour_replace);
                                            }
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
                                    Console.Write("Which toppings would you like to add?: ");
                                    string topping_response = Console.ReadLine();
                                    if (IsToppings(topping_response.ToLower()))
                                    {
                                        Topping topping = new Topping(topping_response.ToLower());
                                        if(iceCream.Toppings.Contains(topping))
                                        {
                                            Console.WriteLine("Unable to add duplicate toppings");
                                            continue;
                                        }
                                        iceCream.Toppings.Add(topping);
                                        Console.WriteLine("Topping added");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Topping");
                                    }
                                }
                            }
                            else if (option == "2")
                            {
                                while (true)
                                {
                                    bool key = false;
                                    Console.WriteLine("Which toppings would you like to remove?: ");
                                    string topping_response = Console.ReadLine();
                                    if (IsToppings(topping_response.ToLower()))
                                    {
                                        foreach (Topping t in iceCream.Toppings)
                                        {
                                            if (t.Type.ToLower() == topping_response.ToLower())
                                            {
                                                iceCream.Toppings.Remove(t);
                                                key = true;
                                            }
                                        }
                                        if (key)
                                        {
                                            Console.WriteLine("Topping removed");
                                            break;
                                        }
                                        if(!key)
                                        {
                                            Console.WriteLine("Toppings not found");
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Topping");
                                    }
                                }
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

                    else if(choice == "5")
                    {

                        if (iceCream.Option == "Cone")
                        {

                            Cone cone = (Cone)iceCream;
                            while (true)
                            {
                                Console.WriteLine("[1] Add dipped cone");
                                Console.WriteLine("[2] Remove dipped cone");
                                try
                                {

                                    Console.WriteLine("Please enter your option: ");
                                    string option = Console.ReadLine();
                                    if (option == "1")
                                    {
                                        cone.Dipped = true;
                                        break;
                                    }
                                    else if (option == "2")
                                    {
                                        cone.Dipped = false;
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
                        else
                        {
                            Waffle waffle = (Waffle)iceCream;
                            Console.WriteLine("This is the information of your waffle");
                            Console.WriteLine(waffle.ToString());
                            List<string> waffle_list = new List<string> { "pandan","red velvet","characoal","original"};
                            try
                            {
                                while (true)
                                {
                                    Console.WriteLine();
                                    Console.Write("What waffle flavour would you like");
                                    string flavour = Console.ReadLine();
                                    string temp = flavour.ToLower();
                                    if (waffle_list.Contains(temp))
                                    {
                                        waffle.WaffleFlavour = flavour;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid flavour");
                                    }
                                }
                            }
                            catch(FormatException)
                            {
                                Console.WriteLine("Invalid Input");
                            }
                        }
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
            iceCreamList.Add(i);
        }

        public void DeleteIceCream(int i)
        {
            IceCreamsList.Remove(IceCreamsList[i - 1]); //i-1 because int i is option of the icecream chosen from the icecreams displayed in the order starting by 1
        }

        public double CalculateTotal()
        {
            double total = 0;

            foreach (IceCream i in iceCreamList)
            {
                total += i.CalculatePrice();
            }

            return total;
        }

        public override string ToString()
        {
            string str = null;
            foreach (IceCream i in iceCreamList)
            {
                str += i.ToString() + "\n";
            }
            return "Id: " + Id + " TimeReceived: " + TimeReceived + " TimeFulfilled: " + TimeFulfilled + "\nIceCreams: " + str;
        }
    }
}
