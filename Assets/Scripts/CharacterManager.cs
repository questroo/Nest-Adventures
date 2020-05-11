using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    // Controls
    PlayerControls control;
    public GameObject[] Characters;
    private int m_CharacterIndex = 1;
    public float swapTime = 1.0f;
    public float iFrameTime = 1.0f;

    private bool swapping = false;
    private void Awake()
    {
        control = new PlayerControls();

        control.ActionMap.CharacterSwap.performed += ctx => Swap();
    }
    private void Start()
    {
        Characters[0].SetActive(false);
    }
    IEnumerator CharacterSwapping()
    {
        swapping = true;
        m_CharacterIndex = ++m_CharacterIndex % 2;
        //disable input
        Characters[m_CharacterIndex].GetComponentInParent<PlayerController>().DisableInput();
        Characters[m_CharacterIndex].GetComponentInParent<AttackManager>().DisableInput();
        //start IFrame
        Characters[m_CharacterIndex].GetComponentInParent<PlayerStats>().StartIFrame();
        //start anim
        yield return new WaitForSeconds(swapTime);
        //enable input
        Characters[m_CharacterIndex].GetComponentInParent<PlayerController>().EnableInput();
        Characters[m_CharacterIndex].GetComponentInParent<AttackManager>().EnableInput();
        //endIframe
        Characters[m_CharacterIndex].GetComponentInParent<PlayerStats>().EndIFrame();
        //anim done
        Characters[m_CharacterIndex].SetActive(true);
        if (m_CharacterIndex == 0)
        {
            Characters[1].SetActive(false);
        }
        else
        {
            Characters[0].SetActive(false);
        }
        swapping = false;
        //Characters[m_CharacterIndex].GetComponentInParent<PlayerController>().ResetCharacterComponents();
    }
    public string GetCurrentPlayerTag()
    {
        return Characters[m_CharacterIndex].tag;
    }
    void Swap()
    {
        if (!swapping)
        {
            StartCoroutine("CharacterSwapping");
        }
    }
    private void OnEnable()
    {
        control.ActionMap.Enable();
    }
    private void OnDisable()
    {
        control.ActionMap.Disable();
    }
}

