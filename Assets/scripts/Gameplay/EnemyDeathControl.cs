using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathControl : MonoBehaviour
{
    [SerializeField]
    GameObject healingItemPrefab;

    [SerializeField]
    GameObject invisibleItemPrefab;

    [SerializeField]
    GameObject coinPrefab;

    [SerializeField]
    GameObject heartPrefab;

    // cached for efficiency
    Animator anim;

    /// <summary>
    /// Use for initialization
    /// </summary>
    void Start()
    {
        AudioManager.Play(AudioClipName.EnemyDie);
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // destroy the game object if the explosion has finished its animation
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95)
        {
            //Randonly created a item 
            float chance = Random.value;
            Vector3 positionOfItem = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            if (chance <= 0.3f) // instantiate healing item chanse 30%
            {
                GameObject healingItem = Instantiate(healingItemPrefab, positionOfItem, Quaternion.identity);
                healingItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            }
            else if (chance <= 0.45f) // instaitiate invisible Item chanse 15%
            {
                GameObject invisibleItem = Instantiate(invisibleItemPrefab, positionOfItem, Quaternion.identity);
                invisibleItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            }

            else if (chance <= 0.9) // Instantiate coin chanse 45%
            {
                GameObject coin = Instantiate(coinPrefab, positionOfItem, Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            }
            else if (chance <= 0.95) // Instantiate heart chanse 5%
            {
                GameObject heart = Instantiate(heartPrefab, positionOfItem, Quaternion.identity);
                heart.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }
}

