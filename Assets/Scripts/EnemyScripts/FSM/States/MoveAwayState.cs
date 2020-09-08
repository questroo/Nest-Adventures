using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Assets.Scripts.EnemyScripts.FSM.States
{
    [CreateAssetMenu(fileName = "MoveAwayState", menuName = "Finite State Machine/States/MoveAwayState", order = 4)]
    public class MoveAwayState : AbstractFSMState
    {
        float changePositionCooldown = 0.0f;

        public override void OnEnable()
        {
            StateType = FSMStateType.MOVEAWAY;
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
                Debug.Log("Entered MoveAway State");
            }

            ChangePositionToMoveAway();

            return enteredState;
        }

        public override void UpdateState()
        {
            if(enteredState)
            {
                if(changePositionCooldown > 0)
                {
                    changePositionCooldown -= Time.deltaTime;
                }

                float distance = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
                if (distance <= rangedEnemy.minWeaponRange)
                {
                    if (changePositionCooldown <= 0)
                    {
                        ChangePositionToMoveAway();
                    }
                }
                else if(distance > rangedEnemy.maxWeaponRange)
                {
                    finiteStateMachine.EnterState(FSMStateType.CHASE);
                    return;
                }
                else if(distance <= rangedEnemy.maxWeaponRange && distance >= rangedEnemy.minWeaponRange)
                {
                    finiteStateMachine.EnterState(FSMStateType.ATTACK);
                    return;
                }
            }
        }

        void ChangePositionToMoveAway()
        {
            Vector3 newPosition = (navMeshAgent.transform.position - player.transform.position).normalized;
            newPosition *= (rangedEnemy.minWeaponRange * 2.0f);
            newPosition += navMeshAgent.transform.position;

            navMeshAgent.SetDestination(newPosition);

            Debug.Log("Running Away!!");
            changePositionCooldown = rangedEnemy.forcedPositionChangeCooldown;
        }

        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("Exiting MoveAway State");

            return true;
        }
    }
}