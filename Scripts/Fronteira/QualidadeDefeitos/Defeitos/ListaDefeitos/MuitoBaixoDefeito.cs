using System;
using Server.Engines.XmlSpawner2;
using Server.Mobiles;

namespace Server.Fronteira.QualidadeDefeitos.ListaDefeitos
{
    public class MuitoBaixoDefeito
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

            if (attacker is Ettin || attacker is BaseOgre || attacker is Titan) //TODO Vericar a existencia de outros gigantes
            {
                PlayerMobile playerMobile = defender as PlayerMobile;
                DefeitosXmlAttachment defeitosXmlAttachment =
                    XmlAttach.FindAttachment(playerMobile, typeof(DefeitosXmlAttachment)) as DefeitosXmlAttachment;
                if (defeitosXmlAttachment != null && defeitosXmlAttachment.Defeitos.TemDefeito(Defeito.MuitoBaixo))
                {
                    BaseCreature baseCreature = attacker as BaseCreature;
                    //Gigantes causam 30% a mais de dano
                    baseCreature.TempDamageBonus = (int) (((baseCreature.DamageMax + baseCreature.DamageMin) / 2) * 0.3);
                }
            }
        }
    }
}
