using System;
using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefBlacksmithy), typeof(GargishBardiche))]
    [FlipableAttribute(0xF4D, 0xF4E)]
    public class Bardiche : BasePoleArm
    {
        [Constructable]
        public Bardiche()
            : base(0xF4D)
        {
            this.Weight = 7.0;
            this.Name = "Bardiche";
        }

        public Bardiche(Serial serial)
            : base(serial)
        {
        }

        public override Habilidade PrimaryAbility
        {
            get
            {
                return Habilidade.ParalyzingBlow;
            }
        }
        public override Habilidade SecondaryAbility
        {
            get
            {
                return Habilidade.Dismount;
            }
        }
        public override int AosStrengthReq
        {
            get
            {
                return 45;
            }
        }
        public override int AosMinDamage
        {
            get
            {
                return 17;
            }
        }
        public override int AosMaxDamage
        {
            get
            {
                return 20;
            }
        }
        public override int AosSpeed
        {
            get
            {
                return 28;
            }
        }
        public override float MlSpeed
        {
            get
            {
                return 3.75f;
            }
        }
        public override int DefMaxRange
        {
            get
            {
                return 2;
            }
        }
        public override int OldStrengthReq
        {
            get
            {
                return 40;
            }
        }
        public override int OldMinDamage
        {
            get
            {
                return 5;
            }
        }
        public override int OldMaxDamage
        {
            get
            {
                return 43;
            }
        }
        public override int OldSpeed
        {
            get
            {
                return 26;
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
                return 100;
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