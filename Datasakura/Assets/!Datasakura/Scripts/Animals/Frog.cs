using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Лягушка (Жертва)
    /// </summary>
    public class Frog : Animal
    {
        private Rigidbody rb;
        private float jumpInterval = 1f; // Прыжок каждую секунду
        private float jumpForceUp = 300f; // Дистанция прыжка
        private float jumpForceForvard = 400f; // Дистанция прыжка

        void Start()
        {
            Type = AnimalType.Victim;
            rb = GetComponent<Rigidbody>();
            movementStrategy = new JumpMovement(rb, jumpInterval, jumpForceUp, jumpForceForvard);
        }

        void Update()
        {
            Move(); // Вызываем движение через стратегию
            KeepInBounds();
        }

        public override void OnCollisionAnimal(IAnimal animal)
        {
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