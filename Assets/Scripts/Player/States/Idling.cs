using UnityEngine;
using UnityEngine.InputSystem;
public class Idling : Grounded
{
    public Idling(Player player, StateMachine stateMachine) : base(player, stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Idling");
        speed = 0f;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.body.velocity.x != 0)
            stateMachine.ChangeState(player.moving);
    }
}
