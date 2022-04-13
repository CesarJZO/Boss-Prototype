using UnityEngine;
using UnityEngine.Events;

using UnityEngine.InputSystem;
namespace EKP.Bosses.Centry
{
    public class CentryBoss : MonoBehaviour
    {
        [Header("Debug")]
        public bool automaticBehaviour;
        [SerializeField] LayerMask _groundLayer;
        [SerializeField] float _groundDistance;
        [SerializeField] float _margin;

        [Header("Sword attack")]
        public float swordDuration;
        public float swordDrag;

        [Header("Movement")]
        public float smoothTime;
        public Transform leftBuilding;
        public Transform rightBuilding;

        [Header("Jump")]
        [SerializeField, Range(0f, 180f)] float jumpAngle;
        [SerializeField] float _alignmentMargin;
        public float jumpStrength;
        public float fallingDrag;
        
        [Header("Waiting time range")]
        public float minTime;
        public float maxTime;
        public float attacksDuration;

        [Header("Player")]
        public GameObject player;

        [HideInInspector] public Vector2 jumpDirection;
        public UnityEvent<string> OnChangeState;

        public bool Grounded => Physics2D.Raycast(
            transform.position, Vector2.down, _groundDistance, _groundLayer
        );
        public bool CloseToTarget => Vector2.Distance(body.position, target) < _margin;
        public bool IsAlignedWithTarget => Vector2.Distance(
            new Vector2(transform.position.x, 0), new Vector2(target.x, 0)
        ) < _alignmentMargin;

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
            
        }

        void Start()
        {
            target = leftBuilding.position;
            _bossMachine.Initialize(idling);
        }

        void Update()
        {
            _bossMachine.CurrentState.LogicUpdate();
            jumpDirection = Quaternion.Euler(0, 0, jumpAngle) * Vector2.right;
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
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, jumpAngle) * Vector3.right);

            Gizmos.color = Color.blue;
            float distance = 100;
            Gizmos.DrawLine(leftBuilding.position + Vector3.down * distance, leftBuilding.position + Vector3.up * distance);
            Gizmos.DrawLine(rightBuilding.position + Vector3.down * distance, rightBuilding.position + Vector3.up * distance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, Vector3.down * _groundDistance + transform.position);
        }
        #endregion

        public void SwitchTarget()
        {
            if (target == (Vector2) leftBuilding.position)
                target = rightBuilding.position;
            else
                target = leftBuilding.position;
        }
    }
}
