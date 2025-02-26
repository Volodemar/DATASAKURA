using System.Collections.Generic;
using UnityEngine;

namespace MyShortcut
{
    /// <summary>
    /// Хранилище ссылок
    /// </summary>
    public class MyShortcutData : ScriptableObject
    {
        [System.Serializable]
        public class MyShortcutDataInfo
        {
            public string name = null;
            public Object obj = null;

            public MyShortcutDataInfo(Object _obj)
            {
                Debug.Assert(_obj != null, "Объект не может быть null.");

                name = _obj.name;
                obj = _obj;
            }
        }

        public List<MyShortcutDataInfo> dataInfoList = new List<MyShortcutDataInfo>();
    }
}