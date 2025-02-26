using System.Collections.Generic;
using UnityEngine;

namespace DATASAKURA
{
	/// <summary>
	/// Класс для управления пользовательским интерфейсом
	/// </summary>	
	public class UIManager : MonoBehaviour
	{
		private List<UIWindow> uiWindows = new List<UIWindow>();

		public void Init()
		{
			foreach(var window in this.transform.GetComponentsInChildren<UIWindow>(includeInactive: true))
			{
				uiWindows.Add(window);  
			}

			HideAllUIWindows();
		}

		/// <summary>
		/// Возвращает окно по типу. Если окна нет, возвращает null.
		/// </summary>
		public T GetWindow<T>() where T : UIWindow
		{
			foreach (var window in uiWindows)
			{
				if (window is T windowsType)
				{
					return windowsType;
				}
			}
			return null;
		}

		/// <summary>
		/// Скрывает все открытые окна.
		/// </summary>
		public void HideAllUIWindows()
		{
			foreach (var window in uiWindows)
			{
				window.Hide();
			}
		}	    
	}
}