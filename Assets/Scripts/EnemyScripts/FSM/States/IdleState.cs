using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EnemyScripts.FSM.States
{
    [CreateAssetMenu(fileName = "IdleState", menuName ="Unity-FSM/States/Idle", order =1)]
    public class IdleState : AbstractFSMState
    {
        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override bool EnterState()
        {
            base.EnterState();
            Debug.Log("Entered Idle State");

            return true;
        }

        public override void UpdateState()
        {
        }

        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("Exiting Idle State");

            return true;
        }
    }
}