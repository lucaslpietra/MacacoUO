using System;
using System.Collections.Generic;
using Server.Engines.XmlSpawner2;
using Server.Mobiles;

namespace Server.Fronteira.QualidadeDefeitos
{
    public class DefeitosXmlAttachment : XmlAttachment
    {
        private Mobile _From = null;
        private Defeitos _Defeitos = null;
        private int _EndividadoCount = 1;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile From
        {
            get => _From;
            set => _From = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Defeitos Defeitos
        {
            get => _Defeitos;
            set => _Defeitos = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int EndividadoCount
        {
            get => _EndividadoCount;
            set {
                if (_EndividadoCount == 1 && value == -1)
                {
                    _EndividadoCount = 1;
                }
                else
                {
                    _EndividadoCount = value;
                }
            }
        }

        public DefeitosXmlAttachment(Mobile from, Defeitos defeitos)
        {
            From = from;
            Defeitos = defeitos;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            // version 0
            writer.Write((int) 0);
            writer.Write((Mobile) From);
            writer.Write(EndividadoCount);

            Defeitos.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            // version 0
            int version = reader.ReadInt();
            From = reader.ReadMobile();
            EndividadoCount = reader.ReadInt();

            Defeitos.Deserialize(reader);
        }
    }
}
