
namespace Server.Fronteira.RP.ClassesRP
{
    public class Mago : ClasseRP
    {
        private static CapClasse Caps;

        public Mago()
        {
            if(Caps == null)
            {
                Caps = new CapClasse();
                Caps.S70.Add(SkillName.Magery);
                Caps.S70.Add(SkillName.EvalInt);
                Caps.S70.Add(SkillName.Meditation);
                Caps.S70.Add(SkillName.MagicResist);

                Caps.S50.Add(SkillName.Macing);
                Caps.S50.Add(SkillName.Wrestling);
                Caps.S50.Add(SkillName.Inscribe);
                
                Caps.S50.Add(SkillName.Healing);
                Caps.S50.Add(SkillName.Anatomy);

                Caps.S40.Add(SkillName.Focus);
                Caps.S40.Add(SkillName.Herding);
                Caps.S40.Add(SkillName.Alchemy);
                Caps.S40.Add(SkillName.Imbuing);
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
