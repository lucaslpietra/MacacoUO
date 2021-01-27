using System.Collections.Generic;
using System.Linq;

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

        public static List<ClassePersonagem> GetClasses()
        {
            return _classes.Values.ToList();
        }

        public static ClassePersonagem GetClasse(int id)
        {
            if (!_classes.ContainsKey(id))
                return null;
            return _classes[id];
        }

        private static void AddClass(ClassePersonagem classe)
        {
            var id = _classes.Count + 1;
            classe.ID = id;
            _classes[id] = classe;
        }

        static ClassDef()
        {
            if (_classes.Count != 0)
                return;

            AddClass(new ClassePersonagem("Guerreiro", 40344, "Nego parrudaum", new SkillName[] {
                SkillName.Tactics, SkillName.Anatomy, SkillName.Swords
            }));

            AddClass(new ClassePersonagem("Arqueiro", 40345, "Neguim dos tiro", new SkillName[] {
                SkillName.Archery, SkillName.Anatomy, SkillName.Tactics
            }));

            AddClass(new ClassePersonagem("Mago", 40346, "Mano das firebola", new SkillName[] {
                SkillName.Magery, SkillName.Meditation, SkillName.EvalInt
            }));

        }
    }
}
