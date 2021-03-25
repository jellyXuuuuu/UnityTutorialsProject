using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class EnemyBehaviourBase : MonoBehaviour
{
    private SphereCollider enemyTrackingRangeCollider;

    [SerializeField] private float baseTrackingRange;
    [SerializeField] private float activeTrackingRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;

    protected GameObject TrackedGameObject { get; set; }
    protected EnemyMovement MovementController { get; private set; }
    public float AttackRange { get { return attackRange; } }
    public float AttackCooldown { get { return attackCooldown; } }

    protected virtual void Awake()
    {
        enemyTrackingRangeCollider = GetComponent<SphereCollider>();

        MovementController = GetComponentInParent<EnemyMovement>();
    }

    protected virtual void Start()
    {
        enemyTrackingRangeCollider.radius = baseTrackingRange;
        MovementController.StoppingDistance = attackRange;
    }

    protected virtual void OnEnable()
    {
        MovementController.ReachedTarget += OnReachedTarget;
    }

    protected virtual void OnDisable()
    {
        MovementController.ReachedTarget -= OnReachedTarget;
    }

    protected abstract void OnReachedTarget(Vector3 position, Action resumeTrackingCallback);

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && TrackedGameObject == null)
        {
            enemyTrackingRangeCollider.radius = activeTrackingRange;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject == TrackedGameObject)
        {
            enemyTrackingRangeCollider.radius = baseTrackingRange;
        }
    }
}
