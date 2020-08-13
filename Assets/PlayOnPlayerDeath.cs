using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayOnPlayerDeath : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += TurnOnCanvas;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= TurnOnCanvas;
    }

    private void TurnOnCanvas()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}