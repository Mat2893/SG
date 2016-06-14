using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnerSave : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.


	void Start ()
	{
        // Start calling the Spawn function repeatedly after a delay .
        StartCoroutine(delay(spawnDelay));
	}       

    IEnumerator delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(spawn(spawnTime));
    }

    IEnumerator spawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        int enemyIndex = Random.Range(0, enemies.Length);
        Instantiate(enemies[enemyIndex], transform.position, transform.rotation);

        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
        Debug.Log(spawnTime);
        StartCoroutine(spawn(spawnTime));
    }   
}
