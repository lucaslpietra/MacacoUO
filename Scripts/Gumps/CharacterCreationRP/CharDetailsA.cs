using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Fronteira.Classes;

namespace Server.Gumps.CharacterCreationRP
{
    public class CharDetailsAGump : Gump
    {
        Mobile caller;

        public static void Initialize()
        {
            //CommandSystem.Register("CharDetailsA", AccessLevel.Administrator, new CommandEventHandler(RaceGump_OnCommand));
        }

        [Usage("")]
        [Description("Gump de seleção de raça na criação de personagem.")]
        public static void RaceGump_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(CharDetailsAGump)))
                caller.CloseGump(typeof(CharDetailsAGump));
            caller.SendGump(new CharDetailsAGump(caller));
        }

        public CharDetailsAGump() : base(0, 0)
        {
            return;
        }

        public CharDetailsAGump(Mobile from) : this()
        {
            caller = from;
        }

        public CharDetailsAGump(int selectedRace, ClassePersonagem selectedClasse, List<int> selectedSkills) : base(0, 0)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddImage(0, 1, 40319);
            AddLabel(318, 129, 1153, @"Nome:");
            AddTextEntry(378, 130, 198, 20, 1153, 0, @"");
            AddLabel(318, 175, 1153, @"Sexo:");
            AddLabel(403, 176, 1153, @"Feminino");
            AddLabel(502, 176, 1153, @"Masculino");
            AddButton(382, 177, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(480, 177, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(318, 218, 1153, @"Idade:");
            AddButton(381, 221, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(446, 222, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(522, 221, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(402, 219, 1153, @"Joven");
            AddLabel(468, 219, 1153, @"Maduro");
            AddLabel(544, 219, 1153, @"Velho");
            AddLabel(318, 258, 1153, @"Atributos:");
            AddLabel(338, 282, 1153, @"Força:");
            AddLabel(338, 308, 1153, @"Destreza:");
            AddLabel(338, 332, 1153, @"Inteligência:");
            AddLabel(462, 278, 1153, @"20");
            AddLabel(461, 305, 1153, @"20");
            AddLabel(461, 332, 1153, @"20");
            AddButton(490, 277, 1543, 1544, 0, GumpButtonType.Reply, 0);
            AddButton(490, 304, 1543, 1544, 0, GumpButtonType.Reply, 0);
            AddButton(490, 331, 1543, 1544, 0, GumpButtonType.Reply, 0);
            AddButton(442, 278, 1545, 1546, 0, GumpButtonType.Reply, 0);
            AddButton(442, 305, 1545, 1546, 0, GumpButtonType.Reply, 0);
            AddButton(442, 331, 1545, 1546, 0, GumpButtonType.Reply, 0);
            AddButton(738, 479, 9903, 9904, 0, GumpButtonType.Reply, 0);
            AddButton(427, 479, 9909, 9910, 0, GumpButtonType.Reply, 0);
            AddImage(469, 383, 40383);
            AddHtml(76, 292, 176, 313, @"", (bool)false, (bool)true);
            AddLabel(609, 125, 1153, @"Descrição Física:");
            AddTextEntry(608, 152, 253, 194, 1153, 0, @"");
            AddButton(837, 572, 4005, 4007, 0, GumpButtonType.Reply, 0);

            /*
            TextRelay entry0 = info.GetTextEntry(0);
            string text0 = (entry0 == null ? "" : entry0.Text.Trim());

            TextRelay entry0 = info.GetTextEntry(0);
            string text0 = (entry0 == null ? "" : entry0.Text.Trim());
            */
        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
            caller = sender.Mobile;

            switch (info.Switches[0])
            {
                case 11:
                    {
                        Console.WriteLine("Aludia");
                        break;
                    }
                case 22:
                    {
                        Console.WriteLine("Daru");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Deu Ruim no Gump da Raça");
                        break;
                    }
            }
            caller.SendGump(new ClassSelectionGump(info.Switches[0],null,null));
            return;
        }
    }
}
