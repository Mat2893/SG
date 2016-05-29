using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour
{
	public float HP = 100;					// How many times the Ally can be hit before it dies.
    public GameObject healthBar;

    private float maxHP;
    private float lifePercentage;
    private bool dead = false;

    void Start()
	{
        maxHP = HP;
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
	}
}
