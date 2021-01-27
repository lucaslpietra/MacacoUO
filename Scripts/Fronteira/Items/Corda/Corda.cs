
using Server.Fronteira.Items.Corda;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;
using System;
using System.Collections.Generic;

namespace Server.Items
{
    // Item principal da corda
    public class Rope : Item
    {
        [Constructable]
        public Rope() : base(0x14F8)
        {
            Name = "Corda";
            Weight = 6;
        }

        public Rope(Serial s) : base(s)
        {

        }

        public static int ITEMID_PRESO = 0x268C;

        public PlayerMobile QuemPrendeu;

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add("Permite amarrar jogadores desmaiados");
        }

        public void Refresh()
        {

        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.RootParent != from)
            {
                from.SendMessage("Precisa estar em sua mochila");
                return;
            }
            from.SendMessage("Selecione o que deseja amarrar");
            from.Target = new AlvoDaCorda(this);
        }

        public static bool Preso(PlayerMobile p)
        {
            return _presos.ContainsKey(p.Serial);
        }

        private static Dictionary<Serial, CordaAmarrada> _presos = new Dictionary<Serial, CordaAmarrada>();

        public void TrySolta(object o)
        {
            var p = o as PlayerMobile;

            if(p != null)
            {
                if (Preso(p))
                {
                    if (p.Alive)
                    {
                        p.OverheadMessage("* se soltou *");
                        p.SendMessage("Voce se soltou");
                    }
                    p.Frozen = false;
                }
                if(_presos.ContainsKey(p.Serial))
                {
                    var i = _presos[p.Serial];
                    if (i != null)
                        i.Delete();
                    _presos.Remove(p.Serial);
                    var corda = new Rope();
                    corda.MoveToWorld(p.Location, p.Map);
                }
            } else if(o is Item)
            {
                var ii = (Item)o;
                var i = _presos[ii.Serial];
                if (i != null)
                    i.Delete();
                _presos.Remove(ii.Serial);
                var corda = new Rope();
                corda.MoveToWorld(ii.Location, ii.Map);
            }
          
        }

        private Timer t;

        public void Prende(object o)
        {
            PlayerMobile p = o as PlayerMobile;
            if (p != null && p.Mounted)
            {
                return;
            }

            if (p != null)
            {
                if (p.Alive)
                {
                    if (Preso(p))
                    {
                        p.OverheadMessage("* apertado *");
                        p.SendMessage("Sua corda foi apertada.");
                    }
                    else
                    {
                        p.OverheadMessage("* amarrado *");
                        p.SendMessage("Voce foi amarrado.");
                    }
                }
                var tempo = TimeSpan.FromMinutes(26 - p.Str / 4);
                p.Freeze(tempo);
                if (t != null && t.Running)
                    t.Stop();

                t = Timer.DelayCall(tempo - TimeSpan.FromMinutes(0.5), () =>
                {
                    TrySolta(p);
                });
            }

            var cordaAmarrada = new CordaAmarrada(o, this);
            if (p != null)
            {
                cordaAmarrada.MoveToWorld(new Point3D(p.Location.X, p.Location.Y, p.Location.Z + 7), p.Map);
                _presos.Add(p.Serial, cordaAmarrada);
            }
            else if (o is Item)
            {
                var i = (Item)o;
                var loc = i.Location;
                cordaAmarrada.MoveToWorld(new Point3D(loc.X, loc.Y, loc.Z + 2), i.Map);
                _presos.Add(i.Serial, cordaAmarrada);
            }
            
            this.Delete();
        }

        // Alvo de qnd usa a corda
        private class AlvoDaCorda : Target
        {
            private Rope corda;

            public AlvoDaCorda(Rope corda) : base(2, false, TargetFlags.None)
            {
                this.corda = corda;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                var pl = targeted as PlayerMobile;
                if (pl != null)
                {
                    if (!pl.Alive)
                        return;

                    corda.QuemPrendeu = from as PlayerMobile;
                    from.SendMessage("Tentando gentilmente amarrar " + pl.Name);
                    from.OverheadMessage("* estende uma corda *");
                    pl.SendGump(new ConfirmaPreso(corda));
                    return;
                }
                var item = targeted as IArrastavel;
                if (item != null)
                {
                    corda.QuemPrendeu = from as PlayerMobile;
                    corda.Prende(item);
                    from.OverheadMessage("* amarrou *");
                }
            }
        }

        public class ConfirmaPreso : BaseConfirmGump
        {
            public PlayerMobile prendedor;
            private Rope corda;

            public override string LabelString { get { return "Deseja se render e ser amarrado ?"; } }
            public override string TitleString
            {
                get
                {
                    return "Rendicao";
                }
            }

            public ConfirmaPreso(Rope c)
            {
                this.corda = c;
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
            }

            public override void Confirm(Mobile from)
            {
                if (from.GetDistance(corda.QuemPrendeu) > 2)
                {
                    from.SendMessage("Muito longe");
                    corda.QuemPrendeu.SendMessage("Muito longe");
                    return;
                }
                if (from.Mounted)
                {
                    corda.QuemPrendeu.SendMessage("Voce nao consegue amarrar alguem montado");
                    return;
                }
                corda.Prende((PlayerMobile)from);
            }

            public override void Refuse(Mobile from)
            {
                from.OverheadMessage("* nega *");
                from.SendMessage("Voce recusou a ser amarrado(a)");
                corda.QuemPrendeu.SendMessage(from.Name + " se recusou a ser amarrado");
            }
        }
    }



}
