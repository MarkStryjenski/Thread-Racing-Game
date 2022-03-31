using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thread_Racing_Game.Core.Models;

namespace Thread_Racing_Game.Classes
{
    public class Team
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public RepairTeam RepairTeam { get; set; }
        public Car Car { get; set; }
        public Country Country { get; set; }

        public Team(string name, RepairTeam repairTeam, Car car, Country country)
        {
            Name = name;
            RepairTeam = repairTeam;
            Car = car;
            Country = country;
        }

        public Team GetTeamOverview() {
            return new Team(Name, RepairTeam, Car, Country);
        }



        // TODO: Get team country flag
    }
}
