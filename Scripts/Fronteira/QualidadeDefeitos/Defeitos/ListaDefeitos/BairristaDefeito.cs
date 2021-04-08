using Server.Engines.XmlSpawner2;
using Server.Mobiles;

namespace Server.Fronteira.QualidadeDefeitos.ListaDefeitos
{
    public class BairristaDefeito
    {
        public static void Initialize()
        {
            EventSink.CheckEquipItem += new CheckEquipItemEventHandler(OnCheckEquipItem);
        }

        private static void OnCheckEquipItem(CheckEquipItemEventArgs e)
        {
            Mobile mobile = e.Mobile;
            Item item = e.Item;

            if (mobile == null || item == null)
                return;

            if (!mobile.Player)
                return;

            PlayerMobile playerMobile = mobile as PlayerMobile;
            DefeitosXmlAttachment defeitosXmlAttachment =
                XmlAttach.FindAttachment(playerMobile, typeof(DefeitosXmlAttachment)) as DefeitosXmlAttachment;
            if (defeitosXmlAttachment != null && defeitosXmlAttachment.Defeitos.TemDefeito(Defeito.Bairrista))
            {
                //TODO Finalizar quando tiver as propriedades de item por raca
                //So consegue equipar itens especiais da raca feitos pelo seu povo.
                // if (playerMobile.Race != item.race)
                // {
                //     playerMobile.SendMessage("Voce nao pode usar esse item.");
                //     playerMobile.AddToBackpack(item);
                // }
            }
        }
    }
}
