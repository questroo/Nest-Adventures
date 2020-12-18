using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalkSound : MonoBehaviour
{
    public void PlaySkeletonWalkSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.skeleton_walk, gameObject.transform.position);
    }
}
