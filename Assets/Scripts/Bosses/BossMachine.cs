namespace EKP.Bosses
{
    public class BossMachine<Boss>
    {
        public BossState<Boss> CurrentState { get; private set; }

        public void Initialize(BossState<Boss> startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }
        public void ChangeState(BossState<Boss> newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            newState.Enter();
        }
    }
}
