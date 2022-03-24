using UnityEngine;
using UnityEngine.Events;
namespace EKP.Bosses.Centry
{
    public class CentryBoss : MonoBehaviour
    {
        [Header("Waiting time range")]
        public float minTime;
        public float maxTime;
        [Header("Behaviour")]
        public Transform leftPosition;
        public Transform rightPosition;
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
        }

        void Start()
        {
            _bossMachine.Initialize(idling);
        }

        void Update()
        {
            _bossMachine.CurrentState.LogicUpdate();
        }

        void FixedUpdate()
        {
            _bossMachine.CurrentState.PhysicsUpdate();
        }
        #endregion
    }
}
