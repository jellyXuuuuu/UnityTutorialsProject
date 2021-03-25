using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collector : MonoBehaviour
{
    private GameObject statefulItemRoot;

    public int Score { get; private set; }

    private void Start()
    {
        statefulItemRoot = Instantiate(new GameObject(), transform);
        statefulItemRoot.name = "Stateful Item Root";
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = statefulItemRoot.transform;
        other.gameObject.SetActive(false);

        Score += 1;
        Debug.Log($"Current Score: {Score}");
    }
}
