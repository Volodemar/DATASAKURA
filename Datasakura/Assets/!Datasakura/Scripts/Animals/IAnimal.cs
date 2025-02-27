using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Базовый интерфейс для всех животных
    /// </summary>
    public interface IAnimal
    {
        Transform transform { get; }  
        GameObject gameObject { get; }        
        AnimalType Type { get; } 
        void Move(); 
        void OnCollisionAnimal(IAnimal other);       
        void Die(); 
    }
}