
namespace Server.Fronteira.RP.ClassesRP
{
    public class Mercador : ClasseRP
    {
        private static CapClasse Caps;

        public Mercador()
        {
            if(Caps == null)
            {
                Caps = new CapClasse();
                Caps.S70.Add(SkillName.Blacksmith);
                Caps.S70.Add(SkillName.Carpentry);
                Caps.S70.Add(SkillName.Tinkering);
                Caps.S70.Add(SkillName.Tailoring);

                Caps.S50.Add(SkillName.Mining);
                Caps.S50.Add(SkillName.Wrestling);
                Caps.S50.Add(SkillName.Swords);
                Caps.S50.Add(SkillName.Macing);
                Caps.S50.Add(SkillName.Tactics);
                Caps.S50.Add(SkillName.Anatomy);
                Caps.S50.Add(SkillName.Lumberjacking);

                Caps.S40.Add(SkillName.MagicResist);
                Caps.S40.Add(SkillName.Fletching);
                Caps.S40.Add(SkillName.Hiding);
                Caps.S40.Add(SkillName.Healing);
            }
        }

        public override CapClasse GetCaps()
        {
            return Caps;
        }

        public override string GetNome()
        {
            return "Mago";
        }

        public override string Desc()
        {
            return "Tankudinho parrudinho<br>Manda legal com todas armas";
        }
    }
}
