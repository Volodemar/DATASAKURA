using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Прыжки для лягушки
    /// </summary>
    public class JumpMovement : IMovementStrategy
    {
        private Rigidbody _rb; 
        private float _jumpInterval;             // Интервал между прыжками
        private float _jumpForceUp = 1f;         // Сила прыжка вверх
        private float _jumpForceForvard = 1f;    // Сила прыжка вперед   
        private float _timer;                    // Таймер для отслеживания интервала

        public JumpMovement(Rigidbody rb, float jumpInterval, float jumpForceUp, float jumpForceForvard)
        {
            _rb = rb;
            _jumpInterval = jumpInterval;
            _jumpForceUp = jumpForceUp;
            _jumpForceForvard = jumpForceForvard;
            _timer = 0f;
        }
        public void Move(Transform transform)
        {
            _timer += Time.deltaTime;
            if (_timer >= _jumpInterval)
            {
                // Прыгаем если стоим на земле
                if (transform.position.y == 0f)
                    Jump(transform);

                _timer = 0f; 
            }
        }

        private void Jump(Transform transform)
        {
            _rb.AddForce(transform.up * _jumpForceUp * Time.deltaTime,  ForceMode.VelocityChange);
            _rb.AddForce(transform.forward + transform.up * _jumpForceForvard * Time.deltaTime,  ForceMode.VelocityChange);           
        }
    }
}