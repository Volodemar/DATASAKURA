using UnityEngine;
using Zenject;

namespace DATASAKURA
{
    /// <summary>
    /// Змея (Хищник)
    /// </summary>
    public class Snake : Animal
    {
        [Inject] private ObjectPool<TextDelicious> _poolTextDelicious;

        private Rigidbody rb;
        private float forceMove = 1f;
        private bool isDead = false;

        void Start()
        {
            Type = AnimalType.Predator;
            rb = GetComponent<Rigidbody>();
            movementStrategy = new LinearMovement(rb, forceMove);          
        }

        void Update()
        {
            Move(); // Вызываем движение через стратегию
            KeepInBounds();
        }

        public override void OnCollisionAnimal(IAnimal other)
        {
            if (isDead) return;

            if (other.Type == AnimalType.Victim)
            {
                other.Die();

                SpawnDeliciousText();
            }
            else if (other.Type == AnimalType.Predator)
            {
                // Кто меньше тот и слабее
                if (other.gameObject.GetInstanceID() < gameObject.GetInstanceID())
                {
                    other.Die(); 

                    SpawnDeliciousText();      
                }          
            }
        }

        private void KeepInBounds()
        {
            Vector3 pos = transform.position;
            if (!IsInScreenBounds(pos) && !IsInScreenBounds(pos + transform.forward * 5f))
                transform.Rotate(0, 180, 0); 
        }

        private bool IsInScreenBounds(Vector3 pos)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(pos);
            return screenPoint.x > 0 && screenPoint.x < Screen.width &&
                screenPoint.y > 0 && screenPoint.y < Screen.height;
        }

        public void SpawnDeliciousText()
        {
            TextDelicious text = _poolTextDelicious.Get();   

            text.Init(transform.position, _poolTextDelicious);
        }

        public override void Die()
        {
            _gameData.LevelData.ModifyPredatorDeaths(1);

            Destroy(gameObject);
        }
    }
}