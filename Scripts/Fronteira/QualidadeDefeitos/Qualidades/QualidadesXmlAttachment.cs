using System;
using Server.Engines.XmlSpawner2;
using Server.Items;
using Server.Mobiles;

namespace Server.Fronteira.QualidadeDefeitos
{
    public class QualidadesXmlAttachment : XmlAttachment
    {
        private Mobile _From = null;
        private Qualidades _Qualidades = null;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile From
        {
            get => _From;
            set => _From = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Qualidades Qualidades
        {
            get => _Qualidades;
            set => _Qualidades = value;
        }

        public override void OnWeaponHit(Mobile attacker, Mobile defender, BaseWeapon weapon, int damageGiven)
        {
            if (attacker == null || defender == null)
                return;

            if (!attacker.Player)
                return;

            PlayerMobile playerMobile = attacker as PlayerMobile;
            QualidadesXmlAttachment qualidadesXmlAttachment =
                XmlAttach.FindAttachment(playerMobile, typeof(QualidadesXmlAttachment)) as QualidadesXmlAttachment;
            if (qualidadesXmlAttachment != null && qualidadesXmlAttachment.Qualidades.TemQualidade(Qualidade.Sadismo))
            {
                //Recupera 30% da stamina mais rapido ao causar dano
                //TODO Verificar esse caso do regen, pq atual recupera no total
                attacker.Stam += Int32.Parse((attacker.Stam * 0.3).ToString());
            }

            base.OnWeaponHit(attacker, defender, weapon, damageGiven);
        }

        public override int OnArmorHit(Mobile attacker, Mobile defender, Item armor, BaseWeapon weapon, int damageGiven)
        {
            return base.OnArmorHit(attacker, defender, armor, weapon, damageGiven);
        }

        public QualidadesXmlAttachment(Mobile from, Qualidades qualidade)
        {
            From = from;
            Qualidades = qualidade;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            // version 0
            writer.Write((int) 0);
            writer.Write((Mobile) From);

            Qualidades.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            // version 0
            int version = reader.ReadInt();
            From = reader.ReadMobile();

            Qualidades.Deserialize(reader);
        }
    }
}
