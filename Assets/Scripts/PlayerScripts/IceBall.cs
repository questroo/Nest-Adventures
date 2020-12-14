using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    [Header("Visual Variables")]
    Transform child;
    public float rotationSpeed = 10.0f;

    [Header("Attack Variables")]
    public float moveSpeed = 10.0f;
    //public float rotationDamping = 0.5f;
    Transform target;
    float damage;

    void Start()
    {
        child = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        child.localRotation = Quaternion.Euler(child.rotation.x, child.rotation.y, child.rotation.z + rotationSpeed);
        if (target)
        {
            transform.LookAt(target);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        }
    }

    public void Init(Transform _target, float _damage)
    {
        target = _target;
        damage = _damage;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyStat stat = collision.collider.GetComponent<EnemyStat>();
            if (stat == null)
            {
                Debug.LogError("Iceball cannot find EnemyStat script!");
            }
            else
            {
                stat.TakeDamage(damage);
            }
        }
    }
}