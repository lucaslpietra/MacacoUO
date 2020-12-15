using System.Collections.Generic;

namespace Server.Fronteira.Talentos
{
    public class Talentos
    {
        public Dictionary<Talento, int> _niveis = new Dictionary<Talento, int>();

        public int GetNivel(Talento t)
        {
            if (_niveis.ContainsKey(t))
                return _niveis[t];
            return 0;
        }

        public void SetNivel(Talento t, int nivel)
        {
            _niveis[t] = nivel;
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(_niveis.Count);
            foreach(var key in _niveis.Keys)
            {
                writer.Write((int)key);
                writer.Write(_niveis[key]);
            }
        }

        public void Deserialize(GenericReader reader)
        {
            var ct = reader.ReadInt();
            for(var x = 0; x < ct; x++)
            {
                var k = (Talento)reader.ReadInt();
                var v = reader.ReadInt();
                _niveis.Add(k, v);
            }
        }
    }
}
