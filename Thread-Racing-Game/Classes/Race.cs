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
    public delegate void MyHandler1(object sender, Car e);
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

        public Race(double distance,List<Team> attendingTeams)
        {
            this.Distance = distance;
            this.AttendingTeams = attendingTeams;
            this.checker = new SemaphoreSlim(this.AttendingTeams.Count());
            this.listOfThreads = new Thread[this.AttendingTeams.Count()];
            this.weather = new Weather();
            setUpPitStop();
        }

        public void setUpPitStop()
        {
            for (int i = 0; i < AttendingTeams.Count; i++)
            {
                MyHandler1 d1 = new MyHandler1(pitStopHandler);
                AttendingTeams[i].Car.Event1 += d1;
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

            for (int i = 0; i < this.listOfThreads.GetLength(0); i++)
            {
                int local = i;
                this.listOfThreads[i] = new Thread(() => checkPitStopAvailability(car));

                this.listOfThreads[i].Start();
                //Thread.Sleep(team.RepairTeam.Repair());
                Thread.Sleep(1000);
            }

            for (int j = 0; j < listOfThreads.GetLength(0); j++)
            {
                this.listOfThreads[j].Join();
            }
        }

        public void checkPitStopAvailability(Car car)
        {
            Debug.WriteLine($"The team {0} requests to enter the pitstop ", car.Name);
            this.checker.Wait();
            Debug.WriteLine($"Team: {0} has entered the pitstop", car.Name);
            //Thread.Sleep(team.RepairTeam.Repair());
            Thread.Sleep(1000);
            Debug.WriteLine($"Team: {0} has repaired the car and is leaving a pitstop", car.Name);
            this.checker.Release();
        }

        private async void checkBuffsForEachTeam()
        {
            for(int i=0;i< this.AttendingTeams.Count(); i++)
            {
                await this.AttendingTeams[i].findBuffMultiplier(this.weather);
            }
        }

        public void testEventHandler()
        {
            for(int i = 0; i < AttendingTeams.Count(); i++)
            {
                Team tmpTeam = AttendingTeams[i];
                Car teamsCar = tmpTeam.Car;
                MyHandler1 d1 = new MyHandler1(pitStopHandler);
                teamsCar.Event1 += d1;
                // get speed per team
                double Speed = tmpTeam.Car.generateCurrentSpeed();

                teamsCar.Event1 += d1;
            }
        }
        public void pitStopHandler(object sender, Car car)
        {
            Debug.WriteLine("Car {0} in pitstop", car.ToString());
            pitStopSemaphore(car);
        }
    }
}
