using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DATASAKURA
{
    public class AnimalFactory
    {
        private DiContainer _container;
        private DataBase _dataBase;  
        private LevelController _levelController; 

        private readonly Dictionary<string, Func<Animal>> animalCreators;

        private Transform _animalsContent;

        public AnimalFactory(DiContainer container)
        {         
            // Подключим зависимости Zenject
            _container = container;
            _dataBase = _container.Resolve<DataBase>();
            _levelController = _container.Resolve<LevelController>();

            // Создаем контейнер для хранения животных
            _animalsContent = new GameObject("AnimalsContent").transform;
            _animalsContent.parent = _levelController.transform;

            // Создаем словарь для быстрого доступа к процедуре создания животных
            animalCreators = new Dictionary<string, Func<Animal>>();            
            foreach(var animal in _dataBase.prefabsData.AnimalPrefabs)
            {
                animalCreators.Add(animal.name, () => InstantiateAnimal(animal.name));
            }
        }

        /// <summary>
        /// Создание животного по имени префаба.
        /// </summary>
        public Animal CreateAnimal(string animalName)
        {
            if (animalCreators.TryGetValue(animalName, out var creator))
                return creator();

            throw new ArgumentException("Error: Нет живтного в списке!");
        }

        /// <summary>
        /// Инстанциирование префаба животного с инициализацией Zenject.
        /// </summary>
        private Animal InstantiateAnimal(string prefabName)
        {
            foreach (var prefab in _dataBase.prefabsData.AnimalPrefabs)
            {
                if(prefab.name == prefabName)
                {
                    var randomRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
                    var randomPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), 3f, UnityEngine.Random.Range(-10f, 10f));

                    return _container.InstantiatePrefabForComponent<Animal>(prefab, randomPosition, randomRotation, _animalsContent); 
                }
            }

            throw new ArgumentException("Error: Неизвестный префаб животного!");
        }
    }
}