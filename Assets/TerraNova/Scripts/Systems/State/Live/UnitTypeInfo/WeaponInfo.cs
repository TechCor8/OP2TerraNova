using DotNetMissionSDK;

namespace TerraNova.Systems.State.Live.UnitTypeInfo
{
	/// <summary>
	/// Contains player weapon type info.
	/// </summary>
	public class WeaponInfo : UnitInfoState
	{
		public int WeaponRange					{ get; private set; }
		public int TurretTurnRate				{ get; private set; }
		public int ConcussionDamage				{ get; private set; }
		public int PenetrationDamage			{ get; private set; }
		public int ReloadTime					{ get; private set; }


		public WeaponInfo(map_id unitTypeID,
			int playerID,
			int oreCost,
			int rareOreCost,
			int buildTime,
			
			int weaponRange,
			int turretTurnRate,
			int concussionDamage,
			int penetrationDamage,
			int reloadTime)
			: base(unitTypeID, playerID, 0, 0, oreCost, rareOreCost, buildTime, 0)
		{
			WeaponRange					= weaponRange;
			TurretTurnRate				= turretTurnRate;
			ConcussionDamage			= concussionDamage;
			PenetrationDamage			= penetrationDamage;
			ReloadTime					= reloadTime;
		}
	}
}
