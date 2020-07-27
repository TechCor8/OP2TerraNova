using DotNetMissionSDK;

namespace TerraNova.Systems.State.Live.UnitTypeInfo
{
	/// <summary>
	/// Contains player unit type info.
	/// </summary>
	public class UnitInfoState
	{
		public map_id UnitType						{ get; private set; }

		/// <summary>
		/// The player ID this info has been pulled from.
		/// </summary>
		public int PlayerID							{ get; private set; }

		public int HitPoints						{ get; private set; }
		public int Armor							{ get; private set; }
		public int OreCost							{ get; private set; }
		public int RareOreCost						{ get; private set; }
		public int BuildTime						{ get; private set; }
		public int SightRange						{ get; private set; }


		public UnitInfoState(map_id unitTypeID, int playerID, int hitPoints, int armor, int oreCost, int rareOreCost, int buildTime, int sightRange)
		{
			UnitType		= unitTypeID;

			PlayerID		= playerID;

			HitPoints		= hitPoints;
			Armor			= armor;
			OreCost			= oreCost;
			RareOreCost		= rareOreCost;
			BuildTime		= buildTime;
			SightRange		= sightRange;
		}
	}
}
