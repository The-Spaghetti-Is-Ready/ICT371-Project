using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private int _gameSceneIndex = 0;
    
    [SerializeField]
    private ShowControls _showControls;

    public void playGame()
    {
        EditorSceneManager.LoadScene(_gameSceneIndex);
    }

    public void quitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void showControls()
    {
        if (_showControls != null)
        {
            _showControls.display();
        }
        else
        {
            Debug.LogWarning("ShowControls not set");
        }
    }
}
