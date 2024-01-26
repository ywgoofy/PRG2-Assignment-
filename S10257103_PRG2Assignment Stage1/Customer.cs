using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10257103
// Student Name : Lim Ze Yu
// Partner Name : Tan Yew Wren
//==========================================================

namespace PRG2_Assignment
{
    class Customer
    {
        private string name;
        public string Name { get; set; }

        private int memberId;
        public int MemberId { get; set; }

        private DateTime dob;
        public DateTime Dob { get; set; }

        private Order currentOrder;
        public Order CurrentOrder { get; set; }

        private List<Order> orderHistory;
        public List<Order> OrderHistory { get; set;} = new List<Order>();

        private PointCard reward;
        public PointCard Reward { get; set; }

        //Constructors
        public Customer() { }

        public Customer(string n, int m, DateTime d)
        {
            Name = n;
            MemberId = m;
            Dob = d;
            CurrentOrder = null;
            Reward = new PointCard(0,0);
        }


        public Order MakeOrder() 
        {
            return CurrentOrder = new Order(0,DateTime.Now); //Default id is set to 0 such that in the program itself we will change the id to its count.
        }

        public bool IsBirthday()
        {
            if (Dob.ToString("dd/MM") == DateTime.Now.ToString("dd/MM"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            string s = null;
            if (orderHistory.count != 0)
            {
                foreach (Order o in OrderHistory)
                {

                    s += o.ToString() + "\n";
                }
            }
            
            return "Name: " + Name + " MemberID: " + MemberId+ " Dob: " + Dob + " CurrentOrder: " + CurrentOrder + "\nOrder History:" + s + "Rewards:" + Reward.ToString();
        }
    }
}
