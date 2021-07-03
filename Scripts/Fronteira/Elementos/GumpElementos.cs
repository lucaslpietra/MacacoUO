using Server.Network;
using Server.Commands;
using Server.Mobiles;
using Server.Items;
using Server.Fronteira.Elementos;
using Server.Leilaum;

namespace Server.Gumps
{
    public class ElementosGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("elementos", AccessLevel.Player, new CommandEventHandler(_OnCommand));
        }

        [Usage("")]
        [Description("Makes a call to your custom gump.")]
        public static void _OnCommand(CommandEventArgs e)
        {
            var caller = e.Mobile as PlayerMobile;

            if (caller.HasGump(typeof(ElementosGump)))
                caller.CloseGump(typeof(ElementosGump));
            caller.SendGump(new ElementosGump(caller));
        }

        public ElementosGump(PlayerMobile pl, ElementoPvM elemento = ElementoPvM.None) : base(0, 0)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(306, 127, 642, 338, 9200);
            AddImageTiled(362, 201, 96, 7, 40);
            AddHtml(365, 173, 71, 19, @"Fogo", (bool)false, (bool)false);
            AddImageTiled(518, 200, 96, 7, 40);
            AddHtml(521, 172, 71, 19, @"Raio", (bool)false, (bool)false);
            AddImageTiled(674, 201, 96, 7, 40);
            AddHtml(677, 173, 71, 19, @"Gelo", (bool)false, (bool)false);
            AddImageTiled(830, 200, 96, 7, 40);
            AddHtml(833, 172, 86, 19, @"Agua", (bool)false, (bool)false);
            AddImageTiled(830, 200, 96, 7, 40);
            AddImageTiled(362, 248, 96, 7, 40);
            AddHtml(365, 220, 71, 19, @"Terra", (bool)false, (bool)false);
            AddImageTiled(518, 247, 96, 7, 40);
            AddHtml(521, 219, 71, 19, @"Vento", (bool)false, (bool)false);
            AddImageTiled(362, 248, 96, 7, 40);
            AddImageTiled(674, 248, 96, 7, 40);
            AddHtml(677, 220, 71, 19, @"Luz", (bool)false, (bool)false);
            AddImageTiled(830, 247, 96, 7, 40);
            AddHtml(833, 219, 90, 19, @"Escuridao", (bool)false, (bool)false);
            AddImageTiled(830, 247, 96, 7, 40);

            AddButton(628, 214, 20742, 20742, (int)ElementoPvM.Luz, GumpButtonType.Reply, 0);
            AddButton(472, 165, 2288, 2288, (int)ElementoPvM.Raio, GumpButtonType.Reply, 0);
            AddButton(628, 165, 21002, 21002, (int)ElementoPvM.Gelo, GumpButtonType.Reply, 0);
            AddButton(784, 166, 2303, 2303, (int)ElementoPvM.Agua, GumpButtonType.Reply, 0);
            AddButton(315, 211, 2294, 2294, (int)ElementoPvM.Terra, GumpButtonType.Reply, 0);
            AddButton(472, 211, 20740, 20740, (int)ElementoPvM.Vento, GumpButtonType.Reply, 0);
            AddButton(315, 161, 2245, 2245, (int)ElementoPvM.Fogo, GumpButtonType.Reply, 0);
            AddButton(783, 212, 20495, 20495, (int)ElementoPvM.Escuridao, GumpButtonType.Reply, 0);

            AddHtml(543, 136, 123, 20, @"Elementos PvM", (bool)false, (bool)false);
            AddBackground(316, 263, 620, 189, 3500);
            AddImage(254, 62, 10440);
            AddImage(916, 65, 10441);

            if (elemento != ElementoPvM.None)
            {
                var nivel = pl.Elementos.GetNivel(elemento);
                AddHtml(533, 282, 124, 20, "Nivel: "+nivel, (bool)false, (bool)false);
                AddHtml(403, 282, 124, 20, Gump.Cor(elemento.ToString(), BaseArmor.CorElemento(elemento)), (bool)false, (bool)false);
                AddImage(344, 283, (int)elemento);
                AddBackground(331, 329, 299, 101, 3500);

                var efeitos = EfeitosElementos.GetEfeitos(elemento);
                var bonusAtual = pl.Elementos.BonusPorNivel(nivel) * 100;
                var proximoNivel = pl.Elementos.BonusPorNivel(nivel + 1) * 100;
                var ganho = proximoNivel - bonusAtual;

                var str = "";
                for (var x= 0; x < efeitos.Length; x++)
                {
                    str += Gump.Cor(bonusAtual.ToString("0.00")+"%", "brown")+" -> "+Gump.Cor(proximoNivel.ToString("0.00")+"% ","green")+efeitos[x]+"<br>";
                }
                AddHtml(355, 342, 249, 73, str, (bool)false, (bool)false);
                AddHtml(404, 316, 124, 20, @"Prox Nivel:", (bool)false, (bool)false);
                AddHtml(728, 315, 124, 20, @"Subir de Nivel", (bool)false, (bool)false);

                var custos = CustosUPElementos.GetCustos(elemento);

                AddBackground(673, 334, 111, 101, 3500);
                AddHtml(721, 350, 83, 22, custos[0].amt.ToString(), (bool)false, (bool)false);
                AddHtml(678, 406, 100, 22, custos[0].name, (bool)true, (bool)false);
                //AddItem(703, 374, custos.Item);
                NewAuctionGump.AddItemCentered(673, 334, 111, 101, custos[0].itemID, custos[0].hue, this);

                AddBackground(784, 335, 111, 101, 3500);
                AddHtml(827, 350, 83, 22, custos[1].amt.ToString(), (bool)false, (bool)false);
                AddHtml(793, 405, 100, 22, custos[1].name, (bool)true, (bool)false);
                //AddItem(811, 367, 576);
                NewAuctionGump.AddItemCentered(784, 335, 111, 101, custos[1].itemID, custos[0].hue, this);

                AddHtml(534, 317, 124, 20, "Exp: "+pl.Elementos.GetExp(elemento)+" / "+CustosUPElementos.CustoUpExp(nivel), (bool)false, (bool)false);
                AddButton(757, 435, 247, 248, (int)ElementoButtons.Upar, GumpButtonType.Reply, 0);
            }
        }

        public enum ElementoButtons
        {
            Nads,
            Upar,
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            var from = sender.Mobile as PlayerMobile;

            switch (info.ButtonID)
            {
                case (int)ElementoPvM.Luz:
                    {
                        from.SendGump(new ElementosGump(from, ElementoPvM.Luz));
                        break;
                    }
                case (int)ElementoPvM.Raio:
                    {

                        from.SendGump(new ElementosGump(from, ElementoPvM.Raio));
                        break;
                    }
                case (int)ElementoPvM.Gelo:
                    {
                        from.SendGump(new ElementosGump(from, ElementoPvM.Gelo));
                        break;
                    }
                case (int)ElementoPvM.Agua:
                    {
                        from.SendGump(new ElementosGump(from, ElementoPvM.Agua));
                        break;
                    }
                case (int)ElementoPvM.Terra:
                    {
                        from.SendGump(new ElementosGump(from, ElementoPvM.Terra));
                        break;
                    }
                case (int)ElementoPvM.Vento:
                    {
                        from.SendGump(new ElementosGump(from, ElementoPvM.Vento));
                        break;
                    }
                case (int)ElementoPvM.Fogo:
                    {
                        from.SendGump(new ElementosGump(from, ElementoPvM.Fogo));
                        break;
                    }
                case (int)ElementoPvM.Escuridao:
                    {
                        from.SendGump(new ElementosGump(from, ElementoPvM.Escuridao));
                        break;
                    }
                case 0:
                    {

                        break;
                    }

            }
        }
    }
}
