using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class BackJump : BossState<CentryBoss>
    {
        public BackJump(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }
        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Back jump: Enter");
            boss.OnChangeState?.Invoke("Back Jump");
            boss.body.AddForce(boss.jumpDirection * boss.jumpStrength, ForceMode2D.Impulse);
        }

        public override void PhysicsUpdate()
        {
            base.LogicUpdate();
            if (boss.IsAlignedWithTarget)
                boss.body.drag = boss.fallingDrag;
            if (boss.Grounded)
                bossMachine.ChangeState(boss.idling);
        }

        void SwitchDirection()
        {
            boss.jumpDirection.x *= -1;
        }

        public override void Exit()
        {
            base.Exit();
            boss.body.drag = 0;
        }
    }
}
