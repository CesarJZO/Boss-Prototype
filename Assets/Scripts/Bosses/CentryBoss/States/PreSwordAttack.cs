using UnityEngine;
using System.Collections;
namespace EKP.Bosses.Centry
{
    public class PreSwordAttack : BossState<CentryBoss>
    {
        private float _elapsed;
        private Vector2 _initialPosition;
        private Vector2 _targetPosition;
        public int Counter { get; private set; } = 0;
        public PreSwordAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) {}

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Sword attack: Anticipation");
            boss.OnChangeState?.Invoke("Pre Sword Attack");
            Counter++;
            _elapsed = 0;
            _initialPosition = boss.Position;
            _targetPosition = (Vector2) boss.player.transform.position - boss.CenterOffset;
            boss.StartCoroutine(Anticipate());
        }

        public override void LogicUpdate()
        {
            float t = Mathf.InverseLerp(0, boss.swordAnticipationTime, _elapsed);
            boss.transform.position = Vector2.Lerp(_initialPosition, _targetPosition, t);
            _elapsed += Time.deltaTime;
        }

        IEnumerator Anticipate()
        {
            yield return new WaitForSeconds(boss.swordAnticipationTime);
            bossMachine.ChangeState(boss.swordAttack);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}