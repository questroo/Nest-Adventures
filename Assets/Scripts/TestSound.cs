using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    private void Awake()
    {
        SoundManager.Initialize();
    }

    public void PlayTest1()
    {
        SoundManager.PlaySound(SoundManager.Sound.test1, Vector3.left);
    }

    public void PlayTest2()
    {
        SoundManager.PlaySound(SoundManager.Sound.test2);
    }

    public void PlayTest3()
    {
        SoundManager.PlaySound(SoundManager.Sound.test3);
    }

    public void PlayTest4()
    {
        SoundManager.PlaySound(SoundManager.Sound.test4);
    }
}
