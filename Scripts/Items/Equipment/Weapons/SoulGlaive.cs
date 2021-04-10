using System;

namespace Server.Items
{
    public class SoulGlaive : BaseThrown
    {
        [Constructable]
        public SoulGlaive()
            : base(0x090A)
        {
           Weight = 8.0;
           Layer = Layer.OneHanded;
        }

        public SoulGlaive(Serial serial)
            : base(serial)
        {
        }

        public override int MinThrowRange { get { return 8; } }

        public override Habilidade PrimaryAbility
        {
            get
            {
                return Habilidade.ArmorIgnore;
            }
        }
        public override Habilidade SecondaryAbility
        {
            get
            {
                return Habilidade.MortalStrike;
            }
        }
        public override int AosStrengthReq
        {
            get
            {
                return 60;
            }
        }
        public override int AosMinDamage
        {
            get
            {
                return 16;
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
                return 25;
            }
        }
        public override float MlSpeed
        {
            get
            {
                return 4.00f;
            }
        }
        public override int OldStrengthReq
        {
            get
            {
                return 20;
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
                return 41;
            }
        }
        public override int OldSpeed
        {
            get
            {
                return 20;
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
                return 65;
            }
        }
        
        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }


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
