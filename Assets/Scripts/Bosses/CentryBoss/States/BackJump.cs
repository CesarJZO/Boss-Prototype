using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class BackJump : BossState<CentryBoss>
    {
        private float _angle;
        private Vector2 _originalDirection;
        public BackJump(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }
        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Back jump: Enter");
            boss.OnChangeState?.Invoke("Back Jump");
            _originalDirection = boss.jumpDirection;
            _angle = boss.jumpAngle;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            boss.transform.Translate(boss.jumpDirection * Time.deltaTime * boss.jumpSpeed);
            _angle = Mathf.Clamp(_angle, -90, 90);
            if (boss.NearOfTargetBuilding)
            {
                if (boss.jumpDirection.normalized != Vector2.down)
                    RotateDirection();
                if (boss.Grounded)
                    bossMachine.ChangeState(boss.idling);
            } 
        }

        void RotateDirection()
        {
            if (boss.jumpDirection.normalized == Vector2.down) return;
            boss.jumpDirection = Quaternion.Euler(0, 0, _angle) * Vector2.right;
            _angle -= boss.rotationSpeed * Time.deltaTime;
        }

        void SwitchDirection()
        {
            boss.jumpDirection.x *= -1;
        }

        public override void Exit()
        {
            base.Exit();
            boss.jumpDirection = _originalDirection;
        }
    }
}
