using Server.Items;
using Server.Mobiles;
using Server.Ziden;
using System.Collections.Generic;
using System.Linq;

namespace Server.Multis
{
    public class Acampamento
    {
        public static Dictionary<string, CampSiteAddon> Points = new Dictionary<string , CampSiteAddon>();

        public void OnEnter(Mobile m)
        {
            if(!m.IsCooldown("msgcamp"))
            {
                m.SetCooldown("msgcamp");
                m.PrivateOverheadMessage("* confortavel *");
                m.SendMessage(78, "Jogadores podem acampar proximo de campings usando a skill Camping para salvar o local para facil locomocao");
            }
        }

        public static void Acampa(PlayerMobile m)
        {
            CampSiteAddon camp = null;
            double dist = int.MaxValue;

            string nomeCamp = null;
            foreach(var nome in Points.Keys)
            {
                var point = Points[nome];
                if(point.Map == m.Map)
                {
                    var d = point.GetDistance(m);
                    if(d < dist && d < 100)
                    {
                        dist = d;
                        camp = point;
                        nomeCamp = nome;
                    }
                }
            }

            if(camp != null)
            {
                var discobertas = m.CampfireLocations.Split(';').ToList();
                if (discobertas.Contains(nomeCamp))
                {
                    return;
                }
                m.CampfireLocations += nomeCamp + ";";
                m.Emote("Local de Camping Descoberto: " + nomeCamp);
                m.SendMessage(78, "Clique duas vezes em uma fogueira segura para se teleportar a este acampamento");
            } else
            {
                m.SendMessage("Nao existe um local de camping proximo deste local. Procure por locais de camping (troncos ao redor, e uma terrinha no meio)");
            }
        }
    }
}
