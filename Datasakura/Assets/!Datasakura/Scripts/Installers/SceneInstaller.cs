using Zenject;
using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Базовый набор инсталляторов для любой сцены
    /// </summary>	
    public class SceneInstaller : MonoInstaller
    {
        [Header("Prefabs")]
        [SerializeField] private GameManager _gameManager;

        [Header("GameObjects")]
        [SerializeField] private LevelController _levelController;
        [SerializeField] private UIManager _uiManager;

        public override void InstallBindings()
        {
            Container.Bind<LevelController>().FromComponentOn(_levelController.gameObject).AsSingle();

            Container.Bind<UIManager>().FromComponentOn(_uiManager.gameObject).AsSingle();

            // Объект должен создаваться на сцене
            Container.Bind<GameManager>().FromComponentInNewPrefab(_gameManager.gameObject).AsSingle().NonLazy();        
        }
    }
}