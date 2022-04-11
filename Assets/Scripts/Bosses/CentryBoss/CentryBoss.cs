using UnityEngine;
using UnityEngine.Events;

using UnityEngine.InputSystem;
namespace EKP.Bosses.Centry
{
    public class CentryBoss : MonoBehaviour
    {
        [Header("Debug")]
        public bool automaticBehaviour;
        public float _groundDistance;
        [SerializeField] float _margin = 0.5f;

        [Header("Waiting time range")]
        public float minTime;
        public float maxTime;
        public float attacksDuration = 2f;

        [Header("Movement")]
        public float smoothTime;
        public Transform leftBuilding;
        public Transform rightBuilding;

        [Header("Jump")]
        public Vector2 jumpDirection;
        public float jumpStrength;
        public UnityEvent<string> OnChangeState;

        public RaycastHit2D Grounded => Physics2D.Raycast(transform.position, Vector2.down * _groundDistance);
        public bool CloseToTarget => Vector2.Distance(body.position, target) < _margin;

        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public Vector2 target;
        BossMachine<CentryBoss> _bossMachine;

        #region States
        public Idling idling;
        public ForwardDash forwardDash;
        public BackJump backJump;
        public PreSwordAttack preSwordAttack;
        public SwordAttack swordAttack;
        public PreChargeAttack preChargeAttack;
        public ChargeAttack chargeAttack;
        public PreSpikeAttack preSpikeAttack;
        public SpikeAttack spikeAttack;
        #endregion

        #region MonoBehaviour methods 
        void Awake()
        {
            _bossMachine = new BossMachine<CentryBoss>();
            idling = new Idling(this, _bossMachine);
            forwardDash = new ForwardDash(this, _bossMachine);
            backJump = new BackJump(this, _bossMachine);
            preSwordAttack = new PreSwordAttack(this, _bossMachine);
            swordAttack = new SwordAttack(this, _bossMachine);
            preChargeAttack = new PreChargeAttack(this, _bossMachine);
            chargeAttack = new ChargeAttack(this, _bossMachine);
            preSpikeAttack = new PreSpikeAttack(this, _bossMachine);
            spikeAttack = new SpikeAttack(this, _bossMachine);

            body = GetComponent<Rigidbody2D>();
            jumpDirection.Normalize();
        }

        void Start()
        {
            target = leftBuilding.position;
            _bossMachine.Initialize(idling);
        }

        void Update()
        {
            _bossMachine.CurrentState.LogicUpdate();
            // * Debug: Restart
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                transform.position = rightBuilding.position;
                _bossMachine.ChangeState(idling);
            }
        }

        void FixedUpdate()
        {
            _bossMachine.CurrentState.PhysicsUpdate();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)jumpDirection);
            Vector3 line = new Vector3(0, 15, 0);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(leftBuilding.position - line, leftBuilding.position + line);
            Gizmos.DrawLine(rightBuilding.position - line, rightBuilding.position + line);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y -_groundDistance));
        }
        #endregion
    }
}
