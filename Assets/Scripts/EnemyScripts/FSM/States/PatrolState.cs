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
        int patrolIndex;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.PATROL;
            patrolIndex = -1;

            player = FindObjectOfType<PlayerStats>().gameObject;
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
                    if (patrolIndex == -1)
                    {
                        patrolIndex = Random.Range(0, patrolPoints.Length);
                        SetDestination(patrolPoints[patrolIndex]);
                        enteredState = true;
                    }
                    else
                    {
                        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                        SetDestination(patrolPoints[patrolIndex]);
                        enteredState = true;
                    }
                }
            }

            Debug.Log("Entering Patrol State. Heading to waypoint index " + patrolIndex);
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

                distance = Vector3.Distance(navMeshAgent.transform.position, patrolPoints[patrolIndex].position);
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