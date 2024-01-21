using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Cup : IceCream
    {
        //Constructors
        public Cup() { }
        public Cup(string o, int s, List<Flavour> f, List<Topping> t) : base(o, s, f, t) { }

        //Methods
        public override double CalculatePrice() //override abstract method
        {
            double cost = 0;
            double singleScoop = 4;
            double doubleScoop = 5.50;
            double tripleScoop = 6.50;

            if (Scoops == 1)
            {
                cost += singleScoop;
            }
            else if (Scoops == 2)
            {
                cost += doubleScoop;
            }
            else if (Scoops == 3)
            {
                cost += tripleScoop;
            }

            //Check for any premium flavours
            double premiumScoop = 2;
            foreach (Flavour f in Flavours)
            {
                if (f.Premium)
                {
                    cost += premiumScoop * f.Quantity; 
                }
            }

            //Add toppings
            cost += (Toppings.Count * 1);
            return cost;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nCost: ${CalculatePrice()}";
        }
    }
}
