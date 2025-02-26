/// <summary>
/// Базовый интерфейс для всех животных
/// </summary>
public interface IAnimal
{
    AnimalType Type { get; } 
    void Move(); 
    void OnCollision(IAnimal other); 
    void Die(); 
    event System.Action<IAnimal> OnDeath; 
}
