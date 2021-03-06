using System;

namespace Server.Items
{
    public class RecarosRiposte : WarFork
	{
		public override bool IsArtifact { get { return true; } }
        [Constructable]
        public RecarosRiposte()
        {
            // LootType = LootType.Blessed;
            // Attributes.AttackChance = 5;
            // Attributes.WeaponSpeed = 10;
            // Attributes.WeaponDamage = 25;
            Resource = CraftResource.Cobre;
        }

        public RecarosRiposte(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1078195;
            }
        }// Recaro's Riposte
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
