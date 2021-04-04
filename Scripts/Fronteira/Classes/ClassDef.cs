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

            AddClass(new ClassePersonagem("Guerreiro", 40324,
               "Os Anteras existem muito antes de serem chamados de Anteras.<br>Desde o primeiro homem que derramou sangue por Kaludes que os livros sagrados são lidos e seguidos por esse seleto grupo de guerreiros.<br>O livro conta a historia do Deus dos Céus e todo seu percurso para conseguir trazer força e poder aos seus seguidores.<br>Os Aludes gostam de acreditar que Kaludes conseguiu sua mais difícil façanha, mas muitos acreditam que é a fé que dá poder aos Anteras.<br>Os segredos dos livros sagrados são passados de pais para filhos e filhas e assim essa classe segue uma linhagem pura.<br>Os guerreiros Anteras são fortes o suficiente para vestir armaduras pesadas, empunhar armas maciças e conjurar alguns feitiços provindos da certeza de que Kaludes os acompanha.<br>Todos os membros da classe se provam guerreiros habilidosos antes de serem designados a sua escolha final entre ser um Guerreiro da luz ou da sombra.<br>Ambos de extrema importância.<br><b>Anteras da Luz</b>: Os Anteras da luz são conhecidos pelo reino como protetores da justiça divina e guardadores de um conhecimento ancestral e secreto. Entre seus lemas, estão a de honrar o nome da família e proteger os mitos de Kaludes acima de todas as coisas.<br><b>Anteras da Sombra</b>: Os Anteras das sombras surgiram pouco depois dos Anteras da luz, mas eles não carregam o mesmo julgamento que eles.<br>São vistos pelos Aludes como desonrados, medíocres e aberrações. Os Aludes não podem estar mais errados, são mal compreendidos por serem regidos por Kalam, o Deus da Terra, por muitos não vistos nem como uma divindade.<br>Kalam foi peça fundamental nas conquistas de Kaludes.<br>Os Anteras das sombras foram criados pelos mesmos ensinamentos, pelo mesmo livro e pelos mesmos mestres.<br>A luz para controlar a luz, e a sombra para conter a sombra.",
               0, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Swords, SkillName.Focus, SkillName.Parry, SkillName.Macing
           }));

            /*
            AddClass(new ClassePersonagem("Anteras", 40324,
                "Os Anteras existem muito antes de serem chamados de Anteras.<br>Desde o primeiro homem que derramou sangue por Kaludes que os livros sagrados são lidos e seguidos por esse seleto grupo de guerreiros.<br>O livro conta a historia do Deus dos Céus e todo seu percurso para conseguir trazer força e poder aos seus seguidores.<br>Os Aludes gostam de acreditar que Kaludes conseguiu sua mais difícil façanha, mas muitos acreditam que é a fé que dá poder aos Anteras.<br>Os segredos dos livros sagrados são passados de pais para filhos e filhas e assim essa classe segue uma linhagem pura.<br>Os guerreiros Anteras são fortes o suficiente para vestir armaduras pesadas, empunhar armas maciças e conjurar alguns feitiços provindos da certeza de que Kaludes os acompanha.<br>Todos os membros da classe se provam guerreiros habilidosos antes de serem designados a sua escolha final entre ser um Guerreiro da luz ou da sombra.<br>Ambos de extrema importância.<br><b>Anteras da Luz</b>: Os Anteras da luz são conhecidos pelo reino como protetores da justiça divina e guardadores de um conhecimento ancestral e secreto. Entre seus lemas, estão a de honrar o nome da família e proteger os mitos de Kaludes acima de todas as coisas.<br><b>Anteras da Sombra</b>: Os Anteras das sombras surgiram pouco depois dos Anteras da luz, mas eles não carregam o mesmo julgamento que eles.<br>São vistos pelos Aludes como desonrados, medíocres e aberrações. Os Aludes não podem estar mais errados, são mal compreendidos por serem regidos por Kalam, o Deus da Terra, por muitos não vistos nem como uma divindade.<br>Kalam foi peça fundamental nas conquistas de Kaludes.<br>Os Anteras das sombras foram criados pelos mesmos ensinamentos, pelo mesmo livro e pelos mesmos mestres.<br>A luz para controlar a luz, e a sombra para conter a sombra.",
                0, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Swords, SkillName.Focus, SkillName.Parry, SkillName.Macing
            }));

            AddClass(new ClassePersonagem("Durah", 40344, "Lore Durah", 1, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Swords, SkillName.Focus, SkillName.Parry, SkillName.Macing
            }));

            AddClass(new ClassePersonagem("Caeros", 40325, "Lore Caeros", 2, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.AnimalTaming, SkillName.Veterinary
            }));

            AddClass(new ClassePersonagem("Lair", 40348, "Lore Lair", 3, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.AnimalTaming, SkillName.Veterinary
            }));

            AddClass(new ClassePersonagem("Eres", 40328, "Lore Eres", 4, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Spellweaving, SkillName.Meditation, SkillName.Focus, SkillName.DetectHidden
            }));

            AddClass(new ClassePersonagem("Gimur", 40346, "Lore Gimur", 5, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Spellweaving, SkillName.Meditation, SkillName.Focus, SkillName.DetectHidden
            }));

            AddClass(new ClassePersonagem("Vussaras", 40339, "Lore Vussaras", 6, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Fencing, SkillName.Macing, SkillName.Swords, SkillName.Focus
            }));

            AddClass(new ClassePersonagem("Sodah", 40354, "Lore Sodah", 7, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Fencing, SkillName.Macing, SkillName.Swords, SkillName.Focus
            }));

            AddClass(new ClassePersonagem("Famiros", 40329, "Lore Famiros", 8, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Musicianship, SkillName.Focus, SkillName.Fencing, SkillName.Meditation
            }));

            AddClass(new ClassePersonagem("Madur", 40349, "Lore Madur", 9, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Musicianship, SkillName.Focus, SkillName.Fencing, SkillName.Meditation
            }));

            AddClass(new ClassePersonagem("Lamares", 40331, "Lore Lamares", 10, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Archery, SkillName.RemoveTrap, SkillName.Focus
            }));

            AddClass(new ClassePersonagem("Berlan", 40342, "Lore Berlan", 11, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Archery, SkillName.RemoveTrap, SkillName.Focus
            }));

            AddClass(new ClassePersonagem("Dostaras", 40334, "Lore Dostaras", 12, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Hiding, SkillName.Stealth, SkillName.Fencing, SkillName.Tactics
            }));

            AddClass(new ClassePersonagem("Vomir", 40357, "Lore Vomir", 13, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Hiding, SkillName.Stealth, SkillName.Fencing, SkillName.Tactics
            }));

            AddClass(new ClassePersonagem("Pidaras", 40323, "Lore Pidaras", 14, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Macing, SkillName.Parry, SkillName.Focus
            }));

            AddClass(new ClassePersonagem("Colan", 40352, "Lore Colan", 15, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Macing, SkillName.Parry, SkillName.Focus
            }));

            AddClass(new ClassePersonagem("Amiros", 40333, "Lore Amiros", 16, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Magery, SkillName.Meditation
            }));

            AddClass(new ClassePersonagem("Ostah", 40343, "Lore Ostah", 17, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Magery, SkillName.Meditation
            }));

            AddClass(new ClassePersonagem("Idures", 40335, "Lore Idures", 18, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Swords, SkillName.Focus, SkillName.Tactics
            }));

            AddClass(new ClassePersonagem("Murah", 40350, "Lore Murah", 19, new SkillName[] {
                SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Swords, SkillName.Focus, SkillName.Tactics
            }));

            AddClass(new ClassePersonagem("Ysbares", 40338, "Lore Ysbares", 20, new SkillName[] {
                SkillName.Cooking, SkillName.Fishing, SkillName.Herding, SkillName.Inscribe, SkillName.Cartography, SkillName.Tailoring, SkillName.Fletching, SkillName.Carpentry, SkillName.Lumberjacking, SkillName.ArmsLore, SkillName.Alchemy, SkillName.Mining
            }));

            AddClass(new ClassePersonagem("Argur", 40341, "Lore Argur", 21, new SkillName[] {
                SkillName.Cooking, SkillName.Fishing, SkillName.Herding, SkillName.Inscribe, SkillName.Cartography, SkillName.Tailoring, SkillName.Fletching, SkillName.Carpentry, SkillName.Lumberjacking, SkillName.ArmsLore, SkillName.Alchemy, SkillName.Mining
            }));
            */
        }
    }
}
