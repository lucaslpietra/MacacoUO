
using Server.Gumps.Newbie;
using Server.Fronteira.Classes;
using Server.Network;

namespace Server.Gumps
{
    public class ClassGump : Gump
    {
        string chosen = null;
        string desc = null;

        private StarterKits.Kit k;
        private bool newCharacter;

        public ClassGump(ClassePersonagem vendo = null) : base(0, 0)
        {
            this.Closable = !newCharacter;
            this.Disposable = !newCharacter;
            this.Dragable = false;
            this.Resizable = false;

            AddPage(0);
            AddBackground(79, 69, 493, 464, 9200);
            AddImage(29, 13, 10440);
            AddImage(540, 14, 10441);
            AddImage(234, -167, 1418);

            var classes = ClassDef.GetClasses();

            var x = 100;
            var y = 84;
            foreach (var classe in classes)
            {
                AddHtml(x, y, 86, 19, classe.Nome, (bool)false, (bool)false);
                AddButton(x + 7, y + 20, classe.Icone, classe.Icone + 1, 1, GumpButtonType.Reply, 0);
                x += 100;
            }

            var desc = "Selecione uma template de skills iniciais.";

            if (vendo != null)
            {
                desc = vendo.Nome + "<br>";

                AddHtml(103, 258, 441, 83, desc, (bool)true, (bool)false);
                AddButton(473, 498, 247, 248, 0, GumpButtonType.Reply, 0);

                AddHtml(104, 368, 441, 83, "", (bool)true, (bool)false);
                AddHtml(106, 345, 200, 20, @"Skills", (bool)false, (bool)false);

                x = 0;
                y = 0;

                foreach (var skillname in vendo.ClassSkills)
                {
                    AddHtml(110 + x, 370 + y, 441, 83, skillname.ToString(), false, false);
                    x += 120;
                    if (x > 330)
                    {
                        x = 0;
                        y += 30;
                    }
                }

            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            var pl = sender.Mobile;
            var escolhida = info.ButtonID;
            var classe = ClassDef.GetClasse(escolhida);
            if(classe != null)
            {
                pl.CloseAllGumps();
                pl.SendGump(new ClassGump(classe));
            }
        }
    }
}
