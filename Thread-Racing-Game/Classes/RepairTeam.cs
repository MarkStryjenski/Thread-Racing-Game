using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    public class RepairTeam
    {
        private int repairSkill;
        public int RepairSkill
        {
            get { return repairSkill; }
            set { repairSkill = value; }
        }

        public RepairTeam(int repairSkill)
        {
            if(repairSkill>10 || repairSkill < 1)
            {
                this.repairSkill = 5;
            }
            else
            {
                this.repairSkill = repairSkill;
            }
        }

        //public async Task Repair()
        //{
        //    await Task.Run(() => Console.WriteLine("To be implemented"));
        //}
        public int Repair()
        {
            return this.repairSkill;
        }

        //public void RepairWheel()
        //{

        //}

        //public void RepairHealth()
        //{

        //}

    }
}
