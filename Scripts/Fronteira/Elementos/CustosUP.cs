using Server.Items;
using System;
using System.Collections.Generic;

namespace Server.Fronteira.Elementos
{
    public class CustosUPElementos
    {
        public class Custo
        {
            Type type;
            short amt;

            public Custo(Type t, short s)
            {
                type = t;
                amt = s;
            }
        }

        private static Dictionary<ElementoPvM, Custo[]> _custos = new Dictionary<ElementoPvM, Custo[]>();

        public static Custo[] GetCustos(ElementoPvM elemento)
        {
            Custo[] custo;
            if (_custos.TryGetValue(elemento, out custo))
                return custo;

            switch (elemento)
            {
                case ElementoPvM.Fogo:
                    custo = new Custo[] {
                       new Custo(typeof(Ruby), 1),
                       new Custo(typeof(FireRuby), 1),
                    };
                    break;
                case ElementoPvM.Agua:
                    custo = new Custo[] {
                       new Custo(typeof(Amethyst), 1),
                       new Custo(typeof(Turquoise), 1),
                    };
                    break;
                case ElementoPvM.Terra:
                    custo = new Custo[] {
                       new Custo(typeof(Emerald), 1),
                       new Custo(typeof(PerfectEmerald), 1),
                    };
                    break;
                case ElementoPvM.Raio:
                    custo = new Custo[] {
                        new Custo(typeof(Amber), 1),
                        new Custo(typeof(BrilliantAmber), 1),
                    };
                    break;
                case ElementoPvM.Luz:
                    custo = new Custo[] {
                        new Custo(typeof(StarSapphire), 1),
                        new Custo(typeof(WhitePearl), 1),
                    };
                    break;
                case ElementoPvM.Escuridao:
                    custo = new Custo[] {
                       new Custo(typeof(Sapphire), 1),
                       new Custo(typeof(DarkSapphire), 1),
                    };
                    break;
                case ElementoPvM.Gelo:
                    custo = new Custo[] {
                       new Custo(typeof(Diamond), 1),
                       new Custo(typeof(BlueDiamond), 1),
                    };
                    break;
                case ElementoPvM.Vento:
                    custo = new Custo[] {
                       new Custo(typeof(Citrine), 1),
                       new Custo(typeof(EcruCitrine), 1),
                    };
                    break;
                default:
                    custo = new Custo[] { };
                    break;
            }
            _custos[elemento] = custo;
            return custo;
        }
    }
}
