using UnityEngine;
using System.Collections;
namespace EKP.Bosses.Centry
{
    public class SwordAttack : BossState<CentryBoss>
    {
        public SwordAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) {}

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Sword attack: Enter");
            boss.OnChangeState?.Invoke("Sword Attack");
            boss.StartCoroutine(Attack());
        }

        IEnumerator Attack()
        {
            boss.body.drag = boss.swordDrag;
            // Attack
            yield return new WaitForSeconds(boss.swordDuration);
            boss.body.drag = 0;
            bossMachine.ChangeState(boss.forwardDash);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}