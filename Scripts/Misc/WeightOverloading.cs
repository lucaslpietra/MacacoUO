using System;
using System.Collections.Generic;

using Server.Mobiles;
using Server.Items;

namespace Server.Misc
{
    public class WeightOverloading
    {

        public const int OverloadAllowance = 4;// We can be four stones overweight without getting fatigued

        public static void Initialize()
        {
            EventSink.Movement += new MovementEventHandler(EventSink_Movement);
            Mobile.FatigueHandler = FatigueOnDamage;
        }

        public static void FatigueOnDamage(Mobile m, int damage, DFAlgorithm df)
        {
            var stam = (damage / 5) * (m.Dex / 100);
            m.Stam -= stam;
            return;
        }

        public static void EventSink_Movement(MovementEventArgs e)
        {
            Mobile from = e.Mobile;
            if (!from.Player)
                return;

            if (!from.Alive || from.IsStaff())
                return;

            int maxWeight = from.MaxWeight + OverloadAllowance;// 4
            int myWeight = (Mobile.BodyWeight + from.TotalWeight);
            int pctPeso = myWeight * 100 / maxWeight;

            if (from.Stam == 0)
            {
                from.SendMessage("Voce esta muito fadigado para se mover"); // Your mount is too fatigued to move. : You are too fatigued to move.
                e.Blocked = true;
                return;
            }

            var pm = from as PlayerMobile;

            if (pm != null)
            {
                if (pctPeso <= 99)
                    return;
                --from.Stam;
            }
        }
        public static bool IsOverloaded(Mobile m)
        {
            if (!m.Player || !m.Alive || m.IsStaff())
                return false;

            return ((Mobile.BodyWeight + m.TotalWeight) > (m.MaxWeight + OverloadAllowance));
        }
    }
}
