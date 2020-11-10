using Server.Commands;
using Server.Engines.Points;
using Server.Engines.VvV;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Spells;
using System;

namespace Server.Ziden.Kills
{
    public class Exp : PointsSystem
    {
        public override TextDefinition Name { get { return "Exp"; } }
        public override PointsType Loyalty { get { return PointsType.Exp; } }
        public override bool AutoAdd { get { return true; } }
        public override double MaxPoints { get { return 2000; } }
        public override bool ShowOnLoyaltyGump { get { return true; } }
        public static bool Enabled = true;
    }

    public class PontosPvm : PointsSystem
    {
        public override TextDefinition Name { get { return "PvM"; } }
        public override PointsType Loyalty { get { return PointsType.PvM; } }
        public override bool AutoAdd { get { return true; } }
        public override double MaxPoints { get { return double.MaxValue; } }
        public override bool ShowOnLoyaltyGump { get { return true; } }
        public static bool Enabled = true;
        public static void Initialize()
        {

            CommandSystem.Register("pvm", AccessLevel.Player, Cmd);
            EventSink.CreatureDeath += CreatureDeath;
        }

        [Usage("Kills")]
        private static void Cmd(CommandEventArgs e)
        {
            if (SpellHelper.CheckCombat(e.Mobile))
            {
                e.Mobile.SendMessage("Voce nao pode usar este comando no calor da batalha");
                return;
            }

            if (e.Mobile.RP)
            {
                e.Mobile.SendMessage("Voce nao pode usar este comando em um personagem RP");
                return;
            }

            e.Mobile.SendGump(new PvmRewardsGump(e.Mobile, e.Mobile as PlayerMobile));
        }

        public static void CreatureDeath(CreatureDeathEventArgs e)
        {
            BaseCreature bc = e.Creature as BaseCreature;
            var pontos = bc.PontosPvm;

            if (pontos <= 0)
                return;

            if (bc.IsChampionSpawn && !(bc is BaseChampion))
                pontos = pontos / 4;

            var c = e.Corpse;
            var killer = e.Killer;

            var exp = (int)(pontos * 1.5);

            if (SkillCheck.BONUS_GERAL != 0)
                exp = (int)(exp * SkillCheck.BONUS_GERAL);

            if (bc != null && bc.GetLootingRights() != null)
            {
                foreach (var m in bc.GetLootingRights())
                {
                    var pl = m.m_Mobile as PlayerMobile;
                    if (pl != null)
                    {
                        if (Shard.EXP)
                        {
                            c.PrivateOverheadMessage(Network.MessageType.Regular, 66, false, string.Format("+{0} EXP", exp), pl.NetState);
                            PointsSystem.Exp.AwardPoints(pl, pontos, false, false);
                            if (!pl.IsCooldown("xpp"))
                            {
                                pl.SetCooldown("xpp", TimeSpan.FromHours(1));
                                pl.SendMessage(78, "Digite .xp para usar sua EXP para subir skills");
                            }
                        }
                        PointsSystem.PontosPvmEterno.AwardPoints(pl, pontos / 2, false, false);
                        if (pl.RP)
                        {
                            continue;
                        }
                        var pts = PointsSystem.PontosPvm.AwardPoints(pl, pontos, false, false);
                        if (pts > 500 && !pl.IsCooldown("pvmp"))
                        {
                            pl.SetCooldown("pvmp", TimeSpan.FromHours(1));
                            pl.SendMessage(78, "Voce pode trocar seus pontos PvM por recompensas usando o comando .pvm");
                        }
                     


                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {

            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}
