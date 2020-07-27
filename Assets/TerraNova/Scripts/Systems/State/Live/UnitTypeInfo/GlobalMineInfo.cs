using DotNetMissionSDK;

namespace TerraNova.Systems.State.Live.UnitTypeInfo
{
	/// <summary>
	/// Contains global mining beacon info.
	/// </summary>
	public class GlobalMineInfo
	{
		public Yield MineYield					{ get; private set; }
		public Variant MineVariant				{ get; private set; }
		
		public int InitialYield					{ get; private set; } // %
		public int PeakTruck					{ get; private set; }
		public int PeakYield					{ get; private set; } // %
		public int MinTruck						{ get; private set; }
		public int MinYield						{ get; private set; } // %

		
		public GlobalMineInfo(
			Yield yield,
			Variant variant,

			int initialYield,
			int peakTruck,
			int peakYield,
			int minTruck,
			int minYield
			)
		{
			MineYield					= yield;
			MineVariant					= variant;

			InitialYield				= initialYield;
			PeakTruck					= peakTruck;
			PeakYield					= peakYield;
			MinTruck					= minTruck;
			MinYield					= minYield;
		}
	}

	public struct MineInfoKey
	{
		public Yield MineYield		{ get; private set; }
		public Variant MineVariant	{ get; private set; }

		public MineInfoKey(Yield yield, Variant variant)
		{
			MineYield = yield;
			MineVariant = variant;
		}
	}
}
