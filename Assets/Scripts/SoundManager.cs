using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        test1, test2, test3, test4,
        player1_whoosh, player1_hit_pow, player1_hit_bone, player1_hit_metal,
        player1_walk, player1_get_hit, player_sword, player1_arrow, player1_axe,
        player1_fall, player1_land, player1_death,
        player2_walk, player2_windup, player2_missile_travel, player2_missile_hit, player2_winddown,
        player2_get_hit, player2_sword, player2_arrow, player2_axe,
        player2_fall, player2_land, player2_death,
        skeleton_walk, skeleton_ambient, skeleton_swing, skeleton_strike, skeleton_get_hit, skeleton_death,
        archer_walk, archer_ambient, archer_pull_bow, archer_release_arrow, archer_rearm, archer_get_hit,
        door_open, door_close,
        boss_land, boss_swing, boss_slam, boss_walk, boss_death, boss_get_hit
    }

    private static Dictionary<Sound, float> soundTimeDictionary;
    private static GameObject oneshotGO;
    private static AudioSource oneshotAudioSource;

    public static void Initialize()
    {
        soundTimeDictionary = new Dictionary<Sound, float>();
        soundTimeDictionary[Sound.test1] = 0.0f;
        soundTimeDictionary[Sound.player2_walk] = 0.0f;
        soundTimeDictionary[Sound.archer_walk] = 0.0f;
        soundTimeDictionary[Sound.skeleton_walk] = 0.0f;
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
            case Sound.player2_walk:
                return DelayPlaySound(sound, SoundLibrary.Instance.player2_walk_delay);

            case Sound.archer_walk:
                return DelayPlaySound(sound, SoundLibrary.Instance.archer_walk_delay);

            case Sound.skeleton_walk:
                return DelayPlaySound(sound, SoundLibrary.Instance.skeleton_walk_delay);
        }
    }

    private static bool DelayPlaySound(Sound sound, float time)
    {
        if (soundTimeDictionary.ContainsKey(sound))
        {
            float lastPlayTime = soundTimeDictionary[sound];
            float delay = time;
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
