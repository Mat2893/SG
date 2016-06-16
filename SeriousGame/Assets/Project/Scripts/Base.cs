using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Base : MonoBehaviour
{
	public float HP = 100;					// How many times the Ally can be hit before it dies.
    public GameObject healthBar;

    private float maxHP;
    private string sceneName;
    private float lifePercentage;
    private bool dead = false;
    private Score score;
    private MenusScript menuController;

    void Start()
	{
        PlayerPrefs.SetInt("GameOver", 0); // 0 = false, 1 = true, used to know if the game must go back to level selection or not
        maxHP = HP;
        sceneName = SceneManager.GetActiveScene().name;
        menuController = GameObject.Find("MenuController").GetComponent<MenusScript>();
        score = GameObject.Find("Score").GetComponent<Score>();

        if (gameObject.tag.Equals("AllyBase"))
        {
            menuController.LoadInfo(sceneName + "_Intro");
        }
	}

	void Update ()
	{

	}
	
	public void TakeDamage(float damage)
	{
        HP -= damage;
        lifePercentage = HP / maxHP;

        healthBar.transform.localScale = new Vector3(lifePercentage, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        if (HP < 0)
        {
            HP = 0;
        }

        // If the Ally has zero or fewer hit points and isn't dead yet...
        if (HP <= 0 && !dead)
            // ... call the death function.
            Death();
    }
	
	void Death()
	{
		// Set dead to true.
		dead = true;
        Time.timeScale = 0;

        if (gameObject.tag.Equals("EnnemyBase"))
        {
            int scoreToSave = score.score;

            PlayerPrefs.SetInt(sceneName, scoreToSave);

            if (menuController)
            {
                menuController.LoadInfo(sceneName + "_Victoire");
            }
        }

        if (gameObject.tag.Equals("AllyBase"))
        {
            if (menuController)
            {
                menuController.LoadInfo(sceneName + "_Defaite");
            }
        }

        PlayerPrefs.SetInt("GameOver", 1);
	}
}
