namespace EKP.Bosses
{
    public abstract class BossState<Boss>
    {
        protected Boss boss;
        protected BossMachine<Boss> bossMachine;
        protected BossState(Boss boss, BossMachine<Boss> bossMachine)
        {
            this.boss = boss;
            this.bossMachine = bossMachine;
        }

        public virtual void Enter() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void Exit() { }
    }
}
