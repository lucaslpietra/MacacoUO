using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Spells.Necromancy
{
    public abstract class NecromancerSpell : Spell
    {
        public NecromancerSpell(Mobile caster, Item scroll, SpellInfo info)
            : base(caster, scroll, info)
        {
        }

        public abstract double RequiredSkill { get; }
        public abstract int RequiredMana { get; }
        public override SkillName CastSkill
        {
            get
            {
                return SkillName.Necromancy;
            }
        }
        public override SkillName DamageSkill
        {
            get
            {
                return SkillName.SpiritSpeak;
            }
        }
        //public override int CastDelayBase{ get{ return base.CastDelayBase; } } // Reference, 3
        public override bool ClearHandsOnCast
        {
            get
            {
                return false;
            }
        }

        public override TimeSpan GetCastDelay()
        {
            var d = base.GetCastDelay();
            return TimeSpan.FromSeconds(0.4) + d;
        }

        public override double CastDelayFastScalar
        {
            get
            {
                return (Core.SE ? base.CastDelayFastScalar : 0);
            }
        }// Necromancer spells are not affected by fast cast items, though they are by fast cast recovery
        public override int ComputeKarmaAward()
        {
            //TODO: Verify this formula being that Necro spells don't HAVE a circle.
            //int karma = -(70 + (10 * (int)Circle));
            int karma = -(40 + (int)(10 * (this.CastDelayBase.TotalSeconds / this.CastDelaySecondsPerTick)));

            if (Core.ML) // Pub 36: "Added a new property called Increased Karma Loss which grants higher karma loss for casting necromancy spells."
                karma += AOS.Scale(karma, AosAttributes.GetValue(this.Caster, AosAttribute.IncreasedKarmaLoss));

            return karma;
        }

        public virtual bool CheckResisted(Mobile target, int circle)
        {
          
            if (target == Caster)
                return false;

            circle -= 1;
            double n = GetResistPercentForCircle(target, circle);

            n /= 100.0;

            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            int maxSkill = (1 + circle * 10);
            maxSkill += (1 + (circle / 6)) * 25;

            if (target.Skills[SkillName.MagicResist].Value < maxSkill && target != Caster)
                target.CheckSkillMult(SkillName.MagicResist, 0.0, target.Skills[SkillName.MagicResist].Cap);

            var resisted = (n >= Utility.RandomDouble());

            if (resisted)
            {
                Caster.PlaySound(0x1E6);
                target.FixedEffect(0x42CF, 10, 5);
            }
            return resisted;
        }

        /*
        public override void SayMantra()
        {
            if (Info.Mantra != null && Info.Mantra.Length > 0 && (m_Caster.Player || (m_Caster is BaseCreature && ((BaseCreature)m_Caster).ShowSpellMantra)))
            {
                m_Caster.PublicOverheadMessage(MessageType.Regular, 723, false, Info.Mantra);
            }
        }
        */

        public override void GetCastSkills(out double min, out double max)
        {
            min = this.RequiredSkill;
            max = this.Scroll != null ? min : this.RequiredSkill + 40.0;
            if (max > 100)
                max = 100;
        }

        public virtual double GetResistPercentForCircle(Mobile target, int circle)
        {
            var resist = Caster.Skills[SkillName.MagicResist].Value;
            var cap = resist / 5;
            if (cap < 15)
                cap = 15;

            var magery = Caster.Skills[CastSkill].Value;
            var circ = 1 + circle;

            var chance = ((magery * 2) / 15 + circ * circ);
            chance = resist - chance;
            if (chance < cap)
                chance = cap;
            return chance;
        }

        public override bool ConsumeReagents()
        {
            if (base.ConsumeReagents())
                return true;

            if (ArcaneGem.ConsumeCharges(this.Caster, 1))
                return true;

            return false;
        }

        public override int GetMana()
        {
            return this.RequiredMana;
        }
    }
}
