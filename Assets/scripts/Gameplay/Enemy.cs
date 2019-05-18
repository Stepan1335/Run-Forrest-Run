using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IntEventInvoker
{
    [SerializeField]
    GameObject enemyBulletPrefab;

    [SerializeField]
    GameObject enemyDeathPrefab;

    //retake in start
    float speed;
    float shootDelay;
    float bulletSpeed;
    bool run = true;
    float health;
    int points = 10;
    int direction = -1;

    //save for effectivity
    BoxCollider2D boxCollider;
    float halfWeidth;
    float halfHeigh;

    //Animation
    Animator animator;

    //Death support
    bool enemyDead = false;

    //Health HUD
    [SerializeField]
    GameObject[] shilds;

    // Start is called before the first frame update
    void Start()
    {
        speed = ConfigurationUtils.EnemySpeed;
        bulletSpeed = ConfigurationUtils.EnemyBulletSpeed;
        health = ConfigurationUtils.EnemyHealth;

        //save for effectivity
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        halfWeidth = boxCollider.size.x / 2;
        halfHeigh = boxCollider.size.y / 2;

        //Animation support
        animator = GetComponent<Animator>();

        //Check how many health
        Health();

        //events
        unityEvents.Add(EventName.PointsAddedEvent, new PointsAddedEvent());
        EventManager.AddInvoker(EventName.PointsAddedEvent, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyDead) // I need it and destroy game object because I cant to play death animation
        {
            //moving support
            if (run)
            {
                Run();
            }
            //if rin animation run two times stop and play aniation of attack
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "EnemyRun"
                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.9 * animator.GetCurrentAnimatorClipInfo(0)[0].clip.length
                && run)
            {
                run = false;
                animator.SetInteger("State", 1);
            }
            //after attack animation shoot and then run and play a run animation
            else if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack"
                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.65 * animator.GetCurrentAnimatorClipInfo(0)[0].clip.length
                && !run)
            {
                Shoot();
                run = true;
                animator.SetInteger("State", 0);
            }

        }
        else
        {
            Instantiate(enemyDeathPrefab, transform.position, Quaternion.identity);
            unityEvents[EventName.PointsAddedEvent].Invoke(points);
            Destroy(gameObject);
        }
    }

    void Run()
    {
        //make a run animation
        animator.SetInteger("State", 0);
        //change a position
        Vector2 position;
        position.x = transform.position.x;
        position.y = transform.position.y;
        position.x += speed * direction;
        transform.position = position;
    }

    void Shoot()
    {
        AudioManager.Play(AudioClipName.EnemyAttack);
        Vector2 bulletPosition = gameObject.transform.position;
        bulletPosition.x += 3 * halfWeidth * direction;
        bulletPosition.y += 3.8f * halfHeigh;
        GameObject bullet = Instantiate(enemyBulletPrefab, bulletPosition, Quaternion.identity);
        //make bullet impulce force
        bullet script = bullet.GetComponent<bullet>();
        script.ApplyForce(new Vector2(bulletSpeed * direction, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;
        //Destroy bullet
        if (coll.CompareTag("CharacterBullet"))
        {
            health--;
            //destroy 
            Destroy(coll);
        }

        if (coll.CompareTag("Forrest") || coll.CompareTag("DieZone"))
        {
            health = 0;
        }

        //update a number of health
        Health();

        if (health <= 0)
        {
            enemyDead = true;
        }

    }

    void Health()
    {
        for (int i = 0; i < shilds.Length; i++)
        {
            if (i < health)
            {
                shilds[i].SetActive(true);
            }
            else
            {
                shilds[i].SetActive(false);
            }
        }
    }

    //Flip the enemy if it need to go in right side
    public void Flip()
    {
        direction *= -1;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = false;
    }
}