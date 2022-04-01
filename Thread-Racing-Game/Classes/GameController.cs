using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    public delegate void MyHandler1(object sender, Car e);
    public class GameController
    {
        public Thread[] gameThreads;
        public GameState gameState;
        private Object locker = new object();

        //ProcessBusinessLogic bl = new ProcessBusinessLogic();
        //bl.ProcessCompleted += bl_ProcessCompleted;
        public GameController(int numThreads)
        {
            this.gameThreads = new Thread[numThreads];
            InitialGameHelper.Initiate();
            this.gameState = new GameState(new Race(1000, InitialGameHelper.InitialTeamsList), null);
            setUpPitStop();
        }

        public void ExecuteGameCycle()
        {
            // game setup
            SetUpThreads();
            // game cycle consists of the following steps
            // loop through all teams in race and get cars
            // generate speed of car depending on country and weather condition
            // calculate distance progress completed
            // update car distance in race progress
            ExecuteGameThreads();
            // check if raceOverState should execute
     
        }

        public void RaceOverState()
        {
            // check gambling bets

            // give out rewards if applied

            // wait for input and repeat or close game
        }

        // this method should be executed by the game threads

        // game will run on even number threads and even number cars always AND # of theads is always EQUAL OR LESS threads than teams.
        public void SetUpThreads()
        {
            int numberOfThreads = gameThreads.Length;

            int numCarsHandledPerThread = gameState.race.AttendingTeams.Count / numberOfThreads; // 2

            int threadListIndex = 0;

            int teamIndex = -1;

            // teams = 4, threads = 4
            for (int i = 0; i < gameState.race.AttendingTeams.Count; i += numCarsHandledPerThread)
            {
                gameThreads[threadListIndex] = new Thread(() => {
                    for (int j = 0; j < numCarsHandledPerThread; j++)
                    {
                        teamIndex++;
                        Team tmpTeam = gameState.race.AttendingTeams[teamIndex];
                        lock (locker)
                        {
                            // get speed per team
                            if (!tmpTeam.Car.RequiresPitStop)
                            {
                                
                                double Speed = tmpTeam.Car.generateCurrentSpeed();
                                // update dictionary in gameState
                                //gameState.race.RaceProgress[tmpTeam] = gameState.race.RaceProgress[tmpTeam] + Speed;
                                gameState.race.RaceProgress[tmpTeam] = gameState.race.RaceProgress[tmpTeam] + Speed;
                                Debug.WriteLine("I am speed Loop nr: {0} => {1}", tmpTeam.Name, Speed);
                                Debug.WriteLine("total distance traveled: {0} => {1}", tmpTeam.Name, gameState.race.RaceProgress[tmpTeam]);
                           
                            }
                        }

                        // check if crossed finishline event if yes assign winner.
                        //if (gameState.race.RaceProgress[tmpTeam] >= gameState.race.Distance)
                        //{
                        //    // add too winner list!
                        //}
                    }
                });
                threadListIndex++;
            }
        }

        public void ExecuteGameThreads()
        {
            for (int i = 0; i < gameThreads.Length; i++)
            {
                gameThreads[i].Start();
            }
            for (int i = 0; i < gameThreads.Length; i++)
            {
                gameThreads[i].Join();
            }
        }


        public void testEventHandler()
        {
            for (int i = 0; i < gameState.race.AttendingTeams.Count(); i++)
            {
                Team tmpTeam = gameState.race.AttendingTeams[i];
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
            Thread testThread = new Thread(() =>
            {
                //Debug.WriteLine($"Pitstop called by {0}", car.Name);
                Debug.WriteLine("Car {0} in pitstop", car.ToString());
                gameState.race.pitStopSemaphore(car);
            });
            testThread.Start();
        }

        public void setUpPitStop()
        {
            for (int i = 0; i < gameState.race.AttendingTeams.Count; i++)
            {
                MyHandler1 d1 = new MyHandler1(pitStopHandler);
                gameState.race.AttendingTeams[i].Car.Event1 += d1;
            }
        }
        //public Car GetCarFromTeam(Team team)
        //{
        //    return team.GetTeamCar();
        //}

        //change requirePitStop to false by default and change it to true if the car breaks
    }
}
