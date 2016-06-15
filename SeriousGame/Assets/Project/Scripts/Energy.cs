using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Energy : MonoBehaviour
{
	private PlayerControl playerControl;	// Reference to the player control script.
	private int previousScore = 0;			// The score in the previous frame.

    private GameObject energyScript;
    private int energy;

    void Start()
    {
        energyScript = GameObject.Find("SpawnerAlly");
        if(energyScript)
        {
            energy = (int) energyScript.GetComponent<SpawnerAlly>().getEnergy();
        }
        else
        {
            Debug.Log("Erreur script SpawnerAlly non trouvé");
        }
    }

	void Update ()
	{
        energy = (int) energyScript.GetComponent<SpawnerAlly>().getEnergy();
        // Set the score text.
        GetComponent<Text>().text = "Energie: " + energy;
	}

}
