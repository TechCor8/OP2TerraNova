using UnityEngine;

namespace TerraNova.Systems
{
	/// <summary>
	/// Keeps an object between scenes.
	/// </summary>
	public class PersistentObject : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
