using System;
using Server.Gumps;
using Server.Network;

namespace Server.Misc
{
    public class DecayFomeSede : Timer
    {
        public DecayFomeSede()
            : base(TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10))
        {
            this.Priority = TimerPriority.OneMinute;
        }

        public static void Initialize()
        {
            new DecayFomeSede().Start();
            EventSink.HungerChanged += Hunger;
        }

        public static void Hunger(HungerChangedEventArgs e)
        {
            if(e.Mobile.RP)
            {
                if(e.Hunger && e.OldValue < e.Mobile.Hunger)
                    e.Mobile.SendGump(new FomeSede(e.Mobile));
                if(!e.Hunger && e.OldValue < e.Mobile.Thirst)
                    e.Mobile.SendGump(new FomeSede(e.Mobile));
            }
        }

        public static void FoodDecay()
        {
            var decayFome = Utility.RandomBool();
            foreach (NetState state in NetState.Instances)
            {
                if ((decayFome && HungerDecay(state.Mobile)) || ThirstDecay(state.Mobile))
                {
                    if (state.Mobile.RP)
                        state.Mobile.SendGump(new FomeSede(state.Mobile));
                }


            }
        }

        public static bool HungerDecay(Mobile m)
        {
            if (m != null && m.Hunger >= 1)
            {
                m.Hunger -= 1;
                if (m.RP && m.Hunger <= 5)
                {
                    if (m.Hunger == 0)
                    {
                        m.Kill();
                        m.SendMessage(38, "Voce morreu de fome...");
                    }
                    else
                        m.SendMessage(38, "Voce esta com muita fome... coma algo para nao desmaiar !");
                }
                return true;
            }
            return false;
        }

        public static bool ThirstDecay(Mobile m)
        {
            if (m != null && m.Thirst >= 1)
            {
                m.Thirst -= 1;
                if (m.RP && m.Thirst <= 5)
                {
                    if (m.Thirst == 0)
                    {
                        m.Kill();
                        m.SendMessage(38, "Voce morreu desidratado...");
                    }
                    else
                        m.SendMessage(38, "Voce esta com muita sede... beba algo para se hidratar !");
                }
                return true;
            }
            return false;

        }

        protected override void OnTick()
        {
            FoodDecay();
        }
    }
}
