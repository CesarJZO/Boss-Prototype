using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class SpikeAttack : BossState<CentryBoss>
    {
        public SpikeAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Spike attack: Enter");
            boss.OnChangeState?.Invoke("Spike Attack");
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