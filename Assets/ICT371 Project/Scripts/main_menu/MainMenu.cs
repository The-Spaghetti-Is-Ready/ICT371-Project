using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <Author>
/// Lane O'Rafferty
/// </Author>
/// <summary>
/// This class is responsible for controlling the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private int _gameSceneIndex = 0;

    /// <summary>
    /// Loads the game scene.
    /// </summary>
    public void playGame()
    {
        EditorSceneManager.LoadScene(_gameSceneIndex);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void quitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
