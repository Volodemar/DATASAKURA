using TMPro;
using UnityEngine;
using Zenject;

namespace DATASAKURA
{
    /// <summary>
    /// Счетчик смертей жищников
    /// </summary>	
    public class UICounterPredatorDeath : MonoBehaviour
    {
        private TextMeshProUGUI textNumberPredatorDeath;

        [Inject] GameData _gameData;

        private void OnEnable()
        {
            textNumberPredatorDeath = GetComponent<TextMeshProUGUI>();

            LevelData.OnPredatorDeath += UpdateCounter;

            UpdateCounter();
        }

        private void OnDisable()
        {
            LevelData.OnPredatorDeath -= UpdateCounter;
        }

        private void UpdateCounter()
        {
            textNumberPredatorDeath.text =  _gameData.LevelData.GetPredatorDeaths().ToString();
        }
    }
}