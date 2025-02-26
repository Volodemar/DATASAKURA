using UnityEngine;

/// <summary>
/// Змея (Хищник)
/// </summary>
public class Snake : Animal
{
    private bool isDead = false;
    private TextMesh deliciousText;

    void Start()
    {
        Type = AnimalType.Predator;
        movementStrategy = new LinearMovement(3f); // Скорость 3 единицы/сек
        deliciousText = CreateDeliciousText();
    }

    void Update()
    {
        Move(); // Вызываем движение через стратегию
        KeepInBounds();
    }

    public override void OnCollision(IAnimal other)
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
        if (!IsInScreenBounds(pos))
            transform.Rotate(0, 180, 0); // Разворот при выходе за экран
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
        Invoke(nameof(HideDeliciousText), 1f);
    }

    private void HideDeliciousText() => deliciousText.gameObject.SetActive(false);

    private TextMesh CreateDeliciousText()
    {
        GameObject textObj = new GameObject("DeliciousText");
        textObj.transform.SetParent(transform);
        textObj.transform.localPosition = Vector3.zero;
        TextMesh text = textObj.AddComponent<TextMesh>();
        text.text = "Вкусно!";
        text.anchor = TextAnchor.MiddleCenter;
        text.gameObject.SetActive(false);
        return text;
    }
}