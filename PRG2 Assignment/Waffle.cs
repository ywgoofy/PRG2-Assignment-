using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Waffle : IceCream
    {
        //attributes
        private string waffleFlavour;
        public string WaffleFlavour { get; set; }

        Waffle() { }
        Waffle(string o, int s, List<Flavour> f, List<Topping> t, string wf) : base(o, s, f, t)
        {
            WaffleFlavour = wf;
        }
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
            double premiumScoops = 2;
            foreach (Flavour f in Flavours)
            {
                if (f.Premium)
                {
                    cost += f.Quantity * premiumScoops;
                }
            }

            //Add waffle cost
            cost += 3;

            //Add cost of toppings
            cost += (Toppings.Count * 1);
            return cost;
        }
    }
}
