using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSound : MonoBehaviour
{
    public void PlayPunchSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.player1_whoosh);

    }
}
