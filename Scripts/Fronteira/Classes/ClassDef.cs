using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.Classes
{
    public static class ClassDef
    {
        public static SkillName[] SKILLS_PUBLICAS = new SkillName[] {
            SkillName.Hiding, SkillName.DetectHidden
        };

        private static Dictionary<int, ClassePersonagem> _classes = new Dictionary<int, ClassePersonagem>();

        public static void _add(int ID, ClassePersonagem classe)
        {
            _classes[ID] = classe;
        }

        static ClassDef()
        {
            if (_classes.Count != 0)
                return;

            _classes.Add(1, new ClassePersonagem(1, "Guerreiro", new SkillName[] {
                SkillName.Tactics, SkillName.Anatomy, SkillName.Healing
            }));
        }
    }
}
