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
            Debug.Log("Sword attack: Enter");
            boss.OnChangeState?.Invoke("Sword Attack");
            boss.StartCoroutine(Attack());
        }

        IEnumerator Attack()
        {
            yield return new WaitForSeconds(2);
            bossMachine.ChangeState(boss.idling);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}