using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    // Singleton Instance
    public static CharacterManager instance = null;
    //public Text countdownText;
    // Controls
    PlayerControls control;
    public GameObject[] Characters;
    public Transform mainPlayer;
    private int m_CharacterIndex = 1;
    public float swapTime = 0.10f;
    public float iFrameTime = 1.0f;
    public float jumpForwardDistance = 1.0f;

    private bool isSwapping = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        control = new PlayerControls();

        control.ActionMap.CharacterSwap.performed += ctx => Swap();
    }
    private void Start()
    {
        Characters[1].SetActive(true);
        Characters[0].SetActive(false);
    }
    IEnumerator CharacterSwapping()
    {
        isSwapping = true;
        Characters[m_CharacterIndex].GetComponentInChildren<Animator>().SetTrigger("Jump");
        var nextPosition = mainPlayer.transform.position;
        var positionAbove = nextPosition;
        positionAbove.y += 7.0f;
        StartCoroutine(MoveToPosition(mainPlayer, positionAbove, swapTime / 2));
        m_CharacterIndex = ++m_CharacterIndex % 2;
        //disable input
        Characters[m_CharacterIndex].GetComponentInParent<PlayerController>().DisableInput();
        Characters[m_CharacterIndex].GetComponentInParent<AttackManager>().DisableInput();
        //start IFrame
        Characters[m_CharacterIndex].GetComponentInParent<PlayerStats>().StartIFrame();
        //start anim
        //enable input
        Characters[m_CharacterIndex].SetActive(true);
        if (m_CharacterIndex == 0)
        {
            Characters[1].SetActive(false);
        }
        else
        {
            Characters[0].SetActive(false);
        }
        StartCoroutine(MoveToPosition(mainPlayer, nextPosition, swapTime / 2));

        Characters[m_CharacterIndex].GetComponentInParent<PlayerController>().EnableInput();
        Characters[m_CharacterIndex].GetComponentInParent<AnimationController>().RegetAnimator();
        Characters[m_CharacterIndex].GetComponentInParent<AttackManager>().EnableInput();
        Characters[m_CharacterIndex].GetComponentInChildren<Animator>().SetTrigger("Landing");
        //endIframe
        Characters[m_CharacterIndex].GetComponentInParent<PlayerStats>().EndIFrame();
        //anim done
        //Invoke("TurnOffSwapping", Characters[m_CharacterIndex].GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
        isSwapping = false;
        yield return null;
    }
    public IEnumerator StartTimer()
    {
        float totalTime = swapTime;
        while (totalTime >= 0)
        {
            totalTime -= Time.deltaTime;
            //countdownText.text = totalTime.ToString("n2");
            yield return null;
        }
    }
    public string GetCurrentPlayerTag()
    {
        return Characters[m_CharacterIndex].tag;
    }
    public bool GetIfMeleeCharacter()
    {
        return m_CharacterIndex == 0 ? true : false;
    }
    void Swap()
    {
        if (!isSwapping)
        {
            StartCoroutine("CharacterSwapping");
        }
    }
    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
    public IEnumerator JumpBehindCamera()
    {

        yield return new WaitForSeconds(swapTime / 2);
    }

    public IEnumerator JumpInFrontOfCamera()
    {
        yield return new WaitForSeconds(swapTime / 2);
    }

    public bool IsCharacterSwapping()
    {
        return isSwapping;
    }

    private void TurnOffSwapping()
    {
        isSwapping = false;
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