using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    class Race
    {
        private double distance;
        public double Distance { get { return distance; } set { distance = value; } }

        private Team[] attendingTeams;
        public Team[] AttendingTeams { get { return attendingTeams; } set { attendingTeams = value; } }

        private Dictionary<Team, int> attendingTeamsCount;
        public Dictionary<Team, int> AttendingTeamsCount { get { return attendingTeamsCount; } set { attendingTeamsCount = value; } }

        //TODO: Change to class Weather
        private string weather;
        public string Weather { get { return weather; } set { weather = value; } }

        private string country;
        public string Country { get { return country; } set { country = value; } }

        //private State state;

        private Team winner;
        public Team Winner { get { return winner; } set { winner = value; } }


        public Race(double distance, Team[] attendingTeams, string weather, string country)
        {
            this.distance = distance;
            this.attendingTeams = attendingTeams;
            this.weather = weather;
            this.country = country;
        }

        public async Task PitStopSemaphore(Car car)
        {
            
        }

        public void startRace()
        {

        }

        public void stopRace()
        {

        }
    }
}
