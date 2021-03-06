using System;

namespace Server.Items
{
    public class OrcishBow : Bow
    {
        public override int LabelNumber { get { return 1153778; } } // an orcish bow

        [Constructable]
        public OrcishBow()
        {
            Name = "Arco Orc";
            Hue = 1107;
            Attributes.BonusDex = 2;
            Attributes.WeaponDamage = 25;
            WeaponAttributes.DurabilityBonus = 70;
        }

        public OrcishBow(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
