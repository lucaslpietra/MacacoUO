using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Fourth
{
    public class GreaterHealSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Greater Heal", "In Vas Mani",
            204,
            9061,
            Reagent.Garlic,
            Reagent.Ginseng,
            Reagent.MandrakeRoot,
            Reagent.SpidersSilk);
        public GreaterHealSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fourth;
            }
        }
        
        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!this.Caster.CanSee(m))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (m is BaseCreature && ((BaseCreature)m).IsAnimatedDead)
            {
                this.Caster.SendLocalizedMessage(1061654); // You cannot heal that which is not alive.
            }
            else if (m.IsDeadBondedPet)
            {
                this.Caster.SendLocalizedMessage(1060177); // You cannot heal a creature that is already dead!
            }
            else if (m is IRepairableMobile)
            {
                this.Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500951); // You cannot heal that.
            }
            else if (Shard.SPHERE_STYLE && m.Poisoned)
            {
                this.Caster.PrivateOverheadMessage("Voce esta envenenado...");
            }
            else if (Server.Items.MortalStrike.IsWounded(m))
            {
                this.Caster.LocalOverheadMessage(MessageType.Regular, 0x22, (this.Caster == m) ? 1005000 : 1010398);
            }
            else if (this.CheckBSequence(m))
            {
                int toHeal = (int)(this.Caster.Skills[SkillName.Magery].Value * 0.2);
                toHeal += Utility.Random(1, 10);

                var inscript = this.Caster.Skills[SkillName.Inscribe].Value;
                if (Caster is BaseCreature)
                    inscript += 100;

                var inscriptBonus = (int)(inscript * 0.15);
                toHeal += inscriptBonus;
                
                /*
                if(this.Caster.GetRepeatedTypes(this.GetType(), TimeSpan.FromSeconds(20)) >= 2)
                {
                    this.Caster.SendMessage("Por usar a magia repetidamente seu poder nao e tao eficaz");
                    toHeal = (int)(toHeal * 0.85);
                }
                */

                //m.Heal( toHeal, Caster );
                SpellHelper.Heal(toHeal, m, this.Caster);

                Caster.MovingParticles(m, 0x376A, 7, 0, false, false, 9502, 0x376A, 0x1F2);
                m.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
                m.PlaySound(0x202);
            }

            this.FinishSequence();
        }

        public virtual bool PunishRepeatedSpells { get { return false; } }

        public class InternalTarget : Target
        {
            private readonly GreaterHealSpell m_Owner;
            public InternalTarget(GreaterHealSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Beneficial)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    this.m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
