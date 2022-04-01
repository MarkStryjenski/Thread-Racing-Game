using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Object locker = new object();

        public Race(double distance,List<Team> attendingTeams)
        {
            this.Distance = distance;
            this.AttendingTeams = attendingTeams;
            this.checker = new SemaphoreSlim(this.AttendingTeams.Count());
            this.listOfThreads = new Thread[this.AttendingTeams.Count()];
            this.weather = new Weather();
            this.RaceProgress = new Dictionary<Team, double> { };
            setUpRaceProgress();
        }

        private void setUpRaceProgress()
        {
            for (int i = 0; i < AttendingTeams.Count; i++)
            {
                RaceProgress.Add(AttendingTeams[i], 0.0);
            }
        }

        public void startRace()
        {

        }

        public void stopRace()
        {

        }

        public void pitStopSemaphore(Car car)
        {
            if (!car.RequiresPitStop)
            {
                Thread thread = new Thread(() => checkPitStopAvailability(car));

                thread.Start();
                thread.Join();
            }
        }

        public void checkPitStopAvailability(Car car)
        {
            car.RequiresPitStop = true;
            Debug.WriteLine($"The team {0} requests to enter the pitstop ", car.Name);
            this.checker.Wait();

            Debug.WriteLine($"Team: {0} has entered the pitstop", car.Name);
            //Thread.Sleep(team.RepairTeam.Repair());
            RepairTeam repairTeam = assembleRepairTeam(car);
            Thread.Sleep(repairTeam.Repair());
            lock (this.locker)
            {
                car.EngineHealth = 100;
                car.WheelHealth = 100;
            }
            
            //Thread.Sleep(10000);
            //car.EngineHealth = 100;
            //car.WheelHealth = 100;
            Debug.WriteLine($"Team: {0} has repaired the car and is leaving a pitstop", car.Name);
            car.RequiresPitStop = false;

            this.checker.Release();
        }

        private RepairTeam assembleRepairTeam(Car car)
        {
            for (int i = 0; i < AttendingTeams.Count; i++)
            {
                if (AttendingTeams[i].Car == car)
                {
                    return AttendingTeams[i].RepairTeam;
                }
            }
            return new RepairTeam(5);
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
