using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelsSelectors : MonoBehaviour {

    public Image star1;
    public Image star2;
    public Image star3;

    private string sceneName;
    private int score;
    private Color color;

	// Use this for initialization
	void Start () {
        sceneName = gameObject.name;

        Debug.Log(sceneName);

        score = PlayerPrefs.GetInt(sceneName, 500);

        color = new Color(1,1,1,1);

        if (score >= 100)
        {
            star1.color = color;
        }
        if (score >= 500)
        {
            star2.color = color;
        }
        if (score >= 1000)
        {
            star2.color = color;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
