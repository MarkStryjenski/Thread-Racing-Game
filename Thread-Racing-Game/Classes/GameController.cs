using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    public delegate void MyHandler1 (object sender, Car e);
    public class GameController
    {
        public Thread[] gameThreads;
        public GameState gameState;

        //ProcessBusinessLogic bl = new ProcessBusinessLogic();
        //bl.ProcessCompleted += bl_ProcessCompleted;
        public GameController(int numThreads)
        {
            this.gameThreads = new Thread[numThreads];
            InitialGameHelper.Initiate();
            RepairTeam repairTeam = new RepairTeam(10);
            Car car1 = new Car(100);
            car1.Name = "ALFA";
            car1.WheelHealth = 90;
            car1.EngineHealth = 90;

            Car car2 = new Car(100);
            car2.Name = "BETA";
            car2.WheelHealth = 30;
            car2.EngineHealth = 30;

            Car car3 = new Car(100);
            car3.Name = "OMEGA";
            car3.WheelHealth = 40;
            car3.EngineHealth = 50;

            Team alfaTeam = new Team("Alfa", repairTeam, car1, null);
            Team betaTeam = new Team("Beta", repairTeam, car2, null);
            Team gammaTeam = new Team("Gamma", repairTeam, car1, null);
            Team omegaTeam = new Team("Omega", repairTeam, car3, null);
            List<Team> teamsList = new List<Team>();
            teamsList.Add(alfaTeam);
            teamsList.Add(betaTeam);
            teamsList.Add(gammaTeam);
            teamsList.Add(omegaTeam);
            Weather weather = new Weather();
            Race race = new Race(150, teamsList);
            this.gameState = new GameState(race, null);
            //this.gameState = new GameState(new Race(1000, InitialGameHelper.InitialTeamsList), null);
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
                        // get speed per team

                        Car teamsCar = tmpTeam.Car;
                        MyHandler1 d1 = new MyHandler1(pitStopHandler);
                        teamsCar.Event1 += d1;
                        double Speed = tmpTeam.Car.generateCurrentSpeed();

                        // update dictionary in gameState
                        //gameState.race.RaceProgress[tmpTeam] = gameState.race.RaceProgress[tmpTeam] + Speed;
                        Debug.WriteLine("I am speed Loop nr: {0} => {1}", tmpTeam.Name, Speed);


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
        public void pitStopHandler(object sender, Car car)
        {
            Debug.WriteLine($"Car {0} in pitstop", car.Name);
            gameState.race.pitStopSemaphore(car);
        }

        public void ExecuteGameThreads()
        {
            Debug.WriteLine("here");
            for (int i = 0; i < gameThreads.Length; i++)
            {
                gameThreads[i].Start();
            }
            for (int i = 0; i < gameThreads.Length; i++)
            {
                gameThreads[i].Join();
            }
            Debug.WriteLine("done");
        }

        //public Car GetCarFromTeam(Team team)
        //{
        //    return team.GetTeamCar();
        //}

        //change requirePitStop to false by default and change it to true if the car breaks
    }
}
