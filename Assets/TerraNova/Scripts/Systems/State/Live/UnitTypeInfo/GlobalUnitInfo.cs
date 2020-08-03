using TerraNova.Systems.Constants;

namespace TerraNova.Systems.State.Live.UnitTypeInfo
{
	/// <summary>
	/// Contains global unit type info.
	/// </summary>
	public class GlobalUnitInfo
	{
		public map_id UnitType										{ get; private set; }

		/// <summary>
		/// Gets the unit's research topic.
		/// Returns the TechInfo array index NOT the techID.
		/// </summary>
		public int ResearchTopic									{ get; private set; }
		public string UnitName										{ get; private set; }
		public string CodeName										{ get; private set; }

		// Modifiable by research
		public int HitPoints										{ get; private set; }
		public int Armor											{ get; private set; }
		public int OreCost											{ get; private set; }
		public int RareOreCost										{ get; private set; }
		public int BuildTime										{ get; private set; }
		public int SightRange										{ get; private set; }
		
		
		public bool CanColonyUseUnit(bool isEden)
		{
			if (isEden)
				return (GetOwnerFlags() & ColonyType.Eden) != 0;
			else
				return (GetOwnerFlags() & ColonyType.Plymouth) != 0;
		}

		public ColonyType GetOwnerFlags()
		{
			switch (UnitType)
			{
				case map_id.CargoTruck:						return ColonyType.Both;
				case map_id.ConVec:							return ColonyType.Both;
				case map_id.Spider:							return ColonyType.Plymouth;
				case map_id.Scorpion:						return ColonyType.Plymouth;
				case map_id.Lynx:							return ColonyType.Both;
				case map_id.Panther:						return ColonyType.Both;
				case map_id.Tiger:							return ColonyType.Both;
				case map_id.RoboSurveyor:					return ColonyType.Both;
				case map_id.RoboMiner:						return ColonyType.Both;
				case map_id.GeoCon:							return ColonyType.Eden;
				case map_id.Scout:							return ColonyType.Both;
				case map_id.RoboDozer:						return ColonyType.Both;
				case map_id.EvacuationTransport:			return ColonyType.Both;
				case map_id.RepairVehicle:					return ColonyType.Eden;
				case map_id.Earthworker:					return ColonyType.Both;
				case map_id.SmallCapacityAirTransport:		return ColonyType.Gaia;

				case map_id.Tube:							return ColonyType.Both;
				case map_id.Wall:							return ColonyType.Both;
				case map_id.LavaWall:						return ColonyType.Plymouth;
				case map_id.MicrobeWall:					return ColonyType.Eden;

				case map_id.CommonOreMine:					return ColonyType.Both;
				case map_id.RareOreMine:					return ColonyType.Both;
				case map_id.GuardPost:						return ColonyType.Both;
				case map_id.LightTower:						return ColonyType.Both;
				case map_id.CommonStorage:					return ColonyType.Both;
				case map_id.RareStorage:					return ColonyType.Both;
				case map_id.Forum:							return ColonyType.Plymouth;
				case map_id.CommandCenter:					return ColonyType.Both;
				case map_id.MHDGenerator:					return ColonyType.Plymouth;
				case map_id.Residence:						return ColonyType.Both;
				case map_id.RobotCommand:					return ColonyType.Both;
				case map_id.TradeCenter:					return ColonyType.Both;
				case map_id.BasicLab:						return ColonyType.Both;
				case map_id.MedicalCenter:					return ColonyType.Both;
				case map_id.Nursery:						return ColonyType.Both;
				case map_id.SolarPowerArray:				return ColonyType.Both;
				case map_id.RecreationFacility:				return ColonyType.Both;
				case map_id.University:						return ColonyType.Both;
				case map_id.Agridome:						return ColonyType.Both;
				case map_id.DIRT:							return ColonyType.Both;
				case map_id.Garage:							return ColonyType.Both;
				case map_id.MagmaWell:						return ColonyType.Eden;
				case map_id.MeteorDefense:					return ColonyType.Eden;
				case map_id.GeothermalPlant:				return ColonyType.Eden;
				case map_id.ArachnidFactory:				return ColonyType.Plymouth;
				case map_id.ConsumerFactory:				return ColonyType.Eden;
				case map_id.StructureFactory:				return ColonyType.Both;
				case map_id.VehicleFactory:					return ColonyType.Both;
				case map_id.StandardLab:					return ColonyType.Both;
				case map_id.AdvancedLab:					return ColonyType.Both;
				case map_id.Observatory:					return ColonyType.Eden;
				case map_id.ReinforcedResidence:			return ColonyType.Plymouth;
				case map_id.AdvancedResidence:				return ColonyType.Eden;
				case map_id.CommonOreSmelter:				return ColonyType.Both;
				case map_id.Spaceport:						return ColonyType.Both;
				case map_id.RareOreSmelter:					return ColonyType.Both;
				case map_id.GORF:							return ColonyType.Both;
				case map_id.Tokamak:						return ColonyType.Both;

				case map_id.AcidCloud:						return ColonyType.Eden;
				case map_id.EMP:							return ColonyType.Both;
				case map_id.Laser:							return ColonyType.Eden;
				case map_id.Microwave:						return ColonyType.Plymouth;
				case map_id.RailGun:						return ColonyType.Eden;
				case map_id.RPG:							return ColonyType.Plymouth;
				case map_id.Starflare:						return ColonyType.Both;
				case map_id.Supernova:						return ColonyType.Plymouth;
				case map_id.Starflare2:						return ColonyType.Eden;
				case map_id.Supernova2:						return ColonyType.Plymouth;
				case map_id.ESG:							return ColonyType.Plymouth;
				case map_id.Stickyfoam:						return ColonyType.Plymouth;
				case map_id.ThorsHammer:					return ColonyType.Eden;
				case map_id.EnergyCannon:					return ColonyType.Plymouth;

				case map_id.EDWARDSatellite:				return ColonyType.Both;
				case map_id.SolarSatellite:					return ColonyType.Both;
				case map_id.IonDriveModule:					return ColonyType.Both;
				case map_id.FusionDriveModule:				return ColonyType.Both;
				case map_id.CommandModule:					return ColonyType.Both;
				case map_id.FuelingSystems:					return ColonyType.Both;
				case map_id.HabitatRing:					return ColonyType.Both;
				case map_id.SensorPackage:					return ColonyType.Both;
				case map_id.Skydock:						return ColonyType.Both;
				case map_id.StasisSystems:					return ColonyType.Both;
				case map_id.OrbitalPackage:					return ColonyType.Both;
				case map_id.PhoenixModule:					return ColonyType.Both;

				case map_id.RareMetalsCargo:				return ColonyType.Both;
				case map_id.CommonMetalsCargo:				return ColonyType.Both;
				case map_id.FoodCargo:						return ColonyType.Both;
				case map_id.EvacuationModule:				return ColonyType.Both;
				case map_id.ChildrenModule:					return ColonyType.Both;

				case map_id.SULV:							return ColonyType.Both;
				case map_id.RLV:							return ColonyType.Eden;
				case map_id.EMPMissile:						return ColonyType.Plymouth;

				case map_id.ImpulseItems:					return ColonyType.Eden;
				case map_id.Wares:							return ColonyType.Eden;
				case map_id.LuxuryWares:					return ColonyType.Eden;

				case map_id.Spider3Pack:					return ColonyType.Plymouth;
				case map_id.Scorpion3Pack:					return ColonyType.Plymouth;
			}

			return ColonyType.Gaia;
		}


		public GlobalUnitInfo(
			map_id unitTypeID,
			int researchTopic,
			string unitName,
			string codeName,

			int hitPoints,
			int armor,
			int oreCost,
			int rareOreCost,
			int buildTime,
			int sightRange)
		{
			UnitType		= unitTypeID;
			ResearchTopic	= researchTopic;
			UnitName		= unitName;
			CodeName		= codeName;

			HitPoints		= hitPoints;
			Armor			= armor;
			OreCost			= oreCost;
			RareOreCost		= rareOreCost;
			BuildTime		= buildTime;
			SightRange		= sightRange;
		}
	}
}
