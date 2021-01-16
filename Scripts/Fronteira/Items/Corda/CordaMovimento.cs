using Server.Items;
using Server.Mobiles;
using System;

namespace Server.Fronteira.Items.Corda
{
    public class CordaMovimento
    {
        public static void Initialize()
        {
            EventSink.Movement += Move;
        }

        public static void Move(MovementEventArgs e)
        {
            var pl = e.Mobile as PlayerMobile;
            if (pl != null && pl.Arrastando != null)
            {
                if (pl.Stam > 3)
                {
                    pl.Stam -= 3;
                    if(pl.Stam < 10 && !pl.IsCooldown("ofega"))
                    {
                        pl.SetCooldown("ofega", TimeSpan.FromSeconds(4));
                        pl.OverheadMessage("* ofegante *");
                    } 
                }
                else
                {
                    pl.SendMessage("Voce esta muito cansado para arrastar esta pessoa");
                    pl.Arrastando = null;  
                    CordaAmarrada.Arrastando.Remove(pl);
                    pl.Freeze(TimeSpan.FromSeconds(1));
                    return;
                }

                var corda = CordaAmarrada.Arrastando[pl];
                pl.Arrastando.MoveToWorld(pl.Location, pl.Map);
                corda.MoveToWorld(new Point3D(pl.Location.X, pl.Location.Y, pl.Location.Z + 7), pl.Map);
                if (!pl.IsCooldown("arrastarmsg"))
                {
                    pl.SetCooldown("arrastarmsg", TimeSpan.FromSeconds(4));
                    pl.OverheadMessage("* arrastou *");
                }

                pl.Direction = pl.GetDirectionTo(pl.Arrastando);
                pl.Freeze(TimeSpan.FromSeconds(0.6));
                pl.Arrastando.Direction = pl.Direction;
            }
        }
    }
}
