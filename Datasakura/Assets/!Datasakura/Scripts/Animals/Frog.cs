using UnityEngine;

/// <summary>
/// Лягушка (Жертва)
/// </summary>
public class Frog : Animal
{
    private float jumpInterval = 1f; // Прыжок каждую секунду
    private float jumpDistance = 2f; // Дистанция прыжка

    void Start()
    {
        Type = AnimalType.Victim;
        movementStrategy = new JumpMovement(jumpInterval, jumpDistance);
    }

    void Update()
    {
        Move(); // Вызываем движение через стратегию
        KeepInBounds();
    }

    public override void OnCollision(IAnimal other)
    {
        if (other.Type == AnimalType.Predator)
            Die();
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
}