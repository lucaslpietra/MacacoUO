using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells.Third;

namespace Server.Spells
{
    public abstract class MagerySpell : Spell
    {
        public virtual void SayMantra()
        {
            if (Info.Mantra != null && Info.Mantra.Length > 0 && (m_Caster.Player || (m_Caster is BaseCreature && ((BaseCreature)m_Caster).ShowSpellMantra)))
            {
                m_Caster.PublicOverheadMessage(MessageType.Regular, 0, false, Info.Mantra);
                /*
                if (m_Caster is PlayerMobile)
                {
                    m_Caster.PublicOverheadMessage(MessageType.Emote, 1153, false, m_Info.Mantra);
                }
                else
                    m_Caster.PublicOverheadMessage(MessageType.Regular, 0, false, "* conjurando uma magia *");
                //m_Caster.PublicOverheadMessage(MessageType.Spell, m_Caster.SpeechHue, true, m_Info.Mantra, false);
                */
            }
        }
        // Magias q vao ficar mais dificeis castar andando
        public static readonly Type[] MovementNerfWhenRepeated =
        {
            typeof(TeleportSpell)
        };

        public virtual int GetMinSkill { get { return 0; } }
        private static readonly int[] m_ManaTable = new int[] { 4, 6, 9, 11, 14, 20, 40, 50 };
        private const double ChanceOffset = 20.0, ChanceLength = 100.0 / 7.0;
        public MagerySpell(Mobile caster, Item scroll, SpellInfo info)
            : base(caster, scroll, info)
        {
        }

        public abstract SpellCircle Circle { get; }
        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds((3 + (int)Circle) * CastDelaySecondsPerTick);
            }
        }
        public override bool ConsumeReagents()
        {
            if (base.ConsumeReagents())
                return true;

            if (ArcaneGem.ConsumeCharges(Caster, (Core.SE ? 1 : 1 + (int)Circle)))
                return true;

            return false;
        }

        public override bool ValidateCast(Mobile from)
        {
            var circleMax = this.CicloArmadura(from);
            if(circleMax < (int)this.Circle+1)
            {
                from.SendMessage("Esta armadura e muito pesada para esta magia");
                return false;
            }
            return true;
        }

        public override int SkillNeeded
        {
            get
            {
                if (Circle == SpellCircle.Eighth)
                    return 100;
                if (Circle == SpellCircle.Seventh)
                    return 80;
                if (Circle == SpellCircle.Sixth)
                    return 65;

                return (int)((double)(Circle + 1) * 11);
            }
        }

        public override void GetCastSkills(out double min, out double max)
        {
            int circle = (int)Circle;

            if (Scroll != null)
                circle -= 2;

            double avg = ChanceLength * circle;

            min = avg - ChanceOffset;
            max = avg + ChanceOffset;

            var m = GetMinSkill;
            if (m != 0)
            {
                min = m;
            }
        }

        public override int GetMana()
        {
            if (Scroll is BaseWand)
                return 0;

            return m_ManaTable[(int)Circle];
        }

        public virtual bool CheckResisted(Mobile target)
        {
            if (target == Caster)
                return false;

            double n = GetResistPercent(target);

            Shard.Debug("Resist % " + n, target);

            n /= 100.0;

         

            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            int maxSkill = (1 + (int)Circle) * 10;
            maxSkill += (1 + ((int)Circle / 6)) * 25;

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

        public virtual double GetResistPercentForCircle(Mobile target, SpellCircle circle)
        {
            var resist = target.Skills[SkillName.MagicResist].Value;
            var cap = resist / 5;

            var magery = Caster.Skills[CastSkill].Value;
            var circ = 1 + (double)circle;

            var chance = ((magery * 2) / 10 + circ * circ);

            if(Shard.DebugEnabled)
                Shard.Debug("Chance Base: " + chance+" circulo "+circ);

            chance = resist - chance;
            if (chance < cap)
                chance = cap;

            if(Shard.SPHERE_STYLE)
                chance *= 0.35; // sem pre cast mais dificil de resistir
            else
                chance *= 0.80;

            if (Caster is BaseCreature && target is PlayerMobile)
                chance /= 1.5;

            Shard.Debug("Chance RS: " + chance, target);

            return chance;
        }

        public virtual double GetResistPercent(Mobile target)
        {
            return GetResistPercentForCircle(target, Circle);
        }

        public override TimeSpan GetCastDelay()
        {
            if (!Core.ML && Scroll is BaseWand)
                return TimeSpan.Zero;

            if (!Core.AOS)
            {
                if(Caster is BaseCreature)
                    return TimeSpan.FromSeconds(0.5 + 0.35 * (int)Circle);
                //if(Circle < SpellCircle.Third)
                //    return TimeSpan.FromSeconds(1.25);
                return TimeSpan.FromSeconds(0.5 + 0.35 * (int)Circle);
            }
            return base.GetCastDelay();
        }
    }
}
