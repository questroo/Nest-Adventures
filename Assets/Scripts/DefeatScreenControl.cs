using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreenControl : MonoBehaviour
{
    [SerializeField]
    private GameObject defeatScreen;

    private PlayerStats player;

    public Transform spawnPoint;

    private void Start()
    {
        player = ServiceLocator.Get<PlayerStats>();
    }

    public void Retry()
    {
        if(player.reachCheakPoint)
        {
            player.gameObject.transform.position = spawnPoint.position;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GiveUp()
    {
        Application.Quit();
    }
}
