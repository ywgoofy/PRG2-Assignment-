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
        IceCream(string o, int s, List<Flavour> f, List<Topping> l)
        {
            Option = o;
            Scoops = s;
            Flavours = f;
            Toppings = l;
        }
        
        //Methods
        double CalculatePrice()
        {
                
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

            return "Options: " + Option + " Scoops: " + Scoops + " Flavours: " + flavour_string + "Toppings: " + ;
        }



    }
}
