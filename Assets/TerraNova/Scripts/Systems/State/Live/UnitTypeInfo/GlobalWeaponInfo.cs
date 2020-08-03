using TerraNova.Systems.Constants;

namespace TerraNova.Systems.State.Live.UnitTypeInfo
{
	/// <summary>
	/// Contains global weapon type info.
	/// </summary>
	public class GlobalWeaponInfo : GlobalUnitInfo
	{
		public int DamageRadius					{ get; private set; }
		public int PixelsSkippedWhenFiring		{ get; private set; }
		
		public int WeaponRangeInTiles			{ get; private set; }
		public int TurretTurnRate				{ get; private set; }
		public int ConcussionDamage				{ get; private set; }
		public int PenetrationDamage			{ get; private set; }
		public int ReloadTime					{ get; private set; }

		public int WeaponStrength
		{
			get
			{
				switch (UnitType)
				{
					case map_id.AcidCloud:				return 4;
					case map_id.EMP:					return 3;
					case map_id.Laser:					return 2;
					case map_id.Microwave:				return 2;
					case map_id.RailGun:				return 4;
					case map_id.RPG:					return 4;
					case map_id.Starflare:				return 2;
					case map_id.Supernova:				return 3;
					case map_id.Starflare2:				return 1;
					case map_id.Supernova2:				return 2;
					case map_id.ESG:					return 5;
					case map_id.Stickyfoam:				return 3;
					case map_id.ThorsHammer:			return 6;
					case map_id.EnergyCannon:			return 1;
				}

				return 0;
			}
		}

		public GlobalWeaponInfo(map_id unitTypeID,
			int researchTopic,
			string unitName,
			string codeName,

			int oreCost,
			int rareOreCost,
			int buildTime,

			int damageRadius,
			int pixelsSkippedWhenFiring,

			int weaponRange,
			int turretTurnRate,
			int concussionDamage,
			int penetrationDamage,
			int reloadTime
			)
			: base(unitTypeID, researchTopic, unitName, codeName,
				  0, 0, oreCost, rareOreCost, buildTime, 0)
		{
			DamageRadius				= damageRadius;
			PixelsSkippedWhenFiring		= pixelsSkippedWhenFiring;

			WeaponRangeInTiles			= weaponRange;
			TurretTurnRate				= turretTurnRate;
			ConcussionDamage			= concussionDamage;
			PenetrationDamage			= penetrationDamage;
			ReloadTime					= reloadTime;
		}
	}
}
