
namespace Server.Fronteira.RP.ClassesRP
{
    public class Bardo : ClasseRP
    {
        private static CapClasse Caps;

        public Bardo()
        {
            if(Caps == null)
            {
                Caps = new CapClasse();
                Caps.S70.Add(SkillName.Musicianship);
                Caps.S70.Add(SkillName.Peacemaking);
                Caps.S70.Add(SkillName.Provocation);
                Caps.S70.Add(SkillName.Discordance);

                Caps.S50.Add(SkillName.Magery);
                Caps.S50.Add(SkillName.Meditation);
                Caps.S50.Add(SkillName.MagicResist);
                Caps.S50.Add(SkillName.AnimalTaming);
                Caps.S50.Add(SkillName.AnimalLore);
                Caps.S50.Add(SkillName.Archery);
                Caps.S50.Add(SkillName.Tactics);

                Caps.S40.Add(SkillName.EvalInt);
                Caps.S40.Add(SkillName.Fletching);
                Caps.S40.Add(SkillName.Anatomy);
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
