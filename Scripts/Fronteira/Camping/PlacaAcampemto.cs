using Server.Items;
using Server.Multis;

namespace Server.Fronteira.Addons
{
    public class PlacaAcampamento : Sign
    {
        public Acampamento Camp;

        public PlacaAcampamento(SignType type, SignFacing facing) : base(type, facing) { }

        public PlacaAcampamento(Serial s) : base(s) { }

        public string NomeAcampamento
        {
            get { return Name; }
            set
            {
                if (Camp != null && value != null)
                {
                    Camp.Renomeia(value);
                }
                Name = value;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void OnDoubleClick(Mobile from)
        {
            base.OnDoubleClick(from);
            from.SendMessage("Este acampamento parece ser muito bom para repousar.");
        }

        public override void OnAfterDelete()
        {
            if (Camp != null && !Camp.Deleted)
            {
                Camp.Delete();
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            if (Camp != null && Name != null)
            {
                Camp.Renomeia(Name);
            }
        }
    }
}
