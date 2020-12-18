using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public void PlayPunchSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.player1_whoosh);
    }

    public void PlayWalkSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.player2_walk);
    }

    public void PlayMagicSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.player2_windup);
    }
}
