using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    GameLoader gameLoader;
    string sceneName = "Level";

    private void Awake()
    {
        gameLoader = FindObjectOfType<GameLoader>();
        if(!gameLoader)
        {
            Debug.LogError("GameLoader script not found, cannot start level.");
        }
    }

    public void PlayButtonPressed()
    {
        gameLoader.RequestSceneLoad(sceneName);
    }
}