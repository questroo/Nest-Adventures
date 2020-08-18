using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public delegate void BossDied();
    public static event BossDied OnBossDeath;

    public void BossIsDead()
    {
        OnBossDeath?.Invoke();
    }
}
