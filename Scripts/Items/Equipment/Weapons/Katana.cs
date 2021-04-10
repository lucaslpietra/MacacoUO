using System;
using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefBlacksmithy), typeof(GargishKatana))]
    [FlipableAttribute(0x13FF, 0x13FE)]
    public class Katana : BaseSword
    {
        [Constructable]
        public Katana()
            : base(0x13FF)
        {
            this.Weight = 6.0;
            Name = "Katana";
        }

        public Katana(Serial serial)
            : base(serial)
        {
            Name = "Katana";
        }

        public override Habilidade PrimaryAbility
        {
            get
            {
                return Habilidade.DoubleStrike;
            }
        }
        public override Habilidade SecondaryAbility
        {
            get
            {
                return Habilidade.ArmorIgnore;
            }
        }
        public override int AosStrengthReq
        {
            get
            {
                return 25;
            }
        }
        public override int AosMinDamage
        {
            get
            {
                return 10;
            }
        }
        public override int AosMaxDamage
        {
            get
            {
                return 14;
            }
        }
        public override int AosSpeed
        {
            get
            {
                return 46;
            }
        }
        public override float MlSpeed
        {
            get
            {
                return 2.50f;
            }
        }
        public override int OldStrengthReq
        {
            get
            {
                return 10;
            }
        }
        public override int OldMinDamage
        {
            get
            {
                return 9;
            }
        }
        public override int OldMaxDamage
        {
            get
            {
                return 21;
            }
        }
        public override int OldSpeed
        {
            get
            {
                return 56;
            }
        }
        public override int DefHitSound
        {
            get
            {
                return 0x23B;
            }
        }
        public override int DefMissSound
        {
            get
            {
                return 0x23A;
            }
        }
        public override int InitMinHits
        {
            get
            {
                return 31;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 90;
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
