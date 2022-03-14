using UnityEngine;
using UnityEngine.InputSystem;
public class Moving : Grounded
{
    public Moving(Player player, StateMachine stateMachine) : base(player, stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Moving");
    }
    public override void HandleInput(PlayerInput input)
    {
        base.HandleInput(input);        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.body.velocity.x == 0)
            stateMachine.ChangeState(player.idling);
    }
}
