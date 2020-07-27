using DotNetMissionSDK;
using OP2UtilityDotNet;
using System.Collections.Generic;
using System.IO;
using TerraNova.Systems.State.Live.UnitTypeInfo;

namespace TerraNova.Systems.Constants
{
	/// <summary>
	/// Parses sheets.vol data into class objects.
	/// </summary>
	public static class SheetData
	{
		/// <summary>
		/// Read building sheet.
		/// </summary>
		public static Dictionary<map_id, GlobalStructureInfo> ReadBuildingSheet()
		{
			string[][] buildingSheet = ReadSheet("building.txt");
			if (buildingSheet == null)
				return null;

			Dictionary<map_id, GlobalStructureInfo> infoRecords = new Dictionary<map_id, GlobalStructureInfo>(buildingSheet.GetLength(0));

			// Read rows
			for (int i=0; i < buildingSheet.GetLength(0); ++i)
			{
				string[] row = buildingSheet[i];

				string codeName			= row[0];				// Code_Name
				string produceName		= row[1];				// Produce List Name
				string buildingName		= row[2];				// Building Name
				int sizeX				= ReadInt(row[3]);		// X-size
				int sizeY				= ReadInt(row[4]);		// y-size
				ColonyType owner		= ReadOwner(row[5]);	// Owner [P/E/B]
				int hitPoints			= ReadInt(row[6]);		// Hit Points
				int armorClass			= ReadInt(row[7]);		// Armor Class
				bool border				= ReadBool(row[8]);		// Border [Y/N]
				int commonOreCost		= ReadInt(row[9]);		// Common Ore Cost
				int rareOreCost			= ReadInt(row[10]);		// Rare Ore Cost
				bool structureKit		= ReadBool(row[11]);	// Structure Kit [Y/N]
				int buildingPoints		= ReadInt(row[12]);		// Building Points
				int commonRubble		= ReadInt(row[13]);		// Normal Rubble (tile count)
				int rareRubble			= ReadInt(row[14]);		// Rare Rubble (tile count)
				int powerRequired		= ReadInt(row[15]);		// Power Required
				int workersRequired		= ReadInt(row[16]);		// Workers Required
				int scientistsRequired	= ReadInt(row[17]);		// Scientists Required
				int sightRange			= ReadInt(row[18]);		// Sight Range
				bool dockingAll			= ReadBool(row[19]);	// Docking All [Y/N]
				bool dockingTruck		= ReadBool(row[20]);	// Docking Truck [Y/N]
				bool dockingConvec		= ReadBool(row[21]);	// Docking Convec [Y/N]
				bool dockingEvac		= ReadBool(row[22]);	// Docking Evac [Y/N]
				int dockingLocationE	= ReadInt(row[23]);		// Docking Loc E
				int dockingLocationP	= ReadInt(row[24]);		// Docking Loc P
				int storageCapacity		= ReadInt(row[25]);		// Storage Capacity
				int productionCapacity	= ReadInt(row[26]);		// Production Capacity
				int storageBays			= ReadInt(row[27]);		// Storage bays
				bool autoTargeted		= ReadBool(row[28]);	// Can be autotargeted [Y/N]
				int explosionSize		= ReadInt(row[29]);		// Explosion size
				int resourcePriority	= ReadInt(row[30]);		// Resource priority
				int researchTopic		= ReadInt(row[31]);		// Research topic (leading zero, tech ID)

				map_id unitTypeId = GetMapIdFromCodeName(codeName);

				BuildingFlags buildingFlags = 0;

				if (structureKit) buildingFlags |= BuildingFlags.StructureKit;
				if (dockingAll) buildingFlags |= BuildingFlags.DockingAll;
				if (dockingTruck) buildingFlags |= BuildingFlags.DockingTruck;
				if (dockingConvec) buildingFlags |= BuildingFlags.DockingConvec;
				if (dockingEvac) buildingFlags |= BuildingFlags.DockingEvac;
				if (autoTargeted) buildingFlags |= BuildingFlags.CanBeAutoTargeted;

				infoRecords[unitTypeId] = new GlobalStructureInfo(unitTypeId, researchTopic, buildingName, codeName,
					hitPoints, armorClass, commonOreCost, rareOreCost, buildingPoints, sightRange,
					produceName, sizeX, sizeY, buildingFlags, resourcePriority, rareRubble, commonRubble, dockingLocationE, dockingLocationP,
					powerRequired, workersRequired, scientistsRequired, storageCapacity, productionCapacity, storageBays, explosionSize);
			}

			return infoRecords;
		}

		/// <summary>
		/// Read vehicle sheet.
		/// </summary>
		public static Dictionary<map_id, GlobalVehicleInfo> ReadVehicleSheet()
		{
			string[][] vehicleSheet = ReadSheet("vehicles.txt");
			if (vehicleSheet == null)
				return null;

			Dictionary<map_id, GlobalVehicleInfo> infoRecords = new Dictionary<map_id, GlobalVehicleInfo>(vehicleSheet.GetLength(0));

			// Read rows
			for (int i=0; i < vehicleSheet.GetLength(0); ++i)
			{
				string[] row = vehicleSheet[i];

				string codeName			= row[0];				// Code_Name
				string unitName			= row[1];				// Unit Name
				string strTrackType		= row[2];				// Track Type [W/T/L/M]
				ColonyType owner		= ReadOwner(row[3]);	// Owner [P/E/B]
				int hitPoints			= ReadInt(row[4]);		// Hit Points
				int armorClass			= ReadInt(row[5]);		// Armor Class
				int movePoints			= ReadInt(row[6]);		// Move Points
				int turnRate			= ReadInt(row[7]);		// Turn Rate
				int commonOreCost		= ReadInt(row[8]);		// Common Ore Cost
				int rareOreCost			= ReadInt(row[9]);		// Rare Ore Cost
				int buildTime			= ReadInt(row[10]);		// Build Time
				bool vehicleFactory		= ReadBool(row[11]);	// VF [Y/N]
				bool arachnidFactory	= ReadBool(row[12]);	// AF [Y/N]
				int cargoCapacity		= ReadInt(row[13]);		// Cargo Capacity
				int sightRange			= ReadInt(row[14]);		// Sight Range
				bool weaponEnabled		= ReadBool(row[15]);	// Weapon Enabled [Y/N]
				int researchTopic		= ReadInt(row[16]);		// Research topic (leading zero, tech ID)
				int productionRate		= ReadInt(row[17]);		// Production Rate
				int repairAmount		= ReadInt(row[18]);		// Repair Amount

				map_id unitTypeId = GetMapIdFromCodeName(codeName);

				VehicleFlags vehicleFlags = 0;

				if (vehicleFactory) vehicleFlags |= VehicleFlags.VehicleFactory;
				if (arachnidFactory) vehicleFlags |= VehicleFlags.ArachnidFactory;
				if (weaponEnabled) vehicleFlags |= VehicleFlags.WeaponEnabled;

				TrackType trackType = TrackType.Wheeled;

				switch (strTrackType)
				{
					case "W":	trackType = TrackType.Wheeled;	break;
					case "T":	trackType = TrackType.Tracked;	break;
					case "L":	trackType = TrackType.Legged;	break;
					case "M":	trackType = TrackType.Miner;	break;
				}

				infoRecords[unitTypeId] = new GlobalVehicleInfo(unitTypeId, researchTopic, unitName, codeName,
					hitPoints, armorClass, commonOreCost, rareOreCost, buildTime, sightRange,
					vehicleFlags, trackType, movePoints, turnRate, cargoCapacity, productionRate, repairAmount
					);
			}

			return infoRecords;
		}

		/// <summary>
		/// Read weapon sheet.
		/// </summary>
		public static Dictionary<map_id, GlobalWeaponInfo> ReadWeaponSheet()
		{
			string[][] weaponSheet = ReadSheet("weapons.txt");
			if (weaponSheet == null)
				return null;

			Dictionary<map_id, GlobalWeaponInfo> infoRecords = new Dictionary<map_id, GlobalWeaponInfo>(weaponSheet.GetLength(0));

			// Read rows
			for (int i=0; i < weaponSheet.GetLength(0); ++i)
			{
				string[] row = weaponSheet[i];

				string codeName			= row[0];				// Code_Name
				string weaponName		= row[1];				// Weapon Name
				int damageRadius		= ReadInt(row[2]);		// Damage Radius
				int pixelsSkippedFiring	= ReadInt(row[3]);		// Pixels skipped when firing
				int weaponRange			= ReadInt(row[4]);		// Weapon range in tiles
				int turretTurnRate		= ReadInt(row[5]);		// Turret turn rate
				ColonyType owner		= ReadOwner(row[6]);	// Owner [P/E/B/G]
				int concussionDamage	= ReadInt(row[7]);		// Concussion damage
				int penetrationDamage	= ReadInt(row[8]);		// Penetration damage
				int commonOreCost		= ReadInt(row[9]);		// Common Ore Cost
				int rareOreCost			= ReadInt(row[10]);		// Rare Ore Cost
				int buildTime			= ReadInt(row[11]);		// Build Time
				int reloadTime			= ReadInt(row[12]);		// Reload Time
				int researchTopic		= ReadInt(row[13]);		// Research topic (leading zero, tech ID)

				map_id unitTypeId = GetMapIdFromCodeName(codeName);

				infoRecords[unitTypeId] = new GlobalWeaponInfo(unitTypeId, researchTopic, weaponName, codeName,
					/*hitPoints, armorClass,*/ commonOreCost, rareOreCost, buildTime, /*sightRange,*/
					damageRadius, pixelsSkippedFiring, weaponRange, turretTurnRate, concussionDamage, penetrationDamage, reloadTime
					);
			}

			return infoRecords;
		}

		/// <summary>
		/// Read weapon sheet.
		/// </summary>
		public static Dictionary<map_id, GlobalUnitInfo> ReadStarshipSheet()
		{
			string[][] starshipSheet = ReadSheet("space.txt");
			if (starshipSheet == null)
				return null;

			Dictionary<map_id, GlobalUnitInfo> infoRecords = new Dictionary<map_id, GlobalUnitInfo>(starshipSheet.GetLength(0));

			// Read rows
			for (int i=0; i < starshipSheet.GetLength(0); ++i)
			{
				string[] row = starshipSheet[i];

				string codeName			= row[0];				// Code_Name
				string unitName			= row[1];				// Unit Name
				ColonyType owner		= ReadOwner(row[2]);	// Owner [P/E/B]
				int commonOreCost		= ReadInt(row[3]);		// Common Ore Cost
				int rareOreCost			= ReadInt(row[4]);		// Rare Ore Cost
				int buildTime			= ReadInt(row[5]);		// Build Time
				int researchTopic		= ReadInt(row[6]);		// Research topic (leading zero, tech ID)

				map_id unitTypeId = GetMapIdFromCodeName(codeName);

				infoRecords[unitTypeId] = new GlobalUnitInfo(unitTypeId, researchTopic, unitName, codeName,
					0,0, commonOreCost, rareOreCost, buildTime, 0
					);
			}

			return infoRecords;

			//string[][] minesSheet = ReadSheet("mines.txt");
			//string[][] moraleSheet = ReadSheet("morale.txt");
		}

		private static string[][] ReadSheet(string sheetPath)
		{
			List<string[]> rows = new List<string[]>();
			
			try
			{
				using (ResourceManager resourceManager = new ResourceManager("."))
				{
					using (Stream stream = resourceManager.GetResourceStream(sheetPath, true))
					using (StreamReader reader = new StreamReader(stream))
					{
						reader.ReadLine(); // Discard header line
						
						string line = reader.ReadLine();

						// Skip header lines that continue on the next line
						while (line.Length == 0 || line[0] == '\t')
							line = reader.ReadLine();

						// Read rows
						do
						{
							// Parse row
							rows.Add(line.Split('\t'));

							// Read next line
							line = reader.ReadLine();
						}
						while (!string.IsNullOrEmpty(line));
					}
				}
			}
			catch (System.Exception)
			{
				return null;
			}

			return rows.ToArray();
		}

		private static bool ReadBool(string val)
		{
			return val == "Y";
		}

		private static int ReadInt(string val)
		{
			int result;
			int.TryParse(val, out result);

			return result;
		}

		private static ColonyType ReadOwner(string val)
		{
			switch (val)
			{
				case "G": return ColonyType.Gaia;
				case "E": return ColonyType.Eden;
				case "P": return ColonyType.Plymouth;
				case "B": return ColonyType.Both;
			}

			return ColonyType.Gaia;
		}

		private static map_id GetMapIdFromCodeName(string codeName)
		{
			switch (codeName)
			{
				// Structures
				case "COMMAND":					return map_id.CommandCenter;
				case "TOKAMAK": 				return map_id.Tokamak;
				case "ASE": 					return map_id.MHDGenerator;
				case "GEOTHERMAL": 				return map_id.GeothermalPlant;
				case "SOLAR": 					return map_id.SolarPowerArray;
				case "FACT_STRUCTURE": 			return map_id.StructureFactory;
				case "TOWER_GUARD": 			return map_id.GuardPost;
				case "SMELTER": 				return map_id.CommonOreSmelter;
				case "STORAGE_ORE": 			return map_id.CommonStorage;
				case "LAB_ADV": 				return map_id.AdvancedLab;
				case "LAB_STANDARD":			return map_id.StandardLab;
				case "LAB": 					return map_id.BasicLab;
				case "SMELTER_ADV": 			return map_id.RareOreSmelter;
				case "STORAGE_RARE": 			return map_id.RareStorage;
				case "FACT_VEHICLE": 			return map_id.VehicleFactory;
				case "UNIVERSITY": 				return map_id.University;
				case "NURSERY": 				return map_id.Nursery;
				case "AGRIDOME": 				return map_id.Agridome;
				case "ROBOT_COMMAND": 			return map_id.RobotCommand;
				case "SPACEPORT": 				return map_id.Spaceport;
				case "MED_CENTER": 				return map_id.MedicalCenter;
				case "FACT_ANDROID": 			return map_id.ArachnidFactory;
				case "DIRT": 					return map_id.DIRT;
				case "GARAGE": 					return map_id.Garage;
				case "GORF": 					return map_id.GORF;
				case "RESIDENCE_E": 			return map_id.AdvancedResidence;
				case "RESIDENCE	0": 			return map_id.Residence;
				case "RESIDENCE_P": 			return map_id.ReinforcedResidence;
				case "OBSERVATORY": 			return map_id.Observatory;
				case "METEOR_DEF": 				return map_id.MeteorDefense;
				case "RECREATION": 				return map_id.RecreationFacility;
				case "FACT_LUXURY": 			return map_id.ConsumerFactory;
				case "FORUM": 					return map_id.Forum;
				case "TRADE": 					return map_id.TradeCenter;
				case "TOWER_LIGHT": 			return map_id.LightTower;
				case "MAGMA_WELL": 				return map_id.MagmaWell;
				case "MINE": 					return map_id.CommonOreMine;
				case "MINE_ADV": 				return map_id.RareOreMine;
				case "TUBE": 					return map_id.Tube;
				case "WALL": 					return map_id.Wall;
				case "WALL_LAVA": 				return map_id.LavaWall;
				case "WALL_MICROBE": 			return map_id.MicrobeWall;

				// Vehicles
				case "GEO_METRO":				return map_id.GeoCon;
				case "MINER":					return map_id.RoboMiner;
				case "BIG_TANK":				return map_id.Tiger;
				case "BIG_TRUCK":				return map_id.CargoTruck;
				case "MED_TANK":				return map_id.Panther;
				case "LIGHT_TANK":				return map_id.Lynx;
				case "CON_TRUCK":				return map_id.ConVec;
				case "WALL_TRUCK":				return map_id.Earthworker;
				case "REPAIR_TRUCK":			return map_id.RepairVehicle;
				case "BULL_DOZER":				return map_id.RoboDozer;
				case "MOBILE_HOME":				return map_id.EvacuationTransport;
				case "SURVEYOR":				return map_id.RoboSurveyor;
				case "SCOUT":					return map_id.Scout;
				case "SCORPION":				return map_id.Scorpion;
				case "SCORPION_PACK":			return map_id.Scorpion3Pack;
				case "SPIDER":					return map_id.Spider;
				case "SPIDER_PACK":				return map_id.Spider3Pack;

				// Weapons
				case "LASER":					return map_id.Laser;
				case "MICROWAVE":				return map_id.Microwave;
				case "FOAM":					return map_id.Stickyfoam;
				case "RAIL_GUN":				return map_id.RailGun;
				case "EMP":						return map_id.EMP;
				case "CANNON":					return map_id.RPG;
				case "TURRET_DESTRUCT":			return map_id.Starflare2;
				case "SELF_DESTRUCT":			return map_id.Starflare;
				case "SPAM":					return map_id.ESG;
				case "TURRET_DESTRUCT_ADV":		return map_id.Supernova2;
				case "SELF_DESTRUCT_ADV":		return map_id.Supernova;
				case "ACID":					return map_id.AcidCloud;
				case "THORS_HAMMER":			return map_id.ThorsHammer;
				case "SCORPION_WEAPON":			return map_id.EnergyCannon;
				case "EMP_BIG":					return map_id.Blast;
				case "SHUTTLE":					return map_id.InterColonyShuttle;
				case "MAGMA_VENT":				return map_id.MagmaVent;
				case "FUMAROLE":				return map_id.Fumarole;
				case "BEACON":					return map_id.MiningBeacon;
				case "AMBIENT_ANIM":			return map_id.PrettyArt; // Not sure what this is. Assuming "PrettyArt" for now.
				case "EARTHQUAKE":				return map_id.Earthquake;
				case "ERUPTION":				return map_id.Eruption;
				case "LIGHTNING":				return map_id.Lightning;
				case "TORNADO":					return map_id.Vortex;
				case "METEOR":					return map_id.Meteor;
				case "SCUTTLE_DESTRUCT":		return map_id.NormalUnitExplosion;
				case "BLD_EXPL_SML":			return map_id.DisasterousBuildingExplosion;
				case "BLD_EXPL_MED":			return map_id.CatastrophicBuildingExplosion;
				case "BLD_EXPL_BIG":			return map_id.AtheistBuildingExplosion;

				// Starship
				case "EDWARD":					return map_id.EDWARDSatellite;
				case "SOLAR_SAT":				return map_id.SolarSatellite;
				case "SPACE_1":					return map_id.IonDriveModule;
				case "SPACE_2":					return map_id.FusionDriveModule;
				case "SPACE_3":					return map_id.CommandModule;
				case "SPACE_4":					return map_id.FuelingSystems;
				case "SPACE_5":					return map_id.HabitatRing;
				case "SPACE_6":					return map_id.SensorPackage;
				case "SPACE_7":					return map_id.Skydock;
				case "SPACE_8":					return map_id.StasisSystems;
				case "SPACE_9":					return map_id.OrbitalPackage;
				case "SPACE_10":				return map_id.PhoenixModule;
				case "SPACE_11":				return map_id.RareMetalsCargo;
				case "SPACE_12":				return map_id.CommonMetalsCargo;
				case "SPACE_13":				return map_id.FoodCargo;
				case "SPACE_14":				return map_id.EvacuationModule;
				case "SPACE_15":				return map_id.ChildrenModule;
				case "SULV":					return map_id.SULV;
				case "RLV":						return map_id.RLV;
				case "EMP_MISSILE":				return map_id.EMPMissile;

				case "LUX_1":					return map_id.ImpulseItems;
				case "LUX_2":					return map_id.Wares;
				case "LUX_3":					return map_id.LuxuryWares;
			}

			return map_id.None;
		}
	}
}