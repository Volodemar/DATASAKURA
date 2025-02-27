using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Лягушка (Жертва)
    /// </summary>
    public class Frog : Animal
    {
        private Rigidbody _rb;
        private float _jumpInterval = 1f; // Прыжок каждую секунду
        private float _jumpForceUp = 300f; // Дистанция прыжка
        private float _jumpForceForvard = 400f; // Дистанция прыжка

        void Start()
        {
            Type = AnimalType.Victim;
            _rb = GetComponent<Rigidbody>();
            movementStrategy = new JumpMovement(_rb, _jumpInterval, _jumpForceUp, _jumpForceForvard);
        }

        void Update()
        {
            Move(); // Вызываем движение через стратегию
            KeepInBounds();
        }

        public override void OnCollisionAnimal(IAnimal animal)
        {
            if (animal.Type == AnimalType.Victim)
            {
                // Отбрасываем от встреченной жертвы
                _rb.AddExplosionForce(100f, animal.transform.position, 5f); 
            }
        }

        private void KeepInBounds()
        {
            Vector3 pos = transform.position;

            // Поворачиваем если вышли за пределы экрана и не возвращаемся обратно
            if (!IsInScreenBounds(pos) && !IsInScreenBounds(pos + transform.forward * 2f))
                transform.Rotate(0, 180, 0); 
        }

        /// <summary>
        /// Проверка, находится ли объект в пределах экрана
        /// </summary>
        private bool IsInScreenBounds(Vector3 pos)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(pos);
            return screenPoint.x > 0 && screenPoint.x < Screen.width &&
                screenPoint.y > 0 && screenPoint.y < Screen.height;
        }

        public override void Die()
        {
            _gameData.LevelData.ModifyVictimsDeaths(1);

            Destroy(gameObject);
        }        
    }
}