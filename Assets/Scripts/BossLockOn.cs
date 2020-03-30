using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossLockOn : MonoBehaviour
{
    public float range;
    public Transform emptyTarget;

    public CinemachineTargetGroup group;
    public int enemyCount;
    public List<Transform> enemiesToLock;
    public Transform closestEnemy;
    public Transform selectedEnemy;
    public Transform priorityEnemy;
    public bool foundPriorityEnemy;

    public float xScaleIncrement;
    public float yScaleIncrement;
    public float zScaleIncrement;
    public float xScaleMax;
    public float zScaleMax;
    public GameObject targetingCone;
    public GameObject targetingConePivot;
    public Transform coneHolder;
    private Vector3 selectorDirection;
    private bool parentChangeInitializationPerformed;

    private bool temp = false;

    private TargetLockOnCamera cam;
    private PlayerController playerController;
    private TargetingConeTrigger trigger;

    private void Awake()
    {
        cam = GetComponent<TargetLockOnCamera>();
        playerController = GetComponent<PlayerController>();
        trigger = targetingCone.GetComponent<TargetingConeTrigger>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            temp = !temp;
        }
        if (temp)
        {
            RunEnemySearchSphereCollider();
        }
        else
        {
            enemiesToLock.Clear();
            cam.targetLockCam = false;
        }

        enemyCount = enemiesToLock.Count;

        if (enemyCount == 0 || priorityEnemy == null)
        {
            InitializeTargetGroup();
            cam.targetLockCam = false;
            foundPriorityEnemy = false;
            closestEnemy = null;
            selectedEnemy = null;
            priorityEnemy = null;
            InitializeConeParent();
            ResetTargetingCone();
        }

        if (enemyCount != 0)
        {
            cam.targetLockCam = true;
            FindClosestEnemy();
            if (closestEnemy != null && foundPriorityEnemy == false)
            {
                SetPriorityEnemy(closestEnemy);
            }
            SwitchTarget();
            if (selectedEnemy)
            {
                SetPriorityEnemy(selectedEnemy);
            }
            if (priorityEnemy)
            {
                BuildTargetGroup();
            }
        }
    }
    void RunEnemySearchSphereCollider()
    {
        Collider[] enemyDetect = Physics.OverlapSphere(transform.position, range);
        enemiesToLock = new List<Transform>();
        foreach (Collider col in enemyDetect)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemiesToLock.Add(col.transform);
            }
        }
    }
    void FindClosestEnemy()
    {
        float closest = range;
        closestEnemy = null;
        for (int i = 0; i < enemyCount; i++)
        {
            float distanceToPlayer = Vector3.Distance(enemiesToLock[i].position, transform.position);
            if (distanceToPlayer < closest)
            {
                closest = distanceToPlayer;
                closestEnemy = enemiesToLock[i];
            }
        }
    }
    void SetPriorityEnemy(Transform enemy)
    {
        priorityEnemy = enemy;
        foundPriorityEnemy = true;
        SetConeToParent();
    }
    void BuildTargetGroup()
    {
        CinemachineTargetGroup.Target enem;
        enem.target = priorityEnemy;
        enem.weight = priorityEnemy.GetComponent<Enemy>().camWeight;
        enem.radius = priorityEnemy.GetComponent<Enemy>().camRadius;

        group.m_Targets[1].target = enem.target;
        group.m_Targets[1].weight = enem.weight;
        group.m_Targets[1].radius = enem.radius;
    }
    void InitializeTargetGroup()
    {
        CinemachineTargetGroup.Target defaultTarget;
        defaultTarget.target = emptyTarget;
        defaultTarget.weight = emptyTarget.GetComponent<EmptyTargetData>().camWeight;
        defaultTarget.radius = emptyTarget.GetComponent<EmptyTargetData>().camRadius;

        group.m_Targets[1].target = defaultTarget.target;
        group.m_Targets[1].weight = defaultTarget.weight;
        group.m_Targets[1].radius = defaultTarget.radius;

    }
    void SwitchTarget()
    {
        float r = 0.0f;
        r += Input.GetAxis("Mouse X");
        Mathf.Clamp(r, -1.0f, 1.0f);
        float y = 0.0f;
        y += Input.GetAxis("Mouse Y");
        Mathf.Clamp(r, -1.0f, 1.0f);
        if (r == 0 && y == 0)
        {
            ResetTargetingCone();
        }
        else
        {
            selectorDirection = ((playerController.camForward * y) + (playerController.camRight * r)).normalized;
            targetingConePivot.transform.rotation = Quaternion.LookRotation(selectorDirection);
            targetingCone.SetActive(true);

            if (trigger.selectedEnemy != null && trigger.selectedEnemy != priorityEnemy)
            {
                parentChangeInitializationPerformed = false;
                selectedEnemy = trigger.selectedEnemy.transform;
            }
            else
            {
                if (targetingCone.transform.localScale.y <= range) { targetingCone.transform.localScale += new Vector3(0, yScaleIncrement, 0); }
                if (targetingCone.transform.localScale.x <= xScaleMax) { targetingCone.transform.localScale += new Vector3(xScaleIncrement, 0, 0); }
                if (targetingCone.transform.localScale.y <= zScaleMax) { targetingCone.transform.localScale += new Vector3(0, 0, zScaleIncrement); }
            }
        }
    }
    void SetConeToParent()
    {
        targetingConePivot.transform.SetParent(priorityEnemy);
        if (!parentChangeInitializationPerformed)
        {
            targetingConePivot.transform.localPosition = Vector3.zero;
            targetingConePivot.transform.localScale = new Vector3(trigger.coneScaleX, trigger.coneScaleY, trigger.coneScaleZ);
            parentChangeInitializationPerformed = true;
        }
    }
    void InitializeConeParent()
    {
        targetingConePivot.transform.SetParent(coneHolder);
        targetingConePivot.transform.localPosition = Vector3.zero;
        targetingConePivot.transform.localRotation = Quaternion.identity;
        parentChangeInitializationPerformed = false;
    }
    void ResetTargetingCone()
    {
        trigger.selectedEnemy = null;
        targetingCone.SetActive(false);
        targetingConePivot.transform.rotation = transform.rotation;
        targetingCone.transform.localScale = new Vector3(trigger.coneScaleX, trigger.coneScaleY, trigger.coneScaleZ);
    }
}
