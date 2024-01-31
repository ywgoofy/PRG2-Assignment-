using System;
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
    class Topping
    {
        //Attributes
        private string type;
        public string Type { get; set; }

        //Constructors
        public Topping() { }
        public Topping(string type) 
        { 
            Type = type;
        }

        //Methods
        public override string ToString()
        {
            return "Type: " + Type;
        }
    }
}
