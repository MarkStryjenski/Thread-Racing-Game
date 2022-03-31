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

        public async Task<double> findBuffMultiplier(Weather weatherDescription)
        {
            double multiplier = 0.0;
            List<Country> countries = await Helpers.Utility.GetCountries();

            foreach (Country c in countries)
            {
                if (c.Name == Country.Name && c.Buff.Description == weatherDescription.Condition)
                {
                    multiplier = c.Buff.Multiplier;
                    this.Car.multiplier = c.Buff.Multiplier;
                    break;
                }
            }

            return multiplier;
        }


        // TODO: Get team country flag
    }
}
