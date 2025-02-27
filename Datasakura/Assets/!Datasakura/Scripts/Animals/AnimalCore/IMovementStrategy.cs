using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Интерфейс для стратегии движения
    /// </summary>
    public interface IMovementStrategy
    {
        void Move(Transform transform);
    }
}