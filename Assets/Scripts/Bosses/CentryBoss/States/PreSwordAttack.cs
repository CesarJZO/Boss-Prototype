using UnityEngine;
using System.Collections;
namespace EKP.Bosses.Centry
{
    public class PreSwordAttack : BossState<CentryBoss>
    {
        public int counter { get; private set; } = 0;
        public PreSwordAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) {}

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Sword attack: Anticipation");
            boss.OnChangeState?.Invoke("Pre Sword Attack");
            counter++;
            boss.StartCoroutine(Anticipate());
        }

        IEnumerator Anticipate()
        {
            yield return new WaitForSeconds(boss.attacksDuration);
            bossMachine.ChangeState(boss.swordAttack);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}