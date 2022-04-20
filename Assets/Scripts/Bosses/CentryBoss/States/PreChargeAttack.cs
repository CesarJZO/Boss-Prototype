using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class PreChargeAttack : BossState<CentryBoss> 
    {
        public PreChargeAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) {}

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Charge attack: Anticipation");
            boss.OnChangeState?.Invoke("Pre Charge Attack");
            boss.StartCoroutine(Anticipate());
        }

        IEnumerator Anticipate()
        {
            yield return new WaitForSeconds(boss.chargeAnticipationTime);
            bossMachine.ChangeState(boss.chargeAttack);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
