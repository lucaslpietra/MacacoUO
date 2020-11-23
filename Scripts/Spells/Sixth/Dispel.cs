using System;
using Server.Items;
using Server.Mobiles;
using Server.SkillHandlers;
using Server.Spells.Fifth;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Mysticism;
using Server.Spells.Necromancy;
using Server.Spells.Second;
using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class DispelSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Dispel", "An Ort",
            218,
            9002,
            Reagent.Garlic,
            Reagent.MandrakeRoot,
            Reagent.SulfurousAsh);
        public DispelSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Sixth;
            }
        }
        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public class InternalTarget : Target
        {
            private readonly DispelSpell m_Owner;
            public InternalTarget(DispelSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                this.m_Owner = owner;
            }

            public void RemoveUmCurse(Mobile m)
            {
                if (FeeblemindSpell.RemoveEffects(m))
                    m.SendMessage("Voce removeu Feeblemind");
                else if (WeakenSpell.RemoveEffects(m))
                    m.SendMessage("Voce removeu Weaken");
                else if (ClumsySpell.RemoveEffects(m))
                    m.SendMessage("Voce removeu Clumsy");
                else if (Discordance.RemoveEffect(m))
                    m.SendMessage("Voce removeu Discordance");
                else if (CurseSpell.RemoveEffectBool(m))
                    m.SendMessage("Voce removeu Curse");
                else if (EvilOmenSpell.TryEndEffect(m))
                    m.SendMessage("Voce removeu Pressagio");
                else if (StrangleSpell.RemoveCurse(m))
                    m.SendMessage("Voce Removeu Estrangular");
                else if (CorpseSkinSpell.RemoveCurse(m))
                    m.SendMessage("Voce Removeu Pele Morta");
                else if (MortalStrike.EndWound(m))
                    m.SendMessage("Voce Removeu Golpe Mortal");
                else if (BloodOathSpell.RemoveCurse(m))
                    m.SendMessage("Voce Removeu Pacto de Sangue");
                else if (MindRotSpell.ClearMindRotScalar(m))
                    m.SendMessage("Voce Removeu Mente Podre");
                else if (SpellPlagueSpell.RemoveFromList(m))
                    m.SendMessage("Voce Removeu Praga Mistica");
                else
                    m.SendMessage("Nao havia nenhuma magia negativa para dissipar");
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    Mobile m = (Mobile)o;
                    BaseCreature bc = m as BaseCreature;

                    if(m is GolemMecanico)
                    {
                        var golem = (GolemMecanico)m;
                        if(golem.Carregando)
                        {
                            if(Utility.RandomDouble() < 0.75)
                            {
                                from.SendMessage("Voce dissipou a energia que o mecanoide estava carregando.");
                                golem.Carregando = false;
                                golem.OverheadMessage("* dissipa a energia *");
                                AOS.Damage(golem, 50, DamageType.Spell, from);
                                m.FixedEffect(0x3779, 10, 20);
                                return;
                            } else
                            {
                                m.SendMessage("Voce nao conseguiu dissipar a energia");
                            }
                        }
                    }

                    if (!from.CanSee(m))
                    {
                        from.SendLocalizedMessage(500237); // Target can not be seen.
                    }
                    else if (bc == null && m.Player)
                    {
                        if (m_Owner.CheckHSequence(m))
                        {
                            Effects.PlaySound(m, m.Map, 0x201);
                            m.FixedEffect(0x3779, 10, 20);

                            if (m.Paralyzed)
                            {
                                from.SendMessage("Voce retirou a magia paralizante");
                                m.OverheadMessage("* livre *");
                                m.Paralyzed = false;
                            }
                            
                            else
                            {
                                if(m == from)
                                {
                                    if (MagicReflectSpell.EndReflect(m))
                                        m.SendMessage("Sua reflexao magica se dissipou");
                                    if (ProtectionSpell.EndProtection(m))
                                        m.SendMessage("Sua magia de protecao se dissipou");
                                    if (ReactiveArmorSpell.EndArmor(m))
                                        m.SendMessage("Sua armadura reativa se dissipou");
                                    if(!m.CanBeginAction(typeof(DefensiveSpell)))
                                    {
                                        m.SendMessage("Voce pode usar outra magia defensiva.");
                                        m.EndAction(typeof(DefensiveSpell));
                                    }
                                }
                                from.MovingParticles(m, 0x3779, 10, 0, false, false, 9502, 4019, 0x160);
                                RemoveUmCurse(m);
                            }
                            this.m_Owner.FinishSequence();
                        }
                      
                    } 
                    else if (bc == null || !bc.IsDispellable)
                    {
                        from.SendLocalizedMessage(1005049); // That cannot be dispelled.
                    }
                    else if (bc.SummonMaster == from || m_Owner.CheckHSequence(m))
                    {
                        SpellHelper.Turn(from, m);

                        double dispelChance = (50.0 + ((100 * (from.Skills.Magery.Value - bc.GetDispelDifficulty())) / (bc.DispelFocus * 2))) / 100;

                        if(bc.SummonMaster == from)
                        {
                            dispelChance = 1; // eu dispelo os meus sem errar
                        }
                        //Skill Masteries
                        dispelChance -= ((double)SkillMasteries.MasteryInfo.EnchantedSummoningBonus(bc) / 100);

                        if (dispelChance > Utility.RandomDouble())
                        {
                            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                            Effects.PlaySound(m, m.Map, 0x201);

                            m.Delete();
                        }
                        else
                        {
                            m.FixedEffect(0x3779, 10, 20);
                            from.SendLocalizedMessage("A criatura resistiu ao dispel"); // The creature resisted the attempt to dispel it!
                        }
                    }
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
