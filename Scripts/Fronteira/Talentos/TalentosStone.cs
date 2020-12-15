using System.Collections.Generic;

namespace Server.Fronteira.Talentos
{
    public class TalentosStone : Item
    {
        [Constructable]
        public TalentosStone(): base(0xED4) {
            this.Hue = 1154;
        }

        public TalentosStone(Serial s) { }

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
