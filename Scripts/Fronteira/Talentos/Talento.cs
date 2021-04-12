
using Server.Engines.Points;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;

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
                case Talento.Comandante: // ja upa pra 100 pq essa precisa de 100 nas 2 pra ser util
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
                case Talento.Ranger:
                    m.Skills[SkillName.Veterinary].Cap = 90;
                    m.Skills[SkillName.AnimalTaming].Cap = 90;
                    break;
                case Talento.Assassino:
                    m.Skills[SkillName.Poisoning].Cap = 90;
                    m.Skills[SkillName.Tactics].Cap = 90;
                    m.Skills[SkillName.Alchemy].Cap = 70;
                    break;
                case Talento.CacadorDeTesouros:
                    m.Skills[SkillName.Lockpicking].Cap = 90;
                    m.Skills[SkillName.RemoveTrap].Cap = 90;
                    m.Skills[SkillName.Cartography].Cap = 90;
                    break;
                case Talento.Provocacao:
                    m.Skills[SkillName.Musicianship].Cap = 70;
                    m.Skills[SkillName.Provocation].Cap = 70;
                    break;
                case Talento.AnimalLore:
                    m.Skills[SkillName.AnimalLore].Cap = 90;
                    break;
            }
        }
    }

    public enum Talento // sempre adicionar no fim da lista
    {
        Nenhum,

        /// TALENTOS DE SKILLS ///
        Hab_ArmorIgnore,
        Hab_BleedAttack,
        Hab_CrushingBlow,
        Hab_Disarm,
        Hab_Dismount,
        Hab_DoubleStrike,
        Hab_Infectar,
        Hab_AtaqueMortal,
        Hab_MovingSHot,
        Hab_ParalizeBlow,
        Hab_Shadowstrike,
        Hab_Wirlwind,
        Hab_RidingSwipe,
        Hab_FrenziedWirlwing,
        Hab_Block,
        Hab_DefenseMastery,
        Hab_NerveStrike,
        Hab_TalonStrike,
        Hab_Feint,
        Hab_DuelWeild,
        Hab_Doubleshot,
        Hab_ArmorPierce,
        Hab_Bladeweave,
        Hab_ForceArrow,
        Hab_LightArrow,
        Hab_PsyAttack,
        Hab_SerpentArrow,
        Hab_ForceOfNature,
        Hab_InfusedThrow,
        Hab_MysticArc,
        Hab_Disrobe,
        Hab_ColWind,
        Hab_Concussion,

        // "SubClasses"

        // Subs de Guerreiro
        Paladino,
        Necrowar,
        Comandante,
        AnimalLore,

        Ranger, // 90 taming 90 veterinary
        Assassino, // 90 Poisoning 90 Tactics 70 alchemy
        CacadorDeTesouros, // 90 lockpick 90 remove trap 90  cartography 

        /// TALENTOS DE HABILIDADES UNICAS //

        Provocacao,

        CorrerStealth,

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
        Adagas,

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
        // Corre em stealth
        Gatuno,
        // Cura aliados rapido
        Curandeiro,
        // + regen de HP
        Regeneracao

        // Loja anywhere
        // Comerciante,

    }
}
