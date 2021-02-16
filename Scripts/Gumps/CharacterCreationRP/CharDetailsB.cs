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
    public class CharDetailsBGump : Gump
    {
        Mobile caller;
        int raceEscolhida;
        ClassePersonagem classeEscolhida;
        List<int> skillsEscolhidas;
        List<int> detailsAInteirosEscolhidos;
        List<string> detailsAStringsEscolhidos;

        List<int> defeitosEscolhidos;
        List<int> qualidadesEscolhidas;
        List<int> skillsComplementaresEscolhidas;
        List<int> detailsBInteirosEscolhidos;

        public static void Initialize()
        {
            //CommandSystem.Register("CharDetailsB", AccessLevel.Administrator, new CommandEventHandler(CharDetailsBGump_OnCommand));
        }

        [Usage("")]
        [Description("Gump de seleção detalhes B na criação de personagem.")]
        public static void CharDetailsBGump_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(CharDetailsBGump)))
                caller.CloseGump(typeof(CharDetailsBGump));
            caller.SendGump(new CharDetailsBGump(caller));
        }

        public CharDetailsBGump() : base(0, 0)
        {
            return;
        }

        public CharDetailsBGump(Mobile from) : this()
        {
            caller = from;
        }


        public CharDetailsBGump(int selectedRace, ClassePersonagem selectedClasse, List<int> selectedSkills, List<int> detailAint, List<string> detailAstr, List<int> detailBint, List<int> selectedDefeitos, List<int> selectedQualidades, List<int> selectedSkillsComplementares) : base(0, 0)
        {
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;

            raceEscolhida = selectedRace;

            if (selectedClasse != null)
            {
                classeEscolhida = selectedClasse;
            }
            if (selectedSkills != null)
            {
                skillsEscolhidas = selectedSkills;
            }
            if (detailAint != null)
            {
                detailsAInteirosEscolhidos = detailAint;
            }
            if (detailAstr != null)
            {
                detailsAStringsEscolhidos = detailAstr;
            }

            if (detailBint != null)
            {
                detailsBInteirosEscolhidos = detailBint;
            }
            else
            {
                detailsBInteirosEscolhidos = new List<int>();
                detailsBInteirosEscolhidos.Insert(0, detailsAInteirosEscolhidos[0] == 5 ? 101 : 501);  //ID do Tipo de cabelo
                detailsBInteirosEscolhidos.Insert(1, detailsAInteirosEscolhidos[0] == 5 ? 11 : 21); //Tipo de vestimenta
            }
            if (selectedDefeitos != null)
            {
                defeitosEscolhidos = selectedDefeitos;
            }
            else
            {
                defeitosEscolhidos = new List<int>();
            }
            if (selectedQualidades != null)
            {
                qualidadesEscolhidas = selectedQualidades;
            }
            else
            {
                qualidadesEscolhidas = new List<int>();
            }

            int idSexo, idIdade;

            idSexo = detailsAInteirosEscolhidos[0];
            idIdade = detailsAInteirosEscolhidos[1];

            AddPage(0);
            AddImage(0, -1, 40320);
            AddHtml(68, 290, 177, 315, @"", (bool)false, (bool)true);
            AddLabel(318, 123, 1153, @"Defeitos:");
            AddLabel(318, 255, 1153, @"Qualidades:");
            AddLabel(320, 382, 1153, @"Profissões:");
            AddLabel(690, 378, 1153, @"Cabelo:");
            AddLabel(694, 512, 1153, @"Roupa:");
            AddLabel(320, 533, 1153, @"");
            AddButton(318, 149, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 170, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 193, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 215, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 282, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 303, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 326, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 348, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(450, 150, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(450, 172, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddHtml(604, 129, 259, 100, @"", (bool)false, (bool)false);
            AddHtml(605, 266, 259, 100, @"", (bool)false, (bool)false);
            AddButton(450, 282, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(450, 303, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 404, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 423, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 443, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 463, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 483, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(422, 407, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(422, 428, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(398, 577, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(398, 557, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 502, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(340, 401, 1153, @"Tailoring");
            AddLabel(340, 419, 1153, @"Blacksmithing");
            AddLabel(340, 440, 1153, @"Carpentry");
            AddLabel(340, 461, 1153, @"Alchemy");
            AddLabel(340, 481, 1153, @"Tinkering");
            AddLabel(444, 404, 1153, @"Bowcrafting");
            AddLabel(445, 426, 1153, @"Inscription");
            AddLabel(338, 574, 1153, @"Fishing");
            AddLabel(340, 498, 1153, @"Cooking");
            AddButton(318, 577, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(318, 557, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(339, 554, 1153, @"Mining");
            AddLabel(420, 575, 1153, @"Lumberjacking");
            AddLabel(420, 556, 1153, @"Farming");
            AddButton(422, 449, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(444, 445, 1153, @"Cartography");
            AddButton(398, 538, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(420, 536, 1153, @"Arms Lore");
            AddButton(318, 537, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(422, 469, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(444, 466, 1153, @"Jewel Craftfing");
            AddLabel(339, 534, 1153, @"Skinning");
            AddLabel(339, 146, 1153, @"Amaldiçoado");
            AddLabel(339, 167, 1153, @"Asmático");
            AddLabel(339, 190, 1153, @"Endividado");
            AddLabel(339, 213, 1153, @"Bairrista");
            AddLabel(472, 147, 1153, @"Baixinho");
            AddLabel(472, 171, 1153, @"Mudo");
            AddLabel(339, 279, 1153, @"Hipermnésia");
            AddLabel(339, 300, 1153, @"Sadismo");
            AddLabel(339, 323, 1153, @"Herdeiro");
            AddLabel(339, 344, 1153, @"Imunidade de Ferro");
            AddLabel(472, 278, 1153, @"Conhecedor");
            AddLabel(472, 301, 1153, @"Mingal Garantido");
            AddButton(690, 400, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(690, 420, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(690, 440, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(690, 460, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(690, 480, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(733, 400, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(733, 420, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(733, 440, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(733, 460, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(733, 480, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(780, 400, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(780, 420, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(780, 440, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(780, 460, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(780, 480, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(827, 400, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(827, 420, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(827, 440, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(827, 460, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(827, 480, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(713, 397, 1153, @"1");
            AddLabel(713, 417, 1153, @"2");
            AddLabel(713, 437, 1153, @"3");
            AddLabel(713, 457, 1153, @"4");
            AddLabel(713, 477, 1153, @"5");
            AddLabel(757, 397, 1153, @"6");
            AddLabel(757, 417, 1153, @"7");
            AddLabel(757, 437, 1153, @"8");
            AddLabel(757, 457, 1153, @"9");
            AddLabel(757, 477, 1153, @"10");
            AddLabel(804, 397, 1153, @"11");
            AddLabel(804, 417, 1153, @"12");
            AddLabel(804, 437, 1153, @"13");
            AddLabel(804, 457, 1153, @"14");
            AddLabel(804, 477, 1153, @"15");
            AddLabel(850, 397, 1153, @"16");
            AddLabel(850, 417, 1153, @"17");
            AddLabel(850, 437, 1153, @"18");
            AddLabel(850, 457, 1153, @"19");
            AddLabel(850, 477, 1153, @"20");
            AddButton(691, 537, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(691, 558, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddButton(691, 579, 40308, 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(714, 535, 1153, @"Vestimenta tipo 1");
            AddLabel(714, 555, 1153, @"Vestimenta tipo 2");
            AddLabel(714, 577, 1153, @"Vestimenta tipo 3");
            AddImage(521, 358, 52559);
            AddImage(521, 362, 53144);
            AddImage(521, 363, 53220);
            AddImage(521, 357, 53505);
            AddButton(838, 573, 4005, 4007, 0, GumpButtonType.Reply, 0);

        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
            caller = sender.Mobile;

            /*
                        detailsAInteirosEscolhidos[1] = info.Switches[0]; //idade
                        detailsAStringsEscolhidos[0] = info.TextEntries[0].Text; //nome
                        detailsAStringsEscolhidos[1] = info.TextEntries[1].Text; //descrição física

                        switch (info.ButtonID)
                        {
                            case 5:
                                {
                                    Console.WriteLine("feminino");
                                    detailsAInteirosEscolhidos[0] = 5;
                                    //Ajusta imagem quando mudou o sexo do personagem
                                    if (detailsAInteirosEscolhidos[5] > 40679 || detailsAInteirosEscolhidos[5] < 40519)
                                    {
                                        detailsAInteirosEscolhidos[5] = 40519;
                                    }                        
                                    break;
                                }
                            case 6:
                                {
                                    Console.WriteLine("masculino");
                                    detailsAInteirosEscolhidos[0] = 6;
                                    //Ajusta imagem quando mudou o sexo do personagem
                                    if (detailsAInteirosEscolhidos[5] > 40518 || detailsAInteirosEscolhidos[5] < 40359)
                                    {
                                        detailsAInteirosEscolhidos[5] = 40359;
                                    }
                                    break;
                                }
                            case -10:
                                {
                                    Console.WriteLine("-STR");
                                    if (detailsAInteirosEscolhidos[2] > 10)
                                    {
                                        detailsAInteirosEscolhidos[2]--;
                                    }
                                    break;
                                }
                            case 10:
                                {
                                    Console.WriteLine("+STR");
                                    if (detailsAInteirosEscolhidos[2] + detailsAInteirosEscolhidos[3] + detailsAInteirosEscolhidos[4] < atributosMax) //TODO: Sinalizar no gump quanto ja foi utilizado
                                    {
                                        detailsAInteirosEscolhidos[2]++;
                                    }
                                    break;
                                }
                            case -20:
                                {
                                    Console.WriteLine("-DEX");
                                    if (detailsAInteirosEscolhidos[3] > 10)
                                    {
                                        detailsAInteirosEscolhidos[3]--;
                                    }
                                    break;

                                }
                            case 20:
                                {
                                    Console.WriteLine("+DEX");
                                    if (detailsAInteirosEscolhidos[2] + detailsAInteirosEscolhidos[3] + detailsAInteirosEscolhidos[4] < atributosMax) //TODO: Sinalizar no gump quanto ja foi utilizado
                                    {
                                        detailsAInteirosEscolhidos[3]++;
                                    }
                                    break;

                                }
                            case -30:
                                {
                                    Console.WriteLine("-INT");
                                    if (detailsAInteirosEscolhidos[4] > 10)
                                    {
                                        detailsAInteirosEscolhidos[4]--;
                                    }
                                    break;

                                }
                            case 30:
                                {
                                    Console.WriteLine("+INT");
                                    if (detailsAInteirosEscolhidos[2] + detailsAInteirosEscolhidos[3] + detailsAInteirosEscolhidos[4] < atributosMax) //TODO: Sinalizar no gump quanto ja foi utilizado
                                    {
                                        detailsAInteirosEscolhidos[4]++;
                                    }
                                    break;

                                }
                            case -110:
                                {
                                    //idImagem homem 40359 a 40518
                                    //idImagem mulher 40519 a 40679
                                    Console.WriteLine("-IMG");
                                    if (detailsAInteirosEscolhidos[0] == 5) //feminino
                                    {
                                        if(detailsAInteirosEscolhidos[5] > 40519)
                                        {
                                            detailsAInteirosEscolhidos[5]--;
                                        }
                                        else if (detailsAInteirosEscolhidos[5] == 40519)
                                        {
                                            detailsAInteirosEscolhidos[5] = 40679;
                                        }
                                    }
                                    else //masculino
                                    {
                                        if (detailsAInteirosEscolhidos[5] > 40359)
                                        {
                                            detailsAInteirosEscolhidos[5]--;
                                        }
                                        else if (detailsAInteirosEscolhidos[5] == 40359)
                                        {
                                            detailsAInteirosEscolhidos[5] = 40518;
                                        }
                                    }
                                    break;
                                }
                            case 110:
                                {
                                    //idImagem homem 40359 a 40518
                                    //idImagem mulher 40519 a 40679
                                    Console.WriteLine("+IMG");
                                    if (detailsAInteirosEscolhidos[0] == 5) //feminino
                                    {
                                        if (detailsAInteirosEscolhidos[5] < 40679)
                                        {
                                            detailsAInteirosEscolhidos[5]++;
                                        }
                                        else if (detailsAInteirosEscolhidos[5] == 40679)
                                        {
                                            detailsAInteirosEscolhidos[5] = 40519;
                                        }
                                    }
                                    else //masculino
                                    {
                                        if (detailsAInteirosEscolhidos[5] < 40518)
                                        {
                                            detailsAInteirosEscolhidos[5]++;
                                        }
                                        else if (detailsAInteirosEscolhidos[5] == 40518)
                                        {
                                            detailsAInteirosEscolhidos[5] = 40359;
                                        }
                                    }
                                    break;
                                }
                            case -999:
                                {
                                    Console.WriteLine("Voltar para Classe");
                                    caller.SendGump(new ClassSelectionGump(raceEscolhida, classeEscolhida, skillsEscolhidas));
                                    return;
                                }
                            case 999:
                                {
                                    Console.WriteLine("Avançar para Detalhes B");
                                    //Adicionar aqui a verificação de se tudo está ok para avançar
                                    //caller.SendGump(new CharDetailsBGump(raceEscolhida, classeEscolhida, skillsEscolhidas, List<int> detailAint, List<string> detailAstr, null, null);
                                    return;
                                }
                            default:
                                {
                                    Console.WriteLine("Char Detail A");
                                    break;
                                }
                        }
            */
            caller.SendGump(new CharDetailsAGump(raceEscolhida, classeEscolhida, skillsEscolhidas, detailsAInteirosEscolhidos, detailsAStringsEscolhidos));
            return;
        }
    }
}
