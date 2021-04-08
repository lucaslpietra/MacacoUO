using Server.Engines.XmlSpawner2;
using Server.Items;
using Server.Mobiles;

namespace Server.Fronteira.QualidadeDefeitos.ListaQualidades
{
    public class MingauGarantidoQualidade
    {
        public static void Initialize()
        {
            //TODO Adicionar evento de finalizar criacao do char
            //EventSink.FinalizadaCriacaoChar += new FinalizadaCriacaoCharEventHandler(OnCharCriado);
        }

         // private static void OnCharCriado(FinalizadaCriacaoCharEventHandler e)
         // {
         //     if (e.Mobile == null)
         //         return;
         //
         //     if (!e.Mobile.Player)
         //         return;
         //
         //     PlayerMobile playerMobile = e.Mobile as PlayerMobile;
         //     QualidadesXmlAttachment qualidadesXmlAttachment =
         //         XmlAttach.FindAttachment(playerMobile, typeof(QualidadesXmlAttachment)) as QualidadesXmlAttachment;
         //
         //     //Comeca com um mingau com 200 usos
         //     if (qualidadesXmlAttachment != null && qualidadesXmlAttachment.Qualidades.TemQualidade(Qualidade.MingauGarantido))
         //     {
         //       // TODO Adicionar o mingau
         //       BreadLoaf loaf = new BreadLoaf(20);
         //       playerMobile.Backpack.AddItem(loaf);
         //     }
         // }
    }
}
