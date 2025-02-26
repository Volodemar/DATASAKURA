using Zenject;
using UnityEngine;
using System.Linq;

/// <summary>
/// Контроллер геймплея уровня.
/// </summary>	
public class LevelController : MonoBehaviour
{
    [Inject] private GameData _gameData;
    [Inject] private DataBase _dataBase;

    /// <summary>
    /// Иниализация уровня.
    /// </summary>    
    public void OnInit()
    {
        OnStart();
    }

    /// <summary>
    /// Стартуем уровень.
    /// </summary>
    private void OnStart()
    {
        // TODO: Test
        var levelData = _gameData.LevelData;

        levelData.ModifyPredatorDeaths(+1);
        levelData.ModifyVictimsDeaths(+1);

        Debug.Log(levelData.GetPredatorDeaths() + " смертей хищников.");

        Debug.Log(levelData.GetVictimsDeaths() + " смертей жертв.");

        Debug.Log(_dataBase.prefabsData.Animals.Count() + " количество зверей.");
    }
}
