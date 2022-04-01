using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class BackJump : BossState<CentryBoss>
    {
        public BackJump(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) {}
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Back jump: Enter");
            boss.OnChangeState?.Invoke("Back Jump");
            boss.body.AddForce(boss.jumpDirection * boss.jumpStrength, ForceMode2D.Impulse);
            boss.StartCoroutine(Jump());
        }

        IEnumerator Jump()
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
