using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Flavour
    {
        private string type;
        public string Type { get; set; }

        private bool premium;
        public bool Premium { get; set; }
        private int quantity;
        public int Quantity { get; set; }

        //contructors
        public Flavour() { }
        public Flavour(string type, bool premium, int quantity) 
        { 
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }

        //Methods
        public override string ToString()
        {
            return "Type: " + Type + "/n" +
                "Premium: " + Premium + "/n" +
                "Quantity: " + Quantity;
        }
    }
}
