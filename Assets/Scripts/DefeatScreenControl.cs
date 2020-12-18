using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    private UnityAction action;

    private void Start()
    {
        player = ServiceLocator.Get<PlayerStats>();
        mainControls = ServiceLocator.Get<PlayerControls>();
        playerController = ServiceLocator.Get<DualPlayerController>();
        fade = FindObjectOfType<ScreenFadeToBlack>();
    }

    public void Retry()
    {
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
        defeatScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TurnoffDefeatScreen()
    {
        defeatScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
