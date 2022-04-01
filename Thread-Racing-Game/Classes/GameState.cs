using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thread_Racing_Game.Views;

namespace Thread_Racing_Game.Classes
{
    public class GameState
    {
        public Race race { get; set; }
        public Casino casino{ get; set; }
        public User user { get; set; }
        public Bet bet { get; set; }

        public Team team { get; set; }

        public GameState(Race race, Casino casino)
        {
            this.race = race;
            this.casino = casino;
            this.user = new User(100);
            //this.team = team;
        }

        public void createRace() {
           
        }

        public void evaluateBet()
        {
            //Task t1 = new Task();
        }

    }
}
