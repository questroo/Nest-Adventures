﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public int sceneIndex = 2;
    
    public void PlayButtonPressed()
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}