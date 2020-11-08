using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.RP
{
    class TeleporterRP : Teleporter
    {
        public override bool OnMoveOver(Mobile m)
        {
            if(!m.RP)
            {
                m.SendMessage("Parece que voce nao pode entrar aqui...");
                return false;
            }
            return base.OnMoveOver(m);
        }
    }
}
