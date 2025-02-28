using DATASAKURA.Borders;
using UnityEngine;
using Zenject;

namespace DATASAKURA
{
    /// <summary>
    /// Абстрактный базовый класс реализующий животных
    /// </summary>
    public abstract class Animal : MonoBehaviour, IAnimal
    {
        [Inject] protected DiContainer _container;        
        [Inject] protected DataBase _dataBase;
        [Inject] protected GameData _gameData;

        public AnimalType Type { get; protected set; }
        protected IMovementStrategy movementStrategy;
        public void Move() => movementStrategy.Move(transform);
        public abstract void OnCollisionAnimal(IAnimal other);
        public virtual void OnCollisionBorder(IBorder other)
        {
            transform.position += transform.forward * 0.1f;        
            transform.Rotate(0, 180, 0);
        }
        public abstract void Die();

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent<IAnimal>(out IAnimal animal))
            {
                OnCollisionAnimal(animal);
            }

            if (other.transform.TryGetComponent<IBorder>(out IBorder border))
            {
                OnCollisionBorder(border);
            }
        }		
    }
}