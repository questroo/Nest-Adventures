using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    //public Transform attackPoint;
    private EnemyStat enemyStat;
    private PlayerStats ps;
    //public float attackRange = 0.5f;
    //public LayerMask playerLayer;

    //[SerializeField]
    //private ParticleSystem trail_particle;

    private void Start()
    {
        enemyStat = GetComponentInParent<EnemyStat>();
        ps = ServiceLocator.Get<PlayerStats>();
    }

    public void Attack()
    {
    //    trail_particle.Play();
    //    Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange);

    //    if(hitPlayer.Length == 0)
    //    {
    //        int i = 0;
    //    }

    //    foreach (Collider player in hitPlayer)
    //    {
    //        player.GetComponent<PlayerStats>().TakeDamage(enemyStat.bossDamage);            
    //    }
    //    StartCoroutine(TurnOffParticles());
    }

    //IEnumerator TurnOffParticles()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    trail_particle.Stop();
    //}
   
    //private void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //        return;
    //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            SoundManager.PlaySound(SoundManager.Sound.player2_get_hit);
            ps.TakeDamage(enemyStat.bossDamage);
        }
    }
}
