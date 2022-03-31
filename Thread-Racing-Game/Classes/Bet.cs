using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    public class Bet
    {
        public double amount { get; set; }
        public Team team { get; set; }
        public double returnRate { get; set; }
        public double profitLoss { get; set; }

        public Bet(double amount, Team team)
        {
            this.amount = amount;
            this.team = team;
        }

        public double getProfitLoss()
        {
            //this.team.
            return 0;
        }
    }
}
