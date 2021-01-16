using Server.Gumps;
using Server.Mobiles;
using System;
using System.Collections.Generic;

namespace Server.Items
{
    public class CordaAmarrada : Item, IScissorable
    {
        public static Dictionary<PlayerMobile, CordaAmarrada> Arrastando = new Dictionary<PlayerMobile, CordaAmarrada>();

        private PlayerMobile Preso;
        private Rope Corda;
       
        public CordaAmarrada(PlayerMobile preso, Rope corda) : base(0x268C)
        {
            Corda = corda;
            Preso = preso;
            Movable = false;
            Name = "Corda Amarrada";
        }

        public CordaAmarrada(Serial s) : base(s) { }

        public override void OnDoubleClick(Mobile from)
        {
            var pl = from as PlayerMobile;
            from.SendGump(new GumpOpcoes("O que deseja fazer ?", (opt) =>
            {
                if(Shard.DebugEnabled)
                    Shard.Debug("OPT " + opt);

                if (opt == 0)
                {
                    from.SendMessage("Voce apertou a corda");
                    Corda.Refresh();
                }
                else if (opt == 1) 
                    Corta(pl, Preso);
                else if (opt == 2) 
                    Desamarra(pl, Preso);
                else if (opt == 3)
                    Arrasta(pl, Preso);
            }, 0x14F8, 0, "Apertar o no", "Cortar Corda", "Desamarrar", Arrastando.ContainsKey(pl) ? "Largar" : "Arrastar"));
        }

        public bool Scissor(Mobile from, Scissors scissors)
        {
            Corta(from as PlayerMobile, Preso, false);
            return true;
        }

        public void Arrasta(PlayerMobile from, PlayerMobile target)
        {
            if(from.Str < 90)
            {
                from.SendMessage("Voce nao e forte suficiente para arrastar alguem");
                return;
            }
        
            if(from.GetDistance(target) > 2)
            {
                from.SendMessage("Muito longe");
                return;
            }

            if(!Arrastando.ContainsKey(from))
            {
                Arrastando.Add(from, this);
                from.SendMessage("Voce agarrou o alvo e esta pronto para arrasta-lo");
                from.OverheadMessage("* segurou firme *");
                from.Arrastando = target;
            } else
            {
                Arrastando.Remove(from);
                from.SendMessage("Voce largou a pessoa");
                from.OverheadMessage("* largou *");
                from.Arrastando = null;
            } 
        }

        public void Desamarra(PlayerMobile from, PlayerMobile target)
        {
            Timer.DelayCall(TimeSpan.FromSeconds(3), () =>
            {
                DesamarraTick(0, from, target);
            });
        }

        private void DesamarraTick(int tick, PlayerMobile from, PlayerMobile target)
        {
            if (!from.Alive || !target.Alive)
                return;

            if (from.GetDistance(target) > 2)
                return;


            var chanceSucesso = from.Dex / 5 + tick * from.Str/10; 
            if(Utility.Random(100) < chanceSucesso)
            {
                Corda.TrySolta(target);
            } else
            {
                from.OverheadMessage("* tentando desamarrar *");
            }

            tick++;
            Timer.DelayCall(TimeSpan.FromSeconds(3), () =>
            {
                DesamarraTick(tick, from, target);
            });

        }

        public void Corta(PlayerMobile from, PlayerMobile target, bool checaArma = true)
        {
            if(checaArma)
            {
                BaseWeapon arma = from.Backpack.FindItemByType<BaseKnife>();
                if (arma == null)
                    arma = from.Backpack.FindItemByType<BaseAxe>();

                if (arma == null || !from.SmoothForceEquip(arma))
                {
                    from.SendMessage("Voce precisa de alguma arma cortante para cortar a corda");
                    return;
                }
            }
            from.OverheadMessage("* cortou a corda *");
            this.Delete();
            Corda.TrySolta(target);

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(Preso);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            Preso = reader.ReadMobile() as PlayerMobile;
            if (Preso.Frozen)
            {
                Preso.Frozen = false;
            }
            Delete();
        }

     
    }

}
