using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : IntEventInvoker
{
    [SerializeField]
    GameObject bulletPrefab;

    float speed;
    float jumpForce;
    bool canJump = false;
    bool canShoot = true;
    Vector2 bulletDirectionVector = new Vector2 (2, 0);
    int bulletDirection = 1;
    float bulletOffsetX;
    float bulletOffsetY;
    float currentHealth;
    float currentLives;
    float defaultGravitiScale;

    //invisiable support
    bool invisibleItemActive;
    float invisibleDuration = 5f;
    Timer invisibleTimer;


    //death support 
    bool characterDead = false;

    //save for effectivity
    Rigidbody2D rb2d;
    SpriteRenderer sprite;
    CapsuleCollider2D capsuleCollider2D;
    float halfWeidth;
    float halfHeigh;

    //Animation support
    Animator animatorCharacter;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = ConfigurationUtils.ForrestHealth;

        //save for effectivity
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        defaultGravitiScale = rb2d.gravityScale;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        halfWeidth = capsuleCollider2D.size.x / 2;
        halfHeigh = capsuleCollider2D.size.y / 2;
        animatorCharacter = GetComponent<Animator>();

        //timer support
        invisibleTimer = gameObject.AddComponent<Timer>();
        invisibleTimer.Duration = invisibleDuration;

        //events 
        unityEvents.Add(EventName.HealthChangedEvent, new HealthChangedEvent());
        EventManager.AddInvoker(EventName.HealthChangedEvent, this);
        EventManager.AddListener(EventName.GetAnotherLifeEvent, GetAnotherLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterDead) // check if character dead
        {
            Move();
            Shoot();
            Jump();

            //animation idle
            if (!Input.anyKey && canJump)
            {
                animatorCharacter.SetInteger("State", 0);
                bulletOffsetY = 1.5f * halfHeigh;
            }

        }

        if (invisibleTimer.Finished)
        {
            invisibleItemActive = false;
            CharacterInvisible(invisibleItemActive);
        }
    }

    void Move()
    {
        //Moving support
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            //to make animation only if character on ground
            if (canJump)
            {
                animatorCharacter.SetInteger("State", 1);
            }

            if (horizontalInput < 0)
            {
                sprite.flipX = true; //flip sprite
                bulletDirection = -1; // change bullet direction
                bulletOffsetX = - 4 * halfWeidth;

            }

            else if (horizontalInput > 0)
            {
                sprite.flipX = false; //flip sprite
                bulletDirection = 1;  // change bullet direction
                bulletOffsetX = 4 * halfWeidth;
            }

            bulletOffsetY = 1.2f * halfHeigh;
            if (!canJump)
            {

                bulletOffsetY = 2f * halfHeigh;
            }
            //move a character
            Vector2 position;
            position.x = transform.position.x;
            position.y = transform.position.y;
            position.x += horizontalInput * ConfigurationUtils.ForrestSpeed; 
            transform.position = position;
        }
    }

    /// <summary>
    /// jump support
    /// </summary>
    void Jump()
    {
        //jump support
        if (Input.GetAxis("Jump") > 0 && canJump)
        {
            if (rb2d.velocity.y >= 0) //don't jump if character in air 
            {
                AudioManager.Play(AudioClipName.CharacterJump);
                canJump = false;
                rb2d.AddForce(new Vector2(0, ConfigurationUtils.ForrestJumpForce), ForceMode2D.Impulse);
                animatorCharacter.SetInteger("State", 2);
            }
            else
            {
                canJump = false;
            }
        }
    }

    /// <summary>
    /// if you press left ctrl it create a bullet
    /// </summary>
    void Shoot()
    {
            //shoot support
            float fire = Input.GetAxis("Fire");
            if (fire == 0)
            {
                canShoot = true;
            }

            if (fire != 0 && canShoot)
            {
                canShoot = false;
                AudioManager.Play(AudioClipName.CharacterAttack);

                //create a bullet
                Vector2 bulletPosition = transform.position;
                bulletPosition.x += bulletOffsetX;
                bulletPosition.y += bulletOffsetY;
                GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

                //make bullet impulce force
                bullet script = bullet.GetComponent<bullet>();
                script.ApplyForce(bulletDirectionVector * bulletDirection);
                if (bulletDirection == -1) // flip the bullet 
                {
                    bullet.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
            GameObject coll = collision.gameObject;

            //can jump 1 time per button press
            if (coll.CompareTag("Ground") && !canJump)
                {
            float offset = 0.02f; //
            if (coll.transform.position.y + coll.GetComponent<BoxCollider2D>().size.y / 2 - offset
            < transform.position.y)// to do it only if character is located on top of platorm

            {
                canJump = true;
                AudioManager.Play(AudioClipName.CharacterLand);// play land sound when character land the ground                

            }           
                }

                //reduse health of character if it collides with enemy bullet
            if (coll.CompareTag("EnemyBullet"))
                {
                    Destroy(coll);// Destroy bullet
                    //reduce health
                    currentHealth--;
                    TakeDamage();
                    AudioManager.Play(AudioClipName.CharacterLoseHealth);
                }

            //reduse health of character if it collides with emeny or
            if (coll.CompareTag("Enemy"))
                {
                    //reduce health
                    currentHealth--;
                    TakeDamage();
                    AudioManager.Play(AudioClipName.CharacterLoseHealth);
                }

            //kill the character if fall from platform
            if (coll.CompareTag("DieZone"))
                {
                    currentHealth = 0;
                    TakeDamage();
                }

            //healt if it get a healing Item
            if (coll.CompareTag("HealingItem"))
            {
                AudioManager.Play(AudioClipName.HealthPickup);
                GetHealComponent();
                Destroy(coll);
            }

            if (coll.CompareTag("InvisibleItem"))
            {
            AudioManager.Play(AudioClipName.GenericPickup);
            invisibleItemActive = true;
            CharacterInvisible(invisibleItemActive);
            invisibleTimer.Run();
            Destroy(coll);
            }
    }

    /// <summary>
    /// make a character invisible for enemies
    /// </summary>
    /// <param name="itemActive"> Active or unactive</param>
    void CharacterInvisible(bool itemActive)
    {
        Physics2D.IgnoreLayerCollision(10, 9, itemActive); // Character don't collide with enemy bullet 
        Physics2D.IgnoreLayerCollision(10, 11, itemActive); // Character don't collide with enemy
        Physics2D.IgnoreLayerCollision(12, 9, itemActive); // Character bullet don't collide with enemy bullet 
        Physics2D.IgnoreLayerCollision(12, 11, itemActive); // Character bullet don't collide with enemy
        //Make sprite a little bit transparent
        Color color = Color.white;
        if (itemActive)
        {
            color.a = 0.3f;
        }
        else
        {
            color.a = 1;
        }
        sprite.color = color;
    }

    /// <summary>
    /// If character get healing Item and he was less tham 3 health his health increase by 1
    /// </summary>
    void GetHealComponent()
    {
        if (currentHealth <= 3)
        {
            currentHealth++;
            unityEvents[EventName.HealthChangedEvent].Invoke((int)currentHealth);
        }
    }

    /// <summary>
    /// reduse the amount of hearts
    /// </summary>
    void TakeDamage()
    {
        if (currentHealth <= 0)
        {
            animatorCharacter.SetInteger("State", 3); // death animation            
            capsuleCollider2D.enabled = false; // don't collide with enemies when character dead
            rb2d.gravityScale = 0; // don't fall because you don't have collider
            characterDead = true;
        }
        unityEvents[EventName.HealthChangedEvent].Invoke((int)currentHealth);
    }

    /// <summary>
    /// reset a character if he died
    /// </summary>
    /// <param name="something">I don't use this parameter it's only for event system</param>
    void GetAnotherLife(int something)
    {
        animatorCharacter.SetInteger("State", 0); // set a idle animation
        capsuleCollider2D.enabled = true;
        rb2d.gravityScale = defaultGravitiScale;
        currentHealth = ConfigurationUtils.ForrestHealth;
        characterDead = false;
        transform.position = GameObject.FindGameObjectWithTag("RespawnPoint").transform.position;
        unityEvents[EventName.HealthChangedEvent].Invoke((int)currentHealth);
    }

}
