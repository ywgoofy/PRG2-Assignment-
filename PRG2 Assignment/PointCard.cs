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
            if(Points < 50)
            {
                Tier = "Ordinary";
            }
            else if (Points <100)
            {
                Tier = "Sliver";
            }
            else
            {
                Tier = "Gold";
            }
        }

        //Methods
        public void AddPoints(int i)
        {
            Points += i;
            if (Tier == "Oridinary")
            {
                if (Points >= 50 && Points < 100)
                {
                    Tier = "Sliver";
                }
                else
                {
                    Tier = "Gold";
                }
            }
            else if (Tier == "Sliver")
            {
                if (Points > 100)
                {
                    Tier = "Gold";
                }
            }
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
            PunchCard++;
            if(PunchCard==11)
            {
                PunchCard = 0;
            }
        }

        public override string ToString()
        {
            return "Points: " + Points + " PunchCard: " + PunchCard + " Tier: " + Tier;

        }
    }
}
