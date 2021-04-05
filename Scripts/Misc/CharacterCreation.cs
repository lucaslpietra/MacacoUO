#region References
using System;
using System.Linq;
using Server.Accounting;
using Server.Gumps.Newbie;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Ziden;
#endregion

namespace Server.Misc
{
    public class CharacterCreation
    {
        public static Point3D INICIO = new Point3D(5160, 1810, 0);

        private static readonly CityInfo m_NewHavenInfo = new CityInfo(
            "New Haven",
            "The Bountiful Harvest Inn",
            3503,
            2574,
            14,
            Map.Trammel);

        private static readonly CityInfo m_SiegeInfo = new CityInfo(
            "Britain",
            "The Wayfarer's Inn",
            1075074,
            1602,
            1591,
            20,
            Map.Felucca);

        private static Mobile m_Mobile;

        public static void Initialize()
        {
            // Register our event handler
            EventSink.CharacterCreated += EventSink_CharacterCreated;
        }

        public static bool VerifyProfession(int profession)
        {
            return true;
        }

        private static void AddBackpack(Mobile m)
        {
            var pack = m.Backpack;

            if (pack == null)
            {
                pack = new Backpack();
                pack.Movable = false;
                m.AddItem(pack);
            }

            var color = StarterKits.GetNoobColor();
            m.EquipItem(new Robe(color));

            if (!Shard.WARSHARD)
            {
                pack.AddItem(new RedBook("seu diario", m.Name, 20, true));
                pack.AddItem(new Gold(500)); // Starting gold can be customized here
                pack.AddItem(new Candle());
            } else
            {
                m.EquipItem(new Shoes(color));
                m.EquipItem(new Robe(color));
                m.EquipItem(StarterKits.GetRandomHat(color));
            }

            var book = new Spellbook((ulong)0x382A8C38);
            /*
            if (book.BookCount == 64)
            {
                book.Content = ulong.MaxValue;
            }
            else
            {
                book.Content = (1ul << book.BookCount) - 1;
            }
            */
            book.LootType = LootType.Blessed;
            pack.AddItem(book);
        }

        private static Mobile CreateMobile(Account a)
        {
            if (a.Count >= a.Limit)
                return null;

            for (var i = 0; i < a.Length; ++i)
            {
                if (a[i] == null)
                    return (a[i] = new PlayerMobile());
            }

            return null;
        }

        private static void EventSink_CharacterCreated(CharacterCreatedEventArgs args)
        {
            if (!VerifyProfession(args.Profession))
                args.Profession = 0;

            var state = args.State;

            if (state == null)
                return;

            var newChar = CreateMobile(args.Account as Account);

            if (newChar == null)
            {
                Utility.PushColor(ConsoleColor.Red);
                Console.WriteLine("Login: {0}: Character creation failed, account full", state);
                Utility.PopColor();
                return;
            }

            args.Mobile = newChar;
            m_Mobile = newChar;

            newChar.Player = true;
            newChar.AccessLevel = args.Account.AccessLevel;
            newChar.Female = args.Female;

            newChar.Race = Race.Human;

            newChar.Hue = args.Hue;

            if (args.Race != Race.Human)
                newChar.Hue = 33770;

            newChar.Hunger = 20;

            var young = false;

            if (newChar is PlayerMobile)
            {
                var pm = (PlayerMobile)newChar;

                pm.AutoRenewInsurance = true;

                var skillcap = Config.Get("PlayerCaps.SkillCap", 1000.0d) / 10;

                if (skillcap != 100.0)
                {
                    for (var i = 0; i < Enum.GetNames(typeof(SkillName)).Length; ++i)
                        pm.Skills[i].Cap = skillcap;
                }

                pm.Profession = 0;

                if (pm.IsPlayer() && pm.Account.Young && !Shard.RP)
                    young = pm.Young = true;
            }

            SetName(newChar, args.Name);

            AddBackpack(newChar);

            SetStats(newChar, state, args.Str, args.Dex, args.Int);

            var skills = args.Skills.Select(s => s.Name).ToList();

            var race = newChar.Race;

            if (race.ValidateHair(newChar, args.HairID))
            {
                newChar.HairItemID = args.HairID;
                newChar.HairHue = args.HairHue;
            }

            if (race.ValidateFacialHair(newChar, args.BeardID))
            {
                newChar.FacialHairItemID = args.BeardID;
                newChar.FacialHairHue = args.BeardHue;
            }

            var faceID = args.FaceID;

            if (faceID > 0 && race.ValidateFace(newChar.Female, faceID))
            {
                newChar.FaceItemID = faceID;
                newChar.FaceHue = args.FaceHue;
            }
            else
            {
                newChar.FaceItemID = race.RandomFace(newChar.Female);
                newChar.FaceHue = newChar.Hue;
            }

            if (TestCenter.Enabled)
                TestCenter.FillBankbox(newChar);

            if (young)
            {
                var ticket = new NewPlayerTicket
                {
                    Owner = newChar
                };

                newChar.BankBox.DropItem(ticket);
            }

            var city = args.City;
            //var map = Siege.SiegeShard && city.Map == Map.Trammel ? Map.Felucca : city.Map;


            newChar.MoveToWorld(city.Location, city.Map);

            Utility.PushColor(ConsoleColor.Green);
            Console.WriteLine("Login: {0}: New character being created (account={1})", state, args.Account.Username);
            Utility.PopColor();
            Utility.PushColor(ConsoleColor.DarkGreen);
            Console.WriteLine(" - Character: {0} (serial={1})", newChar.Name, newChar.Serial);
            Console.WriteLine(" - Started: {0} {1} in {2}", city.City, city.Location, city.Map);
            Utility.PopColor();

            //new WelcomeTimer(newChar).Start();
        }

        private static void FixStats(ref int str, ref int dex, ref int intel, int max)
        {
            var vMax = max - 30;

            var vStr = str - 10;
            var vDex = dex - 10;
            var vInt = intel - 10;

            if (vStr < 0)
                vStr = 0;

            if (vDex < 0)
                vDex = 0;

            if (vInt < 0)
                vInt = 0;

            var total = vStr + vDex + vInt;

            if (total == 0 || total == vMax)
                return;

            var scalar = vMax / (double)total;

            vStr = (int)(vStr * scalar);
            vDex = (int)(vDex * scalar);
            vInt = (int)(vInt * scalar);

            FixStat(ref vStr, (vStr + vDex + vInt) - vMax, vMax);
            FixStat(ref vDex, (vStr + vDex + vInt) - vMax, vMax);
            FixStat(ref vInt, (vStr + vDex + vInt) - vMax, vMax);

            str = vStr + 10;
            dex = vDex + 10;
            intel = vInt + 10;
        }

        private static void FixStat(ref int stat, int diff, int max)
        {
            stat += diff;

            if (stat < 0)
                stat = 0;
            else if (stat > max)
                stat = max;
        }

        private static void SetStats(Mobile m, NetState state, int str, int dex, int intel)
        {
            var max = state.NewCharacterCreation ? 90 : 80;

            FixStats(ref str, ref dex, ref intel, max);

            if (str < 10 || str > 60 || dex < 10 || dex > 60 || intel < 10 || intel > 60 || (str + dex + intel) != max)
            {
                str = 10;
                dex = 10;
                intel = 10;
            }

            if(str < 45)
            {
                str = 45;
            }

            m.InitStats(str, dex, intel);
        }

        private static void SetName(Mobile m, string name)
        {
            name = name.Trim();

            if (!NameVerification.Validate(name, 2, 16, true, false, true, 4, NameVerification.SpaceDashPeriodQuote))
            {
                name = "Nick Invalido";
                m.SendGump(new AtualizaNick());
            }

            m.Name = name;
        }

        public static string NICK_INVALIDO = "Nick Invalido";

        private static bool ValidSkills(SkillNameValue[] skills)
        {
            var total = 0;

            for (var i = 0; i < skills.Length; ++i)
            {
                if (skills[i].Value < 0 || skills[i].Value > 50)
                    return false;

                total += skills[i].Value;

                for (var j = i + 1; j < skills.Length; ++j)
                {
                    if (skills[j].Value > 0 && skills[j].Name == skills[i].Name)
                        return false;
                }
            }

            return (total == 100 || total == 120);
        }

        private class BadStartMessage : Timer
        {
            readonly Mobile m_Mobile;
            readonly int m_Message;

            public BadStartMessage(Mobile m, int message)
                : base(TimeSpan.FromSeconds(3.5))
            {
                m_Mobile = m;
                m_Message = message;
                Start();
            }

            protected override void OnTick()
            {
                m_Mobile.SendLocalizedMessage(m_Message);
            }
        }
    }
}
