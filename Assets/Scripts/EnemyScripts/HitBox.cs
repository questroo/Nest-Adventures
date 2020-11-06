using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Transform attackPoint;
    private EnemyStat enemyStat;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    [SerializeField]
    private ParticleSystem trail_particle;

    private void Start()
    {
        enemyStat = GetComponentInParent<EnemyStat>();
    }

    public void Attack()
    {
        trail_particle.Play();
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            player.GetComponentInParent<PlayerStats>().TakeDamage(enemyStat.bossDamage);
            
        }
        StartCoroutine(TurnOffParticles());
    }

    IEnumerator TurnOffParticles()
    {
        yield return new WaitForSeconds(0.5f);
        trail_particle.Stop();
    }
   
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
