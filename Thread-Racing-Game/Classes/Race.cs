using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thread_Racing_Game.Enums;

namespace Thread_Racing_Game.Classes
{
    public class Race
    {
        public double Distance { get; set; }
        public List<Team> AttendingTeams { get; set; }
        public Dictionary<Team,double> RaceProgress { get; set; }
        //public Weather weatherCondition { get; set; }
        //public Country country { get; set; }
        //public State state { get; set; }
        public Team Winner { get; set; }
        public EventHandler PitStopEventTrigger { get; set; }
        public SemaphoreSlim checker { get; set; }
        public Thread[] listOfThreads { get; set; }
        public Team team { get; set; }
        public Weather weather { get; set; }

        public Race(double distance,List<Team> attendingTeams,Weather weather)
        {
            this.Distance = distance;
            this.AttendingTeams = attendingTeams;
            this.checker = new SemaphoreSlim(this.AttendingTeams.Count());
            this.listOfThreads = new Thread[this.AttendingTeams.Count()];
            this.weather = weather;
            //RepairTeam repairTeam = new RepairTeam(10);
            //Car car = new Car(100);
            //this.team = new Team("Alfa", repairTeam, car, null);
        }

        public void startRace()
        {

        }

        public void stopRace()
        {

        }

        public void pitStopSemaphore(Team team)
        {
            //team.Car.ProcessCompleted += car_ProcessCompleted;
            //team.Car.generateCurrentSpeed();

            for (int i = 0; i < this.listOfThreads.GetLength(0); i++)
            {
                int local = i;
                this.listOfThreads[i] = new Thread(() => checkPitStopAvailability(team));

                this.listOfThreads[i].Start();
                //Thread.Sleep(team.RepairTeam.Repair());
                Thread.Sleep(1000);
            }

            for (int j = 0; j < listOfThreads.GetLength(0); j++)
            {
                this.listOfThreads[j].Join();
            }
        }

        public void checkPitStopAvailability(Team team)
        {
            Console.WriteLine("The team {0} requests to enter the pitstop", team.Name);
            this.checker.Wait();
            Console.WriteLine("Team: {0} has entered the pitstop", team.Name);
            Thread.Sleep(team.RepairTeam.Repair());
            Console.WriteLine("Team: {0} has repaired the car and is leaving a pitstop",team.Name);
            this.checker.Release();
        }

        public static void car_ProcessCompleted(object sender,EventArgs e)
        {
            Console.WriteLine("Process completed");
        }

        private async void checkBuffsForEachTeam()
        {
            for(int i=0;i< this.AttendingTeams.Count(); i++)
            {
                await this.AttendingTeams[i].findBuffMultiplier(this.weather);
            }
        }

    }
}
