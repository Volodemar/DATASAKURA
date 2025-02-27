using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Линейное движение для змеи
    /// </summary>
    public class LinearMovement : IMovementStrategy
    {
        private float _forceMove; // Сила движения
        private Rigidbody _rb; 

        public LinearMovement(Rigidbody rb, float forceMove)
        {
            _forceMove = forceMove;
            _rb = rb;
        }

        public void Move(Transform transform)
        {
            // Обнуляем скорость перед движением
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f); 
            
            // Двигаем объект вперед с постоянной скоростью
            _rb.AddForce(transform.forward * _forceMove, ForceMode.VelocityChange);
        }
    }
}