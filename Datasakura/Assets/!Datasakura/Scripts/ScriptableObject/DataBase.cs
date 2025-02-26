using UnityEngine;

namespace DATASAKURA
{
	/// <summary>
	/// База данных
	/// </summary>
	[CreateAssetMenu(fileName = "DataBase", menuName = "ScriptableObjects/DataBase")]
	public class DataBase : ScriptableObject
	{
		public PrefabsScriptableObject prefabsData;
	}
}