using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitBoxProjection : MonoBehaviour
{
    public CameraController cameraController;
    public CharacterManager characterManager;

    public GameObject firstProjectile;
    public GameObject secondProjectile;
    public GameObject thirdProjectile;
    public GameObject firstProjectileClear;
    public GameObject secondProjectileClear;
    public GameObject thirdProjectileClear;
    public Transform projectileSpawnLocation;
    public float projectileForce = 2.0f;
    public float projectileLifeTime = 0.3f;

    public void DoAttack(int combo)
    {
        if (characterManager.GetCurrentPlayerTag() == "MeleeCharacter")
        {
            ProjectHitbox(combo);
        }
        else
        {
            ShootProjectile(combo);
        }
    }

    public void ProjectHitbox(int combo)
    {
        switch (combo)
        {
            case 1:
                GameObject go1 = Instantiate(firstProjectileClear, projectileSpawnLocation.position, Quaternion.identity) as GameObject;
                StartCoroutine(DestroyProjectile(go1));
                break;
            case 2:
                GameObject go2 = Instantiate(secondProjectileClear, projectileSpawnLocation.position, Quaternion.identity) as GameObject;
                StartCoroutine(DestroyProjectile(go2));
                break;
            case 3:
                GameObject go3 = Instantiate(thirdProjectileClear, projectileSpawnLocation.position, Quaternion.identity) as GameObject;
                StartCoroutine(DestroyProjectile(go3));
                break;
            default:
                break;
        }
    }
    public void ShootProjectile(int combo)
    {
        SoundManager.PlaySound(SoundManager.Sound.player2_windup, gameObject.transform.position);
        GameObject go = new GameObject();
        switch (combo)
        {
            case 1:
                go = Instantiate(firstProjectile, projectileSpawnLocation.position, Quaternion.identity) as GameObject;
                //StartCoroutine(DestroyProjectile(go));
                break;
            case 2:
                go = Instantiate(secondProjectile, projectileSpawnLocation.position, Quaternion.identity) as GameObject;
                //StartCoroutine(DestroyProjectile(go));
                break;
            case 3:
                go = Instantiate(thirdProjectile, projectileSpawnLocation.position, Quaternion.identity) as GameObject;
                //StartCoroutine(DestroyProjectile(go));
                break;
            default:
                break;
        }
        go.transform.forward = transform.forward;
        if (cameraController.GetLockOn())
        {
            Transform target = cameraController.GetCurrentlyLockedOnTransform();
            if (target)
            {

                go.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(target.position - go.transform.position) * projectileForce);
                StartCoroutine("DestroyProjectile", go.gameObject);
            }
        }
        else
        {
            go.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForce);
            StartCoroutine("DestroyProjectile", go.gameObject);
        }
    }
    IEnumerator DestroyProjectile(GameObject hitbox)
    {
        yield return new WaitForSeconds(projectileLifeTime);
        if (hitbox)
        {
            Destroy(hitbox);
        }
    }
}