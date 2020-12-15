using System;
using System.Collections.Generic;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Commands
{
    public class CmdTalentos
    {
        public static void Initialize()
        {
            CommandSystem.Register("talentos", AccessLevel.Player, new CommandEventHandler(CMD));
        }

        [Usage("talentos")]
        [Description("Visualiza seus talentos")]
        public static void CMD(CommandEventArgs arg)
        {
            var pl = arg.Mobile as PlayerMobile;
            pl.SendGump(new TalentosGump(pl));
        }
    }
}
