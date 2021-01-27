using System.Collections.Generic;

namespace Server.Fronteira.Talentos
{
    public class DefTalentos
    {
        private static Dictionary<Talento, DefTalentos> _talentos = new Dictionary<Talento, DefTalentos>();

        public static DefTalentos GetDef(Talento t)
        {
            return _talentos[t];
        }

        static DefTalentos()
        {
            _talentos.Add(Talento.ArmaduraMagica, new DefTalentos()
            {
                Desc1 = "Magias usando armaduras pesadas",
                Desc2 = "Meditacao com armaduras",
                Desc3 = "Medita perfeito com armadura",
                Icone = 39841,
                Nome = "Armadura Magica"
            });

            _talentos.Add(Talento.Hipismo, new DefTalentos()
            {
                Desc1 = "Montar em cavalos porem pode cair",
                Desc2 = "Nao cair correndo e com magias",
                Desc3 = "Nao cair em combate",
                Icone = 20745,
                Nome = "Hipismo"
            });

            _talentos.Add(Talento.Perseveranca, new DefTalentos()
            {
                Desc1 = "Vida +10",
                Desc2 = "Vida +10",
                Desc3 = "Vida +20",
                Icone = 21000,
                Nome = "Perseveranca"
            });

            _talentos.Add(Talento.Defensor, new DefTalentos()
            {
                Desc1 = "+10% Bloquio para Aliados",
                Desc2 = "+20% Bloquio de Aliados",
                Desc3 = "+30% Bloquio de Aliados",
                Icone = 39846,
                Nome = "Defensor"
            });

            _talentos.Add(Talento.Esquiva, new DefTalentos()
            {
                Desc1 = "+5% Esquiva",
                Desc2 = "+5% Esquiva",
                Desc3 = "+10% Esquiva",
                Icone = 39828,
                Nome = "Agilidade"
            });

            _talentos.Add(Talento.Sabedoria, new DefTalentos()
            {
                Desc1 = "+10 Mana",
                Desc2 = "+10 Mana",
                Desc3 = "+20 Mana",
                Icone = 39830,
                Nome = "Sabedoria"
            });

            _talentos.Add(Talento.Sagacidade, new DefTalentos()
            {
                Desc1 = "+20% Cast Rapido",
                Desc2 = "+20% Cast Rapido",
                Desc3 = "+20% Cast Rapido",
                Icone = 39819,
                Nome = "Sagacidade"
            });

            _talentos.Add(Talento.Concentracao, new DefTalentos()
            {
                Desc1 = "+5% Penetracao Magic Resist",
                Desc2 = "+5% Penetracao Magic Resist",
                Desc3 = "+10% Penetracao Magic Resist",
                Icone = 39820,
                Nome = "Concentracao"
            });

            _talentos.Add(Talento.Precisao, new DefTalentos()
            {
                Desc1 = "+10% Acerto",
                Desc2 = "+10% Acerto",
                Desc3 = "+20% Acerto",
                Icone = 39839,
                Nome = "Precisao"
            });

            _talentos.Add(Talento.Potencia, new DefTalentos()
            {
                Desc1 = "+10% Bonus Dano",
                Desc2 = "+10% Bonus Dano",
                Desc3 = "+20% Bonus Dano",
                Icone = 39840,
                Nome = "Potencia"
            });

            _talentos.Add(Talento.Brutalidade, new DefTalentos()
            {
                Desc1 = "+20% Menos Custo Dex",
                Desc2 = "+20% Menos Custo Dex",
                Desc3 = "+20% Menos Custo Dex",
                Icone = 39852,
                Nome = "Brutalidade"
            });

            _talentos.Add(Talento.FisicoPerfeito, new DefTalentos()
            {
                Desc1 = "+10 Stamina",
                Desc2 = "+10 Stamina",
                Desc3 = "+20 Stamina",
                Icone = 39853,
                Nome = "Fisico Perfeito"
            });

            _talentos.Add(Talento.Forjador, new DefTalentos()
            {
                Desc1 = "+20% Chance Exp",
                Desc2 = "+20% Chance Exp",
                Desc3 = "+30% Chance Exp",
                Icone = 39822,
                Nome = "Forjador"
            });

            _talentos.Add(Talento.Naturalista, new DefTalentos()
            {
                Desc1 = "+2 Recursos por spot",
                Desc2 = "+2 Recursos por spot",
                Desc3 = "+5 Recursos por spot",
                Icone = 24001,
                Nome = "Naturalista"
            });

            /*
            _talentos.Add(Talento.Mamonita, new DefTalentos()
            {
                Desc1 = "Usa 100 moedas +5 Dano",
                Desc2 = "Usa 200 moedas +15 Dano",
                Desc3 = "Usa 300 moedas +30 Dano",
                Icone = 39854,
                Nome = "Mamonita"
            });
            */

            _talentos.Add(Talento.PeleArcana, new DefTalentos()
            {
                Desc1 = "+10% Resist",
                Desc2 = "+10% Resist",
                Desc3 = "+20% Resist",
                Icone = 39843,
                Nome = "Pele Arcana"
            });

            _talentos.Add(Talento.ProtecaoPesada, new DefTalentos()
            {
                Desc1 = "+5 Armor",
                Desc2 = "+5 Armor",
                Desc3 = "+20 Armor",
                Icone = 39845,
                Nome = "Protecao Pesada"
            });

            _talentos.Add(Talento.Silencioso, new DefTalentos()
            {
                Desc1 = "+31% Chance Nao Ser Notado",
                Desc2 = "+31% Chance Nao Ser Notado",
                Desc3 = "+31% Chance Nao Ser Notado",
                Icone = 21286,
                Nome = "Silencioso"
            });

            _talentos.Add(Talento.Curandeiro, new DefTalentos()
            {
                Desc1 = "-2 Segundos Bands Curar Aliados",
                Desc2 = "-2 Segundos Bands Curar Aliados",
                Desc3 = "-2 Segundos Bands Curar Aliados",
                Icone = 21286,
                Nome = "Curandeiro"
            });
        }

        public string Desc1;
        public string Desc2;
        public string Desc3;
        public int Icone;
        public string Nome;

        public string Desc(int nivel)
        {
            if (nivel == 0) return Desc1;
            if (nivel == 1) return Desc2;
            return Desc3;
        }
    }
}
