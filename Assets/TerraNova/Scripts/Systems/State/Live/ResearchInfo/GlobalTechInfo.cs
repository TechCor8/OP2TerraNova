using DotNetMissionSDK;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TerraNova.Systems.Constants;

namespace TerraNova.Systems.State.Live.ResearchInfo
{
	/// <summary>
	/// Contains global tech info.
	/// </summary>
	public class GlobalTechInfo
	{
		public int TechID											{ get; private set; }
		public TechCategory Category								{ get; private set; }
		//public int TechLevel										{ get; private set; }
		public int PlymouthCost										{ get; private set; }
		public int EdenCost											{ get; private set; }
		public int MaxScientists									{ get; private set; }
		public LabType LabType										{ get; private set; }
		//public int playerHasTech									{ get; private set; }
		public string TechName										{ get; private set; }
		public string Description									{ get; private set; }
		public string Teaser										{ get; private set; }
		public string ImproveDescription							{ get; private set; }

		public ReadOnlyCollection<int> RequiredTechIDs				{ get; private set; }
		public ReadOnlyCollection<UnitPropertyInfo> UnitProperties	{ get; private set; }

		
		public GlobalTechInfo(int techID, TechCategory category, int plymouthCost, int edenCost, int maxScientists, LabType labType,
			string techName, string description, string teaser, string improveDescription, IList<int> requiredTechIDs, IList<UnitPropertyInfo> unitProperties)
		{
			TechID					= techID;
			Category				= category;
			PlymouthCost			= plymouthCost;
			EdenCost				= edenCost;
			MaxScientists			= maxScientists;
			LabType					= labType;
			TechName				= techName;
			Description				= description;
			Teaser					= teaser;
			ImproveDescription		= improveDescription;

			RequiredTechIDs			= new ReadOnlyCollection<int>(requiredTechIDs);
			UnitProperties			= new ReadOnlyCollection<UnitPropertyInfo>(unitProperties);
		}
	}

	public class UnitPropertyInfo
	{
		public map_id UnitType			{ get; private set; }
		public UnitProperty Property	{ get; private set; }
		public int Value				{ get; private set; }


		public UnitPropertyInfo(map_id unitType, UnitProperty property, int value)
		{
			UnitType = unitType;
			Property = property;
			Value = value;
		}
	}
}
