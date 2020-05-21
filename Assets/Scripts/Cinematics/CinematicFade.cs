using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicFade : MonoBehaviour
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
    public float blackDuration = 1.0f;
    public CanvasGroup fadeToBlackCanvasGroup;

    float timer;
    FadeState fadeState = FadeState.Clear;

    bool isFadingToBlack = false;
    bool isFadingFromBlackToCamera = false;

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

    public void TriggerFadeToBlack()
    {
        fadeState = FadeState.FadingIn;
        timer = 0.0f;
    }

    public void TriggerFadeBlackToCamera()
    {
        fadeState = FadeState.FadingOut;
        timer = fadeDuration;
    }
}