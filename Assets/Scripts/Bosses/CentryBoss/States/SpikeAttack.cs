using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class SpikeAttack : BossState<CentryBoss>
    {
        private RaycastHit2D _ground;
        private Vector2 _target;
        private float _totalDistance;

        public SpikeAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Spike attack: Enter");
            boss.OnChangeState?.Invoke("Spike Attack");
            boss.StartCoroutine(Attack());
            _ground = Physics2D.Raycast(boss.centryFlock.transform.position, Vector2.down);
            _target = _ground.point;
            boss.centryFlock.GetComponent<Rigidbody2D>().simulated = true;
            _totalDistance = _ground.distance;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        IEnumerator Attack()
        {
            float elapsed = 0;
            while (elapsed <= boss.attacksDuration)
            {
                elapsed += Time.deltaTime;
                float x = Mathf.Sin(Mathf.InverseLerp(0, boss.attacksDuration, elapsed)
                                    * 90 * Mathf.Deg2Rad);
                boss.centryFlock.transform.position = Vector2.Lerp(boss.centryFlock.transform.position, _target, x);
                yield return null;
            }
            bossMachine.ChangeState(boss.idling);
        }

        float easeInBack(float x)
        {
            const float c1 = 1f;
            const float c3 = c1 + 1;
            return c3 * x * x * x - c1 * x * x;
        }

        public override void Exit()
        {
            base.Exit();
            boss.centryFlock.SetActive(false);
        }
    }
}