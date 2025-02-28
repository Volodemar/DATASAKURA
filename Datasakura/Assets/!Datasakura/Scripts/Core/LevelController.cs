using Zenject;
using UnityEngine;
using System.Collections;

namespace DATASAKURA
{
    /// <summary>
    /// Контроллер геймплея уровня.
    /// </summary>	
    public class LevelController : MonoBehaviour
    {
        [Inject] private DiContainer _container;        
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

            StartCoroutine(SpawnTimer(1f));
        }

        private IEnumerator SpawnTimer(float time)
        {
            AnimalFactory animalFactory = new AnimalFactory(_container);

            while(true)
            {
                yield return new WaitForSeconds(time);

                string[] types = { "Frog", "Snake" };
                string randomType = types[Random.Range(0, types.Length)];
                Animal animal = animalFactory.CreateAnimal(randomType);               
            }
        }
    }
}