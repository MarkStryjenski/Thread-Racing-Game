using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    class Bet
    {
        private double amount;
        private Team team;
        private double returnRate;
        private double profitLoss;
        
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
