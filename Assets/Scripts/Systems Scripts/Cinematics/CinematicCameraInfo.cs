using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameraInfo : MonoBehaviour
{
    public CinematicFade fader;

    [Tooltip("This marks the cinematic camera with the correct cinematic sequencer of the same name! Make sure its right")]
    public string cinematicName = "DEFAULT";
    [Tooltip("This integer tells the sequencer which order the cameras should be activated in")]
    public int cameraOrder = 0;

    public bool isUsingFadeIn = false;
    public bool isUsingFadeOut = false;

    [Tooltip("This will be autofilled by the script. Here for Debugging!")]
    public float camTime = 0.0f;

    [Tooltip("This is a small delay to ensure the animation does not loop")]
    public float delayPadding = 0.0f;

    void Awake()
    {
        AnimatorClipInfo[] clipInfo = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);

        camTime = clipInfo[0].clip.length - delayPadding;

        fader = FindObjectOfType<CinematicFade>();
        if (!fader)
        {
            Debug.LogError("No Cinematic Fader found!! Cutscenes will not fade to black!");
        }
    }

    public void Fade()
    {
        if(isUsingFadeIn)
        {
            fader.TriggerFadeBlackToCamera();
        }
        if(isUsingFadeOut)
        {
            StartCoroutine(CamFade());
        }
    }

    public IEnumerator CamFade()
    {
        yield return new WaitForSeconds(camTime - fader.fadeDuration - fader.blackDuration);
        fader.TriggerFadeToBlack();
    }
}