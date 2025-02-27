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
            _rb.velocity = Vector3.zero; 
            
            // Двигаем объект вперед с постоянной скоростью
            _rb.AddForce(transform.forward * _forceMove * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}