using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    class User
    {
        private double money;
        private Bet currenBet;
        private List<Bet> pastBets;

        public User(double money)
        {
            this.money = money;
        }

        public void placeBet(Bet bet)
        {
            this.currenBet = bet;
        }
    }
}
