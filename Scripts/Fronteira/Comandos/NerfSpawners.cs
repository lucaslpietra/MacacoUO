using Server.Commands;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.Comandos
{
    class NerfSpawners
    {
        public static void Initialize()
        {
            CommandSystem.Register("nerfartodosspawners", AccessLevel.Administrator, new CommandEventHandler(CMD));
        }

        public static void CMD(CommandEventArgs arg)
        {
            var pl = arg.Mobile as PlayerMobile;
            pl.SendMessage("Nerfando todos spawners da natureza de trammel");
            foreach (var item in World.Items.Values)
            {
                if(item.Map == Map.Trammel && item is XmlSpawner)
                {
                    var spawner = (XmlSpawner)item;
                    var region = spawner.GetRegion();
                    if(region == null || region.Name == null)
                    {
                        var count = spawner.MaxCount;
                        spawner.MaxCount = (int)Math.Floor(count / 2d);
                    }
                }
            }
        }
    }
}
