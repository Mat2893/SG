﻿using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ally")
        {
            if (coll.gameObject.GetComponent<Ally>())
            {
                coll.gameObject.GetComponent<Ally>().TakeDamage(0.1f);
            }
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ally")
        {
            if (coll.gameObject.GetComponent<Ally>())
            {
                coll.gameObject.GetComponent<Ally>().TakeDamage(0.1f);
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
