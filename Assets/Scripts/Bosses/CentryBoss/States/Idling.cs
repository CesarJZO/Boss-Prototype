using System.Collections;
using UnityEngine;
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
            boss.StartCoroutine(StateManager());
        }

        IEnumerator StateManager()
        {   //  Idle -> 2-3 sword attacks -> All random
            float randomTime = Random.Range(boss.minTime, boss.maxTime);
            Debug.Log($"Idling for {randomTime} seconds");
            yield return new WaitForSeconds(randomTime);

            if (boss.preSwordAttack.counter < _totalOfSwordAttacks)
            {
                Debug.Log($"Sword attacks so far: {boss.preSwordAttack.counter}");
                bossMachine.ChangeState(boss.preSwordAttack);
            }
            else
            {
                Debug.Log("Enough of sword, lo que caiga!");
                StartRandomMove(Random.Range(1, 6));
            }
        }

        void StartRandomMove(int randomMove)
        {
            switch (randomMove)
            {
                case 1:
                    bossMachine.ChangeState(boss.forwardDash);
                    break;
                case 2:
                    bossMachine.ChangeState(boss.backJump);
                    break;
                case 3:
                    bossMachine.ChangeState(boss.preSwordAttack);
                    break;
                case 4:
                    bossMachine.ChangeState(boss.preSpikeAttack);
                    break;
                case 5:
                    bossMachine.ChangeState(boss.preChargeAttack);
                    break;
            }
        }
    }
}