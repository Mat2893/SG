using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Vaccin : MonoBehaviour {

    public GameObject specialUnit;

	// Use this for initialization
	void Start () {
        StartCoroutine(VaccinWait(10));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator VaccinWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        specialUnit.SetActive(true);
    }
}
