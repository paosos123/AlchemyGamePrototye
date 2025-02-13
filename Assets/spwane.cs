using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwane : MonoBehaviour
{
    public List<GameObject> prefabs; 
    public List<GameObject> prefabs1;// List of prefabs to spawn
    public float spawnInterval = 1.75f;
    public float spawnInterval1 =3f;// Time interval between spawns (using Time.timeScale)
    public Transform spawnPoint; // The point where objects will be spawned (can be the spawner's transform)
    private float timer1;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime * Time.timeScale; // Increment timer based on scaled time
        timer1 += Time.deltaTime * Time.timeScale; 
        if (timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f; // Reset the timer
        }
        if (timer1 >= spawnInterval1)
        {
            SpawnPrefab1();
            timer1 = 0f; // Reset the timer
        }
    }

    void SpawnPrefab()
    {
        if (prefabs.Count == 0)
        {
            Debug.LogWarning("No prefabs assigned to the spawner.");
            return;
        }

        // 1. Randomly select a prefab from the list:
        int randomIndex = Random.Range(0, prefabs.Count);
        GameObject prefabToSpawn = prefabs[randomIndex];

        // 2. Instantiate the prefab at the spawn point:
        if (spawnPoint == null)
        {
            Instantiate(prefabToSpawn, transform.position,
                transform.rotation); // Use spawner's transform if no spawnPoint is set.
        }
        else
        {
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
    void SpawnPrefab1()
    {
        if (prefabs1.Count == 0)
        {
            Debug.LogWarning("No prefabs assigned to the spawner.");
            return;
        }

        // 1. Randomly select a prefab from the list:
        int randomIndex = Random.Range(0, prefabs1.Count);
        GameObject prefabToSpawn1 = prefabs1[randomIndex];

        // 2. Instantiate the prefab at the spawn point:
        if (spawnPoint == null)
        {
            Instantiate(prefabToSpawn1, transform.position,
                transform.rotation); // Use spawner's transform if no spawnPoint is set.
        }
        else
        {
            Instantiate(prefabToSpawn1, spawnPoint.position, spawnPoint.rotation);
        }
    }
}