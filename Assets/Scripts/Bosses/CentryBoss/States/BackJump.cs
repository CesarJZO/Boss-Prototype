using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class BackJump : BossState<CentryBoss>
    {
        bool falling;
        public BackJump(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }
        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Back jump: Enter");
            boss.OnChangeState?.Invoke("Back Jump");
            boss.body.bodyType = RigidbodyType2D.Dynamic;
            falling = false;
            SetDirection();
            boss.body.AddForce(boss.jumpDirection * boss.jumpSpeed, ForceMode2D.Impulse);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (boss.NearOfTargetBuilding)
            {
                if (!falling)
                {
                    boss.body.drag = boss.fallingDrag;
                    falling = true;
                    boss.StartCoroutine(AhoritaLeCambioElNombre());
                }
                if (boss.Grounded)
                    bossMachine.ChangeState(boss.idling);
            }
        }

        IEnumerator AhoritaLeCambioElNombre()
        {
            yield return new WaitForSeconds(0.1f);
            boss.body.drag = 0;
            boss.body.AddForce(Vector2.down);
        }

        void SetDirection()
        {
            boss.jumpDirection.x = Mathf.Abs(boss.jumpDirection.x);
            if (boss.buildingTarget == (Vector2) boss.leftBuilding.position)
                boss.jumpDirection.x *= -1;
        }

        public override void Exit()
        {
            base.Exit();
            boss.body.bodyType = RigidbodyType2D.Kinematic;
            boss.StopCoroutine(AhoritaLeCambioElNombre());
            boss.SwitchTarget();
        }
    }
}
