using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Fronteira.Talentos;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Gain a defensive advantage over your primary opponent for a short time.
    /// </summary>
    public class Feint : Habilidade
	{
        private static Dictionary<Mobile, FeintTimer> m_Registry = new Dictionary<Mobile, FeintTimer>();
        public static Dictionary<Mobile, FeintTimer> Registry { get { return m_Registry; } }

		public Feint()
		{
		}

		public override int BaseMana { get { return 30; } }

        public override SkillName GetSecondarySkill(Mobile from)
        {
            return from.Skills[SkillName.Ninjitsu].Base > from.Skills[SkillName.Bushido].Base ? SkillName.Ninjitsu : SkillName.Bushido;
        }

        public override Talento TalentoParaUsar { get { return Talento.Hab_Feint; } }

        public override void OnHit( Mobile attacker, Mobile defender, int damage )
		{
			if( !Validate( attacker ) || !CheckMana( attacker, true ) )
				return;

			if( Registry.ContainsKey( attacker ) )
			{
                if (m_Registry[attacker] != null)
                    m_Registry[attacker].Stop();

                Registry.Remove(attacker);
			}

            bool creature = attacker is BaseCreature;
			ClearCurrentAbility( attacker );

			attacker.SendLocalizedMessage( "Voce finta o inimigo" ); // You baffle your target with a feint!
			defender.SendLocalizedMessage( "O inimigo fintou e te confundiu" ); // You were deceived by an attacker's feint!

			attacker.FixedParticles( 0x3728, 1, 13, 0x7F3, 0x962, 0, EffectLayer.Waist );
            attacker.PlaySound(0x525);

            double skill = creature ? attacker.Skills[SkillName.Bushido].Value : 
                                                 attacker.Skills[SkillName.Tactics].Value;

            int bonus = (int)(20.0 + 3.0 * (skill - 50.0) / 7.0);

			FeintTimer t = new FeintTimer( attacker, defender, bonus );	//20-50 % decrease
   
			t.Start();
			m_Registry[attacker] = t;

            string args = String.Format("{0}\t{1}", defender.Name, bonus);
            BuffInfo.AddBuff(attacker, new BuffInfo(BuffIcon.Feint, 1151308, 1151307, TimeSpan.FromSeconds(6), attacker, args));

            if (creature)
                PetTrainingHelper.OnWeaponAbilityUsed((BaseCreature)attacker, SkillName.Bushido);
		}

        public class FeintTimer : Timer
        {
            private Mobile m_Owner;
            private Mobile m_Enemy;
            private int m_DamageReduction;

            public Mobile Owner { get { return m_Owner; } }
            public Mobile Enemy { get { return m_Enemy; } }

            public int DamageReduction { get { return m_DamageReduction; } }

            public FeintTimer(Mobile owner, Mobile enemy, int DamageReduction)
                : base(TimeSpan.FromSeconds(6.0))
            {
                m_Owner = owner;
                m_Enemy = enemy;
                m_DamageReduction = DamageReduction;
                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                Registry.Remove(m_Owner);
            }
        }
    }
}
