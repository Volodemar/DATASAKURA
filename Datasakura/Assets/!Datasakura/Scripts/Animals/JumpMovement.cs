using UnityEngine;

/// <summary>
/// Прыжки для лягушки
/// </summary>
public class JumpMovement : IMovementStrategy
{
    private float jumpInterval; // Интервал между прыжками
    private float jumpDistance; // Дистанция прыжка
    private float timer; // Таймер для отслеживания интервала

    public JumpMovement(float jumpInterval, float jumpDistance)
    {
        this.jumpInterval = jumpInterval;
        this.jumpDistance = jumpDistance;
        this.timer = 0f;
    }

    public void Move(Transform transform)
    {
        timer += Time.deltaTime;
        if (timer >= jumpInterval)
        {
            // Двигаем объект вперед на фиксированное расстояние
            transform.position += transform.forward * jumpDistance;
            timer = 0f; // Сбрасываем таймер
        }
    }
}