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
        public static Car InitialCar = new Car(100);
        public static Team alfaTeam = new Team("Alfa", InitialRepairTeam, InitialCar, null);
        public static Team betaTeam = new Team("Beta", InitialRepairTeam, InitialCar, null);
        public static Team gammaTeam = new Team("Gamma", InitialRepairTeam, InitialCar, null);
        public static Team omegaTeam = new Team("Omega", InitialRepairTeam, InitialCar, null);
        public static List<Team> InitialTeamsList = new List<Team>();

        public static void Initiate()
        {
            InitialTeamsList.Add(alfaTeam);
            InitialTeamsList.Add(betaTeam);
            InitialTeamsList.Add(gammaTeam);
            InitialTeamsList.Add(omegaTeam);
        }
    }
}
