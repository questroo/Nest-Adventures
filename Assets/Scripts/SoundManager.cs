using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        test1,
        test2,
        test3,
        test4,
    }

    private static Dictionary<Sound, float> soundTimeDictionary;
    private static GameObject oneshotGO;
    private static AudioSource oneshotAudioSource;

    public static void Initialize()
    {
        soundTimeDictionary = new Dictionary<Sound, float>();
        soundTimeDictionary[Sound.test1] = 0.0f;
    }

    public static void PlaySound(Sound sound)
    {
        if(CanPlaySound(sound))
        {
            if(oneshotGO == null)
            {
                oneshotGO = new GameObject("One Shot Sound");
                oneshotAudioSource  = oneshotGO.AddComponent<AudioSource>();
            }
            oneshotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        if(CanPlaySound(sound))
        {
            GameObject soundGO = new GameObject("Sound");
            AudioSource audioSource = soundGO.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.Play();

            GameObject.Destroy(soundGO, audioSource.clip.length);
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;

            case Sound.test1:
                if (soundTimeDictionary.ContainsKey(sound))
                {
                    float lastPlayTime = soundTimeDictionary[sound];
                    float delay = 0.5f;
                    if (lastPlayTime + delay < Time.time)
                    {
                        soundTimeDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return true;
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundLibrary.SoundAudioClip soundAudioClip in SoundLibrary.Instance.soundAudioClipArray)
        {
            if(soundAudioClip.name == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.Log("[SoundManager] --- No soundclip is found");
        return null;
    }
}
