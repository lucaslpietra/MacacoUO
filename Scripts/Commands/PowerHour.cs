#region References
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Server.Commands.Generic;
using Server.Engines.BulkOrders;
using Server.Items;
using Server.Misc;
using Server.Network;
#endregion

namespace Server.Commands
{
    public class PowerCmd
    {
        public static void Initialize()
        {
            CommandSystem.Register("powerhour", AccessLevel.Administrator, OnAction);
        }

        [Usage("Action")]
        private static void OnAction(CommandEventArgs e)
        {
            var horas = 1;
            if(e.Arguments.Count() > 0)
                horas = e.GetInt32(0);

            if(SkillCheck.BONUS_GERAL > 0)
            {
                e.Mobile.SendMessage("Ja tem um bonus ativo");
                return;
            }

            SkillCheck.BONUS_GERAL = 1.5;

            var str = horas + "hora";
            if (horas > 1)
                str += "s";

            Anuncio.Anuncia("POWEHOUR !! Bonus de UP por " + str);

            Timer.DelayCall(TimeSpan.FromHours(horas), () => {
                SkillCheck.BONUS_GERAL = 0;
                Anuncio.Anuncia("O PowerHour de XP Terminou !");
            });

            
        }
    }
}
