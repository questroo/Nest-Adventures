using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBottle : MonoBehaviour
{
    public bool isMoving = true;
    public float scale = 0.005f;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(0.0f, (Mathf.Sin(Time.time) * scale), 0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("RangedCharacter") || other.CompareTag("MeleeCharacter"))
        {
            PlayerStats statsHandle = FindObjectOfType<PlayerStats>();

            if (statsHandle != null)
            {
                bool canDestroy = statsHandle.PickupHealthPotion();
                if (canDestroy)
                    Destroy(gameObject);
            }
        }
    }
}