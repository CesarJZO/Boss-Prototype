using System.Collections;
using UnityEngine;

using UnityEngine.InputSystem;

namespace EKP.Bosses.Centry
{
    public class Idling : BossState<CentryBoss>
    {
        int _totalOfSwordAttacks;
        public Idling(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine)
        {
            _totalOfSwordAttacks = Random.Range(2, 4);
        }
        public override void Enter()
        {
            base.Enter();
            boss.OnChangeState?.Invoke("Idle");
            if (boss.automaticBehaviour) boss.StartCoroutine(StateManager());
        }

        IEnumerator StateManager()
        {   //  Idle -> 2-3 sword attacks -> All random
            float randomTime = Random.Range(boss.minTime, boss.maxTime);
            Debug.Log($"Idling for {randomTime} seconds");
            yield return new WaitForSeconds(randomTime);

            if (boss.preSwordAttack.Counter < _totalOfSwordAttacks)
                bossMachine.ChangeState(boss.preSwordAttack);
            else
                StartRandomMove(Random.Range(1, 6));
        }

        void StartRandomMove(int randomMove)
        {
            switch (randomMove)
            {
                case 1: bossMachine.ChangeState(boss.forwardDash); break;
                case 2: bossMachine.ChangeState(boss.backJump); break;
                case 3: bossMachine.ChangeState(boss.preSwordAttack); break;
                case 4: bossMachine.ChangeState(boss.preSpikeAttack); break;
                case 5: bossMachine.ChangeState(boss.preChargeAttack); break;
            }
        }

        #region Debug
        
        public override void LogicUpdate()
        {
            if (boss.automaticBehaviour) return;

            base.LogicUpdate();
            var keyboard = Keyboard.current;
            
            if (keyboard.dKey.wasPressedThisFrame)
                bossMachine.ChangeState(boss.forwardDash);
            if (keyboard.jKey.wasPressedThisFrame)
                bossMachine.ChangeState(boss.backJump);
            if (keyboard.sKey.wasPressedThisFrame)
                bossMachine.ChangeState(boss.preSwordAttack);
            if (keyboard.cKey.wasPressedThisFrame)
                bossMachine.ChangeState(boss.preChargeAttack);
            if (keyboard.pKey.wasPressedThisFrame)
                bossMachine.ChangeState(boss.preSpikeAttack);
            
        }

        #endregion
    }
}
