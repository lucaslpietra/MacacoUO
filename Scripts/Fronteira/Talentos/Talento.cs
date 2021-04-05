
using Server.Engines.Points;
using Server.Mobiles;

namespace Server.Fronteira.Talentos
{

    public static class TalentoEffect
    {
        public static void GanhaEfeito(PlayerMobile m, Talento t)
        {
            switch (t)
            {
                case Talento.Experiente:
                    PointsSystem.Exp.AwardPoints(m, 500);
                    break;
                case Talento.Paladino:
                    m.Skills[SkillName.Chivalry].Cap = 90;
                    m.Skills[SkillName.Meditation].Cap = 80;
                    break;
                case Talento.Necrowar:
                    m.Skills[SkillName.Necromancy].Cap = 90;
                    m.Skills[SkillName.Meditation].Cap = 80;
                    break;
                case Talento.Comandante:
                    m.Skills[SkillName.Begging].Cap = 100;
                    m.Skills[SkillName.Forensics].Cap = 100;
                    break;
                case Talento.Curandeiro:
                    m.Skills[SkillName.Healing].Cap = 90;
                    break;
                case Talento.Bloqueador:
                    m.Skills[SkillName.Parry].Cap = 90;
                    break;
                case Talento.Magia:
                    m.Skills[SkillName.Magery].Cap = 90;
                    break;
                case Talento.Ladrao:
                    m.Skills[SkillName.Stealing].Cap = 90;
                    m.Skills[SkillName.Snooping].Cap = 90;
                    break;
                case Talento.Rastreador:
                    m.Skills[SkillName.Tracking].Cap = 90;
                    m.Skills[SkillName.DetectHidden].Cap = 90;
                    break;
            }
        }
    }

    public enum Talento
    {
        /// TALENTOS DE SKILLS ///

        // "SubClasses"

        // Subs de Guerreiro
        Paladino,
        Necrowar,
        Comandante,

        /// TALENTOS DE HABILIDADES UNICAS //

        // 90 Parry
        Bloqueador,
        // 90 Magic Resist
        ResistSpell,
        // 90 Tracking
        Rastreador,
        // 90 Stealing+Snooping
        Ladrao,
        // 90 Magery
        Magia,

        // +500 XP "Free"
        Experiente,

        // Montar
        Hipismo,

        // Bonus de dano com armas especificas
        Espadas,
        Lancas,
        Porretes,
        Machados,
        Hastes,

        // Nao perde dex com armadura
        ArmaduraPesada,

        // Menos chance tomar parry
        Finta,

        // Vitalidade
        Perseveranca,
        // Parry por outros
        Defensor,
        // Esquiva
        Esquiva,

        // Mana
        Sabedoria,
        // Cast Time - TODO
        Sagacidade,
        // Cast Move
        Concentracao,

        // Bonus Acerto 
        Precisao,
        // Bonus Dano
        Potencia,
        // Custo Power Moves
        Brutalidade,
        // Max Dex
        FisicoPerfeito,

        // Item Exp
        Forjador,
        // Bonus harvest
        Naturalista,
        // Ataques pesados usando dinheiro
        // Mamonita,

        // Resist Magias
        PeleArcana,
        // Resist Fisico
        ProtecaoPesada,
        // Plate Castando
        ArmaduraMagica,
        // Inimigos nao reparam tanto
        Silencioso,
        // Cura aliados rapido
        Curandeiro,
        // + regen de HP
        Regeneracao

        // Loja anywhere
        // Comerciante,

    }
}
