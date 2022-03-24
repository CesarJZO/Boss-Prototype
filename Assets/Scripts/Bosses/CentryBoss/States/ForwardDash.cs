using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class ForwardDash : BossState<CentryBoss>
    {
        public ForwardDash(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Forward dash: Enter");
            boss.StartCoroutine(Dash());
        }

        IEnumerator Dash()
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