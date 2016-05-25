using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerAlly : MonoBehaviour
{
	public GameObject[] allies;		// Array of allies prefabs.
    public float[] alliesPrices;    //Price to create allies
    public int[] timeCreatingAllies;//Times to create allies

    private List<GameObject> creationList;
    private bool creating;
    private bool askingCreation;
    private int code;
    private float energy;
    private int[] keyPadCode = { 257,258,259,260,261 };

	void Start ()
	{
        if(allies.Length != alliesPrices.Length && allies.Length != timeCreatingAllies.Length)
        {
            Debug.Log("Allies unit can't be created correctly");
        }

        creationList = new List<GameObject>();
        askingCreation = false;
        creating = false;
        energy = 500;
	}

    void Update()
    {
        energy += 0.2f;

        if(Input.anyKeyDown)    //Allows to create 5 different type of units generically
        {
            for(int i = 0; i < 5; i++)
            {
                if (allies.Length >= (i + 1) && Input.GetKeyDown((UnityEngine.KeyCode) keyPadCode[i]))  //Appui touche Keypad 1
                {
                    askingCreation = true;
                    code = keyPadCode[i] - 257;  //code équivaut à l'index de l'ally à créer
                }
            }
        }

        if (askingCreation)
        {
            askingCreation = false;
            if (allies.Length >= 1 && energy >= alliesPrices[code]) //Demande la création d'unité de façon générique
            {
                energy -= alliesPrices[code];
                creationList.Add(allies[code]);
                if (!creating)
                {
                    StartCoroutine(createFunction(timeCreatingAllies[code]));
                }
            }
        }            
    }

    IEnumerator createFunction(float waitTime)
    {
        creating = true;
        yield return new WaitForSeconds(waitTime);

        Instantiate(creationList[0], transform.position, transform.rotation);
        creationList.Remove(creationList[0]);

        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
        
        if(creationList.Count > 0)
        {
            for(int i = 0; i < allies.Length; i++)
            {
                if (creationList[0].Equals(allies[i]))
                {
                    StartCoroutine(createFunction(timeCreatingAllies[i]));
                }
            }            
        }
        else
        {
            creating = false;
        }       
    }

public float getEnergy()
    {
        return energy;
    }
}
