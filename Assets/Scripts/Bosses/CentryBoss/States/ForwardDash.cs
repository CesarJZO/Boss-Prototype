using System.Collections;
using UnityEngine;
namespace EKP.Bosses.Centry
{
    public class ForwardDash : BossState<CentryBoss>
    {
        Vector2 _target;
        public ForwardDash(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) { }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Forward dash: Enter");
            boss.OnChangeState?.Invoke("Forward dash");

            if (Vector2.Distance(boss.transform.position, boss.leftPosition.position) < 0.1f)
                _target = boss.rightPosition.position;
            else if (Vector2.Distance(boss.transform.position, boss.rightPosition.position) < 0.1f)
                _target = boss.leftPosition.position;
            
            boss.StartCoroutine(Dash());
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Vector2 velocity = Vector2.zero;
            boss.transform.position = Vector2.SmoothDamp(boss.transform.position, _target, ref velocity, 0.1f);
        }

        IEnumerator Dash()
        {
            yield return new WaitForSeconds(2);
            bossMachine.ChangeState(boss.idling);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}