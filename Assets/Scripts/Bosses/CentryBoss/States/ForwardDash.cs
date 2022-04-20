using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class ForwardDash : BossState<CentryBoss>
    {
        private float _elapsed;
        private Vector2 _initialPosition;
        private Vector2 _targetPosition;
        public ForwardDash(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Forward dash: Enter");
            boss.OnChangeState?.Invoke("Forward dash");
            _elapsed = 0;
            _initialPosition = boss.Position;
            _targetPosition = boss.buildingTarget;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            float t = Mathf.InverseLerp(0, boss.dashDuration, _elapsed);
            t = Mathf.Sin(t * 90 * Mathf.Deg2Rad);
            boss.transform.position = Vector2.Lerp(_initialPosition, _targetPosition, t);
            _elapsed += Time.deltaTime;
            if (boss.Position == boss.buildingTarget)
                bossMachine.ChangeState(boss.idling);
        }

        public override void Exit()
        {
            base.Exit();
            boss.SwitchTarget();
        }
    }
}
