using Server.Fronteira.Talentos;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System;

namespace Server.Gumps
{
    /*
   public class GumpLivroTalento : Server.Gumps.Gump
   {

       private Item book;

       private static Talento [] Talentos = Enum.GetValues(typeof(Talento)).CastToArray<Talento>();

       public GumpLivroTalento(PlayerMobile p, Item book) : base(0, 0)
       {
           this.book = book;
           this.Closable = true;
           this.Disposable = true;
           this.Dragable = true;
           this.Resizable = false;
           var page = 1;

           this.AddPage(0);
           this.AddImage(241, 335, 11058);

           for(var x=  0; x < Talentos.Length; x+=2)
           {
               this.AddPage(page);
               this.AddButton(563, 343, 2206, 2206, (int)Buttons.PaginaForward, GumpButtonType.Page, page+1);
               if(page > 0)
                   this.AddButton(284, 344, 2205, 2205, (int)Buttons.CopyofPaginaForward, GumpButtonType.Page, page-1);

               var t1 = (Talento)Talentos[x];
               var v1 = p.Talentos.GetNivel(t1);
               var def = DefTalentos.GetDef(t1);

               this.AddButton(336, 388, def.Icone, def.Icone, (int)t1+1, GumpButtonType.Reply, 0);
               this.AddHtml(291, 433, 146, 112, "Proximo Nivel:<br>"+def.Desc(v1), (bool)false, (bool)false);
               this.AddHtml(387, 404, 54, 20, v1+"/3", (bool)false, (bool)false);
               this.AddHtml(291, 367, 151, 20, def.Nome, (bool)false, (bool)false);

               if(Talentos.Length -1 >=  x + 1)
               {
                   var t2 = (Talento)Talentos[x+1];
                   var v2 = p.Talentos.GetNivel(t2);
                   var def2 = DefTalentos.GetDef(t2);
                   this.AddHtml(449, 434, 146, 110, "Proximo Nivel:<br>" +def2.Desc(v2), (bool)false, (bool)false);
                   this.AddButton(503, 390, def2.Icone, def2.Icone, (int)t2+1, GumpButtonType.Reply, 0);

                   this.AddHtml(451, 368, 145, 20, def2.Nome, (bool)false, (bool)false);

                   this.AddHtml(551, 403, 54, 20, v2+"/3", (bool)false, (bool)false);
               }
               page = page + 1;
           }



       }

       /*
        *      pl.SendMessage("Seu nivel no talento " + Talento.ToString() + " aumentou em 1");
           pl.SendMessage("Nivel em " + Talento.ToString() + ": " + nivel + "/3");
           pl.SendMessage("Use .talentos para ver seus talentos");
           Effects.SendLocationParticles(EffectItem.Create(from.Location, from.Map, EffectItem.DefaultDuration), 0, 0, 0, 78, 0, 5060, 0);
           Effects.PlaySound(from.Location, from.Map, 0x243);

           Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(from.X - 6, from.Y - 6, from.Z + 15), from.Map), from, 0x36D4, 7, 0, false, true, 78, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
           Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(from.X - 4, from.Y - 6, from.Z + 15), from.Map), from, 0x36D4, 7, 0, false, true, 78, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
           Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(from.X - 6, from.Y - 4, from.Z + 15), from.Map), from, 0x36D4, 7, 0, false, true, 78, 0, 9502, 1, 0, (EffectLayer)255, 0x100);

           Effects.SendTargetParticles(from, 0x375A, 35, 90, 78, 0x00, 9502, (EffectLayer)255, 0x100);


       public override void OnResponse(NetState sender, RelayInfo info)
       {
           var from = sender.Mobile as PlayerMobile;
           if (info.ButtonID == 0)
               return;

           if(book==null)
           {
               from.SendMessage("Voce precisa de um livro de especializacao para aprender estes talentos");
               return;
           }

           var talentoID = info.ButtonID - 1;
           var talento = (Talento)talentoID;
           var nivel = from.Talentos.GetNivel(talento);
           if(nivel < 3)
           {
               nivel++;
               from.Talentos.SetNivel(talento, nivel);
               from.SendMessage("Seu nivel no talento " + talento.ToString() + " aumentou em 1");
               from.SendMessage("Nivel em " + talento.ToString() + ": " + nivel + "/3");
               from.SendMessage("Use .talentos para ver seus talentos");
               Effects.SendLocationParticles(EffectItem.Create(from.Location, from.Map, EffectItem.DefaultDuration), 0, 0, 0, 78, 0, 5060, 0);
               Effects.PlaySound(from.Location, from.Map, 0x243);

               Effects.SendMovingParticles(new Entity(0, new Point3D(from.X - 6, from.Y - 6, from.Z + 15), from.Map), from, 0x36D4, 7, 0, false, true, 78, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
               Effects.SendMovingParticles(new Entity(0, new Point3D(from.X - 4, from.Y - 6, from.Z + 15), from.Map), from, 0x36D4, 7, 0, false, true, 78, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
               Effects.SendMovingParticles(new Entity(0, new Point3D(from.X - 6, from.Y - 4, from.Z + 15), from.Map), from, 0x36D4, 7, 0, false, true, 78, 0, 9502, 1, 0, (EffectLayer)255, 0x100);

               Effects.SendTargetParticles(from, 0x375A, 35, 90, 78, 0x00, 9502, (EffectLayer)255, 0x100);
               book.Consume();
           } 
       }

       public enum Buttons
       {
           LeftButton,
           RightButton,
           PaginaForward,
           CopyofPaginaForward,
       }
   }
     */
}

