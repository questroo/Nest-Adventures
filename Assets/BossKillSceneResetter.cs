using UnityEngine;
using UnityEngine.SceneManagement;


public class BossKillSceneResetter : MonoBehaviour
{
    public static bool hasBossDied = false;
    private void ReloadCurrentScene()
    {
        hasBossDied = true;
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void OnEnable()
    {
        BossDeath.OnBossDeath += ReloadCurrentScene;
    }

    private void OnDisable()
    {
        BossDeath.OnBossDeath -= ReloadCurrentScene;
    }
}
