using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Order
    {
        private int id;
        public int Id { get; set;}

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
            IceCream iceCream = iceCreamList[i-1];
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
                        Console.WriteLine("What options would you like? (0 to exit): ");
                        string option = Console.ReadLine();
                        string temp = option.ToLower();
                        if (option == "0" )
                        {
                            break;
                        }
                        else if (option == "cup" || option == "cone" || option == "waffle")
                        {
                            iceCream.Option = option;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Option");
                        }
                    }
                }
                else if (choice == "2")
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("How many Scoops would you like? (0 to exit): ");
                            int scoops = Convert.ToInt32(Console.ReadLine());
                            if (scoops == 0)
                            {
                                break;
                            }
                            else if (scoops >0 && scoops <=3)
                            {
                                iceCream.Scoops = scoops;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Scoops must be >0 and <= 3");
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
                    while(true)
                    {
                        try
                        {
                            foreach(IceCream j in IceCreamsList)
                            {
                                Console.WriteLine(j.Flavours);
                            }
                            Console.Write("Please enter the flavour you want to remove: ");
                            string flavours_removed =  Console.ReadLine();
                            if (flavours_removed == "Vanilla" || flavours_removed == "Chocolate" || flavours_removed == "Strawberry" || flavours_removed == "Durian" || flavours_removed == "Ube" || flavours_removed == "Sea salt")
                            {
                                //iceCream.Flavours.Remove(flavours_removed);
                            }
                                Console.Write("Please enter the new flavours: ");
                            string flavours = Console.ReadLine();
                            flavours = flavours.ToLower();
                            if (flavours == "vanilla" || flavours == "chocolate" || flavours == "strawberry" || flavours == "durian" || flavours == "ube" || flavours == "sea salt")
                            {
                                //iceCream.Flavours.Add(flavours);
                            }
                        } 
                        catch (FormatException) 
                        { 
                            Console.WriteLine("Invalid Input"); 
                        }



                    }
                }

                
            }
        }

        public void AddIceCream(IceCream i)
        {
            iceCreamList.Add(i);
        }

        public void DeleteIceCream(int i)
        {
            IceCreamsList.Remove(IceCreamsList[i-1]); //i-1 because int i is option of the icecream chosen from the icecreams displayed in the order starting by 1
        }

        public double CalculateTotal()
        {
            double total = 0;

            foreach(IceCream i in iceCreamList)
            {
                total += i.CalculatePrice();
            }

            return total;
        }

        public override string ToString()
        {
            string str = null;
            foreach(IceCream i in iceCreamList)
            {
                str += i.ToString()+ "\n";
            }
            return "Id: " + Id + " TimeReceived: " + TimeReceived + " TimeFulfilled: " + TimeFulfilled + "\nIceCreams: " + str;
        }
    }
}
