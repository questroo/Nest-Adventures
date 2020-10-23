using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicSequence : MonoBehaviour
{
    public string sequenceName = "DEFAULT";

    [Tooltip("Sequence will play on start for testing purposes.")]
    public bool isInTestMode = false;

    public GameObject mainCamera;
    CinematicCameraInfo[] cameras;
    float[] delays;

    private void Start()
    {
        CinematicCameraInfo[] allCameras = FindObjectsOfType<CinematicCameraInfo>();                // Grab all cameraInfo scripts in the scene

        int count = 0;
        foreach (CinematicCameraInfo camInfo in allCameras)
        {
            if(camInfo.cinematicName == sequenceName)
            {
                count++;
            }
        }

        cameras = new CinematicCameraInfo[count];
        delays = new float[count];

        foreach (CinematicCameraInfo camInfo in allCameras)
        {
            cameras.SetValue(camInfo, camInfo.cameraOrder);                                     // Set both the camera array and the delay array to appropriate values through cameraInfo script
            delays.SetValue(camInfo.camTime, camInfo.cameraOrder);
            camInfo.gameObject.SetActive(false);
        }

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

    IEnumerator Sequence()
    {
        cameras[0].gameObject.SetActive(true);
        cameras[0].Fade();
        mainCamera.gameObject.SetActive(false);

        yield return new WaitForSeconds(delays[0]);

        for (int index = 1; index < cameras.Length; ++index)
        {
            cameras[index].gameObject.SetActive(true);
            cameras[index].Fade();
            cameras[index-1].gameObject.SetActive(false);

            yield return new WaitForSeconds(delays[index]);
        }

        mainCamera.gameObject.SetActive(true);
        cameras[cameras.Length - 1].gameObject.SetActive(false);
    }
}