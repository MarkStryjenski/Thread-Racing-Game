using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thread_Racing_Game.Enums;

namespace Thread_Racing_Game.Classes
{
    class Race
    {
        private double distance;
        private Team[] attendingTeams;
        private Dictionary<Team, double> raceProgress;
        private Weather weatherCondition;
        private String counry;
        private State state;
        private Team winner;
        public EventHandler pitStopTrigger;
    }
}
