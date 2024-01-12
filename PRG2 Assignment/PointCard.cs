using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class PointCard
    {
        private int points;
        public int Points { get; set; }

        private int punchCard;
        public int PunchCard { get; set; }

        private string tier;
        public string Tier { get; set; }

        //Constructors
        public PointCard() { }
        public PointCard(int pt, int pc)
        {
            Points = pt;
            PunchCard = pc;
        }

        //Methods
        public void AddPoints(int i)
        {
            Points += i;
        }

        public void RedeemPoints(int i)
        {
            if(Tier != "Ordinary")
            {   
                Points -= i;
            }
        }
        public void Punch()
        {
            punchCard++;
        }

        public override string ToString()
        {
            return "Points: " + Points + " PunchCard: " + PunchCard + " Tier: " + Tier;

        }
    }
}
