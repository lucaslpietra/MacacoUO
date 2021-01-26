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
        public int Icone;

        public HashSet<SkillName> ClassSkills = new HashSet<SkillName>();

        public ClassePersonagem(string nome, int icone, params SkillName[] skills)
        {
            this.Nome = nome;
            foreach (var s in skills)
                ClassSkills.Add(s);
            this.Icone = icone;
        }

        public void ViraClasse(PlayerMobile player, ClassePersonagem classe)
        {
            foreach(var skill in player.Skills)
            {
                if(ClassSkills.Contains(skill.SkillName))
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
