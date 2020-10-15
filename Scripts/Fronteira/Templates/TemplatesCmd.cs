using System;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Commands
{
    public class Templates
    {
        public static void Initialize()
        {
            CommandSystem.Register("Templates", AccessLevel.Player, new CommandEventHandler(Cmd));
        }

        [Usage("Templates")]
        [Description("Templates.")]
        public static void Cmd(CommandEventArgs arg)
        {
            arg.Mobile.CloseGump(typeof(TemplatesGump));
            arg.Mobile.SendGump(new TemplatesGump((PlayerMobile)arg.Mobile));
        }
    }
}
