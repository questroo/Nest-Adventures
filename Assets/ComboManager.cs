using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    //Cooldown time between attacks (in seconds)
    public float cooldown = 0.5f;
    //Max time before combo ends (in seconds)
    public float maxTime = 1.0f;
    //Cooldown between combos
    public float comboCooldown = 2.0f;
    //Max number of attacks in combo
    public int maxCombo = 3;
    //Current combo
    int combo = 0;
    //Time of last attack
    float lastTime;

    // Use this for initialization
    void Start()
    {

        //Starts the looping coroutine
        StartCoroutine("Melee");
    }

    IEnumerator Melee()
    {
        //Constantly loops so you only have to call it once
        while (true)
        {
            //Checks if attacking and then starts of the combo
            if (Input.GetButtonDown("Fire1"))
            {
                combo++;
                if (GetComponent<AttackManager>().CheckDisableInputStatus() != true)
                {
                    GetComponent<AttackManager>().StartCoroutine("MeleeAttack", combo);
                }
                Debug.Log("Attack" + combo);
                lastTime = Time.time;

                //Combo loop that ends the combo if you reach the maxTime between attacks, or reach the end of the combo
                while ((Time.time - lastTime) < maxTime && combo < maxCombo)
                {
                    //Attacks if your cooldown has reset
                    if (Input.GetButtonDown("Fire1") && (Time.time - lastTime) > cooldown)
                    {
                        combo++;
                        if (GetComponent<AttackManager>().CheckDisableInputStatus() != true)
                        {
                            GetComponent<AttackManager>().StartCoroutine("MeleeAttack", combo);
                        }
                        Debug.Log("Attack " + combo);
                        lastTime = Time.time;
                    }
                    yield return null;
                }
                //Resets combo and waits the remaining amount of cooldown time before you can attack again to restart the combo
                combo = 0;
                yield return new WaitForSeconds(comboCooldown - (Time.time - lastTime));
            }
            yield return null;
        }
    }
}
