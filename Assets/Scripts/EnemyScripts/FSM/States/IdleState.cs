﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EnemyScripts.FSM.States
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "Finite State Machine/States/Idle", order = 1)]
    public class IdleState : AbstractFSMState
    {
        

        float totalDuration;
        float waitTime;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState()
        {
            enteredState = base.EnterState();

            if (enteredState)
            {
                Debug.Log("Entered Idle State");
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
                if (distance <= 10.0f)
                {
                    finiteStateMachine.EnterState(FSMStateType.CHASE);
                    return;
                }

                totalDuration += Time.deltaTime;
                if (totalDuration >= waitTime)
                {
                    finiteStateMachine.EnterState(FSMStateType.PATROL);
                }
            }
        }

        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("Exiting Idle State");

            return true;
        }
    }
}