using DotNetMissionSDK;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TerraNova.Systems.Constants;

namespace TerraNova.Systems.State.Live.Population
{
	public sealed class MoraleState
	{
		public MoraleLevel MoraleType	{ get; private set; }
		public int RequiredPoints		{ get; private set; }
		public int PowerBonus			{ get; private set; }
		public int ResearchBonus		{ get; private set; }
		public int FactoryBonus			{ get; private set; }
		public int FoodBonus			{ get; private set; }
		public int DefectRate			{ get; private set; }
		public int FertilityRate		{ get; private set; }
		public int DeathRate			{ get; private set; }

		public MoraleState(MoraleLevel level, int requiredPoints, int powerBonus, int researchBonus, int factoryBonus, int foodBonus, int defectRate, int fertilityRate, int deathRate)
		{
			MoraleType = level;
			RequiredPoints = requiredPoints;
			PowerBonus = powerBonus;
			ResearchBonus = researchBonus;
			FactoryBonus = factoryBonus;
			FoodBonus = foodBonus;
			DefectRate = defectRate;
			FertilityRate = fertilityRate;
			DeathRate = deathRate;
		}
	}

	public sealed class GlobalMoraleInfo
	{
		public ReadOnlyCollection<MoraleState> MoraleStates				{ get; private set; }

		public ReadOnlyDictionary<MoraleEvent, int> MoraleEventValues	{ get; private set; }


		public GlobalMoraleInfo(IList<MoraleState> moraleStates, Dictionary<MoraleEvent, int> moraleEventValues)
		{
			MoraleStates = new ReadOnlyCollection<MoraleState>(moraleStates);
			MoraleEventValues = new ReadOnlyDictionary<MoraleEvent, int>(moraleEventValues);
		}
	}
}