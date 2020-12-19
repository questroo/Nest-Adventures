﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuGO;

    private bool isPause;
    private PlayerControls mainControls;

    private void Start()
    {
        mainControls = ServiceLocator.Get<PlayerControls>();
        mainControls.ActionMap.PauseGame.performed += ctx => PauseGame();
    }

    private void Update()
    {
    }

    public void ResumeGame()
    {
        isPause = false;
        pauseMenuGO.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }

    private void PauseGame()
    {
        if (isPause)
        {
            ResumeGame();
        }
        else
        {
            isPause = true;
            pauseMenuGO.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
        }
    }
}