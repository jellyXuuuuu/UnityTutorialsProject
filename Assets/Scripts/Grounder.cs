using System;
using UnityEngine;

public class Grounder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        OnTouchedGround?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnLeftGround?.Invoke(other);        
    }

    public event Action<Collider> OnTouchedGround;
    public event Action<Collider> OnLeftGround;
}
