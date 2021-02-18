using System;
using Server.Gumps;
using Server.Network;

namespace Server.Misc
{
    public class FomeSedeDecay : Timer
    {
        public FomeSedeDecay() : base(TimeSpan.FromMinutes(22), TimeSpan.FromMinutes(22))
        {
            this.Priority = TimerPriority.OneMinute;
        }

        public static void Initialize()
        {
            new FomeSedeDecay().Start();
            EventSink.HungerChanged += Hunger;
        }

        public static void Hunger(HungerChangedEventArgs e)
        {
            if (e.Hunger && e.OldValue < e.Mobile.Hunger)
                e.Mobile.SendGump(new FomeSede(e.Mobile));
            if (!e.Hunger && e.OldValue < e.Mobile.Thirst)
                e.Mobile.SendGump(new FomeSede(e.Mobile));
        }

        public static void FoodDecay()
        {
            foreach (NetState state in NetState.Instances)
            {
                if (state.Mobile != null && state.Mobile.IsPlayer())
                {
                    HungerDecay(state.Mobile);
                    ThirstDecay(state.Mobile);
                    if (state.Mobile.RP)
                        state.Mobile.SendGump(new FomeSede(state.Mobile));
                }
            }
        }

        public static void HungerDecay(Mobile m)
        {
            if (m.Hunger >= 1)
            {
                m.Hunger -= 1;
                if (m.Hunger <= 14 && m.Hunger > 10)
                {
                    m.SendMessage(38, "Voce esta ficando com fome.");
                }
                else if (m.Hunger <= 10 && m.Hunger > 6)
                {
                    m.SendMessage(38, "Voce esta faminto.");
                }
            }
        }

        public static void ThirstDecay(Mobile m)
        {
            if (m.Thirst >= 1)
            {
                m.Thirst -= 2;
                if (m.Thirst <= 14 && m.Thirst > 10)
                {
                    m.SendMessage(38, "Voce esta ficando com sede.");
                }
                if (m.RP && m.Thirst <= 10 && m.Thirst > 6)
                {
                    m.SendMessage(38, "Voce esta com sede.");
                }
            }
        }

        protected override void OnTick()
        {
            FoodDecay();
        }
    }
}
