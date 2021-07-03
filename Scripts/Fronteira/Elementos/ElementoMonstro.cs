using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Fronteira.Elementos
{
    public class ElementoMonstro
    {
        private static Dictionary<Type, ElementoPvM> _cache = new Dictionary<Type, ElementoPvM>();

        public static ElementoPvM DecideElementoMonstro(BaseCreature creature)
        {
            ElementoPvM e;
            if (_cache.TryGetValue(creature.GetType(), out e))
                return e;

            e = Calcula(creature);
            _cache[creature.GetType()] = e;
            return e;
        }

        private static ElementoPvM Calcula(BaseCreature creature)
        {
            var danos = DanosPossiveis(creature);
            var maiorResist = MaiorResistencia(creature);

            if (creature.MeatType == MeatType.Bird || creature.MeatType == MeatType.Chicken || creature.MeatType == MeatType.Duck)
            {
                if (danos.Contains(ElementoPvM.Raio) || maiorResist == ElementoPvM.Raio)
                    return ElementoPvM.Raio;
                return ElementoPvM.Vento;
            }

            if(danos.Contains(ElementoPvM.Vento) || (danos.Contains(ElementoPvM.Gelo) && danos.Contains(ElementoPvM.Raio)) && creature.ColdDamage >0 && creature.EnergyDamage > 0)
            {
                var diff = Math.Abs(creature.ColdDamage - creature.EnergyDamage);
                if (diff < 10)
                    return ElementoPvM.Vento;
            }

            if (danos.Contains(ElementoPvM.Luz))
                return ElementoPvM.Luz;

            if (danos.Contains(ElementoPvM.Escuridao))
                return ElementoPvM.Escuridao;

            if (danos.Contains(maiorResist))
                return maiorResist;

            if (creature.Tribe == TribeType.MortoVivo)
                return ElementoPvM.Escuridao;

            if (creature.Tribe == TribeType.Fada)
                return ElementoPvM.Luz;

            if (danos.Count == 1)
                return danos.First();

            if (creature.Body.IsHuman)
                return ElementoPvM.Luz;

            if (danos.Count == 0 && maiorResist == ElementoPvM.None)
            {
                if (creature.Skills.Magery.Value > 30)
                    return ElementoPvM.Luz;
                return ElementoPvM.Terra;
            }

            if (Shard.DebugEnabled)
            {
                Shard.Debug("Elementos de dano: " + string.Join(",", danos), creature);
                Shard.Debug("Maior Resist: " + maiorResist.ToString(), creature);
            }

            if (maiorResist != ElementoPvM.None)
                return maiorResist;

            return ElementoPvM.Luz;
        }

        private static ElementoPvM MaiorResistencia(BaseCreature creature)
        {
            ElementoPvM e = ElementoPvM.None;
            int res = 30;

            if(creature.ColdResistance > res)
            {
                res = creature.ColdResistance;
                e = ElementoPvM.Gelo;
            }
            if (creature.EnergyResistance > res)
            {
                res = creature.EnergyResistance;
                e = ElementoPvM.Raio;
            }
            if (creature.PhysicalResistance > res)
            {
                res = creature.PhysicalResistance;
                e = ElementoPvM.Terra;
            }
            if (creature.PoisonResistance > res)
            {
                res = creature.PoisonResistance;
                e = ElementoPvM.Agua;
            }
            if (creature.FireResistance > res)
            {
                res = creature.FireResistance;
                e = ElementoPvM.Fogo;
            }
            return e;
        }

        private static HashSet<ElementoPvM> DanosPossiveis(BaseCreature creature)
        {
            HashSet<ElementoPvM> Danos = new HashSet<ElementoPvM>();
            if (creature.ColdDamage > 0)
            {
                Danos.Add(ElementoPvM.Gelo);

                if (creature.PhysicalDamage > 0)
                    Danos.Add(ElementoPvM.Agua);
            }
            if (creature.FireDamage > 0)
            {
                Danos.Add(ElementoPvM.Fogo);
                if (creature.PhysicalDamage > 0)
                    Danos.Add(ElementoPvM.Vento);
            }
            if (creature.ChaosDamage > 0 || creature.Skills.Necromancy.Value > 10 || creature.Skills.SpiritSpeak.Value > 10)
            {
                Danos.Add(ElementoPvM.Escuridao);
            }
            if (creature.EnergyDamage > 0)
            {
                Danos.Add(ElementoPvM.Raio);
                if (creature.PhysicalDamage > 0 || creature.ColdDamage > 0)
                    Danos.Add(ElementoPvM.Vento);
            }
            if (creature.PoisonDamage > 0)
            {
                if (creature.Skills.Magery.Value > 30 || creature.Skills.Necromancy.Value > 0)
                    Danos.Add(ElementoPvM.Escuridao);
                Danos.Add(ElementoPvM.Agua);
            }
            if (creature.HasBreath)
            {
                if (creature.ColdDamage > 0)
                    Danos.Add(ElementoPvM.Gelo);
                else
                    Danos.Add(ElementoPvM.Fogo);
            }
            if (creature.Body.IsHuman)
                Danos.Add(ElementoPvM.Luz);
            return Danos;
        }

        

    }
}
