using TMPro;
using UnityEngine;
using Zenject;

/// <summary>
/// Счетчик погибших жертв.
/// </summary>	
public class UICounterVictimsDeath : MonoBehaviour
{
    private TextMeshProUGUI textNumberVictimsDeath;

    [Inject] GameData _gameData;

    private void OnEnable()
    {
        textNumberVictimsDeath = GetComponent<TextMeshProUGUI>();

        LevelData.OnVictimDeath += UpdateCounter;

        UpdateCounter();
    }

    private void OnDisable()
    {
        LevelData.OnVictimDeath -= UpdateCounter;
    }

    private void UpdateCounter()
    {
        textNumberVictimsDeath.text =  _gameData.LevelData.GetVictimsDeaths().ToString();
    }
}
