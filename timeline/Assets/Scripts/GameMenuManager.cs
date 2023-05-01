using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    private string MenuHomeName = "Home";
    private string GameSceneName = "Game";
    private string StudySceneName = "Study";
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            LoadHomeScene();
        }
    }

    public void LoadHomeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MenuHomeName);
    }
    
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameSceneName);
    }
    
    public void LoadStudyScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(StudySceneName);
    }
}
