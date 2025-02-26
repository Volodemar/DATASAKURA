using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// База данных префабов игры.
/// </summary>	
[CreateAssetMenu(fileName = "PrefabsData", menuName = "ScriptableObjects/PrefabsData")]
public class PrefabsScriptableObject : ScriptableObject
{
    public List<GameObject> Animals = new List<GameObject>();
}