using UnityEngine;
using System.Collections;

public class Ally : MonoBehaviour
{
	public float moveSpeed = 2f;		// The speed the Ally moves at.
	public float HP = 2;					// How many times the Ally can be hit before it dies.
	public Sprite deadAlly;			// A sprite of the Ally when it's dead.
	public Sprite damagedAlly;			// An optional sprite of the Ally when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the Ally dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the Ally dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying


	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the Ally is dead.
	private Score score;				// Reference to the Score script.

	
	void Start()
	{
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();
		score = GameObject.Find("Score").GetComponent<Score>();
	}

	void Update ()
	{
		// Set the Ally's velocity to moveSpeed in the x direction.
		GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);		
	}
	
	public void TakeDamage(float damage)
	{
        HP -= damage;
        if(HP < 0)
        {
            HP = 0;
            StartCoroutine(deleteComponent(2));
        }

        // If the Ally has one hit point left and has a damagedAlly sprite...
        if (HP == 1 && damagedAlly != null)
            // ... set the sprite renderer's sprite to be the damagedAlly sprite.
            ren.sprite = damagedAlly;

        // If the Ally has zero or fewer hit points and isn't dead yet...
        if (HP <= 0 && !dead)
            // ... call the death function.
            Death();
    }
	
	void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		//SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		//foreach(SpriteRenderer s in otherRenderers)
		//{
		//	s.enabled = false;  //Not needed for allies -> no death sprite
		//}

		// Re-enable the main sprite renderer and set it's sprite to the deadAlly sprite.
		//ren.enabled = true;
		//ren.sprite = deadAlly;

		// Set dead to true.
		dead = true;

		// Allow the Ally to rotate and spin it by adding a torque.
		GetComponent<Rigidbody2D>().fixedAngle = false;
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		// Play a random audioclip from the deathClips array.
		/*int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);*/
	}

    IEnumerator deleteComponent(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
