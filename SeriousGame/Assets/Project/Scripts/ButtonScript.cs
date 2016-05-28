using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public int buttonNumber;
    public Text numberText;
    public Text priceText;
    public Image backgroundImage;

    private GameObject allySpawner;   
    private float energyPrice;
    private int numberUnit;
    
	// Use this for initialization
	void Start ()
    {
        allySpawner = GameObject.Find("SpawnerAlly");
        if(!allySpawner)
        {
            Debug.Log("Spawner not found");
        }
        energyPrice = allySpawner.GetComponent<SpawnerAlly>().getPrice(buttonNumber);
        priceText.text = "Energie : " + energyPrice;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(allySpawner.GetComponent<SpawnerAlly>().getEnergy() > energyPrice)
        {
            Color green = new Color(64 / 255.0f, 237 / 255.0f, 64 / 255.0f);
            green.a = 0.35f;
            backgroundImage.color = green;
        }
        else
        {
            Color red = new Color(255 / 255.0f, 13 / 255.0f, 13 / 255.0f);
            red.a = 0.35f;
            backgroundImage.color = red;
        }

        numberUnit = allySpawner.GetComponent<SpawnerAlly>().getNumber(buttonNumber);
        if(numberUnit > 0)
        {
            numberText.text = "" + numberUnit;
        }
        else
        {
            numberText.text = "";
        }

    }
}
