using UnityEngine;
using UnityEngine.Events;

using UnityEngine.InputSystem;
namespace EKP.Bosses.Centry
{

    // TODO: Draw line for gizmos, move using buildings+offset positions, add groundSensor
    public class CentryBoss : MonoBehaviour
    {
        [Header("Debug")]
        public bool automaticBehaviour;

        [Header("Waiting time range")]
        public float minTime;
        public float maxTime;

        [Header("Movement")]
        public float attacksDuration = 2f;
        public float smoothTime;
        public Vector2 jumpDirection;
        public float jumpStrength;
        public Transform leftBuilding;
        public Transform rightBuilding;
        public UnityEvent<string> OnChangeState;
        [HideInInspector] public Rigidbody2D body;
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
            _bossMachine.Initialize(idling);
        }

        void Update()
        {
            _bossMachine.CurrentState.LogicUpdate();
            // * Debug
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
        }
        #endregion
    }
}
