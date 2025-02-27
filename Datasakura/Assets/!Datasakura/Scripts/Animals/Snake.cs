using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Змея (Хищник)
    /// </summary>
    public class Snake : Animal
    {
        private Rigidbody rb;
        private float forceMove = 100f;
        private bool isDead = false;
        private TextMesh deliciousText;

        void Start()
        {
            Type = AnimalType.Predator;
            rb = GetComponent<Rigidbody>();
            movementStrategy = new LinearMovement(rb, forceMove); 
            deliciousText = CreateDeliciousText();
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
                // Реализовать логику умирает один из хищников
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
            deliciousText.gameObject.SetActive(true);

            Invoke((nameof(HideDeliciousText)), 1f);
        }

        private void HideDeliciousText() => deliciousText.gameObject.SetActive(false);

        private TextMesh CreateDeliciousText()
        {
            GameObject textObj = new GameObject("DeliciousText");
            textObj.transform.SetParent(transform);
            textObj.transform.localPosition = new Vector3(0, 0, 2f);
            textObj.transform.localRotation = Quaternion.Euler(90, 0, 0);
            TextMesh text = textObj.AddComponent<TextMesh>();
            text.text = "Вкусно!";
            text.anchor = TextAnchor.MiddleCenter;
            text.gameObject.SetActive(false);
            return text;
        }
    }
}