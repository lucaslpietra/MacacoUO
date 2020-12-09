using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.RP.ClassesRP
{
    public class ControleClasses
    {
        public static void EscolheClasse(PlayerMobile pl, ClasseRP classe)
        {
            foreach(var skill in pl.Skills)
            {
                skill.Cap = 30;
            }

            var caps = classe.GetCaps();
            caps.S100.ForEach(s => pl.Skills[s].Cap = 100);
            caps.S90.ForEach(s => pl.Skills[s].Cap = 90);
            caps.S80.ForEach(s => pl.Skills[s].Cap = 80);
            caps.S70.ForEach(s => pl.Skills[s].Cap = 70);
            caps.S60.ForEach(s => pl.Skills[s].Cap = 60);
            caps.S50.ForEach(s => pl.Skills[s].Cap = 50);
            caps.S40.ForEach(s => pl.Skills[s].Cap = 40);
        }
    }
}
