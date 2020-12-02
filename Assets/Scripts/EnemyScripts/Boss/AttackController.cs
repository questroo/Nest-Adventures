using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Animator animator;
    float meleeDamage = 20.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("AttackKeyPressed");
            animator.Play("Attack");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<EnemyStat>().TakeDamage(meleeDamage);
        }
        if (!CompareTag("Sword"))
        {
            Destroy(gameObject);
        }
    }
}
