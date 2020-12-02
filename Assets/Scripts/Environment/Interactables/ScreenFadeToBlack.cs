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
    [Tooltip("When the player falls off into a kill volume without a specific Teleport position, the screen will fade to black and they will teleport here...")]
    public Transform dungeonStart;
    private Transform teleportPosition;

    GameObject playerHandle;
    float timer;
    FadeState fadeState = FadeState.Clear;

    private void Start()
    {
        playerHandle = ServiceLocator.Get<DualPlayerController>().gameObject;
        teleportPosition = dungeonStart;
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
                // Reset to Dungeon Start to ensure there will always be a teleport position ( see TeleportPlayer() )
                teleportPosition = dungeonStart;
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

    public void TeleportPlayer(Transform specificPosition = null)
    {
        if (specificPosition != null && fadeState == FadeState.Clear)
            teleportPosition = specificPosition;

        if (fadeState == FadeState.Clear)
            fadeState = FadeState.FadingIn;
    }

    public Transform GetDungeonEntrance()
    {
        return teleportPosition;
    }
}