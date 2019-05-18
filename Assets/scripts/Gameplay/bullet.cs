using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //bullet impulseforce
    public void ApplyForce(Vector2 direction)
    {
        const float Magnitude = 6f;
        GetComponent<Rigidbody2D>().AddForce(Magnitude * direction, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;

        //Destroy bullet
        if (coll.CompareTag("CharacterBullet"))
        {
            Destroy(coll);
            Destroy(gameObject);
        }

        if (coll.CompareTag("EnemyBullet"))
        {
            Destroy(coll);
            Destroy(gameObject);
        }

        if (coll.CompareTag("EdgeOfWorld"))
        {
            Destroy(gameObject);
        }
    }
}
