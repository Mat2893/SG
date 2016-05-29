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
        if (coll.gameObject.tag == "EnemyBase")
        {
            if (coll.gameObject.GetComponent<Base>())
            {
                coll.gameObject.GetComponent<Base>().TakeDamage(0.1f);
            }
        }
    }
}
