
using System;
using Server.Regions;

namespace Server.Items
{
    public class Hearthstone : Item
    {
        // Initiate the starting variables to be used
        private DateTime i_lastused;
        private Point3D i_home;
        private Map i_homemap;
        private static readonly TimeSpan delay = TimeSpan.FromMinutes(30);
        private Timer hearth;
        private Mobile m;
        private Item i;

        // Setup the GM access/viewable variables for the item
        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime lastused
        {
            get { return i_lastused; }
            set { i_lastused = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D home
        {
            get { return i_home; }
            set { i_home = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map map
        {
            get { return i_homemap; }
            set { i_homemap = value; }
        }

        // starting the item itself
        [Constructable]
        public Hearthstone() : base(0x2AAA)
        {
            Weight = 1.0;
            Name = "pedra da lareira";
        }

        // this controls the list you see when you mouse-over the stone
        public override void GetProperties(ObjectPropertyList list)
        {
            // make sure the normal mouse-over props show up
            base.GetProperties(list);

            // initial variables for use only inside the property list
            TimeSpan timetouse = ((this.lastused + Server.Items.Hearthstone.delay) - DateTime.Now);
            string lisths;

            // determine the info the next-use display shows
            if (timetouse.Minutes > 0)
            {
                int min = timetouse.Minutes;
                lisths = String.Format("{0} minutos.", min.ToString());
            }
            else if (timetouse.Seconds > 0)
            {
                int sec = timetouse.Seconds;
                lisths = String.Format("{0} segundos.", sec.ToString());
            }
            else
            {
                lisths = ("agora");
            }

            // adding the next-use to the property list
            list.Add("Usavel em: {0}", lisths);

            // because we do not want the server spamming updates, slow down how fast the mouse-over info refreshes
            // Timer.DelayCall(TimeSpan.FromSeconds(1.0), new TimerCallback(InvalidateProperties));
        }

        public override void OnDoubleClick(Mobile from)
        {
            this.InvalidateProperties();
            i = this;
            m = from;

            if (!IsChildOf(from.Backpack)) // initiate the checks for the double-click to restrict use
            {
                from.SendMessage("Precisa estar em sua mochila.");
                return;
            }
            else if (!CheckHearth(m, i)) // execute common method for failure checks
            {
                return;
            }
            else if (from.Region.GetLogoutDelay(from) == TimeSpan.Zero && !(from.Region is HouseRegion))  // see if they are in an "inn" zone and mark if they are
            {
                this.home = from.Location;
                this.lastused = DateTime.Now;
                from.SendMessage("Voce salvou sua pedra aqui.");
                return;
            }
            else // or else teleport the person to their hearth
            {
                m.CantWalk = true;
                hearth = Timer.DelayCall(TimeSpan.FromSeconds(10.0), new TimerCallback(HearthTeleport));
                m.SendMessage("Use sua pedra para retornar para casa.");
            }
        }

        private void HearthTeleport()  // the teleporting method
        {
            if (CheckHearth(m, i))  // double check after "casting time" that its legal still
            {
                m.Map = map;
                m.Location = new Point3D(home);
                this.lastused = DateTime.Now;
                m.CantWalk = false;
            }
            else  // or else fail the check and deny successful use
            {
                m.CantWalk = false;
                m.SendMessage("Sua pedra nao conseguiu te teleportar !");
                return;
            }
        }

        private bool CheckHearth(Mobile m, Item i)  // the common checks to ensure valid use
        {
            if (m.Criminal)  // make sure they are not criminal
            {
                m.SendMessage("Voce e criminoso!");
                return false;
            }
            else if ((this.lastused + Server.Items.Hearthstone.delay) > DateTime.Now) // make sure its not too soon since last use
            {
                m.SendMessage("Aguarde a pedra carregar!");
                return false;
            }
            else if (Server.Spells.SpellHelper.CheckCombat(m)) // use a spell system check to make sure they are not in combat
            {
                m.SendMessage("Voce esta em combate!");
                return false;
            }
            else  // they are eligible to teleport
            {
                return true;
            }

        }

        public Hearthstone(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // generic info to write and save
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write((Point3D)i_home);
            writer.Write((DateTime)i_lastused);
            writer.Write((Map)i_homemap);
        }

        public override void Deserialize(GenericReader reader) // make sure proper variables are read
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            i_home = reader.ReadPoint3D();
            i_lastused = reader.ReadDateTime();
            i_homemap = reader.ReadMap();
        }
    }
}
