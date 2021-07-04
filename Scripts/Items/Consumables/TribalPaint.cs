using System;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class TribalPaint : Item
    {
        [Constructable]
        public TribalPaint()
            : base(0x9EC)
        {
            this.Hue = 2101;
            this.Weight = 2.0;
            this.Stackable = Core.ML;
        }

        public TribalPaint(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1040000;
            }
        }// savage kin paint
        public override void OnDoubleClick(Mobile from)
        {
            if (this.IsChildOf(from.Backpack))
            {
                if (Factions.Sigil.ExistsOn(from))
                {
                    from.SendMessage("Nessa forma nao neh..."); // You cannot disguise yourself while holding a sigil.
                }
                else if (!from.CanBeginAction(typeof(Spells.Fifth.IncognitoSpell)))
                {
                    from.SendMessage("Nessa forma nao neh..."); // You cannot disguise yourself while incognitoed.
                }
                else if (!from.CanBeginAction(typeof(Spells.Seventh.PolymorphSpell)))
                {
                    from.SendMessage("Nessa forma nao neh..."); // You cannot disguise yourself while polymorphed.
                }
                else if (TransformationSpellHelper.UnderTransformation(from))
                {
                    from.SendMessage("Nessa forma nao neh..."); // You cannot disguise yourself while polymorphed.
                }
                else if (Spells.Ninjitsu.AnimalForm.UnderTransformation(from))
                {
                    from.SendMessage("Nessa forma nao neh..."); // You cannot disguise yourself while in that form.
                }
                else if (from.IsBodyMod || from.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                {
                    from.SendMessage("Voce ja esta disfarcado"); // You are already disguised.
                }
                else
                {
                    from.BodyMod = (from.Female ? 184 : 183);
                    from.HueMod = 0;

                    if (from is PlayerMobile)
                        ((PlayerMobile)from).SavagePaintExpiration = TimeSpan.FromDays(7.0);

                    from.SendMessage("Voce agora tem as marcas tribais"); // You now bear the markings of the savage tribe.  Your body paint will last about a week or you can remove it with an oil cloth.
                    from.SendMessage("A tinta dura uma semana e pode ser removida com Pano com Oleo");
                    this.Consume();
                }
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
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