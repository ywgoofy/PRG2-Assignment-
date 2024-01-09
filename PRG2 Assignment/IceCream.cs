using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class IceCream
    {
        //Attributes
        private string option;
        public string Option { get; set; }

        private int scoops;
        public int Scoops { get; set; }

        private List<Flavour> flavours;
        public List<Flavour> Flavours { get;set; } = new List<Flavour>();

        private List<Topping> toppings;
        public List<Topping> Toppings { get; set; } = new List<Topping>();

        //Constructors
        IceCream() { }
        IceCream(string o, int s, List<Flavour> f, List<Topping> t)
        {
            Option = o;
            Scoops = s;
            Flavours = f;
            Toppings = t;
        }
        
        //Methods
        public double CalculatePrice()
        {
            double price = 0;
            if (Option == "Cup")
            {
                if (Scoops == 1)
                {
                    price += 4;
                }
                else if (Scoops == 2)
                {
                    price += 5.5;
                }
                else if (Scoops == 3)
                {
                    price += 6.5;
                }
                if (Toppings.Count > 0)
                {
                    price += Toppings.Count * 1;
                }
            }
            else if (Option == "Cone")
            {
                if (Scoops == 1)
                {
                    price += 4;
                }
                else if (Scoops == 2)
                {
                    price += 5.5;
                }
                else if (Scoops == 3)
                {
                    price += 6.5;
                }
                if (Toppings.Count > 0)
                {
                    /*foreach(Topping t in Toppings)
                    {
                        if(t == "Chocolate-dipped cone")
                        {

                        }
                    }*/
                }
            }
            else if (Option == "Waffle")
            {
                if (Scoops == 1)
                {

                }
                else if (Scoops == 2)
                {

                }
                else if (Scoops == 3)
                {

                }
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
            return price;
        }

        public override string ToString()
        {
            string flavour_string = null; //Default to null else doesn't work
            foreach (Flavour f in Flavours)
            {
                flavour_string = f + " "; 
            }

            string topping_string = null; //Default to null else doesn't work
            foreach(Topping t in Toppings)
            {
                topping_string = t + " ";
            }

            return "Options: " + Option + " Scoops: " + Scoops + " Flavours: " + flavour_string + "Toppings: " + topping_string;
        }



    }
}
