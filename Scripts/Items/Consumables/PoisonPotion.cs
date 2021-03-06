using System;

namespace Server.Items
{
    public class PoisonPotion : BasePoisonPotion
    {
        [Constructable]
        public PoisonPotion()
            : base(PotionEffect.Veneno)
        {
        }

        public PoisonPotion(Serial serial)
            : base(serial)
        {
        }

        public override Poison Poison
        {
            get
            {
                return Poison.Regular;
            }
        }
        public override double MinPoisoningSkill
        {
            get
            {
                return 50.0;
            }
        }
        public override double MaxPoisoningSkill
        {
            get
            {
                return 80.0;
            }
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
