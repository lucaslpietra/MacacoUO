using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.Tutorial.WispGuia
{

    public class ObjetivoGuia
    {
        public Point3D LocalDungeon;
        public Point3D Local;
        public string FraseProgresso;
        public string FraseCompletar;
        public string FraseIniciar;
        public PassoTutorial Proximo;
        public bool PrecisaEvento = false;
        public Action<PlayerMobile> Completar;
    }

    public enum PassoTutorial
    {
        NADA,
        PEGAR_CAVALO,
        IR_BANCO,
        IR_FERREIRO,
        PEGAR_QUEST,
        MATAR_MAGO,
        VOLTAR_QUEST
    }

    public class Guia
    {
        public Dictionary<PassoTutorial, ObjetivoGuia> Objetivos = new Dictionary<PassoTutorial, ObjetivoGuia>();

        public Guia()
        {
            Objetivos.Add(PassoTutorial.PEGAR_CAVALO, new ObjetivoGuia() {
                Local = new Point3D(3526, 2577, 7),
                FraseIniciar = "Ei, o que acha de conseguir um cavalo ? Siga a seta no canto do mapa, eu arrumo um pra voce !",
                FraseProgresso = "Siga a setinha no canto da sua tela para chegar no Coxeiro.",
                FraseCompletar = "Tome aqui, um cavalo. Clique duas vezes para montar.",
                Completar = (pl) => {
                    var c = new Horse();
                    c.SetControlMaster(pl);
                    c.MoveToWorld(pl.Wisp.Location, pl.Wisp.Map);
                    c.AIObject.DoOrderCome();
                },
                Proximo = PassoTutorial.IR_FERREIRO
            });

            Objetivos.Add(PassoTutorial.IR_FERREIRO, new ObjetivoGuia()
            {
                Local = new Point3D(3469, 2540, 10),
                FraseIniciar = "Vamos agora ao ferreiro ! Fale 'Comprar' a ele para se equipar.",
                FraseProgresso = "Encontre o ferreiro e diga Comprar a ele para comprar items.",
                FraseCompletar = "Se voce tiver skills de trabalho, pode falar 'trabalho' para trabalhar pro NPC ou 'recompensas'. ",
                Completar = (pl) => {
                    pl.Backpack.DropItem(new Gold(300));
                    pl.PlaySound(0x2E6);
                },
                Proximo = PassoTutorial.IR_BANCO
            });

            Objetivos.Add(PassoTutorial.IR_BANCO, new ObjetivoGuia()
            {
                Local = new Point3D(3487, 2573, 21),
                FraseIniciar = "Quel tal ir ao banco guardar seu dinheiro ? Va ao banco e fale 'Banco' !.",
                FraseProgresso = "Va ao banco e fale 'Banco' para abrir seu banco.",
                PrecisaEvento = true,
                FraseCompletar = "Voce tambem pode fazer cheques falando 'cheque'. Coloquei um cheque em seu banco.",
                Completar = (pl) => {
                    pl.BankBox.DropItem(new BankCheck(300));
                },
                Proximo = PassoTutorial.PEGAR_QUEST
            });

            Objetivos.Add(PassoTutorial.PEGAR_QUEST, new ObjetivoGuia()
            {
                Local = new Point3D(3503, 2483, 26),
                PrecisaEvento = true,
                FraseIniciar = "Chega de papo, vamos agora para uma aventura !!",
                FraseProgresso = "Va a norte de Haven e encontre o Zeh Sapatinho.",
                FraseCompletar = "Otimo, agora vamos a dungeon",
                Proximo = PassoTutorial.MATAR_MAGO
            });

            Objetivos.Add(PassoTutorial.MATAR_MAGO, new ObjetivoGuia()
            {
                LocalDungeon = new Point3D(59, 1480, -28),
                PrecisaEvento = true,
                FraseIniciar = "Vamos la matar o mago safado que roubou o sapato ! Entre na dungeon",
                FraseProgresso = "Encontre o mago putrido na dungeon norte do Zeh",
                FraseCompletar = "Voce e muito bom ! Pegue o sapato e entregue agora",
                Proximo = PassoTutorial.MATAR_MAGO
            });
        }
    }
}
