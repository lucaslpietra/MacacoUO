using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Fronteira.Elementos
{

    public class StackofIngots : Item
    {
        [Constructable]
        public StackofIngots()
            : base(72789)
        {
            Name = "Estatua Elemental";
            Weight = 200;
        }

        public StackofIngots(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            base.OnDoubleClick(from);
            var pl = from as PlayerMobile;
            if (pl == null)
                return;

            from.SendMessage("Voce sente uma energia muito forte emandando desta estatua...");
            if(from.Skills.Total < 6000)
            {
                from.SendMessage("Voce ainda tem muito a aprender para poder compreender esta estatua. [Skills "+ from.Skills.Total + "/6000]");
                return;
            }
           
            if(pl.Nivel == 0)
            {
                pl.SendGump(new SemElementoGump());
            } else
            {
                pl.SendGump(new ElementosGump(pl));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}

