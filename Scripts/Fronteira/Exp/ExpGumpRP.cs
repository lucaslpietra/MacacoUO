
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using System.Collections.Generic;
using Server.Misc;

namespace Server.Gumps
{
    public class ExpGumpRP : Gump
    {
        Mobile caller;

        private static List<SkillName> UpComXP = new List<SkillName>();
        private static List<SkillName> UpComRepeticao = new List<SkillName>();

        public static void Initialize()
        {
            UpComXP.Add(SkillName.MagicResist);
            UpComXP.Add(SkillName.Magery);
            UpComXP.Add(SkillName.EvalInt);
            UpComXP.Add(SkillName.Meditation);
            UpComXP.Add(SkillName.Swords);
            UpComXP.Add(SkillName.Fencing);
            UpComXP.Add(SkillName.Macing);
            UpComXP.Add(SkillName.Tactics);
            UpComXP.Add(SkillName.Anatomy);
            UpComXP.Add(SkillName.Healing);
            UpComXP.Add(SkillName.ArmsLore);
            UpComXP.Add(SkillName.SpiritSpeak);
            UpComXP.Add(SkillName.Necromancy);
            UpComXP.Add(SkillName.Inscribe);
            UpComXP.Add(SkillName.Wrestling);
            UpComXP.Add(SkillName.Archery);
            UpComXP.Add(SkillName.Tracking);
            UpComXP.Add(SkillName.Hiding);
            UpComXP.Add(SkillName.Stealth);
            UpComXP.Add(SkillName.Ninjitsu);
            UpComXP.Add(SkillName.Focus);
            UpComXP.Add(SkillName.Parry);
            UpComXP.Add(SkillName.DetectHidden);
            UpComXP.Add(SkillName.Poisoning);
            UpComXP.Add(SkillName.Chivalry);
            UpComXP.Add(SkillName.Musicianship);
            UpComXP.Add(SkillName.Provocation);
            UpComXP.Add(SkillName.Discordance);
            UpComXP.Add(SkillName.Peacemaking);

            foreach(var s in Enum.GetValues(typeof(SkillName))) {
                var skill = (SkillName)s;
                if (UpComXP.Contains(skill)) continue;
                if (SkillCheck.Work.Contains(skill)) continue;
                UpComRepeticao.Add(skill);
            }
        }

        public ExpGumpRP(Mobile from) : base(0, 0)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;


            AddPage(0);
            AddBackground(224, 74, 755, 629, 5150);
            AddImage(254, 135, 52);

            double total = 0;
            double capTotal = 0;
            var x = 0;
            foreach(var s in UpComXP)
            {
                if (from.Skills[s].Cap == 0) continue;

                var skill = from.Skills[s];

                AddHtml(251, 268 + x, 153, 21, skill.Value+" " +skill.Name, (bool)false, (bool)false);
                AddButton(407, 270 + x, 55, 55, 0, GumpButtonType.Reply, skill.SkillID);
                AddImage(467, 272 + x, 58);
                AddHtml(424, 270 + x, 40, 18, SkillExpGump.GetCustoUp(from, s), (bool)false, (bool)false);
                AddHtml(487, 270 + x, 70, 18, @"Max "+(int)skill.Cap, (bool)false, (bool)false);
                total += skill.Value;
                capTotal += skill.Cap;
                x += 20;
            }

            x = 0;
            foreach (var s in SkillCheck.Work)
            {
                if (from.Skills[s].Cap == 0) continue;

                var skill = from.Skills[s];

                AddHtml(587, 267+x, 153, 21, skill.Value + " / " + skill.Cap + " " + skill.Name, (bool)false, (bool)false);
                total += skill.Value;
                capTotal += skill.Cap;
                x += 20;
            }

            x = 0;
            foreach (var s in UpComRepeticao)
            {
                if (from.Skills[s].Cap == 0) continue;

                var skill = from.Skills[s];
                AddHtml(790, 265+x, 153, 21, skill.Value + " / " + skill.Cap + " " + skill.Name, (bool)false, (bool)false);
                total += skill.Value;
                capTotal += skill.Cap;
                x += 20;
            }

            var pct = (int)Math.Ceiling((total / capTotal) * 100);

            AddImage(247, 250, 50);
            AddHtml(377, 99, 46, 19, @"Skills", (bool)false, (bool)false);
            AddHtml(300, 139, 98, 21, @"123 Exp", (bool)false, (bool)false);
            AddImage(307, 250, 50);
            AddHtml(449, 141, 108, 18, total+" / "+capTotal, (bool)false, (bool)false);
        
            AddImage(413, 250, 50);
            AddHtml(449, 159, 108, 18, pct+"%", (bool)false, (bool)false);
            AddHtml(449, 122, 54, 19, @"Total:", (bool)false, (bool)false);

            AddHtml(300, 219, 75, 19, @"Combate", (bool)false, (bool)false);
            AddItem(238, 216, 5049);
            AddImage(595, 250, 50);
            AddItem(595, 218, 3717);
            AddHtml(637, 221, 75, 19, @"Trabalho", (bool)false, (bool)false);

            AddImage(792, 249, 50);
            AddItem(593, 222, 4021);
            AddHtml(810, 220, 124, 19, @"Conhecimento", (bool)false, (bool)false);
            AddItem(770, 218, 4029);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {

                        break;
                    }

            }
        }
    }
}
