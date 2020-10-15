using System;
using Server;

namespace Knives.Chat3
{
    public class Mail
    {
        public static void Initialize()
        {
            RUOVersion.AddCommand("mail", AccessLevel.Player, new ChatCommandHandler(OnMail));
            RUOVersion.AddCommand("Ma", AccessLevel.Player, new ChatCommandHandler(OnMail));
        }

        private static void OnMail(CommandInfo e)
        {
            General.List(e.Mobile, 2);
        }
    }
}