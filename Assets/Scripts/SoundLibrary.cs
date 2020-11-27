using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    public float player2_walk_delay;
    public float archer_walk_delay;
    public float skeleton_walk_delay;

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
