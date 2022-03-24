using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class PreSpikeAttack : BossState<CentryBoss>
    {
        public PreSpikeAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Spike attack: Anticipation");
            boss.StartCoroutine(Anticipate());
        }

        IEnumerator Anticipate()
        {
            yield return new WaitForSeconds(2);
            bossMachine.ChangeState(boss.spikeAttack);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}