using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    class GameState
    {
        private Race race;
        private Casino casino;
        private User user;

        public GameState(Race race, Casino casino, User user)
        {
            this.race = race;
            this.casino = casino;
            this.user = user;
        }

        public void createRace() {
        }
    }
}
