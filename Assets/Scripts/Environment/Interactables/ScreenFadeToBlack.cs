using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeToBlack : MonoBehaviour
{
    enum FadeState
    {
        Clear,
        FadingIn,
        FullBlack,
        Wait,
        FadingOut
    }

    [Tooltip("The time it takes for the screen to fully fade from clear to black")]
    public float fadeDuration = 0.8f;
    [Tooltip("The time it waits until it fades from fully black back to clear")]
    public float blackDuration = 0.5f;
    public CanvasGroup fadeToBlackCanvasGroup;
    [Tooltip("When the player falls off into a kill volume, the screen will fade to black and they will teleport here...")]
    public Transform teleportPosition;

    GameObject playerHandle;
    float timer;
    FadeState fadeState = FadeState.Clear;

    private void Start()
    {
        playerHandle = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        switch (fadeState)
        {
            case FadeState.FadingIn:

                timer += Time.deltaTime;
                fadeToBlackCanvasGroup.alpha = timer / fadeDuration;
                if (timer > fadeDuration)
                {
                    fadeState = FadeState.FullBlack;
                    timer = blackDuration;
                }

                break;


            case FadeState.FullBlack:
                // Teleport player to dungeon start
                playerHandle.gameObject.transform.position = teleportPosition.position;
                playerHandle.gameObject.transform.rotation = teleportPosition.rotation;
                fadeState = FadeState.Wait;

                break;

            case FadeState.Wait:

                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    fadeState = FadeState.FadingOut;
                    timer = fadeDuration;
                }

                break;


            case FadeState.FadingOut:

                timer -= Time.deltaTime;
                fadeToBlackCanvasGroup.alpha = timer / fadeDuration;
                if (timer < 0)
                {
                    fadeState = FadeState.Clear;
                    timer = 0.0f;
                }
                break;
        }
    }

    public void TriggerFade()
    {
        if (fadeState == FadeState.Clear)
            fadeState = FadeState.FadingIn;
    }

    public Transform GetDungeonEntrance()
    {
        return teleportPosition;
    }
}