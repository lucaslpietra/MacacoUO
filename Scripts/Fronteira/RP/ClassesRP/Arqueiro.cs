
namespace Server.Fronteira.RP.ClassesRP
{
    public class Arqueiro : ClasseRP
    {
        private static CapClasse Caps;

        public Arqueiro()
        {
            if(Caps == null)
            {
                Caps = new CapClasse();
                Caps.S70.Add(SkillName.Archery);
                Caps.S70.Add(SkillName.ArmsLore);
                Caps.S70.Add(SkillName.Anatomy);
                Caps.S70.Add(SkillName.Hiding);

                Caps.S50.Add(SkillName.Healing);
                Caps.S50.Add(SkillName.Tactics);
                Caps.S50.Add(SkillName.Lumberjacking);
                Caps.S50.Add(SkillName.Tracking);
                Caps.S50.Add(SkillName.MagicResist);

                Caps.S40.Add(SkillName.Fletching);
                Caps.S40.Add(SkillName.Carpentry);
                Caps.S40.Add(SkillName.Fencing);
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
