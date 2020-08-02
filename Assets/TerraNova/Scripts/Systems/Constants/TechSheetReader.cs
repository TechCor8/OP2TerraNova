using DotNetMissionSDK;
using OP2UtilityDotNet;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TerraNova.Systems.State.Live.ResearchInfo;
using UnityEngine;

namespace TerraNova.Systems.Constants
{
	/// <summary>
	/// Parses tech sheets into class objects.
	/// </summary>
	public static class TechSheetReader
	{
		/// <summary>
		/// Reads a tech sheet from resources.
		/// </summary>
		public static Dictionary<int, GlobalTechInfo> ReadTechSheet(string fileName)
		{
			Dictionary<int, GlobalTechInfo> techData = new Dictionary<int, GlobalTechInfo>();
			
			try
			{
				using (ResourceManager resourceManager = new ResourceManager("."))
				{
					using (Stream stream = resourceManager.GetResourceStream(fileName, true))
					using (StreamReader reader = new StreamReader(stream))
					{
						List<List<string>> techValues = null;

						// Read lines
						string line = "";
						int lineIndex = 0;

						while ((line = reader.ReadLine()) != null)
						{
							++lineIndex;

							// Skip blank lines
							if (line.Length == 0)
								continue;

							// Skip comments
							if (line[0] == ';')
								continue;

							// Get line values
							List<string> values = new List<string>();
							
							// Get rid of starting whitespace
							line = line.TrimStart();

							while (line.Length > 0)
							{
								// Check if value is quoted. If it is, get the entire quote as a single value.
								if (line[0] == '\"')
								{
									line = line.Substring(1);
									int endIndex = line.IndexOf('\"');
									if (endIndex < 0)
									{
										Debug.LogError("Line " + lineIndex + ": Could not find end of quoted string.");
										break;
									}
									values.Add(line.Substring(0, endIndex));

									line = line.Substring(endIndex+1);
								}
								else
								{
									// Unquoted value. Parse until next whitespace.
									string value = new string(line.TakeWhile(c => !char.IsWhiteSpace(c)).ToArray());
									values.Add(value);

									line = line.Substring(value.Length);
								}

								// Get rid of starting whitespace
								line = line.TrimStart();
							}
							
							if (values.Count == 0)
								continue;

							// Look for the beginning of a tech section
							if (values[0].StartsWith("BEGIN_TECH"))
							{
								if (techValues != null)
								{
									Debug.LogError("Line " + lineIndex + ": Attempting to use BEGIN_TECH before using END_TECH on preceding BEGIN_TECH.");
									continue;
								}

								if (values.Count < 3)
								{
									Debug.LogError("Line " + lineIndex + ": BEGIN_TECH requires 3 values.");
									continue;
								}

								techValues = new List<List<string>>();
							}
							else if (values[0].StartsWith("END_TECH"))
							{
								if (techValues == null)
								{
									Debug.LogError("Line " + lineIndex + ": Attempting to use END_TECH without a preceding BEGIN_TECH.");
									continue;
								}

								GlobalTechInfo techInfo = ParseTechValues(techValues);
								techData[techInfo.TechID] = techInfo;

								techValues = null;
							}

							// Add values to current tech section
							if (techValues != null)
							{
								techValues.Add(values);
							}
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Debug.LogException(ex);
				return null;
			}

			return techData;
		}

		private static GlobalTechInfo ParseTechValues(List<List<string>> techValues)
		{
			// Process tech header
			string techName = techValues[0][1];

			int techId;
			if (!int.TryParse(techValues[0][2], out techId))
			{
				Debug.LogError("Tech '" + techName + "' has an invalid tech Id!");
			}

			TechCategory category = TechCategory.Basic;
			string description = "";
			string teaser = "";
			string improveDesc = "";
			int edenCost = 0;
			int plymouthCost = 0;
			int maxScientists = 0;
			LabType labType = LabType.Standard;
			List<int> requiredTechIDs = new List<int>();
			List<UnitPropertyInfo> unitProperties = new List<UnitPropertyInfo>();


			// Process tech values
			for (int i=0; i < techValues.Count; ++i)
			{
				if (techValues[i].Count < 2)
				{
					Debug.LogError("TechID " + techId + ": Not enough values specified for type: " + techValues[i][0]);
					continue;
				}

				int value;
				int.TryParse(techValues[i][1], out value);

				switch (techValues[i][0])
				{
					case "CATEGORY":			category = (TechCategory)value;		break;
					case "DESCRIPTION":			description = techValues[i][1];		break;
					case "TEASER":				teaser = techValues[i][1];			break;
					case "IMPROVE_DESC":		improveDesc = techValues[i][1];		break;
					case "REQUIRES":			requiredTechIDs.Add(value);			break;
					case "COST":				edenCost = plymouthCost = value;	break;
					case "EDEN_COST":			edenCost = value;					break;
					case "PLYMOUTH_COST":		plymouthCost = value;				break;
					case "MAX_SCIENTISTS":		maxScientists = value;				break;
					case "LAB":					labType = (LabType)value;			break;
					case "UNIT_PROP":
						if (techValues[i].Count < 4)
						{
							Debug.LogError("TechID " + techId + ": Not enough values specified for type: " + techValues[i][0]);
							continue;
						}
						
						map_id mapId = SheetReader.GetMapIdFromCodeName(techValues[i][1]);
						if (mapId == map_id.None)
						{
							Debug.LogError("TechID " + techId + ": Bad unit specifier: " + techValues[i][1]);
							break;
						}

						UnitProperty property = GetUnitProperty(techValues[i][2]);
						if (property == UnitProperty.None)
						{
							Debug.LogError("TechID " + techId + ": Bad property specifier: " + techValues[i][2]);
							break;
						}

						int.TryParse(techValues[i][3], out value);

						unitProperties.Add(new UnitPropertyInfo(mapId, property, value));

						break;
				}
			}

			return new GlobalTechInfo(techId, category, plymouthCost, edenCost, maxScientists, labType, techName, description, teaser, improveDesc, requiredTechIDs, unitProperties);
		}

		private static UnitProperty GetUnitProperty(string propertyName)
		{
			switch (propertyName)
			{
				case "Armor":				return UnitProperty.Armor;
				case "Build_Points":		return UnitProperty.Build_Points;
				case "Common_Required":		return UnitProperty.Common_Required;
				case "Concussion_Damage":	return UnitProperty.Concussion_Damage;
				case "Hit_Points":			return UnitProperty.Hit_Points;
				case "Improved":			return UnitProperty.Improved;
				case "Move_Speed":			return UnitProperty.Move_Speed;
				case "Penetration_Damage":	return UnitProperty.Penetration_Damage;
				case "Power_Required":		return UnitProperty.Power_Required;
				case "Production_Capacity":	return UnitProperty.Production_Capacity;
				case "Production_Rate":		return UnitProperty.Production_Rate;
				case "Rare_Required":		return UnitProperty.Rare_Required;
				case "Rate_Of_Fire":		return UnitProperty.Rate_Of_Fire;
				case "Sight_Range":			return UnitProperty.Sight_Range;
				case "Storage_bays":		return UnitProperty.Storage_bays;
				case "Storage_Capacity":	return UnitProperty.Storage_Capacity;
				case "Workers_Required":	return UnitProperty.Workers_Required;
			}

			return UnitProperty.None;
		}

		
	}
}