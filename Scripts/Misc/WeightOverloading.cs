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
            var stam = damage / 7;
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

            Shard.Debug("Pct Peso: " + pctPeso+" MAX = "+from.MaxWeight, from);

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

                /*
                 *   int passos = 2;

                if (pctPeso < 50 && pctPeso > 35)
                    passos = (int)(passos / 1.3);
                else if(pctPeso < 75)
                    passos = (int)(passos / 15);
                else if(pctPeso < 90)
                    passos = (int)(passos / 20);
                else 
                    passos = 1;

                if ((++pm.StepsTaken % passos) == 0)
                */
                --from.Stam;
            }
        }

        /*
        public static int GetStamLoss(Mobile from, int percent, bool running)
        {
            if (percent < 25)
            {
                return 0;
            }
            else if (percent < 50)
            {
                if (new Random().Next(100) < 2)
                {

                    return 1;
                }
            }
            else if (percent < 75)
            {
                if (new Random().Next(100) < 10)
                {

                    return 1;
                }
            }
            else if (percent < 90)
            {
                if (new Random().Next(100) < 50)
                {

                    return 1;
                }
            }
            else if (percent < 100)
            {
                return 1;
            }
            else
            {
                // over 8

                return 2;
            }

            return 0;
        }
        */

        public static bool IsOverloaded(Mobile m)
        {
            if (!m.Player || !m.Alive || m.IsStaff())
                return false;

            return ((Mobile.BodyWeight + m.TotalWeight) > (m.MaxWeight + OverloadAllowance));
        }
    }
}
