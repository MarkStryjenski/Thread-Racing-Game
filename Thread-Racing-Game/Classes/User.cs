using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    public class User
    {
        public double money { get; set; }
        public Bet currentBet { get; set; }
        public List<Bet> pastBets { get; set; }

        public User(double money)
        {
            this.money = money;
        }

        public void placeBet(Bet bet)
        {
            this.currentBet = bet;
        }
    }
}
