using Server.Fronteira.Tutorial.WispGuia;
using Server.Gumps;
using Server.Mobiles;
using Server.Scripts.New.Adam.NewGuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira
{
    public class SequenciaLogin
    {
        public static void Initialize()
        {
            Console.WriteLine("Carregando sequencia de login");
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

            if (pm.RP)
            {
                if (pm.Profession == 0)
                {
                    pm.SendMessage("Escolha sua classe");
                    GumpClasse.Mostra(pm);
                }
            }
            else
            {
                if(pm.Wisp != null && pm.Profession==0 && pm.Wisp.Passo > (int)PassoTutorial.PEGAR_KIT)
                {
                    pm.SendGump(new NonRPClassGump());
                }
                else if (pm.Profession == 0 && pm.Wisp == null)
                {
                    if (pm.ContaRP)
                    {
                        pm.SendMessage("Escolha seu kit de skills iniciais");
                        pm.SendGump(new GumpCharRP(pm));
                    }
                    else
                    {
                        //pm.SendGump(new NonRPClassGump());
                        pm.SendGump(new GumpLoreThirdAge());
                    }
                }
                else
                {
                    //NewPlayerGuildAutoJoin.SendStarterGuild(pm);
                }
            }
        }
    }
}
