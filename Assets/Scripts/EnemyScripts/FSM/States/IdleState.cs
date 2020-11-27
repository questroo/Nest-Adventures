using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EnemyScripts.FSM.States
{
    //[CreateAssetMenu(fileName = "IdleState", menuName = "Finite State Machine/States/Idle", order = 1)]
    public class IdleState : AbstractFSMState
    {
        float totalDuration;
        float waitTime;
        public override void OnEnable()
        {
            StateType = FSMStateType.IDLE;
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
                totalDuration = 0f;
                waitTime = UnityEngine.Random.Range(rangedEnemy.idleWaitTimeMin, rangedEnemy.idleWaitTimeMax);
            }

            return enteredState;
        }

        public override void UpdateState()
        {
            if (enteredState)
            {
                float distance = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
                if (distance <= rangedEnemy.maxWeaponRange)
                {
                    finiteStateMachine.EnterState(FSMStateType.CHASE);
                    return;
                }

                totalDuration += Time.deltaTime;
                if (totalDuration >= waitTime)
                {
                    if(rangedEnemy.behaviourType == EnemyBehaviourType.Patrol)
                        finiteStateMachine.EnterState(FSMStateType.PATROL);
                    else
                    {
                        finiteStateMachine.EnterState(FSMStateType.IDLE);
                    }
                }
            }
            AnimStateCheck();
        }

        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("Exiting Idle State");

            return true;
        }
    }
}