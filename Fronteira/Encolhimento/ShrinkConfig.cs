#region AuthorHeader
//
//	Shrink System version 2.0, by Xanthos
//
//
#endregion AuthorHeader
using System;
using System.IO;  
using System.Xml;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using Leilaum.Utilities;

namespace Shrink.ShrinkSystem
{
	// This file is for configuration of the Shrink System.  It is advised
	// that you DO NOT edit this file, instead place ShrinkConfig.xml in the 
	// RunUO/Data directory and modify the values there to configure the system
	// without changing code.  This allows you to take updates to the system
	// without losing your specific configuration settings.

	public class ShrinkConfig
	{
		public enum BlessStatus
		{
			All,		// All shrink items are blessed
			BondedOnly,	// Only shrink items for bonded pets are blessed
			None		// No shrink items are blessed
		}

		public static bool PetAsStatuette = true;		// Deed or statuette form
		public static bool AllowLocking = false;		// Allow players to lock the shrunken pet or not
		public static bool ShowPetDetails = true;		// Show stats and skills on the properties of the shrunken pet
		public static double ShrunkenWeight = 10.0;
		public static bool BlessedLeash = true;
		public static BlessStatus LootStatus = BlessStatus.None;	// How the shruken pet should be as loot
		public static double TamingRequired = 0;		// set to zero for no skill requirement to use shrink tools
		public static int ShrinkCharges = -1;			// set to -1 for infinite uses

		public static Type[] PackAnimals = new Type []
		{
			typeof(Server.Mobiles.PackHorse),
			typeof(Server.Mobiles.PackLlama),
			typeof(Server.Mobiles.Beetle),
		};

		private const string kConfigFile = @"Data/ShrinkConfig.xml";
		private const string kConfigName = "ShrinkSystem";

		public static void Initialize()
		{
			Element element = ConfigParser.GetConfig( kConfigFile, kConfigName );

			if ( null == element || element.ChildElements.Count <= 0 )
				return;

			double tempDouble;
			bool tempBool;
			int tempInt;
			Type[] tempTypeArray;

			foreach( Element child in element.ChildElements ) 
			{
				if ( child.TagName == "PetAsStatuette" && child.GetBoolValue( out tempBool ))
					PetAsStatuette = tempBool;
				
				else if ( child.TagName == "AllowLocking" && child.GetBoolValue( out tempBool ))
					AllowLocking = tempBool;
				
				else if ( child.TagName == "ShowPetDetails" && child.GetBoolValue( out tempBool ))
					ShowPetDetails = tempBool;
				
				else if ( child.TagName == "ShrunkenWeight" && child.GetDoubleValue( out tempDouble ))
					ShrunkenWeight = tempDouble;
				
				else if ( child.TagName == "BlessedLeash" && child.GetBoolValue( out tempBool ))
					ShowPetDetails = tempBool;
				
				else if ( child.TagName == "LootStatus" && null != child.Text && "" != child.Text )
				{
					if ( "BlessStatus.All" == child.Text )
						LootStatus = BlessStatus.All;
					else if ( "BlessStatus.BondedOnly" == child.Text )
						LootStatus = BlessStatus.BondedOnly;
					else if ( "BlessStatus.None" == child.Text )
						LootStatus = BlessStatus.None;
				}
				else if ( child.TagName == "TamingRequired" && child.GetIntValue( out tempInt ))
					TamingRequired = tempInt;
				
				else if ( child.TagName == "ShrinkCharges" && child.GetIntValue( out tempInt ))
					ShrinkCharges = tempInt;
				
				else if ( child.TagName == "PackAnimals" && child.GetArray( out tempTypeArray ))
					PackAnimals = tempTypeArray;
			}
		}
	}
}
