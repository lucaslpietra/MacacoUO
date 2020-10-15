using System;
using Server;
using Server.Ziden;

namespace Server.Items
{
    public class WorldMap : ZMapItem
    {
        [Constructable]
        public WorldMap() : base(-1)
        {
            SetDisplay(0, 0);
        }

        public override void CraftInit(Mobile from)
        {
            // Unlike the others, world map is not based on crafted location

            double skillValue = from.Skills[SkillName.Cartography].Value;
            int x20 = (int)(skillValue * 20);
            int size = 25 + (int)(skillValue * 6.6);

            if (size < 200)
                size = 200;
            else if (size > 400)
                size = 400;

            SetDisplay(1344, 1600);
        }

        public override void SetDisplay(int x, int y)
        {
            mMapCutout = ZMapList.WorldMapCutout;
        }
        public override string DefaultName { get { return "Mapa Mundi"; } } // world map

        public WorldMap(Serial serial) : base(serial)
        {
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
