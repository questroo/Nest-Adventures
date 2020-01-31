﻿using System.Collections;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject[] Characters;
    private int m_CharacterIndex = 0;

    private void Start()
    {
        Characters[1].SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CharacterSwap();
            StartCoroutine(CharacterSwapping());
        }
    }
    private void CharacterSwap()
    {
        m_CharacterIndex = ++m_CharacterIndex % 2;
        Characters[m_CharacterIndex].SetActive(true);
        if (m_CharacterIndex == 0)
        {
            Characters[1].SetActive(false);
        }
        else
        {
            Characters[0].SetActive(false);
        }
    }
    IEnumerator CharacterSwapping()
    {
        //disable input
        Characters[m_CharacterIndex].GetComponentInParent<PlayerController>().DisableInput();
        //start IFrame
        Characters[m_CharacterIndex].GetComponentInParent<PlayerStats>().StartIFrame();
        //start anim
        yield return new WaitForSeconds(1f);
        //enable input
        Characters[m_CharacterIndex].GetComponentInParent<PlayerController>().EnableInput();
        //endIframe
        Characters[m_CharacterIndex].GetComponentInParent<PlayerStats>().EndIFrame();
        //anim done
    }
}

