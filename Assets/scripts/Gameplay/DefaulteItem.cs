using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaulteItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;

        //kill the if if fall from platform
        if (coll.CompareTag("DieZone"))
        {
            Destroy(gameObject);
        }
    }
}
