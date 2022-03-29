using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    class RepairTeam
    {
        private int repairSkill;
        public int RepairSkill
        {
            get { return repairSkill; }
            set { repairSkill = value; }
        }

        public RepairTeam(int repairSkill)
        {
            this.repairSkill = repairSkill;
        }

        private async Task Repair()
        {
            await Task.Run(() => Console.WriteLine("To be implemented"));
        }

        private void RepairWheel()
        {

        }

        private void RepairHealth()
        {

        }
    }
}
