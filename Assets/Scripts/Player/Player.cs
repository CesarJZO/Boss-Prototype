using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    PlayerInput _input;
    [HideInInspector] public Rigidbody2D body;
    public RaycastHit2D groundSensor;

    public StateMachine stateMachine;
    public Moving moving;
    public Jumping jumping;
    public Idling idling;
    [SerializeField] float _groundHeight;
    [SerializeField] LayerMask _groundMask;

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
        groundSensor = Physics2D.Raycast(
            transform.position, Vector2.down,
            _groundHeight, _groundMask);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundHeight);
    }
}
