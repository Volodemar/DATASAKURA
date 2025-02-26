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
        [SerializeField] private GameManager gameManager;

        [Header("GameObjects")]
        [SerializeField] private LevelController levelController;
        [SerializeField] private UIManager uiManager;

        public override void InstallBindings()
        {
            Container.Bind<LevelController>().FromComponentOn(levelController.gameObject).AsSingle();

            Container.Bind<UIManager>().FromComponentOn(uiManager.gameObject).AsSingle();

            // Объект должен создаваться на сцене
            Container.Bind<GameManager>().FromComponentInNewPrefab(gameManager.gameObject).AsSingle().NonLazy();        
        }
    }
}