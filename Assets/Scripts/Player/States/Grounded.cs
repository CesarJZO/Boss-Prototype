using UnityEngine;
using UnityEngine.InputSystem;
public class Grounded : State
{
    protected float speed;
    protected Vector2 velocity;

    bool jump;

    public Grounded(Player player, StateMachine stateMachine) : base(player, stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        speed = player.speed;
        velocity = new Vector2();
        jump = false;
    }
    public override void HandleInput(PlayerInput input)
    {
        base.HandleInput(input);
        velocity.x = input.actions[ActionNames.Move].ReadValue<Vector2>().x * player.speed;
        velocity.y = player.body.velocity.y;

        jump = input.actions[ActionNames.Jump].triggered;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (jump)
        {
            stateMachine.ChangeState(player.jumping);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.body.velocity = velocity;
    }
    public override void Exit()
    {
        base.Exit();
    }
}
