using System;

namespace Server.Spells
{
    public class NinjaMove : SpecialMove
    {
        public double mult = 1;

        public override SkillName MoveSkill
        {
            get
            {
                return SkillName.Ninjitsu;
            }
        }
        public override void CheckGain(Mobile m)
        {
            m.CheckSkillMult(this.MoveSkill, this.RequiredSkill - 12.5, this.RequiredSkill + 37.5, this.mult);	//Per five on friday 02/16/07
        }
    }
}
