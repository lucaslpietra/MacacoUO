using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.Classes
{
    public class ClassePersonagem
    {

        public int ID;
        public string Nome;

        public HashSet<SkillName> Skills = new HashSet<SkillName>();

        public ClassePersonagem(int id, string nome, params SkillName[] skills)
        {
            this.ID = id;
            this.Nome = nome;
            foreach (var s in skills)
                Skills.Add(s);
        }

        public void ViraClasse(PlayerMobile player, ClassePersonagem classe)
        {
            foreach(var skill in player.Skills)
            {
                if(Skills.Contains(skill.SkillName))
                {
                    skill.Cap = 100;
                } else
                {
                    skill.Cap = 0;
                }
            }
            player.Profession = ID;
            player.SendMessage("Voce agora e da classe " + classe.Nome);
        }
    }
}
