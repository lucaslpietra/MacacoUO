using System;

namespace Server.Items
{
    public class FarmableOnion : BaseFarmable
    {
        [Constructable]
        public FarmableOnion()
            : base(GetCropID())
        {
            Name = "Planta de Cebola";
        }

        public FarmableOnion(Serial serial)
            : base(serial)
        {
        }

        public static int GetCropID()
        {
            return 3183;
        }

        public override Item GetCropObject()
        {
            Onion onion = new Onion();

            onion.ItemID = Utility.Random(3181, 2);

            return onion;
        }

        public override int GetPickedID()
        {
            return 3254;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

        }
    }
}
