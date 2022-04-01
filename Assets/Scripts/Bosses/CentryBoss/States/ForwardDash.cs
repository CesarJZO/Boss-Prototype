using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class ForwardDash : BossState<CentryBoss>
    {
        Vector2 _target;
        Vector2 _velocity = Vector3.zero;
        public ForwardDash(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Forward dash: Enter");
            boss.OnChangeState?.Invoke("Forward dash");
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            boss.body.MovePosition(Vector2.SmoothDamp(
                boss.transform.position, boss.leftBuilding.position,
                ref _velocity, boss.smoothTime
            ));
            // Debug.Log(Vector2.Distance(boss.transform.position, boss.leftBuilding.position));
            if (Vector2.Distance(boss.transform.position, boss.leftBuilding.position) < 0.55f)
                bossMachine.ChangeState(boss.idling);
        }
    }
}
