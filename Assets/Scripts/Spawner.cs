using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] sourcePrefabs;
    [SerializeField] private float cooldownSeconds;
    [SerializeField] private float maxSpawnRadius;
    [SerializeField] private float minSpawnRadius;
    [SerializeField] private float spawnHeight;
    [SerializeField] private bool repeat;

    private bool isActive = false;

    private void Start()
    {
        StartSpawner();
    }

    public void StartSpawner()
    {
        repeat = true;

        if (!isActive)
        {
            StartCoroutine(SpawnerCoroutine());
        }
    }

    public void StopSpawner()
    {
        repeat = false;
    }

    private Vector3 RandomPositionInRing()
    {
        float theta = Random.Range(0, 2 * Mathf.PI);
        float hypot = Random.Range(minSpawnRadius, maxSpawnRadius);

        float x = hypot * Mathf.Sin(theta), z = hypot * Mathf.Cos(theta);

        return new Vector3(x, spawnHeight, z);
    }

    private IEnumerator SpawnerCoroutine()
    {
        isActive = true;

        int sourcePrefabIndex = 0;
        do
        {
            yield return new WaitForSeconds(cooldownSeconds);

            GameObject sourcePrefab = sourcePrefabs[sourcePrefabIndex];
            _ = Instantiate(sourcePrefab, transform.position + RandomPositionInRing(), transform.rotation);

            sourcePrefabIndex = (sourcePrefabIndex + 1) % sourcePrefabs.Length;
        } while (repeat);

        isActive = false;
    }
}
