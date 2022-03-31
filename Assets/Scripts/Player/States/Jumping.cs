using UnityEngine;
using UnityEngine.InputSystem;
public class Jumping : State
{
    bool land;
    Vector2 velocity;
    public Jumping(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    void Jump()
    {
        player.body.AddForce(player.jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Jumping");
        Jump();
        land = false;
    }
    public override void HandleInput(PlayerInput input)
    {
        base.HandleInput(input);
        velocity.x = input.actions[ActionNames.Move].ReadValue<Vector2>().x * player.speed / 2;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (land)
        {
            Debug.Log("Landed");
            if (player.body.velocity.x != 0)
                stateMachine.ChangeState(player.moving);
            else
                stateMachine.ChangeState(player.idling);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        velocity.y = player.body.velocity.y;
        land = player.groundSensor;
    }
}
