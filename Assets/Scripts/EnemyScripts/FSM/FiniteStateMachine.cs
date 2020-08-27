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

        NavMeshAgent navMeshAgent;
        RangedEnemy rangedEnemy;

        public void Start()
        {
            currentState = null;

            fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

            navMeshAgent = GetComponent<NavMeshAgent>();
            rangedEnemy = GetComponent<RangedEnemy>();

            foreach (AbstractFSMState state in validStates)
            {
                fsmStates.Add(state.StateType, state);
            }

            if (startingState)
            {
                EnterState(startingState);
            }
        }

        public void FixedUpdate()
        {
            if (currentState)
            {
                SetActor(currentState);

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

            SetActor(currentState);

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

        void SetActor(AbstractFSMState state)
        {
            state.SetExecutingFSM(this);
            state.SetExecutingNavMeshAgent(navMeshAgent);
            state.SetExecutingRangedEnemy(rangedEnemy);
        }
    }
}