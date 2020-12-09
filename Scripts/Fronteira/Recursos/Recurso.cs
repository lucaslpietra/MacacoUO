using Server.Items;
using Server.Misc;
using Server.Services.Harvest;
using System;

namespace Server.Fronteira.Recursos
{
    // Nodulos de recurso pelo mapa
    public class Recurso : Item
    {
        private Item _folha = null;
        private CraftResource _resource;
        private ItemQuality _quality;

        public Mobile Coletando = null;

        [Constructable]
        public Recurso()
        {
            Constroi(Utility.RandomBool() ? CraftResourceType.Metal : CraftResourceType.Wood);
        }

        [Constructable]
        public Recurso(CraftResourceType type)
        {
            Constroi(type);
        }

        public Recurso(Serial s) : base(s) { }


        public void Constroi(CraftResourceType type)
        {
            Quality = QualidadeRandom();
            if (type == CraftResourceType.Metal)
                Resource = MinerioRandom();
            else
                Resource = MadeiraRandom();
            Movable = false;
        }

        public static ItemQuality QualidadeRandom()
        {
            var rnd = Utility.Random(100);
            if (rnd < 10)
                return ItemQuality.Exceptional;
            else if (rnd < 50)
                return ItemQuality.Normal;
            else
                return ItemQuality.Low;
        }

        public static CraftResource MadeiraRandom()
        {
            var rnd = Utility.Random(100);
            if (rnd < 5)
                return CraftResource.Gelo;
            else if (rnd < 10)
                return CraftResource.Carmesim;
            else if (rnd < 20)
                return CraftResource.Mogno;
            else if (rnd < 50)
                return CraftResource.Pinho;
            else
                return CraftResource.Carvalho;
        }

        public static CraftResource MinerioRandom()
        {
            var rnd = Utility.Random(100);
            if (rnd < 5)
                return CraftResource.Adamantium;
            else if (rnd < 10)
                return CraftResource.Vibranium;
            else if (rnd < 20)
                return CraftResource.Quartzo;
            else if (rnd < 30)
                return CraftResource.Berilo;
            else if (rnd < 50)
                return CraftResource.Lazurita;
            else if (rnd < 70)
                return CraftResource.Dourado;
            else
                return CraftResource.Niobio;
        }

        public void StartHarvest(Mobile from, Item tool)
        {

        }

        public SkillName GetSkill()
        {
            if (Metal())
                return SkillName.Mining;
            else
                return SkillName.Lumberjacking;
        }

        public override void OnDelete()
        {
            if (_folha != null)
            {
                _folha.Delete();
                _folha = null;
            }
              
            base.OnDelete();
        }

        public void Coleta(Mobile from)
        {
            Item i = GetItem();
            if (i == null)
            {
                from.SendMessage(38, "Erro, favor reportar a staff...");
                return;
            }

            var diff = Dificuldade.GetDificuldade(_resource);
            var skill = GetSkill();
            if (diff != null)
            {

                if (from.Skills[skill].Value < diff.Required)
                {
                    from.SendMessage("Voce nao tem ideia de como pode coletar isto");
                    return;
                }
                if (!from.CheckSkillMult(skill, diff.Min, diff.Max, 0))
                {
                    from.SendMessage("Voce nao conseguiu extrair o recurso");
                    new ColetaTimer(this, from).Start();
                    return;
                }
            }

            if (from.Skills[skill].Value < diff.Required - 10)
            {
                if(from.Skills[skill].Value < 65)
                    SkillCheck.Gain(from, from.Skills[skill], 10);
                else if (from.Skills[skill].Value < 75)
                    SkillCheck.Gain(from, from.Skills[skill], 8);
                else if (from.Skills[skill].Value < 85)
                    SkillCheck.Gain(from, from.Skills[skill], 5);
                else if (from.Skills[skill].Value < 95)
                    SkillCheck.Gain(from, from.Skills[skill], 2);
                else
                    SkillCheck.Gain(from, from.Skills[skill], 1);
            } else
            {
                if(Utility.RandomDouble() < 0.05)
                    SkillCheck.Gain(from, from.Skills[skill], 1);
            }

            if (!from.PlaceInBackpack(i))
                i.MoveToWorld(from.Location, from.Map);

            from.SendMessage("Voce coletou o recurso");

            Delete();
            if (_folha != null)
            {
                _folha.Delete();
            }
        }

        public bool Metal()
        {
            return CraftResources.GetType(_resource) == CraftResourceType.Metal;
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (CraftResources.GetType(_resource) == CraftResourceType.Metal)
                list.Add("Deposito de " + _resource.ToString());
            else
                list.Add("Arvore de " + _resource.ToString());
        }

        public void SetGrafico()
        {
            if (_folha != null)
            {
                _folha.Delete();
                _folha = null;
            } else
            {
                if (this.timer != null)
                {
                    this.timer.Stop();
                }
            }

            if (CraftResources.GetType(_resource) == CraftResourceType.Metal)
            {
                switch (_quality)
                {
                    case ItemQuality.Exceptional: this.ItemID = 0x08E8; break;
                    case ItemQuality.Normal: this.ItemID = 0x08E6; break;
                    case ItemQuality.Low: this.ItemID = 0x08E3; break;
                }
            }
            else
            {
                var folha = 0;
                var caule = 0;
                switch (_resource)
                {
                    case CraftResource.Cedro: caule = 3286; folha = 3287; break;
                    case CraftResource.Carvalho: caule = 3290; folha = 3292; break;
                    case CraftResource.Pinho: caule = 3286; folha = 3287; break;
                    case CraftResource.Mogno: caule = 3492; folha = 3495; break;
                    case CraftResource.Carmesim: caule = 3480; folha = 3481; break;
                    case CraftResource.Gelo: caule = 3286; folha = 9969; break;
                }
                if (Shard.DebugEnabled)
                {
                    Shard.Debug("ID FOLHA: " + folha);
                }
                if (folha != 0)
                {
                    this.timer = Timer.DelayCall(TimeSpan.FromSeconds(0.1), () =>
                    {
                        if (this.Deleted)
                            return;

                        _folha = new Item(folha);
                        _folha.Name = "Folhas de " + _resource.ToString();
                        _folha.Hue = this.Hue;
                        _folha.Movable = false;
                        _folha.MoveToWorld(Location, Map);
                        this.timer = null;
                    });
                }
                this.ItemID = caule;
            }
        }

        Timer timer;

        public Item GetItem()
        {
            switch (_resource)
            {
                case CraftResource.Cobre: return new CopperOre(Quantidade);
                case CraftResource.Bronze: return new BronzeOre(Quantidade);
                case CraftResource.Niobio: return new NiobioOre(Quantidade);
                case CraftResource.Dourado: return new SilverOre(Quantidade);
                case CraftResource.Adamantium: return new AdamantiumOre(Quantidade);
                case CraftResource.Berilo: return new BeriloOre(Quantidade);
                case CraftResource.Quartzo: return new QuartzoOre(Quantidade);
                case CraftResource.Lazurita: return new LazuritaOre(Quantidade);
                case CraftResource.Vibranium: return new VibraniumOre(Quantidade);
                case CraftResource.Eucalipto: return new HeartwoodLog(Quantidade);
                case CraftResource.Carmesim: return new BloodwoodLog(Quantidade);
                case CraftResource.Gelo: return new FrostwoodLog(Quantidade);
                case CraftResource.Pinho: return new AshLog(Quantidade);
                case CraftResource.Mogno: return new YewLog(Quantidade);
                case CraftResource.Carvalho: return new OakLog(Quantidade);
            }
            return null;
        }

        public int Quantidade { get { return Metal() ? 8 : 15 * (1 + (int)_quality) + Utility.Random(Metal() ? 8 : 15 * (1 + (int)_quality)); } }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("Talvez voce possa obter estes recursos com uma ferramenta...");
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            //base.AddNameProperties(list);
            if (CraftResources.GetType(_resource) == CraftResourceType.Metal)
                list.Add("Deposito de " + _resource.ToString());
            else
                list.Add("Arvore de " + _resource.ToString());

            if (_quality == ItemQuality.Low)
                list.Add("Pequeno");
            else if (_quality == ItemQuality.Exceptional)
                list.Add("Grande");
        }

        [CommandProperty(AccessLevel.Administrator)]
        public CraftResource Resource
        {
            get
            {
                return _resource;
            }
            set
            {
                _resource = value;
                Hue = CraftResources.GetHue(value);
                InvalidateProperties();
                SetGrafico();
                if (CraftResources.GetType(_resource) == CraftResourceType.Metal)
                    Name = "Deposito de " + _resource.ToString();
                else
                    Name = "Arvore de " + _resource.ToString();
            }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public ItemQuality Quality
        {
            get
            {
                return _quality;
            }
            set
            {
                _quality = value;
                InvalidateProperties();
                SetGrafico();
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
            writer.Write((int)_resource);
            writer.Write((int)_quality);
            writer.Write(_folha);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            var v = reader.ReadInt();
            _resource = (CraftResource)reader.ReadInt();
            _quality = (ItemQuality)reader.ReadInt();
            _folha = reader.ReadItem();
        }
    }
}
