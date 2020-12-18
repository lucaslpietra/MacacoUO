
namespace Server.Fronteira.RP.ClassesRP
{
    public class Ladino : ClasseRP
    {
        private static CapClasse Caps;

        public Ladino()
        {
            if(Caps == null)
            {
                Caps = new CapClasse();
                Caps.S80.Add(SkillName.Hiding);

                Caps.S70.Add(SkillName.Fencing);
              
                Caps.S70.Add(SkillName.Anatomy);
                Caps.S70.Add(SkillName.Hiding);
                Caps.S70.Add(SkillName.Lockpicking);

                Caps.S50.Add(SkillName.Poisoning);
                Caps.S50.Add(SkillName.Healing);
                Caps.S50.Add(SkillName.Tactics);
                Caps.S50.Add(SkillName.Archery);

                Caps.S50.Add(SkillName.RemoveTrap);
                Caps.S50.Add(SkillName.MagicResist);

                Caps.S40.Add(SkillName.Mining);
                Caps.S40.Add(SkillName.Alchemy);
                Caps.S40.Add(SkillName.Tailoring);
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
