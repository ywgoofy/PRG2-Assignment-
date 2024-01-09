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
        }

        //Methods
        public void ModifyIceCream(int i) //Nah this one i discuss with that pookie, shit extra confusing
        {
            /*for (int j = 0; j < iceCreamList.Count; j++)
            {
                if (j == (i - 1))
                {
                    IceCreamsList[j].Option
                }
            }*/
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
