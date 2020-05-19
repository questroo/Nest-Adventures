using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicSequence : MonoBehaviour
{
    [Tooltip("Sequence will play on start for testing purposes.")]
    public bool isInTestMode = false;

    public GameObject mainCamera;
    public GameObject[] cameras;
    public float[] delays;

    private void Start()
    {
        if(isInTestMode)
            StartCinematic();
    }

    public void StartCinematic()
    {
        if (cameras.Length == delays.Length)
            StartCoroutine(Sequence());
        else
            Debug.LogError("Number of delays and number of cameras must match!! This sequence will not play from object called: " + gameObject.name);
    }


    /// Sequence starts the camera animation in the scene. Functionality for multiple cameras is included, but a single camera may be used too.
    /// Add an animation to a camera and the delay it takes for the camera to finish its intended sequence. If the delay is too short, the camera will switch to the next too quickly.
    /// 
    /// 

    IEnumerator Sequence()
    {
        cameras[0].SetActive(true);
        mainCamera.SetActive(false);

        yield return new WaitForSeconds(delays[0]);

        for (int index = 1; index < cameras.Length; ++index)
        {
            cameras[index].SetActive(true);
            cameras[index-1].SetActive(false);

            yield return new WaitForSeconds(delays[index]);
        }

        mainCamera.SetActive(true);
        cameras[cameras.Length - 1].SetActive(false);
    }
}