using Server.Fronteira.Talentos;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.Classes
{

    public class SkillClasse
    {
        public ushort Cap;
        public SkillName skill;

        public SkillClasse(SkillName n, ushort c)
        {
            this.Cap = c;
            this.skill = n;
        }
    }

    public class OpcaoTalentos
    {
        public Talento T1;
        public Talento T2;
        public Talento T3;

        public OpcaoTalentos(Talento t1, Talento t2, Talento t3)
        {
            this.T1 = t1;
            this.T2 = t2;
            this.T3 = t3;
        }
    }

    public class ClassePersonagem
    {
        public int ID;
        public string Nome;
        public int Icone;
        public string Desc;

        public Dictionary<SkillName, ushort> ClassSkills = new Dictionary<SkillName, ushort>();
        
        public ClassePersonagem(string nome, int icone, string descricao, params SkillClasse[] skills)
        {
            this.Nome = nome;
            foreach (var s in skills)
            {
                if (ClassSkills.ContainsKey(s.skill)) throw new Exception("Classe " + nome + " ja tinha a skill " + s.skill);
                ClassSkills.Add(s.skill, s.Cap);
            }
            this.Icone = icone;
            this.Desc = descricao;
        }

        public void ViraClasse(PlayerMobile player)
        {
            foreach(var skill in player.Skills)
            {
                if(ClassSkills.ContainsKey(skill.SkillName))
                {
                    skill.Cap = ClassSkills[skill.SkillName];
                } else
                {
                    skill.Cap = 0;
                }
            }
            player.Profession = ID;
            player.SendMessage("Voce agora e da classe " + this.Nome);
        }
    }
}
