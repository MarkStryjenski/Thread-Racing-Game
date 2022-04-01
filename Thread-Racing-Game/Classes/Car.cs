using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Thread_Racing_Game.Core.Models;

namespace Thread_Racing_Game.Classes
{
    public class Car
    {
        public Vector2 Position { get; set; }

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

        public double multiplier { get; set; }

        public event EventHandler ProcessCompleted;
        public event MyHandler1 Event1;

        public string Name { get; set; }

        ///<summary>
        ///Top speed needs to be even number
        ///</summary>
        public Car(double topSpeed)
        {
            if (topSpeed % 2 != 0)
            {
                topSpeed = 50;
            }
            else
            {
                TopSpeed = topSpeed;
            }
            this.WheelHealth = 100;
            this.EngineHealth = 100;
            this.RequiresPitStop = false;
            this.multiplier = 0;
            this.Name = "";
        }

        public virtual void OnProcessCompleted(EventArgs e)
        {
            ProcessCompleted?.Invoke(this,e);
        }

        public double generateCurrentSpeed()
        {
            double speed = 0;
            if (isCarBroken())
            {
                Debug.WriteLine("Process Started!");
                Event1(this, this);
            }
            if (this.multiplier != 0)
            {
                speed = (this.topSpeed + (this.multiplier * 5)) / 2;
            }
            else
            {
                speed = (this.topSpeed) / 2;
            }
            this.wheelHealth = this.wheelHealth - 10;
            this.engineHealth = this.engineHealth - 10;
            return speed;
        }

        //public async Task<double> findBuffMultiplier(string countryName, string weatherDescription)
        //{
        //    double multiplier = 0.0;
        //    List<Country> countries = await Helpers.Utility.GetCountries();

        //    foreach (Country c in countries)
        //    {
        //        if (c.Name == countryName && c.Buff.Description == weatherDescription)
        //        {
        //            multiplier = c.Buff.Multiplier;
        //            break;
        //        }
        //    }

        //    return multiplier;
        //}


        public bool isCarBroken()
        {
            if(WheelHealth < 55 || EngineHealth < 55)
            {
                return true;
            }
            return false;
        }
    }
}
