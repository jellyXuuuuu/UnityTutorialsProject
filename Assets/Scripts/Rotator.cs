using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;

    private void FixedUpdate()
    {
        transform.Rotate(rotation * Time.fixedDeltaTime);
    }
}
