using Server.Engines.XmlSpawner2;
using Server.Mobiles;

namespace Server.Fronteira.QualidadeDefeitos.ListaQualidades
{
    public class HerdeiroQualidade
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
        //     //Comeca o jogo com cavalo, com dinheiro e com um titulo aleatorio
        //     if (qualidadesXmlAttachment != null && qualidadesXmlAttachment.Qualidades.TemQualidade(Qualidade.Herdeiro))
        //     {
        //TODO Verificar valor do deposito do herdeiro
        //         Banker.Deposit(playerMobile, 200);
        //TODO Adicionar parte do titulo
        //         Horse horse = new Horse();
        //         horse.SetControlMaster(playerMobile);
        //         horse.MoveToWorld(playerMobile.Location, playerMobile.Map);
        //     }
        // }
    }
}
