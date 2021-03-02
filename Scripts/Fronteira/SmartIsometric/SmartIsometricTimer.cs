using System;
using Server.Fronteira.SmartCreatures;
using Server.Mobiles;
using Server.Network;

namespace Server.Fronteira.SmartIsometric
{
    public class SmartIsometricTimer : Timer
    {
        public SmartIsometricTimer() : base(TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(50))
        {
            this.Priority = TimerPriority.FiftyMS;
        }

        public static void Initialize()
        {
            new SmartIsometricTimer().Start();
        }

        protected override void OnTick()
        {
            CheckCreatures();
        }

        private void CheckCreatures()
        {
            foreach (NetState state in NetState.Instances)
            {
                if (state != null && state.Mobile != null)
                {
                    SmartIsometricUtils.DoVisibleByAnotherPlayer(state.Mobile);

                    if (state.Mobile is BaseCreature)
                    {
                        BaseCreature creature = state.Mobile as BaseCreature;

                        SmartIsometricUtils.DoIsometricSmart(creature);
                    }
                }
            }
        }
    }
}
