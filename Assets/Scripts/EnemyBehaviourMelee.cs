using System;
using System.Collections;
using UnityEngine;

public class EnemyBehaviourMelee : EnemyBehaviourBase
{
    private IEnumerator AttackCoroutine(Action resumeTrackingCallback)
    {
        yield return new WaitForSeconds(AttackCooldown);

        resumeTrackingCallback();
    }

    protected override void OnReachedTarget(Vector3 position, Action resumeTrackingCallback)
    {
        StartCoroutine(AttackCoroutine(resumeTrackingCallback));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && TrackedGameObject == null)
        {
            TrackedGameObject = other.gameObject;
            MovementController.Target = other.transform;
        }

        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject == TrackedGameObject)
        {
            TrackedGameObject = null;
            MovementController.Target = null;
        }

        base.OnTriggerExit(other);
    }
}
