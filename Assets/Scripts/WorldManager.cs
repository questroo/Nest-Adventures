using System.Collections;
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
        Characters[m_CharacterIndex].GetComponent<PlayerController>().DisableInput();
        //start IFrame
        Characters[m_CharacterIndex].GetComponent<PlayerStats>().StartIFrame();
        //start anim
        yield return new WaitForSeconds(1);
        //enable input
        Characters[m_CharacterIndex].GetComponent<PlayerController>().EnableInput();
        //endIframe
        Characters[m_CharacterIndex].GetComponent<PlayerStats>().EndIFrame();
        //anim done
    }
}

