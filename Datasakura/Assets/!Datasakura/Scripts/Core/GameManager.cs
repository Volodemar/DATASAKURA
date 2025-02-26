using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

/// <summary>
/// Менеджер игры, отвечает за инициализацию сцен и загрузку данных.
/// </summary>	
public class GameManager: MonoBehaviour
{
    [Inject] private LevelController _levelController; 
    [Inject] private GameData _gameData; 

    private void Awake()
    {
        Preload();
    }

    /// <summary>
    /// Предзагружает данные игры, если это еще не было сделано.
    /// </summary>
    private void Preload()
    {
        if (!LoadingHelper.IsPreloaded)
        {
            _gameData.Load();

            LoadingHelper.IsPreloaded = true;
        }   
    }


    /// <summary>
    /// Инициализирует уровень при создании объекта.
    /// </summary>
    private void Start() 
    {
        _levelController?.OnInit();
    }  
}
