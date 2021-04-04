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
        public string Desc;

        public Dictionary<SkillName, ushort> ClassSkills = new Dictionary<SkillName, ushort>();
        
        public ClassePersonagem(string nome, int icone, string descricao, params SkillName[] skills)
        {
            this.Nome = nome;
            foreach (var s in skills)
                ClassSkills.Add(s, 100);
            this.Icone = icone;
            this.Desc = descricao;
        }
        public ClassePersonagem(string nome, int icone, string descricao, int identificador, params SkillName[] skills)
        {
            this.Nome = nome;
            foreach (var s in skills)
                ClassSkills.Add(s, 100);
            this.Icone = icone;
            this.Desc = descricao;
            this.ID = identificador;
        }


        public void ViraClasse(PlayerMobile player, ClassePersonagem classe)
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
            player.SendMessage("Voce agora e da classe " + classe.Nome);
        }
    }
}
