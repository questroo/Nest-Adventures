using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.EnemyScripts.FSM.States
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "Finite State Machine/States/Patrol", order = 2)]
    class PatrolState : AbstractFSMState
    {
        Transform[] patrolPoints;

        public override void Awake()
        {
            StateType = FSMStateType.PATROL;
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override bool EnterState()
        {
            enteredState = false;

            if (base.EnterState())
            {
                patrolPoints = rangedEnemy.GetPatrolPoints();

                if (patrolPoints == null || patrolPoints.Length == 0)
                {
                    Debug.LogError("PatrolState Failed. Patrol points invalid...");
                    enteredState = false;
                }
                else
                {
                    if (rangedEnemy.patrolIndex == -1)
                    {
                        rangedEnemy.patrolIndex = Random.Range(0, patrolPoints.Length);
                        SetDestination(patrolPoints[rangedEnemy.patrolIndex]);
                        enteredState = true;
                    }
                    else
                    {
                        rangedEnemy.patrolIndex = (rangedEnemy.patrolIndex + 1) % patrolPoints.Length;
                        SetDestination(patrolPoints[rangedEnemy.patrolIndex]);
                        enteredState = true;
                    }
                }
            }

            Debug.Log("Entering Patrol State. Heading to waypoint index " + rangedEnemy.patrolIndex);
            return enteredState;
        }

        public override void UpdateState()
        {
            if (enteredState)
            {
                patrolPoints = rangedEnemy.GetPatrolPoints();

                float distance = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
                if (distance <= 10.0f)
                {
                    finiteStateMachine.EnterState(FSMStateType.CHASE);
                    return;
                }

                distance = Vector3.Distance(navMeshAgent.transform.position, patrolPoints[rangedEnemy.patrolIndex].position);
                if (distance <= rangedEnemy.waypointDistanceCheck)
                {
                    finiteStateMachine.EnterState(FSMStateType.IDLE);
                }
            }
        }

        private void SetDestination(Transform destination)
        {
            navMeshAgent.SetDestination(destination.position);
        }

        public override bool ExitState()
        {
            Debug.Log("Exiting Patrol State.");
            return true;
        }
    }
}