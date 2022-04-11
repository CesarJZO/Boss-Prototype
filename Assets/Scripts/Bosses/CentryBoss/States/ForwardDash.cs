using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class ForwardDash : BossState<CentryBoss>
    {
        Vector2 _target;
        Vector2 _velocity = Vector2.zero;
        public ForwardDash(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Forward dash: Enter");
            boss.OnChangeState?.Invoke("Forward dash");
            boss.target = boss.leftBuilding.position;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            boss.body.MovePosition(Vector2.SmoothDamp(
                boss.transform.position, boss.leftBuilding.position,
                ref _velocity, boss.smoothTime
            ));
            if (boss.CloseToTarget)
                bossMachine.ChangeState(boss.idling);
        }

        public override void Exit()
        {
            base.Exit();
            boss.target = boss.rightBuilding.position;
        }
    }
}
