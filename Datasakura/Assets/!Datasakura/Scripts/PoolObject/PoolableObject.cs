using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Базовый класс для объектов пула с поведением по умолчанию
    /// </summary>
    public abstract class PoolableObject : MonoBehaviour, IPoolableObject
    {
        public virtual void OnSpawned()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnDespawned()
        {
            gameObject.SetActive(false);
        }
    }
}