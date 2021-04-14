using System;
using Server.Engines.XmlSpawner2;
using Server.Ethics;
using Server.Mobiles;

namespace Server.Fronteira.QualidadeDefeitos.ListaQualidades
{
    public class SadismoQualidade
    {
        public static void Initialize()
        {
            EventSink.OnAttack += new AttackEventHandler(OnOnAttackAction);
        }

        private static void OnOnAttackAction(AttackEventArgs e)
        {
            Mobile attacker = e.Attacker;
            Mobile defender = e.Defender;

            if (attacker == null || defender == null)
                return;

            if (!attacker.Player || !defender.Player)
                return;

            if (attacker is Player)
            {
                PlayerMobile playerMobile = attacker as PlayerMobile;
                QualidadesXmlAttachment qualidadesXmlAttachment =
                    XmlAttach.FindAttachment(playerMobile, typeof(QualidadesXmlAttachment)) as QualidadesXmlAttachment;
                if (qualidadesXmlAttachment != null && qualidadesXmlAttachment.Qualidades.TemQualidade(Qualidade.Sadismo))
                {
                    //TODO Fazer recuperar stam mais rapido
                }
            }
        }
    }
}
