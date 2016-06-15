using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{ 
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public float HP = 2;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
    public GameObject healthBar;
    public GameObject explosion;
    public string category;             // Unit category, used for the strengths/weaknesses system, small <- medium <- big

    private float maxHP;
    private float lifePercentage;


    private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private Score score;				// Reference to the Score script.

	
	void Start()
	{
        maxHP = HP;
        Flip();
        // Setting up the references.
        //ren = transform.Find("body").GetComponent<SpriteRenderer>();
		score = GameObject.Find("Score").GetComponent<Score>();
	}

	void Update ()
	{
		// Set the enemy's velocity to moveSpeed in the x direction.
		GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);	
	}

    public void TakeDamage(float damage, string category)
    {
        if (!this.category.Equals(category))
        {
            if (this.category.Equals("bacteria") && category.Equals("whiteCell"))
            {
                HP -= damage * 2f;
            }
            else
            {
                HP -= damage * 0.2f;
            }

            if (this.category.Equals("virus") && category.Equals("anticorps"))
            {
                HP -= damage * 2f;
            }
            else
            {
                HP -= damage * 0.2f;
            }
        }
        else
        {
            HP -= damage;
        }

        if((int) HP - (int) (HP + damage) != 0)
        {
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            Instantiate(explosion, transform.position, randomRotation);
        }       

        lifePercentage = HP / maxHP;

        healthBar.transform.localScale = new Vector3(lifePercentage, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        if(HP < 0)
        {
            HP = 0;
        }

        // If the Ally has one hit point left and has a damagedAlly sprite...
        if (HP == 1 && damagedEnemy != null)
            // ... set the sprite renderer's sprite to be the damagedAlly sprite.
            ren.sprite = damagedEnemy;

        // If the Ally has zero or fewer hit points and isn't dead yet...
        if (HP <= 0 && !dead)
        {
            // ... call the death function.    
            dead = true;
            Death();
            StartCoroutine(deleteComponent(2));
        }
    }

    void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();
        GameObject.Find("SpawnerAlly").GetComponent<SpawnerAlly>().gainEnergy(20);        

        // Increase the score by 100 points
        if (score)
        {
            score.score += 100;
        }
		

		// Allow the enemy to rotate and spin it by adding a torque.
		GetComponent<Rigidbody2D>().fixedAngle = false;
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));

        // Find all of the colliders on the gameobject and set them all to be triggers.
        CircleCollider2D[] cols = GetComponents<CircleCollider2D>();
		foreach(CircleCollider2D c in cols)
		{
			c.isTrigger = true;
		}

        BoxCollider2D[] cols2 = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D c in cols2)
        {
            c.isTrigger = true;
        }

        // Play a random audioclip from the deathClips array.
        /*int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);*/

        // Create a vector that is just above the enemy.
        Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;

		// Instantiate the 100 points prefab at this point.
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
	}

    public void Flip()
    {
        // Multiply the x component of localScale by -1.
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }

    IEnumerator deleteComponent(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
