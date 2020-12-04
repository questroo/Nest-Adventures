using UnityEngine;

public class SecondWaveSpawner : MonoBehaviour
{
    public GameObject secondWave;
    public static bool hasBossDied = false;
    private void SpawnSecondWave()
    {
        hasBossDied = true;
        secondWave.SetActive(true);
    }

    private void OnEnable()
    {
        BossDeath.OnBossDeath += SpawnSecondWave;
    }

    private void OnDisable()
    {
        BossDeath.OnBossDeath -= SpawnSecondWave;
    }
}
