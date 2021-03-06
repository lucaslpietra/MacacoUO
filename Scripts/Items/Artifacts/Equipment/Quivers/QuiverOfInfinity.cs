using System;

namespace Server.Items
{
    public class QuiverOfInfinity : BaseQuiver
	{
		public override bool IsArtifact { get { return true; } }
        [Constructable]
        public QuiverOfInfinity()
            : base(0x2B02)
        {
            LootType = LootType.Blessed;
            Weight = 8.0;
            WeightReduction = 30;
            LowerAmmoCost = 20;
            Attributes.DefendChance = 5;
        }

        public QuiverOfInfinity(Serial serial)
            : base(serial)
        {
        }

		public override bool CanAlter
		{
			get
			{
				return false;
			}
		}

        public override int LabelNumber
        {
            get
            {
                return 1075201;
            }
        }// Quiver of Infinity
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(2); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            if (version < 1 && this.DamageIncrease == 0)
                this.DamageIncrease = 10;

            if (version < 2 && this.Attributes.WeaponDamage == 10)
                this.Attributes.WeaponDamage = 0;
        }
    }
}