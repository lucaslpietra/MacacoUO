using System.Linq;
using Server.Mobiles;
using Server.Regions;

namespace Server.Fronteira.SmartIsometric
{
    public static class SmartIsometricUtils
    {
        public static void DoIsometricSmart(BaseCreature creature)
        {
            //Faz parte do sistema de hide para creaturas em dungeons
            //TODO Testar se a parte de house e town estão funcionando
            if (creature == null)
            {
                return;
            }

            if ((creature.Region.IsPartOf(typeof(DungeonRegion)) || creature.Region.IsPartOf(typeof(HouseRegion)) ||
                 creature.Region.IsPartOf(typeof(TownRegion))) && !creature.Controlled)
            {
                var playerCount = creature.GetMobilesInRange(13)
                    .Where(x => (!x.Equals(creature) && x is PlayerMobile && x.IsPlayer() &&
                                 x.AccessLevel == AccessLevel.Player &&
                                 creature.CanSee(x) && creature.InLOS(x)) || creature.Combatant == x /*r||
                                x.Hidden && x.InRange(creature.Location, 8) && creature.InLOS(x)*/).Count();

                if (playerCount > 0)
                {
                    creature.Hidden = false;
                    creature.Frozen = false;
                    creature.Paralyzed = false;
                    creature.CanMove = true;
                }
                else
                {
                    creature.Hidden = true;
                    creature.Frozen = true;
                    creature.Paralyzed = true;
                    creature.CanMove = false;
                }
            }
        }

        public static void DoVisibleByAnotherPlayer(Mobile mobile)
        {
            //TODO Testar se a parte de players esta funcionando
            if (mobile == null)
            {
                return;
            }

            if (!(mobile is PlayerMobile))
            {
                return;
            }

            var parents = mobile.GetMobilesInRange(13)
                .Where(x => (!x.Equals(mobile) && x is PlayerMobile && x.IsPlayer() &&
                             x.AccessLevel == AccessLevel.Player &&
                             mobile.CanSee(x) && mobile.InLOS(x)) || mobile.Combatant == x /*r||
                                x.Hidden && x.InRange(creature.Location, 8) && creature.InLOS(x)*/);

            foreach (Mobile parent in parents)
            {
                if (!(parent is PlayerMobile))
                {
                    continue;
                }

                ((PlayerMobile) parent).VisibilityList.Add(mobile);
            }

            var parentsOutOfRange = mobile.GetMobilesInRange(13)
                .Where(x => (!x.Equals(mobile) && x is PlayerMobile && x.IsPlayer() &&
                             x.AccessLevel == AccessLevel.Player &&
                             !mobile.CanSee(x) && !mobile.InLOS(x)) || mobile.Combatant != x /*r||
                                x.Hidden && x.InRange(creature.Location, 8) && creature.InLOS(x)*/);

            foreach (Mobile parent in parentsOutOfRange)
            {
                if (!(parent is PlayerMobile))
                {
                    continue;
                }

                ((PlayerMobile) parent).VisibilityList.Remove(mobile);
            }
        }
    }
}
