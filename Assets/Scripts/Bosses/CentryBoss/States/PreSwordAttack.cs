using UnityEngine;
using System.Collections;
namespace EKP.Bosses.Centry
{
    public class PreSwordAttack : BossState<CentryBoss>
    {
        private Vector2 _velocity;
        private Vector2 _target;
        public int Counter { get; private set; } = 0;
        public PreSwordAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) {}

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Sword attack: Anticipation");
            boss.OnChangeState?.Invoke("Pre Sword Attack");
            Counter++;
            _target = boss.player.transform.position;
            boss.StartCoroutine(Anticipate());
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            boss.body.MovePosition(Vector2.SmoothDamp(
                boss.body.position, _target, ref _velocity, boss.smoothTime
            ));
        }

        IEnumerator Anticipate()
        {
            yield return new WaitForSeconds(boss.smoothTime * 2);
            bossMachine.ChangeState(boss.swordAttack);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}