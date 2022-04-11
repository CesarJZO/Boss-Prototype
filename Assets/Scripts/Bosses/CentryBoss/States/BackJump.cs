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
            boss.target = boss.rightBuilding.position;
            boss.body.AddForce(boss.jumpDirection * boss.jumpStrength, ForceMode2D.Impulse);
        }

        public override void PhysicsUpdate()
        {
            base.LogicUpdate();
            if (boss.CloseToTarget && boss.Grounded)
                bossMachine.ChangeState(boss.idling);
            if (boss.IsAlignedWithTarget)
                boss.body.drag = boss.fallingDrag;
        }

        public override void Exit()
        {
            base.Exit();
            boss.target = boss.leftBuilding.position;
            boss.body.drag = 0;
        }
    }
}
