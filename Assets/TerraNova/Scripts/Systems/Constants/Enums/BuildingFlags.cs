namespace TerraNova.Systems.Constants
{
	[System.Flags]
	public enum BuildingFlags
	{
		Tubes = 1,
		StructureKit = 2,
		DockingAll = 4,
		DockingTruck = 8,
		DockingConvec = 16,
		DockingEvac = 32,
		CanBeAutoTargeted = 64
	}
}