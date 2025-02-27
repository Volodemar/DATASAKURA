using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

namespace DATASAKURA
{
    /// <summary>
    /// Змея (Хищник)
    /// </summary>
    public class Snake : Animal
    {
        private Rigidbody rb;
        private float forceMove = 1f;
        private bool isDead = false;
        private List<TextDelicious> poolDeliciousTexts = new List<TextDelicious>();

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

                ShowDeliciousText();
            }
            else if (other.Type == AnimalType.Predator)
            {
                // Кто меньше тот и слабее
                if (other.gameObject.GetInstanceID() < gameObject.GetInstanceID())
                {
                    other.Die(); 

                    ShowDeliciousText();      
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

        public void ShowDeliciousText()
        {
            var deliciousText = poolDeliciousTexts.Where(t => !t.gameObject.activeSelf).FirstOrDefault();

            if (deliciousText == null)
            {
                deliciousText = _container.InstantiatePrefabForComponent<TextDelicious>
                    (
                        _dataBase.prefabsData.TextDeliciousPrefab, 
                        transform.position, 
                        Quaternion.identity, 
                        transform
                    );
                    
                poolDeliciousTexts.Add(deliciousText);     
            }

            deliciousText.Show(transform);
        }

        public override void Die()
        {
            _gameData.LevelData.ModifyPredatorDeaths(1);

            // Т.к. текст может быть отвязан от родителя, нужно его уничтожить отдельно.
            foreach (var text in poolDeliciousTexts)
                Destroy(text.gameObject);

            Destroy(gameObject);
        }
    }
}