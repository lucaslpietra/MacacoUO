using System;
using Server;
using Server.Scripts.Custom.RelporMap;

namespace Server.Items
{
    public class CityMap : ZMapItem
    {
        [Constructable]
        public CityMap() : base(-1)
        {
            SetDisplay(0, 0);
        }

        public override void CraftInit(Mobile from)
        {
            double skillValue = from.Skills[SkillName.Cartography].Value;
            int dist = 64 + (int)(skillValue * 4);

            if (dist < 200)
                dist = 200;

            int size = 32 + (int)(skillValue * 2);

            if (size < 200)
                size = 200;
            else if (size > 400)
                size = 400;

            SetDisplay(from.X, from.Y);
        }

        public override void SetDisplay(int x, int y)
        {
            int i = 0;
            foreach (MapCutout cutout in RelPorMapList.CityMapCutouts)
            {
                if (cutout.Rect.Contains(new Point2D(x, y)))
                {
                    break;
                }
                i++;
            }
            if (i < RelPorMapList.CityMapCutouts.Length)
            {
                mMapCutout = RelPorMapList.CityMapCutouts[i];
                Bounds = mMapCutout.Rect;
            }
            else
            {
                mMapCutout = RelPorMapList.CityMapCutouts[8]; //galven
                Bounds = mMapCutout.Rect;
            }
        }

        public override int LabelNumber { get { return 1015231; } } // city map

        public CityMap(Serial serial) : base(serial)
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
