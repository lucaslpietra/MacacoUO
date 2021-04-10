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
            _talentos.Add(Talento.Hab_Concussion, new DefTalentos()
            {
                Desc1 = "Permite usar Concussion Blow",
                Icone = 20495,
                Nome = "Golpe de Contusao"
            });

            _talentos.Add(Talento.Hab_ColWind, new DefTalentos()
            {
                Desc1 = "Permite usar ColdWind",
                Icone = 20495,
                Nome = "Bafo Frio"
            });

            // nao achei o q diabos disrobe siginifica no uo
            _talentos.Add(Talento.Hab_Disrobe, new DefTalentos()
            {
                Desc1 = "Permite usar Disrobe",
                Icone = 21014,
                Nome = "Fazer Disrobar"
            });

            _talentos.Add(Talento.Hab_MysticArc, new DefTalentos()
            {
                Desc1 = "Permite usar Mystic Arc",
                Icone = 21022,
                Nome = "Tiro Ricocheteado"
            });

            _talentos.Add(Talento.Hab_InfusedThrow, new DefTalentos()
            {
                Desc1 = "Permite usar Infused Throw",
                Icone = 21005,
                Nome = "Arremesso Incutido"
            });

            _talentos.Add(Talento.Hab_ForceOfNature, new DefTalentos()
            {
                Desc1 = "Permite usar Force Of Nature",
                Icone = 20491,
                Nome = "Forca da Natureza"
            });

            _talentos.Add(Talento.Hab_SerpentArrow, new DefTalentos()
            { 
                Desc1 = "Permite usar Serpent Arrow",
                Icone = 20480,
                Nome = "Tiro Venenoso"
            });

            _talentos.Add(Talento.Hab_PsyAttack, new DefTalentos()
            {
                Desc1 = "Permite usar Psychic Attack",
                Icone = 20949,
                Nome = "Ataque Psiquico"
            });

            _talentos.Add(Talento.Hab_LightArrow, new DefTalentos()
            {
                Desc1 = "Permite usar Lightning Arrow",
                Icone = 21017,
                Nome = "Tiro Eletrico"
            });

            _talentos.Add(Talento.Hab_ForceArrow, new DefTalentos()
            {
                Desc1 = "Permite usar Force Arrow",
                Icone = 20488,
                Nome = "Tiro Vertigem"
            });

            _talentos.Add(Talento.Hab_Bladeweave, new DefTalentos()
            {
                Desc1 = "Permite usar Bladeweave",
                Icone = 20744,
                Nome = "Arma Inteligente"
            });

            _talentos.Add(Talento.Hab_ArmorPierce, new DefTalentos()
            {
                Desc1 = "Permite usar Armor Pierce",
                Icone = 20992,
                Nome = "Quebra-Armadura"
            });

            _talentos.Add(Talento.Hab_Doubleshot, new DefTalentos()
            {
                Desc1 = "Permite usar Double Shot",
                Icone = 21001,
                Nome = "Tiro Duplo"
            });

            _talentos.Add(Talento.Hab_DuelWeild, new DefTalentos()
            {
                Desc1 = "Permite usar Dual Wield",
                Icone = 20998,
                Nome = "Apunhalda Dupla"
            });

            _talentos.Add(Talento.Hab_Feint, new DefTalentos()
            {
                Desc1 = "Permite usar Feint",
                Icone = 21022,
                Nome = "Esquiva"
            });

            _talentos.Add(Talento.Hab_TalonStrike, new DefTalentos()
            {
                Desc1 = "Permite usar Talon Strike",
                Icone = 20741,
                Nome = "Apunhalada Perigosa"
            });

            _talentos.Add(Talento.Hab_NerveStrike, new DefTalentos()
            {
                Desc1 = "Permite usar Nerve Strike",
                Icone = 20496,
                Nome = "Ataque Nervoso"
            });

            _talentos.Add(Talento.Hab_DefenseMastery, new DefTalentos()
            {
                Desc1 = "Permite usar Defense Mastery",
                Icone = 20742,
                Nome = "Defesa Aumentada"
            });

            _talentos.Add(Talento.Hab_Block, new DefTalentos()
            {
                Desc1 = "Permite usar Block",
                Icone = 20994,
                Nome = "Bloqueia"
            });

            _talentos.Add(Talento.Hab_FrenziedWirlwing, new DefTalentos()
            {
                Desc1 = "Permite usar Frenzied Whirlwind",
                Icone = 21000,
                Nome = "Ataque Agressivo"
            });

            _talentos.Add(Talento.Hab_RidingSwipe, new DefTalentos()
            {
                Desc1 = "Permite usar Riding Swipe",
                Icone = 21005,
                Nome = "Ataque a Montaria"
            });

            _talentos.Add(Talento.Hab_Wirlwind, new DefTalentos()
            {
                Desc1 = "Permite usar Whirlwind Attack",
                Icone = 21004,
                Nome = "Ataque em Area"
            });

            _talentos.Add(Talento.Hab_Shadowstrike, new DefTalentos()
            {
                Desc1 = "Permite usar Shadow Strike",
                Icone = 21008,
                Nome = "Apunhalada Escondida"
            });

            _talentos.Add(Talento.Hab_ParalizeBlow, new DefTalentos()
            {
                Desc1 = "Permite usar Paralyzing Blow",
                Icone = 21010,
                Nome = "Porrada Paralizante"
            });

            _talentos.Add(Talento.Hab_MovingSHot, new DefTalentos()
            {
                Desc1 = "Permite usar Moving Shot",
                Icone = 21016,
                Nome = "Tiro em Movimento"
            });

            _talentos.Add(Talento.Hab_AtaqueMortal, new DefTalentos()
            {
                Desc1 = "Permite usar Mortal Strike",
                Icone = 24012,
                Nome = "Apunhalada Mortal"
            });

            _talentos.Add(Talento.Hab_Infectar, new DefTalentos()
            {
                Desc1 = "Permite usar Infectious Strike",
                Icone = 23013,
                Nome = "Apunhalada Infecciosa"
            });

            _talentos.Add(Talento.Hab_DoubleStrike, new DefTalentos()
            {
                Desc1 = "Permite usar Double Strike",
                Icone = 23010,
                Nome = "Apunhalada Dupla"
            });

            _talentos.Add(Talento.Hab_Dismount, new DefTalentos()
            {
                Desc1 = "Permite usar Dismount",
                Icone = 20997,
                Nome = "Desmonta"
            });

            _talentos.Add(Talento.Hab_Disarm, new DefTalentos()
            {
                Desc1 = "Permite usar Disarm",
                Icone = 23005,
                Nome = "Desarma"
            });

            _talentos.Add(Talento.Hab_CrushingBlow, new DefTalentos()
            {
                Desc1 = "Permite usar Crushing Blow",
                Icone = 23002,
                Nome = "Porrada Esmagadora"
            });

            _talentos.Add(Talento.Hab_BleedAttack, new DefTalentos()
            {
                Desc1 = "Permite usar Bleed Attack",
                Icone = 20737,
                Nome = "Fazer Sangrar"
            });

            _talentos.Add(Talento.Hab_ArmorIgnore, new DefTalentos()
            {
                Desc1 = "Permite usar Armor Ignore",
                Icone = 2304,
                Nome = "Ignorar Armaduras"
            });

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
