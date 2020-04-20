using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDungeonTester : MonoBehaviour
{
    public Slider healthBar;

    public GameObject attack;
    GameObject attackInstance;

    float duration = 0.1f;
    float maxDuration = 0.1f;

    public float movespeed = 15.0f;
    public Transform origin;    

    [SerializeField]
    float maxHealth = 100.0f;
    float currentHealth;
    Material myMat;

    public Color alive;
    public Color dead;

    
    // Start is called before the first frame update
    void Start()
    {
        myMat = GetComponent<Renderer>().material;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackInstance)
        {
            duration -= Time.deltaTime;
            if(duration <= 0)
            {
                Destroy(attackInstance);
            }
        }

        HandleInput();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        //myMat.SetColor("_BaseColor", Color.Lerp(dead, alive, (currentHealth / maxHealth)));

        healthBar.value = (currentHealth / maxHealth);
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * movespeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * movespeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !attackInstance)
        {
            attackInstance = Instantiate(attack, origin.position, origin.rotation);
            duration = maxDuration;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20.0f);
        }
    }
}