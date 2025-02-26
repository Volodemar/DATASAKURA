using UnityEngine;
using Zenject;

/// <summary>
/// Базовый класс окон
/// </summary>	
public class UIWindow : MonoBehaviour
{
    [Inject] private UIManager _uiManager;

    /// <summary>
    /// Отображает текущее окно, скрывающее все другие окна.
    /// </summary>
    public void Show()
    {
        _uiManager.HideAllUIWindows();

        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// Скрывает текущее окно.
    /// </summary>
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
