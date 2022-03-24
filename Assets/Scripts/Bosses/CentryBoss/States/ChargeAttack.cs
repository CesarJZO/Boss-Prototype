using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class ChargeAttack : BossState<CentryBoss>
    {
        public ChargeAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Charge attack: Enter");
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