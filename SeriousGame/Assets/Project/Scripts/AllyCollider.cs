using UnityEngine;
using System.Collections;

public class AllyCollider : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            if (coll.gameObject.GetComponent<Enemy>())
            {
                coll.gameObject.GetComponent<Enemy>().TakeDamage(0.1f);
            }
        }     
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            if (coll.gameObject.GetComponent<Enemy>())
            {
                coll.gameObject.GetComponent<Enemy>().TakeDamage(0.1f);
            }
        }
    }
}
