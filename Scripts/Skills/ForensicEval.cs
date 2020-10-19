using System;
using System.Linq;
using System.Text;
using Server.Engines.Harvest;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using System.Collections.Generic;
using Server.Ziden.Traducao;

namespace Server.SkillHandlers
{
    public interface IForensicTarget
    {
        void OnForensicEval(Mobile m);
    }

    public class ForensicEvaluation
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Forensics].Callback = new SkillUseCallback(OnUse);
        }


        public static bool TellHarvestInformation(Mobile from, HarvestSystem system, object target)
        {
            int tileID;
            Map map;
            Point3D loc;

            if (!system.GetHarvestDetails(from, null, target, out tileID, out map, out loc))
            {
                //from.SendMessage("Este local nao parece ter recursos");
                return false;
            }

            var definition = system.GetDefinition(tileID);

            if (definition == null)
            {
                Shard.Debug("Definition null");
                return false;
            }

            var bank = definition.GetBank(map, loc.X, loc.Y);
            bool available = (bank != null && bank.Current >= definition.ConsumedPerHarvest);

            if (bank == null)
            {
                //.SendMessage("Parece nao haver nenhum tipo de recurso ali");
                return false;
            }

            List<Type> harvestables = new List<Type>();

            if (bank.Vein.FallbackResource != null)
            {
                foreach (var r in bank.Vein.FallbackResource.Types)
                {
                    harvestables.Add(r);
                }
            }

            foreach (var r in bank.Vein.PrimaryResource.Types)
            {
                harvestables.Add(r);
            }

            var jaFoi = new HashSet<CraftResource>();

            var str = "Aqui voce poderia encontrar  ";
            foreach (var t in harvestables)
            {
                var cs = CraftResources.GetFromType(t);
                if (cs != CraftResource.None && !jaFoi.Contains(cs))
                {
                    str += cs.ToString() + "  ";
                    jaFoi.Add(cs);
                }
                else
                    str += Trads.GetNome(t) + "  ";
            }
            from.SendMessage(str);
            return true;
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new ForensicTarget();
            m.RevealingAction();

            m.SendMessage("O que voce gostaria de investigar ?"); // Show me the crime.

            return TimeSpan.FromSeconds(1.0);
        }

        public class ForensicTarget : Target
        {
            public ForensicTarget()
                : base(10, true, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object target)
            {
                double skill = from.Skills[SkillName.Forensics].Value;
                double minSkill = 30.0;

                from.PublicOverheadMessage(Network.MessageType.Emote, 0, false, "* Investigando *");

                if (target is Corpse)
                {
                    Shard.Debug("Corpse");
                    if (skill < minSkill)
                    {
                        from.SendMessage("Voce nao ve nada fora do comum"); //You notice nothing unusual.
                        return;
                    }

                    if (from.CheckTargetSkillMinMax(SkillName.Forensics, target, minSkill, 100))
                    {
                        Corpse c = (Corpse)target;

                        if (c.m_Forensicist != null)
                            from.SendLocalizedMessage(1042750, c.m_Forensicist); // The forensicist  ~1_NAME~ has already discovered that:
                        else
                            c.m_Forensicist = from.Name;

                        if (((Body)c.Amount).IsHuman)
                            from.SendMessage("Isto foi morto por " + (c.Killer == null ? "no one" : c.Killer.Name));//This person was killed by ~1_KILLER_NAME~

                        if (c.Looters.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();

                            for (int i = 0; i < c.Looters.Count; i++)
                            {
                                if (i > 0)
                                    sb.Append(", ");

                                sb.Append(((Mobile)c.Looters[i]).Name);
                            }

                            from.SendLocalizedMessage(1042752, sb.ToString());//This body has been distrubed by ~1_PLAYER_NAMES~
                        }
                        else
                        {
                            from.SendLocalizedMessage(501002);//The corpse has not be desecrated.
                        }
                    }
                    else
                    {
                        from.SendLocalizedMessage(501001);//You cannot determain anything useful.
                    }
                }
                else if (target is Mobile)
                {
                    if (skill < 36.0)
                    {
                        from.SendLocalizedMessage(501003);//You notice nothing unusual.
                    }
                    else if (from.CheckTargetSkillMinMax(SkillName.Forensics, target, 36.0, 100.0))
                    {
                        if (target is PlayerMobile && ((PlayerMobile)target).NpcGuild == NpcGuild.ThievesGuild)
                        {
                            from.SendLocalizedMessage(501004);//That individual is a thief!
                        }
                        else
                        {
                            from.SendLocalizedMessage(501003);//You notice nothing unusual.
                        }
                    }
                    else
                    {
                        from.SendLocalizedMessage(501001);//You cannot determain anything useful.
                    }
                }
                else if (target is ILockpickable)
                {
                    if (skill < 41.0)
                    {
                        from.SendLocalizedMessage("Nada estranho...."); //You notice nothing unusual.
                    }
                    else if (from.CheckTargetSkillMinMax(SkillName.Forensics, target, 41.0, 100.0))
                    {
                        ILockpickable p = (ILockpickable)target;

                        if (p.Picker != null)
                        {
                            from.SendLocalizedMessage(1042749, p.Picker.Name);//This lock was opened by ~1_PICKER_NAME~
                        }
                        else
                        {
                            from.SendLocalizedMessage(501003);//You notice nothing unusual.
                        }
                    }
                    else
                    {
                        from.SendLocalizedMessage(501001);//You cannot determain anything useful.
                    }
                }
                else if (target is Item)
                {
                    Item item = (Item)target;

                    if (item is IForensicTarget)
                    {
                        ((IForensicTarget)item).OnForensicEval(from);
                    }
                    else if (skill < 41.0)
                    {
                        from.SendLocalizedMessage("Voce nao pode ver nada estranho");//You cannot determain anything useful.
                        return;
                    }

                    var honestySocket = item.GetSocket<HonestyItemSocket>();

                    if (honestySocket != null)
                    {
                        if (honestySocket.HonestyOwner == null)
                            Server.Services.Virtues.HonestyVirtue.AssignOwner(honestySocket);

                        if (from.CheckTargetSkillMinMax(SkillName.Forensics, target, 41.0, 100.0))
                        {
                            string region = honestySocket.HonestyRegion == null ? "an unknown place" : honestySocket.HonestyRegion;

                            if (from.Skills.Forensics.Value >= 61.0)
                            {
                                from.SendLocalizedMessage(1151521, String.Format("{0}\t{1}", honestySocket.HonestyOwner.Name, region)); // This item belongs to ~1_val~ who lives in ~2_val~.
                            }
                            else
                            {
                                from.SendLocalizedMessage(1151522, region); // You find seeds from a familiar plant stuck to the item which suggests that this item is from ~1_val~.
                            }
                        }
                    }
                }
                else
                {
                    if (from.CheckTargetSkillMinMax(SkillName.Forensics, target, 0, 100))
                    {
                        if (!ForensicEvaluation.TellHarvestInformation(from, Lumberjacking.System, target))
                            if (!ForensicEvaluation.TellHarvestInformation(from, Mining.System, target))
                                from.SendMessage("Nao parece haver recursos neste local");
                    }
                }
            }
        }
    }
}
