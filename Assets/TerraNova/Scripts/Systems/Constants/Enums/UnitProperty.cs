
namespace TerraNova.Systems.Constants
{
	public enum UnitProperty
	{
		None,
		Armor,					// Changes armor class of a unit
		Build_Points,			// Amount of time it takes to build a unit (Need to verify)
		Common_Required,		// Changes common ore requirement to build
		Concussion_Damage,		// Changes the concussion damage of a weapon turret
		Hit_Points,				// Changes the maximum hit points of a unit
		Improved,				// Increases amount of ore from a common or rare ore mine. Increases amount of ore salvaged from a GORF. Increases Ore gained from smelting at a common or rare ore smelter. Icreases the accuracy of an observatory/Meteor Defense program
		Move_Speed,				// Changes move speed of vehicle. The lower the number, the faster the vehicle moves.
		Penetration_Damage,		// Changes the penetration damage of a weapon turret
		Power_Required,			// Changes power requirement of a structure
		Production_Capacity,	// Increases the number of colonists serviced by a building. For a DIRT number of buildings serviced. For a Garage, the speed repairs are made. For a Power Plant, increases the power produced. For a Magma_Well, increases the amount or rare ore per truck load.
		Production_Rate,		// Changes the production rate or repair rate of a vehicle
		Rare_Required,			// Changes the amount of rare ore required to build a unit
		Rate_Of_Fire,			// Changes rate of fire of a weapon turret
		Sight_Range,			// Increases the light radius around a unit. For a weapon turret, increases the distance the turret can fire.
		Storage_bays,			// Number of storage bays in a factory or garage (Need to verify)
		Storage_Capacity,		// Alters time required to finish training scientists at the University
		Workers_Required,		// Changes the number of workers required
	}
}