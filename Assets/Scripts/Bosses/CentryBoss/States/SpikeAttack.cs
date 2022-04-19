using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class SpikeAttack : BossState<CentryBoss>
    {
        private RaycastHit2D _ground;
        private Vector2 _targetPoint;
        private Vector2 _initialPoint;

        public SpikeAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Spike attack: Enter");
            boss.OnChangeState?.Invoke("Spike Attack");
            _ground = Physics2D.Raycast(boss.centryFlock.transform.position, Vector2.down);
            _initialPoint = boss.centryFlock.transform.position;
            _targetPoint = _ground.point;
            boss.StartCoroutine(Attack());
            boss.centryFlock.GetComponent<Rigidbody2D>().simulated = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        IEnumerator Attack()
        {
            float elapsed = 0;
            while (elapsed <= boss.fallDuration)
            {
                elapsed += Time.deltaTime;
                float x = Mathf.InverseLerp(0, boss.fallDuration, elapsed);
                float t = EaseInBack(x);
                boss.centryFlock.transform.position = Vector2.LerpUnclamped(_initialPoint, _targetPoint, t);
                yield return null;
            }
            bossMachine.ChangeState(boss.idling);
        }

        float EaseInBack(float x)
        {
            return 2.8f * Mathf.Pow(x, 3) - 1.8f * Mathf.Pow(x, 2);
        }

        public override void Exit()
        {
            base.Exit();
            boss.centryFlock.SetActive(false);
        }
    }
}