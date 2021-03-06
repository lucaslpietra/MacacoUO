using System;
using System.Collections.Generic;
using Server.Fronteira.Talentos;
using Server.Mobiles;

namespace Server.Items
{
    public class PsychicAttack : WeaponAbility
    {
        public PsychicAttack()
        {
        }

        public override int BaseMana { get { return 30; } }

        public override Talento TalentoParaUsar { get { return Talento.Hab_PsyAttack; } }


        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!Validate(attacker) || !CheckMana(attacker, true))
                return;

            ClearCurrentAbility(attacker);

            attacker.SendLocalizedMessage("Seu golpe lanca energia psiquica e afeta o inimigo, fazendo-o mais vulneravel a magias"); // Your shot sends forth a wave of psychic energy.
            defender.SendLocalizedMessage("Sua mente foi afetada por energia psiquica, voce esta mais vulneravel a magias"); // Your mind is attacked by psychic force!
            defender.OverheadMessage("* confuso *");
            defender.FixedParticles(0x3789, 10, 25, 5032, EffectLayer.Head);
            defender.PlaySound(0x1F8);

            if (m_Registry.ContainsKey(defender))
            {
                if (!m_Registry[defender].DoneIncrease)
                {
                    m_Registry[defender].SpellDamageMalus *= 2;
                    m_Registry[defender].ManaCostMalus *= 2;
                }
            }
            else
                m_Registry[defender] = new PsychicAttackTimer(defender);

            BuffInfo.RemoveBuff(defender, BuffIcon.PsychicAttack);

            string args = String.Format("{0}\t{1}", m_Registry[defender].SpellDamageMalus, m_Registry[defender].ManaCostMalus);
            BuffInfo.AddBuff(defender, new BuffInfo(BuffIcon.PsychicAttack, 1151296, 1151297, args));
        }

        private static Dictionary<Mobile, PsychicAttackTimer> m_Registry = new Dictionary<Mobile, PsychicAttackTimer>();
        public static Dictionary<Mobile, PsychicAttackTimer> Registry { get { return m_Registry; } }

        public static void RemoveEffects(Mobile defender)
        {
            if (defender == null)
                return;

            BuffInfo.RemoveBuff(defender, BuffIcon.PsychicAttack);

            if (m_Registry.ContainsKey(defender))
                m_Registry.Remove(defender);

            defender.SendLocalizedMessage("Sua mente se recupera"); // You recover from the effects of the psychic attack.
        }

        public class PsychicAttackTimer : Timer
        {
            private Mobile m_Defender;
            private int m_SpellDamageMalus;
            private int m_ManaCostMalus;
            private bool m_DoneIncrease;

            public int SpellDamageMalus { get { return m_SpellDamageMalus; } set { m_SpellDamageMalus = value; m_DoneIncrease = true; } }
            public int ManaCostMalus { get { return m_ManaCostMalus; } set { m_ManaCostMalus = value; m_DoneIncrease = true; } }
            public bool DoneIncrease { get { return m_DoneIncrease; } }

            public PsychicAttackTimer(Mobile defender)
                : base(TimeSpan.FromSeconds(10))
            {
                m_Defender = defender;
                m_SpellDamageMalus = 15;
                m_ManaCostMalus = 15;
                m_DoneIncrease = false;
                Start();
            }

            protected override void OnTick()
            {
                RemoveEffects(m_Defender);
                Stop();
            }
        }
    }
}
