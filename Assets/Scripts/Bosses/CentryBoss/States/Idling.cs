namespace EKP.Bosses.Centry
{
    public class Idling : BossState<CentryBoss>
    {
        public Idling(CentryBoss boss, BossMachine<CentryBoss> bossMachine) : base(boss, bossMachine) {}
        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}