using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    PlayerInput _input;
    [HideInInspector] public Rigidbody2D body;
    public BoxCollider2D groundSensor;

    public StateMachine stateMachine;
    public Moving moving;
    public Jumping jumping;
    public Idling idling;

    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        body = GetComponent<Rigidbody2D>();
    }

    #region Actions


    #endregion

    void Start()
    {
        stateMachine = new StateMachine();

        moving = new Moving(this, stateMachine);
        jumping = new Jumping(this, stateMachine);
        idling = new Idling(this, stateMachine);

        stateMachine.Initialize(idling);
    }

    void Update()
    {
        stateMachine.CurrentState.HandleInput(_input);

        stateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
