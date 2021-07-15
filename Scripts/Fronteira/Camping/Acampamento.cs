using Server.Fronteira.Addons;
using Server.Items;
using Server.Mobiles;
using Server.Ziden;
using System.Collections.Generic;
using System.Linq;

namespace Server.Multis
{
    public class Acampamento : BaseCamp
    {
        public static Dictionary<string, Acampamento> Points = new Dictionary<string , Acampamento>();

        private PlacaAcampamento Placa;

        private string Nome;

        [Constructable]
        public Acampamento(string nome)
            : base(0x1F4)
        {
            this.Nome = nome;
            this.RestrictDecay = true;
        }

        public void Renomeia(string nome)
        {
            Shard.Debug("Renomando para " + nome);
            if(this.Placa.NomeAcampamento != null)
                Points.Remove(this.Placa.NomeAcampamento);
            if(nome != null)
                Points.Add(nome, this);
        }

        public Acampamento(Serial serial)
            : base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();
            Points.Remove(Placa.NomeAcampamento);
        }

        public override void OnEnter(Mobile m)
        {
            if(!m.IsCooldown("msgcamp"))
            {
                m.SetCooldown("msgcamp");
                m.PrivateOverheadMessage("* confortavel *");
                m.SendMessage(78, "Jogadores podem acampar proximo de campings usando a skill Camping para salvar o local para facil locomocao");
            }
        }


        public static void Acampa(PlayerMobile m)
        {
            Acampamento camp = null;
            double dist = int.MaxValue;

            foreach(var point in Points.Values)
            {
                if(point.Map == m.Map)
                {
                    var d = point.GetDistance(m);
                    if(d < dist && d < 50)
                    {
                        dist = d;
                        camp = point;
                    }
                }
            }

            if(camp != null)
            {
                var discobertas = m.CampfireLocations.Split(';').ToList();
                if (discobertas.Contains(camp.Placa.NomeAcampamento))
                {
                    return;
                }
                m.CampfireLocations += camp.Placa.NomeAcampamento + ";";
                m.Emote("Local de Camping Descoberto: " + camp.Placa.NomeAcampamento);
                m.SendMessage(78, "Clique duas vezes em uma fogueira segura para se teleportar a este acampamento");
            }
        }

        public override void AddComponents()
        {
            BaseDoor west, east;

            this.AddItem(west = new LightWoodGate(DoorFacing.WestCW), -4, 4, 7);
            this.AddItem(east = new LightWoodGate(DoorFacing.EastCCW), -3, 4, 7);

            west.Link = east;
            east.Link = west;
            Placa = new PlacaAcampamento(SignType.TradersGuild, SignFacing.West);
            Placa.Camp = this;
            this.AddItem(Placa, -5, 5, -4);

            this.AddItem(new Anvil(), -1, 0, 0);
            this.AddItem(new Forge(), -1, -1, 0);
            this.AddItem(new BankChest(), -1, -3, 0);

            Placa.NomeAcampamento = this.Nome;
            var h = new Healer();
            h.Name = "Curandeiro Campista";
            var b = new Provisioner();
            b.Name = "Campista";
            this.AddMobile(h, -4, 3, 7);
            this.AddMobile(b, 4, -2, 0);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(Placa);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            Placa = reader.ReadItem<PlacaAcampamento>();
          
            Placa.Camp = this;
            if(Placa.Name != null)
                Points.Add(Placa.Name, this);
            this.RestrictDecay = true;
        }
    }
}
