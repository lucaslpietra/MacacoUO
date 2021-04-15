using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.Skills
{
    public class Informacoes
    {

        private static Dictionary<SkillName, string> _infos = new Dictionary<SkillName, string>();

        static Informacoes()
        {
            _infos.Add(
                SkillName.Alchemy,
                @"Descricao de alquimista <br>
                 outra linha <br>
                 mais outra linha ai n tem br no final");
        }
    }
}
