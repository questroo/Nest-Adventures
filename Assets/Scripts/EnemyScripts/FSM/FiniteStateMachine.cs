using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.EnemyScripts.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField]
        AbstractFSMState startingState;
        AbstractFSMState previousState;
        AbstractFSMState currentState;

        [SerializeField]
        List<AbstractFSMState> validStates;
        Dictionary<FSMStateType, AbstractFSMState> fsmStates;

        public void Awake()
        {
            currentState = null;

            fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            RangedEnemy rangedEnemy = GetComponent<RangedEnemy>();

            foreach(AbstractFSMState state in validStates)
            {
                state.SetExecutingFSM(this);
                state.SetExecutingNavMeshAgent(navMeshAgent);
                state.SetExecutingRangedEnemy(rangedEnemy);
                fsmStates.Add(state.StateType, state);
            }
        }

        public void Start()
        {
            if (startingState)
            {
                EnterState(startingState);
            }
        }

        public void Update()
        {
            if (currentState)
            {
                currentState.UpdateState();
            }
        }

        #region STATE MANAGEMENT
        public void EnterState(AbstractFSMState nextState)
        {
            if (!nextState)
            {
                return;
            }

            if (currentState != null)
            {
                currentState.ExitState();
            }

            currentState = nextState;
            currentState.EnterState();
        }

        public void EnterState(FSMStateType stateType)
        {
            if(fsmStates.ContainsKey(stateType))
            {
                AbstractFSMState nextState = fsmStates[stateType];

                EnterState(nextState);
            }
        }
        #endregion
    }
}