using System;
using Server.Mobiles;
using Server.Fronteira.Talentos;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Does damage and paralyses your opponent for a short time.
    /// </summary>
    public class NerveStrike : WeaponAbility
    {
        public NerveStrike()
        {
        }

        public override int BaseMana
        {
            get
            {
                return 30;
            }
        }
        public override bool CheckSkills(Mobile from)
        {
            if (this.GetSkill(from, SkillName.Bushido) < 50.0)
            {
                from.SendLocalizedMessage("Voce precisa de 50 Bushido para fazer isto"); // You need ~1_SKILL_REQUIREMENT~ Bushido skill to perform that attack!
                return false;
            }

            return base.CheckSkills(from);
        }

        public override bool OnBeforeSwing(Mobile attacker, Mobile defender)
        {
            if (!Core.ML && defender.Frozen)
            {
                attacker.SendLocalizedMessage("O alvo ja esta paralizado"); // The target is already frozen.
                return false;
            }

            return true;
        }

        public override Talento TalentoParaUsar { get { return Talento.Hab_NerveStrike; } }


        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!this.Validate(attacker) || !this.CheckMana(attacker, true))
                return;

            ClearCurrentAbility(attacker);

            bool immune = Server.Items.ParalyzingBlow.IsImmune(defender);
            bool doEffects = false;

            if (Core.ML)
            {
                AOS.Damage(defender, attacker, (int)(15.0 * (attacker.Skills[SkillName.Bushido].Value - 50.0) / 70.0 + Utility.Random(10)), true, 100, 0, 0, 0, 0);	//0-25

                if (!immune && ((150.0 / 7.0 + (4.0 * attacker.Skills[SkillName.Bushido].Value) / 7.0) / 100.0) > Utility.RandomDouble())
                {
                    defender.Paralyze(TimeSpan.FromSeconds(2.0));
                    doEffects = true;
                }

                if(attacker is BaseCreature)
                    PetTrainingHelper.OnWeaponAbilityUsed((BaseCreature)attacker, SkillName.Bushido);
            }
            else
            {
                AOS.Damage(defender, attacker, (int)(15.0 * (attacker.Skills[SkillName.Bushido].Value - 50.0) / 70.0 + 10), true, 100, 0, 0, 0, 0); //10-25

                if(!immune)
                {
                    defender.Freeze(TimeSpan.FromSeconds(2.0));
                    doEffects = true;
                }
            }

            if (!immune)
            {
                attacker.SendLocalizedMessage("Voce paralizou o alvo acertando um nervo dele"); // You cripple your target with a nerve strike!
                defender.SendLocalizedMessage("Seu inimigo acertou um nervo seu,  lhe paralizando"); // Your attacker dealt a crippling nerve strike!
            }
            else
            {
                attacker.SendLocalizedMessage("Resistiu ao ataque"); // Your target resists paralysis.
                defender.SendLocalizedMessage("Voce resistiu a um ataque no seu nervo"); // You resist paralysis.
            }

            if (doEffects)
            {
                attacker.PlaySound(0x204);
                defender.FixedEffect(0x376A, 9, 32);
                defender.FixedParticles(0x37C4, 1, 8, 0x13AF, 0, 0, EffectLayer.Waist);
            }

            Server.Items.ParalyzingBlow.BeginImmunity(defender, Server.Items.ParalyzingBlow.FreezeDelayDuration);
        }
    }
}
