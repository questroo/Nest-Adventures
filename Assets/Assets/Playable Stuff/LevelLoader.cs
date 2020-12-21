using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    public GameObject topFloor;

    public GameObject bottomFloor;


    void Start()
    {
        //bottomFloor.SetActive(false);

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Tanjiro") || other.CompareTag("Bertha"))
        {
            topFloor.SetActive(false);
            bottomFloor.SetActive(true);
        }


    }

}
