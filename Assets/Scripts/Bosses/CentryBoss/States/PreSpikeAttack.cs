using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class PreSpikeAttack : BossState<CentryBoss>
    {

        private float _velocity;
        private Vector3 _mover;

        public PreSpikeAttack(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine)
        {
            _mover = new Vector3(0, boss.centryFlock.transform.position.y);
        }

        public override void Enter()
        {
            base.Enter();
            // Debug.Log("Spike attack: Anticipation");
            boss.OnChangeState?.Invoke("Pre Spike Attack");
            boss.centryFlock.SetActive(true);
            boss.centryFlock.GetComponent<Rigidbody2D>().simulated = false;
            boss.StartCoroutine(Anticipate());
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            _mover.x = Mathf.Clamp(Mathf.SmoothDamp(
                boss.centryFlock.transform.position.x, boss.player.transform.position.x,
                ref _velocity, boss.smoothTime, boss.centriesMaxSpeed, Time.deltaTime
            ), boss.centriesMinPosition, boss.centriesMaxPosition);

            boss.centryFlock.transform.position = _mover;
        }

        IEnumerator Anticipate()
        {
            yield return new WaitForSeconds(boss.attacksDuration);
            bossMachine.ChangeState(boss.spikeAttack);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
