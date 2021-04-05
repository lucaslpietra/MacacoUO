using Server.Fronteira.Classes;
using Server.Gumps;
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
            var qtsTalentos = pl.Talentos.Quantidade();
            if(pl.TemTalentoNovo())
            {
                var idClasse = pl.Profession;
                var classe = ClassDef.GetClasse(idClasse);
                if(classe != null)
                {
                    var talentosDoNivel = classe.Talentos[pl.Nivel - 1];
                    pl.SendGump(new GumpTalento(pl, talentosDoNivel));
                } else
                {
                    pl.SendMessage("Deu algo ruim, voce nao tem classe...");
                }
            } else
            {
                pl.SendMessage("Voce nao tem nenhum talento novo");
                if (pl.IsStaff())
                    pl.SendMessage("Voce ta nivel " + pl.Nivel.ToString() + " e tem " + pl.Talentos.Quantidade() + " talentos");
            }
        }


        /*
        public static void Initialize()
        {
            CommandSystem.Register("talentos", AccessLevel.Player, new CommandEventHandler(CMD));
            CommandSystem.Register("resettalentos", AccessLevel.GameMaster, new CommandEventHandler(CMD2));
            CommandSystem.Register("vertalentos", AccessLevel.GameMaster, new CommandEventHandler(CMD3));
        }

        [Usage("talentos")]
        [Description("Visualiza seus talentos")]
        public static void CMD(CommandEventArgs arg)
        {
            var pl = arg.Mobile as PlayerMobile;
            pl.SendGump(new GumpLivroTalento(pl, null));
        }


        [Usage("talentos")]
        [Description("Visualiza seus talentos")]
        public static void CMD3(CommandEventArgs arg)
        {
            arg.Mobile.SendMessage("Selecione um player pra resetar os talentos");
            arg.Mobile.Target = new IT();
        }

        [Usage("talentos")]
        [Description("Visualiza seus talentos")]
        public static void CMD2(CommandEventArgs arg)
        {
            arg.Mobile.SendMessage("Selecione um player pra resetar os talentos");
            arg.Mobile.Target = new IT();
        }

        private class IT : Target
        {
            public IT() :base(10, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if(targeted is PlayerMobile)
                {
                    ((PlayerMobile)targeted).Talentos.Wipa();
                    from.SendMessage("Talentos wipados");
                } else
                {
                    from.SendMessage("Isso nao tem talentos");
                }
            }
        }

        private class IT2 : Target
        {
            public IT2() : base(10, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is PlayerMobile)
                {
                    from.SendGump(new GumpLivroTalento((PlayerMobile)targeted, null));
                }
                else
                {
                    from.SendMessage("Isso nao tem talentos");
                }
            }
        }
        */
    }
}
