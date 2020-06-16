using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EnemyScripts.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField]
        AbstractFSMState startingState;

        AbstractFSMState previousState;
        AbstractFSMState currentState;

        public void Awake()
        {
            currentState = null;
        }

        public void Start()
        {
            if(startingState)
            {
                EnterState(startingState);
            }
        }

        public void Update()
        {
            if(currentState)
            {
                currentState.UpdateState();
            }
        }

        #region STATE MANAGEMENT
        public void EnterState(AbstractFSMState nextState)
        {
            if (!nextState)
                return;

            currentState = nextState;

            currentState.EnterState();
        }
        #endregion

    }
}