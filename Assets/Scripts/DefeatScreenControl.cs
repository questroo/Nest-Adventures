using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreenControl : MonoBehaviour
{
    [SerializeField]
    private GameObject defeatScreen;
    private PlayerStats player;
    private PlayerControls mainControls;
    private ScreenFadeToBlack fade;
    private DualPlayerController playerController;

    public Transform spawnPoint;
    public Transform dungeonStart;

    private void Start()
    {
        player = ServiceLocator.Get<PlayerStats>();
        mainControls = ServiceLocator.Get<PlayerControls>();
        playerController = ServiceLocator.Get<DualPlayerController>();
        fade = FindObjectOfType<ScreenFadeToBlack>();
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        TurnoffDefeatScreen();
        player.Respawn();
        if(player.reachCheakPoint)
        {
            fade.TeleportPlayer(spawnPoint);
        }
        else
        {
            playerController.isReload = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void SwitchCharacter(Scene s, LoadSceneMode m)
    {
        playerController.SwitchToCharacter(DualPlayerController.CharacterClass.Pugilist);
    }
    public void GiveUp()
    {
        Application.Quit();
    }

    public void ShowDefeatScreen()
    {
        Time.timeScale = 0.0f;
        defeatScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TurnoffDefeatScreen()
    {
        Time.timeScale = 1.0f;
        defeatScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
