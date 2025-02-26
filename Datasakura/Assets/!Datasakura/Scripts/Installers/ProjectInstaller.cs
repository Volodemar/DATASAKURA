using Zenject;
using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Инсталляция зависимостей для всего проекта
    /// </summary>	
    public class ProjectInstaller : MonoInstaller
    {
        [Header("ScriptableObjects")]
        [SerializeField] private DataBase dataBase;

        [Header("Prefabs")] 
        [SerializeField] private GameObject eventSystem;  

        private GameData _gameData = new GameData();

        public override void InstallBindings()
        {
            Container.Bind<DataBase>().FromScriptableObject(dataBase).AsSingle();

            Container.Bind<GameData>().FromInstance(_gameData).AsSingle();

            DontDestroyOnLoad(Instantiate(eventSystem));
        } 
    }
}