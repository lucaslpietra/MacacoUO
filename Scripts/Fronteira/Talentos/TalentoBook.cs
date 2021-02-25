using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Fronteira.Talentos
{
    public class TalentoBook : Item
    {
        public TalentoBook(Serial s) : base(s) { }
        /*
        [Constructable]
        public TalentoBook() : base(0x225A)
        {
            Name = "Livro de Especializacao";
        }

        public TalentoBook(Serial s) : base(s) { }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add("Use para ganhar +1 talento");
        }

        public override void OnDoubleClick(Mobile from)
        {

            if (!this.IsChildOf(from.Backpack) && this.Parent != from)
            {
                from.SendMessage("Precisa estar em sua mochila");
                return;
            }

            if(!from.RP)
            {
                from.SendMessage("Voce nao pode usar isto");
                return;
            }

            var pl = from as PlayerMobile;
            if (pl == null)
                return;

            if (from.HasGump(typeof(GumpLivroTalento)))
                from.CloseGump(typeof(GumpLivroTalento));

            from.SendGump(new GumpLivroTalento(pl, this));
        }
        */

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
