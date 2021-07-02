using System;
using Server;

namespace Server.Items
{
	public class WaxUOVase : Item
	{

		[Constructable]
		public WaxUOVase() : base( 15283 )
		{
			Name = " Vaso do UO";
			Weight = 3.0;
			Hue = 0;
			Movable = true;
		}

		public WaxUOVase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
