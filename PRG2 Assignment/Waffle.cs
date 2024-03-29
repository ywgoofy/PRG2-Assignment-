﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10257300E
// Student Name : Tan Yew Wren
// Partner Name : Lim Ze Yu
//==========================================================
namespace PRG2_Assignment
{
    internal class Waffle : IceCream
    {
        //Attributes
        private string waffleFlavour;
        public string WaffleFlavour { get; set; }

        //Constructors
        public Waffle() { }
        public Waffle(string o, int s, List<Flavour> f, List<Topping> t, string wf) : base(o, s, f, t)
        {
            WaffleFlavour = wf;
        }

        //Methods
        public override double CalculatePrice() //override abstract method
        {
            // Ice Cream flavours for waffles are $3 more
            double cost = 0;
            double singleScoop = 7;
            double doubleScoop = 8.50;
            double tripleScoop = 9.50;

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

            //Add upgrade waffle cost
            if (WaffleFlavour == "Pandan" || WaffleFlavour == "Red Velvet" || WaffleFlavour == "Charcoal")
            {
                cost += 3;
            }
               
            //Add cost of toppings
            cost += (Toppings.Count * 1);
            return cost;
        }

        public override string ToString()
        {
            return base.ToString() + "Waffle Flavour: " + WaffleFlavour;
        }
    }
}
