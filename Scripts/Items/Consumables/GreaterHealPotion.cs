using System;

namespace Server.Items
{
    public class GreaterHealPotion : BaseHealPotion
    {
		public override string DefaultName
        {
            get { return "Poção de Vida Forte"; }            
        }
		
        [Constructable]
        public GreaterHealPotion()
            : base(PotionEffect.VidaForte)
        {
            FiverRatio = true;
        }

        public GreaterHealPotion(Serial serial)
            : base(serial)
        {
            FiverRatio = true;
        }



        public override int MinHeal
        {
            get
            {
                return 10;
            }
        }

        public override int MaxHeal
        {
            get
            {
                return 70;
            }
        }

        public override double Delay
        {
            get
            {
                return 10.0;
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
