using System.Collections;
using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Текст "Вкусно"
    /// </summary>	
    public class TextDelicious : PoolableObject
    {
        private ObjectPool<TextDelicious> _pool;

        public void Init(Vector3 position, ObjectPool<TextDelicious> pool)
        {
            _pool = pool;

            transform.position = position;
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            StartCoroutine(WaitAutoDespawn());  
        }

        IEnumerator WaitAutoDespawn() 
        {   
            yield return new WaitForSeconds(1f);

            _pool.Return(this);        
        } 
    }
}