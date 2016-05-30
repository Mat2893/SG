using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

    /*private float ratio;

    void Start()
    {
        Debug.Log(gameObject.name);
        if (gameObject.name.Equals("Bacteria(Clone)"))
        {
            ratio = 0.5f;
        }
        if (gameObject.name.Equals("BigBacteria(Clone)"))
        {
            ratio = 5.0f;
        }
    }*/
    
    
    
    
    
    
    
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ally")
        {
            if (coll.gameObject.GetComponent<Ally>())
            {
                coll.gameObject.GetComponent<Ally>().TakeDamage(0.1f, gameObject.GetComponent<Enemy>().category);
            }
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ally")
        {
            if (coll.gameObject.GetComponent<Ally>())
            {
                coll.gameObject.GetComponent<Ally>().TakeDamage(0.1f, gameObject.GetComponent<Enemy>().category);
            }
        }
        if (coll.gameObject.tag == "AllyBase")
        {
            if (coll.gameObject.GetComponent<Base>())
            {
                coll.gameObject.GetComponent<Base>().TakeDamage(0.1f);
            }
        }
    }
}
