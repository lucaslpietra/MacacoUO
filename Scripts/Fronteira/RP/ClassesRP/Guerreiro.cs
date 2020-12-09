
namespace Server.Fronteira.RP.ClassesRP
{
    public class Guerreiro : ClasseRP
    {
        private static CapClasse Caps;

        public Guerreiro()
        {
            if(Caps == null)
            {
                Caps = new CapClasse();
                Caps.S70.Add(SkillName.Swords);
                Caps.S70.Add(SkillName.Tactics);
                Caps.S70.Add(SkillName.Macing);
                Caps.S70.Add(SkillName.Anatomy);
                Caps.S60.Add(SkillName.Parry);
                Caps.S70.Add(SkillName.Fencing);
                Caps.S70.Add(SkillName.Healing);
                Caps.S70.Add(SkillName.MagicResist);

                Caps.S50.Add(SkillName.ArmsLore);
                Caps.S70.Add(SkillName.Mining);
                Caps.S70.Add(SkillName.Blacksmith);
                Caps.S50.Add(SkillName.Tinkering);
            }
        }

        public override CapClasse GetCaps()
        {
            return Caps;
        }

        public override string GetNome()
        {
            return "Guerreiro";
        }

        public override string Desc()
        {
            return "Tankudinho parrudinho<br>Manda legal com todas armas";
        }
    }
}
