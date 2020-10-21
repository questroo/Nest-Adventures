using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.EnemyScripts.FSM.States
{
    //[CreateAssetMenu(fileName = "ChaseState", menuName = "Finite State Machine/States/Chase", order = 3)]
    public class ChaseState : AbstractFSMState
    {
        float cooldownRemaining = 0.0f;

        float distance;

        public override void OnEnable()
        {
            StateType = FSMStateType.CHASE;
        }

        public override void Awake()
        {
            base.Awake();
        }

        public override bool EnterState()
        {
            enteredState = base.EnterState();

            if (enteredState)
            {
                Debug.Log("Entered Chase State");
            }

            navMeshAgent.speed += rangedEnemy.chaseStateSpeedModifier;

            return enteredState;
        }

        public override void UpdateState()
        {
            if (enteredState)
            {
                float distance;
                distance = Vector3.Distance(player.transform.position, navMeshAgent.transform.position);

                if (distance >= rangedEnemy.targetLossRange)
                {
                    finiteStateMachine.EnterState(FSMStateType.IDLE);
                    return;
                }
                else if (distance >= rangedEnemy.maxWeaponRange)
                {
                    Vector3 newPosition = (player.transform.position - navMeshAgent.transform.position).normalized;
                    newPosition *= (rangedEnemy.maxWeaponRange / 2.0f);
                    newPosition += navMeshAgent.transform.position;
                    navMeshAgent.SetDestination(newPosition);
                }
                else if (distance >= rangedEnemy.minWeaponRange && distance <= rangedEnemy.maxWeaponRange)
                {
                    finiteStateMachine.EnterState(FSMStateType.ATTACK);
                    return;
                }
            }
        }

        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("Exiting Chase State");

            navMeshAgent.speed -= rangedEnemy.chaseStateSpeedModifier;

            return true;
        }
    }
}