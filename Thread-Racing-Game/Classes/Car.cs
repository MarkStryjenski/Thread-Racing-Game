using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thread_Racing_Game.Core.Models;

namespace Thread_Racing_Game.Classes
{
    public class Car
    {
        private double wheelHealth;
        public double WheelHealth
        {
            get { return wheelHealth; }
            set { wheelHealth = value; }
        }
        private double topSpeed;
        public double TopSpeed
        {
            get { return topSpeed; }
            set { topSpeed = value; }
        }
        private double engineHealth;
        public double EngineHealth
        {
            get { return engineHealth; }
            set { engineHealth = value; }
        }
        private bool requiresPitStop;
        public bool RequiresPitStop
        {
            get { return requiresPitStop; }
            set { requiresPitStop = value; }
        }

        public event EventHandler ProcessCompleted;

        public Car(double topSpeed)
        {
            TopSpeed = topSpeed;
            WheelHealth = 0;
            EngineHealth = 0;
            RequiresPitStop = false;

        }
        public void StartProcess()
        {
            Console.WriteLine("Process Started!");
            // some code here..
            OnProcessCompleted(EventArgs.Empty);
        }
        public virtual void OnProcessCompleted(EventArgs e)
        {
            ProcessCompleted?.Invoke(this,e);
        }

        public double generateCurrentSpeed(double multiplier)
        {
            TopSpeed *= multiplier;
            return TopSpeed;
        }

        public async Task<double> findBuffMultiplier(string countryName, string weatherDescription)
        {
            double multiplier = 0.0;
            List<Country> countries = await Helpers.Utility.GetCountries();

            foreach (Country c in countries)
            {
                if (c.Name == countryName && c.Buff.Description == weatherDescription)
                {
                    multiplier = c.Buff.Multiplier;
                    break;
                }
            }

            return multiplier;
        }

        public bool isCarBroken()
        {
            return WheelHealth < 55 || EngineHealth < 55;
        }
    }
}
