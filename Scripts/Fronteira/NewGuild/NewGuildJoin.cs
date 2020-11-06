#region References

using System.Linq;
using System.Net;
using Server.Accounting;
using Server.Network;
using Server.Guilds;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using Server.Ziden.Tutorial;

#endregion

namespace Server.Scripts.New.Adam.NewGuild
{
    public abstract class NewPlayerGuildAutoJoin
    {
        public static string Abrev = "Noob";

        public static void Initialize()
        {
            Console.WriteLine("Carregando sistema de guilda dos novatos");
            EventSink.Login += OnLogin;
        }

        private static void OnLogin(LoginEventArgs e)
        {
            var pm = e.Mobile as PlayerMobile;

            if (pm == null)
                return;

            if (Shard.WARSHARD)
            {
                return;
            }



            if (pm.Profession == 0)
            {
                if(pm.ContaRP)
                {
                    pm.SendMessage("Escolha seu kit de skills iniciais");
                    pm.SendGump(new GumpCharRP(pm));
                } else
                {
                    pm.SendMessage("Escolha se deseja criar um personagem RP ou NORMAL");
                    pm.SendGump(new ClassGump());
                }
            }
            else
            {
                SendStarterGuild(pm);
            }
        }

        public static HashSet<Serial> asked = new HashSet<Serial>();

        public static void SendStarterGuild(PlayerMobile pm)
        {
            Shard.Debug("Invitando pra guilda noob", pm);
            if (pm == null || pm.Guild != null)
            {
                Shard.Debug("R1");
                return;
            }

            if (asked.Contains(pm.Serial))
                return;

            Shard.Debug("Total Skills: " + pm.SkillsTotal, pm); ;
            if (pm.SkillsTotal >= 5000)
            {
                return;
            }

            if (NewGuildPersistence.Instance == null)
            {

                var NGP = new NewGuildPersistence();
            }

            foreach(var guilda in BaseGuild.List.Values)
            {
                Shard.Debug("Guilda Existe: " + guilda.Abbreviation);
            }

            var g = (Guild)BaseGuild.FindByAbbrev(Abrev);

            if (g != null)
            {
                asked.Add(pm.Serial);
                pm.SendGump(new NewPlayerGuildJoinGump(g, pm));

            } else
            {
                TutorialNoob.InicializaWisp(pm);
                Shard.Debug("Nao achei guilda noob com tag " + Abrev);
            }
        }
    }
}
