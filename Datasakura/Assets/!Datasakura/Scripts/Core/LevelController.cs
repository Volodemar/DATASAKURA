using Zenject;
using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Контроллер геймплея уровня.
    /// </summary>	
    public class LevelController : MonoBehaviour
    {
        [Inject] private GameData _gameData;
        [Inject] private DataBase _dataBase;
        [Inject] private UIManager _uiManager;    

        /// <summary>
        /// Иниализация уровня.
        /// </summary>    
        public void OnInit()
        {
            _uiManager.Init();

            OnStart();
        }

        /// <summary>
        /// Стартуем уровень.
        /// </summary>
        private void OnStart()
        {
            _uiManager.GetWindow<UIWindowGame>().Show();
        }
    }
}