using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Cone : IceCream
    {
        //Attributes
        private bool dipped;
        public bool Dipped
        {
            get { return dipped; }
            set { dipped = value; }
        }

        //Constructors
        public Cone() : base() { }
        public Cone(string o, int s, List<Flavour> f, List<Topping> t, bool d) : base(o, s, f, t)
        {
            Dipped = d;
        }

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
            double premiumScoops = 2;
            foreach (Flavour f in Flavours)
            {
                if (f.Premium)
                {
                    cost += f.Quantity * premiumScoops;
                }
            }

            //Check whether cone is dipped
            double dippedcost = 2;
            if (Dipped)
            {
                cost += dippedcost;
            }

            //Add cost of toppings
            cost += (Toppings.Count * 1);
            return cost;
        }

        public override string ToString()
        {
            return base.ToString() + $"Chocolate-Dipped Cone: {Dipped}\nCost: {CalculatePrice()}";
        }
    }
}
