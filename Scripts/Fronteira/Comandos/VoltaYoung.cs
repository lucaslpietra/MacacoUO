using System;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Commands
{
    public class WipaPratinhas
    {
        public static void Initialize()
        {
            CommandSystem.Register("#" +
                "", AccessLevel.Administrator, new CommandEventHandler(CMD));
        }

        [Usage("receitas")]
        [Description("Camping Menu.")]
        public static void CMD(CommandEventArgs arg)
        {
            var gm = arg.Mobile;
            gm.SendMessage("Indo...");
            foreach(var pl in PlayerMobile.Instances)
            {
                pl.PointSystems.ViceVsVirtue = 0;
            }
            gm.SendMessage("Foi");
        }
    }
}
