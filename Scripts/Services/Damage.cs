using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Services
{
    public class DamageNumbers
    {
        public static void ShowDamage(int damage, Mobile from, Mobile to, int hue)
        {
            var dmg = "-" + damage;
            var color = 38;
            if(damage <= 0)
            {
                color = 0;
                dmg = "+" + (-damage);
            }
            to.NewFontPublicMessage(Network.MessageType.Regular, hue, false, dmg);
            /*
            PlayerMobile attacker = null;
            if (from is PlayerMobile)
                attacker = from;
            else if(from is BaseCreature)
            {
                var creature = (BaseCreature)from;
                if(creature.Controlled)
                {
                    Mobile b = creature.Owners
                }
            }
            */
        }
    }


}
