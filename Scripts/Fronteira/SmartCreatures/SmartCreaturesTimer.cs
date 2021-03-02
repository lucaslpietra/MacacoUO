using System;
using Server.Mobiles;
using Server.Network;

namespace Server.Fronteira.SmartCreatures
{
    public class SmartCreaturesTimer : Timer
    {
        public SmartCreaturesTimer() : base(TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(100))
        {
            this.Priority = TimerPriority.FiftyMS;
        }

        public static void Initialize()
        {
            new SmartCreaturesTimer().Start();
        }

        protected override void OnTick()
        {
            CheckCreatures();
        }

        private void CheckCreatures()
        {
            foreach (NetState state in NetState.Instances)
            {
                if (state != null && state.Mobile != null && state.Mobile is BaseCreature)
                {
                    BaseCreature creature = state.Mobile as BaseCreature;

                    SmartCreaturesUtils.DoSmart(creature);
                }
            }
        }
    }
}
