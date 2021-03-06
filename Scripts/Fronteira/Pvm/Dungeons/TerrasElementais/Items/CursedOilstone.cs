using Server;
using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Items
{
    public class CursedOilstone : Item
    {
        public override int LabelNumber { get { return 1151810; } } // Cursed Oilstone

        [Constructable]
        public CursedOilstone()
            : this(1)
        {

        }

        [Constructable]
        public CursedOilstone(int amount) : base(0x0F8B)
        {
            this.Weight = 1;
            Name = "Pedra Elemental do Vento";
            Stackable = true;
            Amount = amount;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            else if (from.Backpack.GetAmount(typeof(QuartzGrit)) == 0)
                from.SendMessage("Voce precisa da Pedra Elemental de Quartzo");
            else if (from.Backpack.GetAmount(typeof(CorrosiveAsh)) == 0)
                from.SendMessage("Voce precisa da Pedra Elemental da Chama");
            else
            {
                from.Backpack.ConsumeTotal(new Type[] { typeof(QuartzGrit), typeof(CorrosiveAsh) },
                                           new int[] { 1, 1 });

                this.Consume();

                from.AddToBackpack(new PedraElementalSuprema());
                from.SendLocalizedMessage(1151812); // You have managed to form the items into a rancid smelling, crag covered, hardened lump. In a moment of prescience, you realize what it must be named. The Whetstone of Enervation!
            }
        }

        public CursedOilstone(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
