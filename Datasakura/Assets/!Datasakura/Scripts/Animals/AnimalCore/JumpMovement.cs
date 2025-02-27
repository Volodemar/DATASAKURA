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
                _timer = 0f;

                // Прыгаем если стоим на земле
                if (_rb.velocity.y <= 0.1f)
                    Jump(transform);
            }
        }

        private void Jump(Transform transform)
        {
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f); 

            _rb.AddForce(transform.up * _jumpForceUp,  ForceMode.VelocityChange);
            _rb.AddForce(transform.forward * _jumpForceForvard,  ForceMode.VelocityChange);           
        }
    }
}