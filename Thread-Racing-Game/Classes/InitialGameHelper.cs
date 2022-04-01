using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    public static class InitialGameHelper
    {
        public static RepairTeam InitialRepairTeam = new RepairTeam(10);
        public static Car InitialCar1 = new Car(100);
        public static Car InitialCar2 = new Car(100);
        public static Car InitialCar3 = new Car(100);
        public static Car InitialCar4 = new Car(100);

        public static Team alfaTeam = new Team("Alfa", InitialRepairTeam, InitialCar1, null);
        public static Team betaTeam = new Team("Beta", InitialRepairTeam, InitialCar2, null);
        public static Team gammaTeam = new Team("Gamma", InitialRepairTeam, InitialCar3, null);
        public static Team omegaTeam = new Team("Omega", InitialRepairTeam, InitialCar4, null);
        public static List<Team> InitialTeamsList = new List<Team>();

        public static void Initiate()
        {
            InitialTeamsList.Add(alfaTeam);
            InitialTeamsList.Add(betaTeam);
            InitialTeamsList.Add(gammaTeam);
            InitialTeamsList.Add(omegaTeam);

            InitialCar1.WheelHealth = 90;
            InitialCar1.EngineHealth = 90;
            InitialCar1.Name = "Alfa";

            InitialCar2.WheelHealth = 100;
            InitialCar2.EngineHealth = 100;
            InitialCar2.Name = "Beta";

            InitialCar3.WheelHealth = 90;
            InitialCar3.EngineHealth = 90;
            InitialCar3.Name = "Gamma";

            InitialCar4.WheelHealth = 80;
            InitialCar4.EngineHealth = 80;
            InitialCar4.Name = "Omega";
        }
    }
}
