using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnerAlly : MonoBehaviour
{
	public GameObject[] allies;		// Array of allies prefabs.
    public float[] alliesPrices;    //Price to create allies
    public int[] timeCreatingAllies;//Times to create allies
    public Button power1;

    private List<GameObject> creationList;
    private bool creating;
    private bool askingCreation;
    private int code;
    private float energy;
    private int[] keyPadCode = { 257,258,259,260,261 };
    private GameObject[] existingAllies;

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
    }

    public void createUnit(int unitNumber)
    {
        if (allies.Length >= (unitNumber + 1) && energy >= alliesPrices[unitNumber])
        {
            energy -= alliesPrices[unitNumber];
            creationList.Add(allies[unitNumber]);
            if (!creating)
            {
                StartCoroutine(createFunction(timeCreatingAllies[unitNumber]));
            }
        }
    }

    public int getNumber(int unit)
    {
        int count = 0;

        for(int i = 0; i < creationList.Count; i++)
        {
            if(creationList[i].Equals(allies[unit]))
            {
                count++;
            }
        }

        return count;
    }

    public float getPrice(int unit)
    {
        return alliesPrices[unit];
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

    public void ActivatePower1() {
        existingAllies = GameObject.FindGameObjectsWithTag("Ally");

        if (existingAllies.Length != 0)
        {
            foreach (GameObject ally in existingAllies)
            {
                ally.GetComponent<Ally>().moveSpeed = 5.0f;
            }
        }

        StartCoroutine(EndPower1(5));
        StartCoroutine(CooldownPower1(10));
    }

    IEnumerator EndPower1(float waitTime)
    {
        power1.interactable = false;

        yield return new WaitForSeconds(waitTime);

        existingAllies = GameObject.FindGameObjectsWithTag("Ally");

        if (existingAllies.Length != 0)
        {
            foreach (GameObject ally in existingAllies)
            {
                ally.GetComponent<Ally>().moveSpeed = 2.0f;
            }
        }
    }

    IEnumerator CooldownPower1(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        power1.interactable = true;
    }

    public float getEnergy()
    {
        return energy;
    }

    public void gainEnergy(float bonus)
    {
        energy += bonus;
    }
}


