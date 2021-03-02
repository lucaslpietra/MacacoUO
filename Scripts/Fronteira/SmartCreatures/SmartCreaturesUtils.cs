using System.Linq;
using Server.Mobiles;
using Server.Regions;

namespace Server.Fronteira.SmartCreatures
{
    public static class SmartCreaturesUtils
    {
        public static void DoSmart(BaseCreature creature, int smartRange = 30, int enemiesRange = 10)
        {
            if (creature == null)
            {
                return;
            }

            var playerCount = creature.GetMobilesInRange(smartRange)
                .Where(x => x is PlayerMobile && x.IsPlayer() && x.AccessLevel == AccessLevel.Player).Count();
            if (playerCount > 0)
            {
                creature.Hidden = false;
                creature.Frozen = false;
                creature.CanMove = true;
                creature.Paralyzed = false;

                foreach (Mobile m in creature.GetMobilesInRange(enemiesRange))
                {
                    if (m == creature || !creature.CanBeHarmful(m))
                        continue;

                    //TODO Depois fazer alguma logica melhor para esses casos
                    // if (creature is IManEater && m.Player && m.AccessLevel == AccessLevel.Player)
                    // {
                    //     creature.Attack(m);
                    //     break;
                    // }
                    else if (IsHerbivore(m) && IsCarnivore(creature))
                    {
                        creature.Attack(m);
                        break;
                    }
                }
            }
            else
            {
                creature.Frozen = true;
                creature.Paralyzed = true;
                creature.CanMove = false;
            }
        }

        private static bool IsHerbivore(Mobile m)
        {
            BaseCreature creature = m as BaseCreature;
            if (creature == null)
            {
                return false;
            }

            if (creature.FavoriteFood.Equals(FoodType.FruitsAndVegies) &&
                creature.FavoriteFood.Equals(FoodType.GrainsAndHay))
            {
                return true;
            }
            else if (creature.FavoriteFood.Equals(FoodType.FruitsAndVegies))
            {
                return true;
            }
            else if (creature.FavoriteFood.Equals(FoodType.GrainsAndHay))
            {
                return true;
            }

            return false;
        }

        private static bool IsCarnivore(Mobile m)
        {
            BaseCreature creature = m as BaseCreature;
            if (creature == null)
            {
                return false;
            }

            if (creature.FavoriteFood.Equals(FoodType.Meat) && creature.FavoriteFood.Equals(FoodType.Fish) &&
                creature.FavoriteFood.Equals(FoodType.Eggs))
            {
                return true;
            }
            else if (creature.FavoriteFood.Equals(FoodType.Meat) && creature.FavoriteFood.Equals(FoodType.Fish))
            {
                return true;
            }
            else if (creature.FavoriteFood.Equals(FoodType.Meat))
            {
                return true;
            }

            return false;
        }
    }
}
