using System.Collections;
using UnityEngine;

namespace DATASAKURA
{
    /// <summary>
    /// Текст Вкусно
    /// </summary>	
    public class TextDelicious : MonoBehaviour
    {
        private Transform _parent;

        public void Show(Transform parent)
        {
            _parent = parent;

            transform.parent = null;
            transform.position = parent.position + Vector3.back;
            gameObject.SetActive(true);

            StartCoroutine(Hide());       
        }

        private IEnumerator Hide() 
        {   
            yield return new WaitForSeconds(1f);

            transform.localPosition = Vector3.zero; 
            transform.parent = _parent;
            gameObject.SetActive(false);         
        }
    }
}