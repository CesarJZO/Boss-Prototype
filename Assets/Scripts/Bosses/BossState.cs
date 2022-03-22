namespace EKP.Bosses
{
    public abstract class BossState<Boss>
    {
        protected Boss _boss;
        protected BossMachine<Boss> _bossMachine;
        protected BossState(Boss boss, BossMachine<Boss> bossMachine)
        {
            _boss = boss;
            _bossMachine = bossMachine;
        }

        public virtual void Enter() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void Exit() { }
    }
}
