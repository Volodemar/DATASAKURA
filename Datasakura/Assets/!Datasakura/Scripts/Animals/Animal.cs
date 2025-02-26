using System;
using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Абстрактный базовый класс реализующий животных
    /// </summary>
    public abstract class Animal : MonoBehaviour, IAnimal
    {
        public AnimalType Type { get; protected set; }
        public event Action<IAnimal> OnDeath;

        protected IMovementStrategy movementStrategy;

        public void Move() => movementStrategy.Move(transform);
        public abstract void OnCollision(IAnimal other);

        public virtual void Die()
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}