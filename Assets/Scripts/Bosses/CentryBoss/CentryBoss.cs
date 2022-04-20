using UnityEngine;
using UnityEngine.Events;

using UnityEngine.InputSystem;
namespace EKP.Bosses.Centry
{
    public class CentryBoss : MonoBehaviour
    {
        [Header("Idle")]
        public bool automaticBehaviour;
        public float minTime;
        public float maxTime;

        [Header("Movement")]
        public float smoothTime;
        public Transform leftBuilding;
        public Transform rightBuilding;

        public Vector2 Position => transform.position;
        public Vector2 CenterOffset => _collider.offset;

        [SerializeField] float _alignmentMargin;
        public bool NearOfTargetBuilding => Vector2.Distance(
            new Vector2(transform.position.x, 0), new Vector2(buildingTarget.x, 0)
        ) <= _alignmentMargin;
        [HideInInspector] public Vector2 buildingTarget;


        [Header("Forward Dash")]
        public float dashDuration;


        [Header("Back Jump")]
        [SerializeField] LayerMask _groundLayer;
        [Range(0f, 90f)] public float jumpAngle;
        public float jumpSpeed;
        public float rotationSpeed;
        public float fallingDrag;
        [SerializeField] float _groundDistance;
        public bool Grounded => Physics2D.Raycast(
            Position + CenterOffset, Vector2.down, _groundDistance, _groundLayer
        );
        [HideInInspector] public Vector2 jumpDirection;


        [Header("Sword attack")]
        public float swordAnticipationTime;
        public float swordDuration;
        public float swordDrag;


        [Header("Spike attack")]
        public float spikeAnticipationTime;
        public float fallDuration;
        public GameObject centryFlock;
        public float centriesMaxSpeed;
        [Range(-12f, 0f)] public float centriesMinPosition;
        [Range(0, 12f)] public float centriesMaxPosition;


        [Header("Charge Attack")]
        public float chargeAnticipationTime;
        public float chargeSpeed;


        [Header("Player")]
        public GameObject player;

        public UnityEvent<string> OnChangeState;

        [HideInInspector] public Rigidbody2D body;
        private CapsuleCollider2D _collider;
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
            _collider = GetComponent<CapsuleCollider2D>();
        }

        void Start()
        {
            buildingTarget = leftBuilding.position;
            _bossMachine.Initialize(idling);
            jumpDirection = Quaternion.Euler(0, 0, jumpAngle) * Vector2.right;
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
            _collider = GetComponent<CapsuleCollider2D>();
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + (Vector3)_collider.offset, transform.position + (Vector3)_collider.offset + Quaternion.Euler(0, 0, jumpAngle) * Vector3.right);

            Gizmos.color = Color.blue;
            float distance = 100;
            Gizmos.DrawLine(leftBuilding.position + Vector3.down * distance, leftBuilding.position + Vector3.up * distance);
            Gizmos.DrawLine(rightBuilding.position + Vector3.down * distance, rightBuilding.position + Vector3.up * distance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine((Vector2)transform.position + _collider.offset, (Vector2)transform.position + Vector2.down * _groundDistance + _collider.offset);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(centryFlock.transform.position + Vector3.right * centriesMinPosition, centryFlock.transform.position + Vector3.right * centriesMaxPosition);
        }
        #endregion

        public void SwitchTarget()
        {
            if (buildingTarget == (Vector2) leftBuilding.position)
                buildingTarget = rightBuilding.position;
            else
                buildingTarget = leftBuilding.position;
        }
    }
}
