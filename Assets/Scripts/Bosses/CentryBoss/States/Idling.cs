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

            if (boss.preSwordAttack.counter < _totalOfSwordAttacks)
            {
                // Debug.Log($"Sword attacks so far: {boss.preSwordAttack.counter}");
                bossMachine.ChangeState(boss.preSwordAttack);
            }
            else
            {
                // Debug.Log("Enough of sword, lo que caiga!");
                StartRandomMove(Random.Range(1, 5));
            }
        }

        void StartRandomMove(int randomMove)
        {
            switch (randomMove)
            {
                case 1: Move(); break;
                case 2: bossMachine.ChangeState(boss.preSwordAttack); break;
                case 3: bossMachine.ChangeState(boss.preSpikeAttack); break;
                case 4: bossMachine.ChangeState(boss.preChargeAttack); break;
            }
        }

        void Move()
        {
            if (boss.target == (Vector2)boss.leftBuilding.position)
                bossMachine.ChangeState(boss.forwardDash);
            else
                bossMachine.ChangeState(boss.backJump);
        }

        // * Debug
        
        public override void LogicUpdate()
        {
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

        // * End Debug
    }
}
