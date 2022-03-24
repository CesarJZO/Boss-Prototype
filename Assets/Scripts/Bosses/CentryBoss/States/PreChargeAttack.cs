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
            Debug.Log("Charge attack: Anticipation");
            boss.StartCoroutine(Anticipate());
        }

        IEnumerator Anticipate()
        {
            yield return new WaitForSeconds(2);
            bossMachine.ChangeState(boss.chargeAttack);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}