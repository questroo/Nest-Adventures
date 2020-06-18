using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EnemyScripts.FSM.States
{
    [CreateAssetMenu(fileName = "AttackState", menuName = "Finite State Machine/States/AttackState", order = 5)]
    public class AttackState : AbstractFSMState
    {
        float cooldown = 0.0f;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.ATTACK;
        }

        public override bool EnterState()
        {
            enteredState = base.EnterState();

            if (enteredState)
            {
                Debug.Log("Entered Attack State");
            }

            navMeshAgent.isStopped = true;

            return enteredState;
        }

        public override void UpdateState()
        {
            if (enteredState)
            {
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                }

                float distance = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
                if (distance <= rangedEnemy.minWeaponRange)
                {
                    finiteStateMachine.EnterState(FSMStateType.MOVEAWAY);
                    return;
                }
                else if(distance >= rangedEnemy.maxWeaponRange)
                {
                    finiteStateMachine.EnterState(FSMStateType.CHASE);
                    return;
                }
                else
                {
                    navMeshAgent.transform.LookAt(player.transform.position);

                    if (cooldown <= 0)
                    {
                        Attack();
                        cooldown = rangedEnemy.attackCooldown;
                    }
                }
            }
        }

        void Attack()
        {
            GameObject arrow = Instantiate(rangedEnemy.projectileWeapon, rangedEnemy.projectileLaunchPosition.position, rangedEnemy.projectileLaunchPosition.rotation);
            arrow.GetComponent<Rigidbody>().velocity = navMeshAgent.transform.forward * rangedEnemy.projectileLaunchForce;
        }

        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("Exiting Attack State");

            navMeshAgent.isStopped = false;

            return true;
        }
    }
}