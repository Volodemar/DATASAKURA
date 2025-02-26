using UnityEngine;

/// <summary>
/// Линейное движение для змеи
/// </summary>
public class LinearMovement : IMovementStrategy
{
    private float speed; // Скорость движения

    public LinearMovement(float speed)
    {
        this.speed = speed;
    }

    public void Move(Transform transform)
    {
        // Двигаем объект вперед с постоянной скоростью
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
