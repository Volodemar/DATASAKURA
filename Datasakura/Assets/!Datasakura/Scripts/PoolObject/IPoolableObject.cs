namespace DATASAKURA
{
    /// <summary>
    /// Интерфейс для объектов пула
    /// </summary>
    public interface IPoolableObject
    {
        void OnSpawned();
        void OnDespawned();
    }
}