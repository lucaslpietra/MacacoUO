using System.Collections.Generic;

namespace Server.Fronteira.Talentos
{
    public class DefTalentos
    {
        private static Dictionary<Talento, DefTalentos> _talentos = new Dictionary<Talento, DefTalentos>();

        public static DefTalentos GetDef(Talento t)
        {
            if (!_talentos.ContainsKey(t))
                throw new System.Exception("Talento " + t.ToString() + " nao ta registrado nas definicoes");
            return _talentos[t];
        }

        static DefTalentos()
        {
            _talentos.Add(Talento.Porretes, new DefTalentos()
            {
                Desc1 = "+15% Dano com Macefight",
                Icone = 40861,
                Nome = "Porretes"
            });

            _talentos.Add(Talento.Lancas, new DefTalentos()
            {
                Desc1 = "+15% Dano com Fencing",
                Icone = 21019,
                Nome = "Lancas"
            });

            _talentos.Add(Talento.Machados, new DefTalentos()
            {
                Desc1 = "+15% Dano com Machados",
                Icone = 21020,
                Nome = "Machados"
            });

            _talentos.Add(Talento.Espadas, new DefTalentos()
            {
                Desc1 = "+15% Dano com Espadas",
                Icone = 21015,
                Nome = "Espadas"
            });

            _talentos.Add(Talento.Hastes, new DefTalentos()
            {
                Desc1 = "+15% Dano com Armas de Aste",
                Icone = 21015,
                Nome = "Armas de Aste"
            });

            _talentos.Add(Talento.Magia, new DefTalentos()
            {
                Desc1 = "Max 90 Magery",
                Icone = 24010,
                Nome = "Magia"
            });

            _talentos.Add(Talento.Rastreador, new DefTalentos()
            {
                Desc1 = "Max 90 Tracking, Max 90 Detect Hidden",
                Icone = 24013,
                Nome = "Rastreador"
            });

            _talentos.Add(Talento.Ladrao, new DefTalentos()
            {
                Desc1 = "Max 90 Stealing, Max 90 Snooping",
                Icone = 40843,
                Nome = "Ladrao"
            });

            _talentos.Add(Talento.ResistSpell, new DefTalentos()
            {
                Desc1 = "Max 90 Magic Resist",
                Icone = 40871,
                Nome = "Resistente"
            });


            _talentos.Add(Talento.Regeneracao, new DefTalentos()
            {
                Desc1 = "+200% Regen de Vida Passivo",
                Icone = 39831,
                Nome = "Eterno"
            });

            _talentos.Add(Talento.Bloqueador, new DefTalentos()
            {
                Desc1 = "Max 90 Parry",
                Icone = 40873,
                Nome = "Bloqueador"
            });

            _talentos.Add(Talento.Paladino, new DefTalentos()
            {
                Desc1 = "Max 90 Chivalry, Max 80 Meditation",
                Icone = 40861,
                Nome = "Paladino"
            });

            _talentos.Add(Talento.Comandante, new DefTalentos()
            {
                Desc1 = "Max 100 Begging, Max 100 Forensics, Max 100 Parry",
                Icone = 40861,
                Nome = "Comandante"
            });

            _talentos.Add(Talento.Necrowar, new DefTalentos()
            {
                Desc1 = "Max 90 Necromancy, Max 80 Meditation",
                Icone = 40861,
                Nome = "Necrowar"
            });

            _talentos.Add(Talento.Finta, new DefTalentos()
            {
                Desc1 = "-20% Chance de ser bloqueado",
                Icone = 40860,
                Nome = "Finta"
            });

            _talentos.Add(Talento.ArmaduraMagica, new DefTalentos()
            {
                Desc1 = "Medita com armaduras pesadas",
                Icone = 39841,
                Nome = "Armadura Magica"
            });

            _talentos.Add(Talento.ArmaduraPesada, new DefTalentos()
            {
                Desc1 = "Nao perde dex com armaduras",
                Icone = 40868,
                Nome = "Armadura Pesada"
            });

            _talentos.Add(Talento.Experiente, new DefTalentos()
            {
                Desc1 = "+500 EXP",
                Icone = 40862,
                Nome = "Experiente"
            });


            _talentos.Add(Talento.Hipismo, new DefTalentos()
            {
                Desc1 = "Monta sem cair",
                Icone = 20745,
                Nome = "Hipismo"
            });

            _talentos.Add(Talento.Perseveranca, new DefTalentos()
            {
                Desc1 = "Vida +40",
                Icone = 21000,
                Nome = "Perseveranca"
            });

            _talentos.Add(Talento.Defensor, new DefTalentos()
            {
                Desc1 = "+30% Bloqueio para Aliados",
                Icone = 39846,
                Nome = "Defensor"
            });

            _talentos.Add(Talento.Esquiva, new DefTalentos()
            {
                Desc1 = "+15% Esquiva",
                Icone = 39828,
                Nome = "Agilidade"
            });

            _talentos.Add(Talento.Sabedoria, new DefTalentos()
            {
                Desc1 = "+40 Mana",
                Icone = 39830,
                Nome = "Sabedoria"
            });

            _talentos.Add(Talento.Sagacidade, new DefTalentos()
            {
                Desc1 = "+20% Cast Rapido",
                Icone = 39819,
                Nome = "Sagacidade"
            });

            _talentos.Add(Talento.Concentracao, new DefTalentos()
            {
                Desc1 = "+10% Penetracao Magic Resist",
                Icone = 39820,
                Nome = "Concentracao"
            });

            _talentos.Add(Talento.Precisao, new DefTalentos()
            {
                Desc1 = "+10% Acerto",
                Icone = 39839,
                Nome = "Precisao"
            });

            _talentos.Add(Talento.Potencia, new DefTalentos()
            {
                Desc1 = "+20% Bonus Dano",
                Icone = 39840,
                Nome = "Potencia"
            });

            _talentos.Add(Talento.Brutalidade, new DefTalentos()
            {
                Desc1 = "+20% Menos Custo Dex",
                Icone = 39852,
                Nome = "Brutalidade"
            });

            _talentos.Add(Talento.FisicoPerfeito, new DefTalentos()
            {
                Desc1 = "+40 Stamina",
                Icone = 39853,
                Nome = "Fisico Perfeito"
            });

            _talentos.Add(Talento.Forjador, new DefTalentos()
            {
                Desc1 = "+30% Chance Exp",
                Icone = 39822,
                Nome = "Forjador"
            });

            _talentos.Add(Talento.Naturalista, new DefTalentos()
            {
                Desc1 = "+3 Recursos por spot",
                Icone = 24001,
                Nome = "Naturalista"
            });

            _talentos.Add(Talento.PeleArcana, new DefTalentos()
            {
                Desc1 = "+10% Resist",
                Icone = 39843,
                Nome = "Pele Arcana"
            });

            _talentos.Add(Talento.ProtecaoPesada, new DefTalentos()
            {
                Desc1 = "+10 Armor",
                Icone = 39845,
                Nome = "Protecao Pesada"
            });

            _talentos.Add(Talento.Silencioso, new DefTalentos()
            {
                Desc1 = "+33% Chance Nao Ser Notado",
                Icone = 21286,
                Nome = "Silencioso"
            });

            _talentos.Add(Talento.Curandeiro, new DefTalentos()
            {
                Desc1 = "Max 90 Healing, -5 Segundos Bands Curar Aliados",
                Icone = 21286,
                Nome = "Curandeiro"
            });
        }

        public string Desc1;
        public int Icone;
        public string Nome;
    }
}
