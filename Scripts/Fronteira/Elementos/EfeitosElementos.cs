using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.Fronteira.Elementos
{
    public class EfeitosElementos
    {
        private static Dictionary<ElementoPvM, string[]> _efeitos = new Dictionary<ElementoPvM, string[]>();

        public static string [] GetEfeitos(ElementoPvM elemento)
        {
            string[] efeitos;
            if (_efeitos.TryGetValue(elemento, out efeitos))
                return efeitos;

            switch(elemento)
            {
                case ElementoPvM.Fogo:
                    efeitos = new string[] {
                        "Dano de Fogo",
                        "Esquiva",
                        "LifeSteal Magico"
                    };
                    break;
                case ElementoPvM.Agua:
                    efeitos = new string[] {
                        "Dano Pocoes",
                        "Magic Resist",
                        "Dano Eletrico",
                    };
                    break;
                case ElementoPvM.Terra:
                    efeitos = new string[] {
                        "Resist e Dano de Venenos",
                        "Armadura",
                        "Dano Fisico"
                    };
                    break;
                case ElementoPvM.Raio:
                    efeitos = new string[] {
                        "Dano Eletrico",
                        "Dano Fisico",
                        "Esquiva"
                    };
                    break;
                case ElementoPvM.Luz:
                    efeitos = new string[] {
                        "Cura ao Atacar",
                        "Resistencia Magica",
                        "Armadura"
                    };
                    break;
                case ElementoPvM.Escuridao:
                    efeitos = new string[] {
                        "Dano Magias Proibidas",
                        "Resistencia Magica",
                        "LifeSteal Magico"
                    };
                    break;
                case ElementoPvM.Gelo:
                    efeitos = new string[] {
                        "Armadura",
                        "Dano Fisico",
                        "Dano Magias Frias"
                    };
                    break;
                case ElementoPvM.Vento:
                    efeitos = new string[] {
                        "Velocidade Ataque",
                        "Dano Fisico",
                        "Esquiva"
                    };
                    break;
                default:
                    efeitos = new string[] { };
                    break;
            }
            _efeitos[elemento] = efeitos;
            return efeitos;
        }
    }
}
