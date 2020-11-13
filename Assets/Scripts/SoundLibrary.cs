using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    public static SoundLibrary Instance { get; set; }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound name;
        public AudioClip audioClip;
    }

    public SoundAudioClip[] soundAudioClipArray;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
