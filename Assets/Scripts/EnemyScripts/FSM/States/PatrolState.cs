using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.EnemyScripts.FSM.States
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-FSM/States/Patrol", order = 2)]
    class PatrolState : AbstractFSMState
    {
        public Transform[] patrolPoints;
        int patrolIndex = -1;

        public override bool EnterState()
        {
            if(base.EnterState())
            {
                if(patrolPoints == null || patrolPoints.Length == 0)
                {
                    Debug.LogError("PatrolState Failed. Patrol points invalid...");
                    return false;
                }

                if(patrolIndex == -1)
                {
                    patrolIndex = Random.Range(0, patrolPoints.Length);
                }
                else
                {
                    patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                }
            }

            return true;
        }

        public override void UpdateState()
        {
            
        }

        private void SetDestination(Transform destination)
        {

        }

        public override bool ExitState()
        {
            return true;
        }
    }
}