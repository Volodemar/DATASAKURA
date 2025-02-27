using System.Collections.Generic;
using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// База данных префабов игры.
    /// </summary>	
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "ScriptableObjects/PrefabsData")]
    public class PrefabsScriptableObject : ScriptableObject
    {
        public List<GameObject> AnimalPrefabs = new List<GameObject>();
        public TextDelicious TextDeliciousPrefab;
    }
}