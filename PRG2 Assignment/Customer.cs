using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public Order MakeOrder() //im gg to ask qn about this i not sure if its memberid or id from order
        {
            return CurrentOrder = new Order(OrderHistory.Count+1,DateTime.Now);
        }

        public bool IsBirthday()
        {
            if (Dob == DateTime.Now)
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
            foreach(Order o in OrderHistory)
            {
                s += o.ToString() + "\n";
            }
            return "Name " + Name + " MemberID: " + MemberId+ " Dob: " + Dob + "CurrentOrder: " + CurrentOrder + "\nOrder History:" + s + "Rewards:" + Reward.ToString();
        }
    }
}
