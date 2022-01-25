using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craniummon : PhysicsObject
{
    public Stunner stun;
    public AudioClip audioattack1;
    public AudioClip audioult;
    public AudioClip audiohit_1;
    public AudioClip audiohit_2;
    public AudioClip audioattack2;
    public AudioClip audiobuff;
    public AudioClip audioattack3;

    public Transform firePoint;
    public CraniumSpear bul;
    public WaltzEnd darke;
    public Hit hit_i;
    public AdelanteQ hit_q;
    public CrouchHit hit_c;
    public GreaterHit hitg;
    public pinkstrike pinks;

    protected float hitTimeLeft = 1f;
    // protected float hp = 100.0f;
    protected bool flipSprite;
    protected SpriteRenderer spriteRenderer;
    protected int jumpArrow = 1;
    protected float cooldownAtk1 = 0f;
    protected float cooldownAtk2 = 0f;
    protected float cooldownAtk3 = 0f;
    public static float energybar = 0f;
    //    public GameObject ParticlePrefab;
    //  public GameObject ParticlePrefab2;
    protected int hitcount = 0;
    //private Rigidbody body;
    Vector3 health = new Vector3(1.0f, 1.0f, 1.0f);
    Vector3 ultb = new Vector3(1.0f, 1.0f, 1.0f);
    public int status = 0;

    bool blessactive = false;

    //    public float Rate = 500; // per second

    //float timeSinceLastSpawn = 0;

    // Use this for initialization
    void Awake()
    {
        weight = 1.85f;
        constMaxSpeed = 7.7f;
        maxSpeed = constMaxSpeed;
        staminaCharge = 1.1f;
        physical = 1.1f;
        buffcooldown = 25f;
        buffDuration = 10f;
        constJump = 13f;
        jumpTakeOffSpeed = constJump;
        magic = 1.2f;
        armor = 1.23f;
        MusicSource = GetComponent<AudioSource>();
        //      body = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        if(blesshp <= 0 && godsbless <= 0)
        {
            blessactive = false;
        }
        if(blessactive)
        {
            if(blesshp <= 0)
            {
                animator.SetTrigger("ultstrike");
                godsbless -= 1;
                blesshp = 10f;
            }
        }
        if (damagedP)
        {
            velocity.y = colY;
            if (highD)
            {
                takeHighdamage(0);
                damagedP = false;
                highD = false;
            }
            else
            {
                takeDamage(0);
                damagedP = false;
            }
        }
        hitTimeLeft -= Time.deltaTime;
        if (hitTimeLeft < 0)
        {
            hitTimeLeft = 1.5f;
            hitcount = 0;
        }
        if (highD && grounded)
            invulnerable = true;
        //stingmonstacks
        stingmonStacksDuration -= Time.deltaTime;
        if (stingmonStacksDuration < 0)
        {
            stingmonStacksDuration = 0f;
            stingmonStacks = 0;
        }
        //end stacks

        if (buff)
        {
            buffDuration -= Time.deltaTime;
            if (buffDuration < 0)
            {
                buff = false;
            }
        }
        else
        {
            buffDuration = 10f;
        }
        if (hp2 <= 0)
        {
            animator.SetBool("dead", true);
        }
        if (colora.a <= 0f)
            Destroy(gameObject);
        if (dead)
        {
            colora.a -= 0.001f;
            spriteRenderer.color = colora;
        }
        if (energybar >= 100)
            energybar = 100;
        if (grounded)
        {
            jumpArrow = 1;
        }

        if (disable == false)
        {
            if (signalHit)
            {
                signalHit = false;
                normalHit();
                hitcooldown = 0f;
            }
            if (signalHardhit)
            {
                stopper();
                signalHardhit = false;
                MusicSource.PlayOneShot(audiohit_1, 0.5F);
                disable = true;
                animator.SetTrigger("adelanteQ");
            }
            if (Input.GetKey(down) && grounded && Input.GetKeyDown(moveleft) == false && Input.GetKeyDown(moveright) == false)
            {
                if (grounded == true && Input.GetKeyDown(jump))
                {
                    platformCooldown = 2f * 0.18f / weight;
                    gameObject.layer = 9;
                }
                stopper();
                animator.SetBool("crouch", true);
                crouch = true;
                GetComponent<CapsuleCollider2D>().size = new Vector2(GetComponent<CapsuleCollider2D>().size.x, 3f);
                GetComponent<CapsuleCollider2D>().offset = new Vector2(GetComponent<CapsuleCollider2D>().offset.x, -1.2f);
                if (Input.GetKey(defend))
                {
                    print(crouch);
                    animator.SetBool("crouchDefended", true);
                    defendedCrouch = true;
                }
                else
                {
                    animator.SetBool("crouchDefended", false);
                    defendedCrouch = false;
                }
                if (Input.GetKeyDown(hit) && defendedCrouch == false)
                {
                    if (Random.Range(0.0f, 1.0f) >= 0.5f)
                        MusicSource.PlayOneShot(audiohit_1, 0.5F);
                    else
                        MusicSource.PlayOneShot(audiohit_2, 0.5F);
                    animator.SetTrigger("hitCrouch");
                }
            }
            else
            {
                animator.SetBool("crouch", false);
                crouch = false;
                GetComponent<CapsuleCollider2D>().offset = saver2;
                GetComponent<CapsuleCollider2D>().size = saver;
            }
            if (Input.GetKey(defend) && grounded)
            {
                move.x = 0;
                animator.SetBool("defended", true);
                defended = true;
            }
            else
            {
                animator.SetBool("defended", false);
                defended = false;
                defendedCrouch = false;
            }

            if (defended == false && crouch == false)
            {
                if (Input.GetKeyDown(moveright) == false && Input.GetKeyDown(moveleft) == false && !darkenergy)
                    move.x = 0f;
                if (Input.GetKey(moveright) && !darkenergy)
                {
                    move.x = 1f;
                }
                if (Input.GetKey(moveleft) && !darkenergy)
                {
                    move.x = -1f;
                }
                if (Input.GetKey(moveright) || Input.GetKey(moveleft))
                {
                    if (Input.GetKeyDown(hit) && grounded)
                    {
                        hitad1();
                        hitcooldown = 2f;
                    }
                }
                if (Input.GetKeyDown(jump) && jumpCount > 0)
                {
                    if (grounded)
                    {
                        velocity.y = jumpTakeOffSpeed;
                        jumper = true;
                        jumpTimer = 0.1f;
                    }
                    else
                    {
                        velocity.y = jumpTakeOffSpeed;
                        jumpCount -= jumpTaker;
                    }
                }
                if (adrenaline >= 30f)
                {
                    if (Input.GetKeyDown(atk1))
                    {
                        stopper();
                        MusicSource.PlayOneShot(audioattack1, 0.7F);
                        velocity.y = 0;
                        gravityModifier = 0;
                        adrenaline -= 30f;
                        jumpArrow = jumpArrow - 1;
                        animator.SetTrigger("attack1");
                        disable = true;
                        if (Input.GetKey(up))
                        {
                            status = 1;
                        }
                        else if (Input.GetKey(down))
                        {
                            status = 2;
                        }
                        else
                            status = 0;
                    }
                }

                if (Input.GetKeyDown(hit) && Input.GetKey(moveright) == false && Input.GetKey(moveleft) == false)
                {
                    normalHit();
                }
                if (Input.GetKeyDown(hit) && grounded == false)
                    normalHit();

                if (adrenaline >= 25f)
                {
                    if (Input.GetKeyDown(atk2))
                    {
                        stopper();
                        gravityModifier = 0f;
                        velocity.y = 0f;
                        MusicSource.PlayOneShot(audioattack2, 0.7F);
                        animator.SetTrigger("attack2");
                        disable = true;
                        adrenaline -= 25f;
                    }
                }

                if (Input.GetKeyDown(ult) && ultBarCharge >= 100)
                {
                    stopper();
                    MusicSource.PlayOneShot(audioult, 0.7F);
                    disable = true;
                    animator.SetTrigger("ult");
                    ultBarCharge = 0f;
                    gravityModifier = 0f;
                    velocity.y = 0f;
                }

                if (adrenaline >= 100f)
                {
                    if (Input.GetKeyDown(boost))
                    {
                        stopper();
                        MusicSource.PlayOneShot(audiobuff, 0.5F);
                        disable = true;
                        animator.SetTrigger("buff");
                        adrenaline -= 100f;
                    }
                }
                if (adrenaline >= 30f)
                {
                    if (Input.GetKeyDown(atk3))
                    {
                        stopper();
                        adrenaline -= 30f;
                        MusicSource.PlayOneShot(audioattack3, 0.7F);
                        disable = true;
                        animator.SetTrigger("meditation");
                        gravityModifier = 0f;
                        velocity.y = 0f;
                    }
                }
            }
        }
        if (move.x > 0 && !right)
        {
            flip();
        }
        else if (move.x < 0 && right)
        {
            flip();
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        if (!externalGravity)
            targetVelocity = move * maxSpeed;
    }
    void normalHit()
    {
        if (grounded == false)
        {
            hitcount = 0;
        }
        stopper();
        if (Random.Range(0.0f, 1.0f) >= 0.5f)
            MusicSource.PlayOneShot(audiohit_1, 0.5F);
        else
            MusicSource.PlayOneShot(audiohit_2, 0.5F);
        if (hitcount == 0)
        {
            animator.SetInteger("hitcount", 0);
            animator.SetTrigger("hit1");
            hitTimeLeft = 3f;
        }
        else if (hitcount == 1)
        {
            animator.SetInteger("hitcount", 1);
            animator.SetTrigger("hit2");
            hitTimeLeft = 3f;
        }
        else
        {
            animator.SetInteger("hitcount", 2);
            animator.SetTrigger("hit3");
            hitTimeLeft = 3f;
            hitcount = -1;
        }
        stopper();
        animator.SetBool("disabled", true);
        disable = true;
        hitcount++;
    }

    void hitad1()
    {
        if (hitcooldown <= 0f)
        {
            Stunner hit_1 = Instantiate(stun, firePoint.position, firePoint.rotation);
            hit_1.transform.parent = transform;
            hit_1.setParent(GetInstanceID());
            hit_1.addtime = 0.7f;
            hit_1.par = this;
        }
    }

    public void takeDamage(float damage)
    {
        rectify();
        if (grounded == false)
            activeInvulernable = true;
        if (alphed)
            gravityModifier = 1.5f;
        animator.SetTrigger("hurt 0");
        disable = true;
        damagedP = false;
        hp2 += damage;
    }

    public void takeHighdamage(float damage)
    {
        rectify();
        activeInvulernable = true;
        if (alphed == false)
            gravityModifier = 1.5f;
        energybar += damage;
        animator.SetTrigger("highdamaged");
        disable = true;
    }
    void Shoot()
    {
        float aux = -0.72f;
        if (right)
            aux = 0.72f;
        CraniumSpear bull = Instantiate(bul, firePoint.position + new Vector3(aux,0.77f,0f), firePoint.rotation);
        bull.par = this;
        if (right)
            bull.speed = 20f;
        bull.par = this; 
        if (automata != null)
        {
            CraniumSpear hit_12 = Instantiate(bul, automata.transform.position, firePoint.rotation);
            if (right)
                hit_12.speed = 20f;
            hit_12.par = automata.par;
        }
    }

    void DarkPrison()
    {
        WaltzEnd hit_1 = Instantiate(darke, transform.position, firePoint.rotation);
        hit_1.par = this;
        if (automata != null)
        {
            WaltzEnd hit_12 = Instantiate(darke, automata.transform.position, firePoint.rotation);
            hit_12.par = automata.par;
        }
    }

    void hurtend()
    {
        gravityModifier = 1.5f;
        disable = false;
        animator.SetBool("hurt", false);
    }

    void attack1end()
    {
        disable = false;
        gravityModifier = 1.5f;
    }

    void disabler()
    {
        move.x = 0;
        velocity.x = 0;
        disable = true;
    }
    void disablerHit()
    {
        move.x = 0;
        velocity.x = 0;
        disable = true;
    }

    void undisabler()
    {
        animator.SetBool("disabled", false);
        disable = false;
    }

    public static void addUltbar(float chargeHit)
    {
        energybar += chargeHit;
    }

    void Hit()
    {
        Hit hit_1 = Instantiate(hit_i, firePoint.position, firePoint.rotation);
        hit_1.transform.parent = transform;
        hit_1.collision.size = new Vector2(3f, 2f);
        hit_1.setParent(GetInstanceID());
        hit_1.par = this;
        if (automata != null)
        {
            Hit hit_12 = Instantiate(hit_i, automata.transform.position, firePoint.rotation);
            hit_12.par = automata.par;
            hit_12.collision.size = new Vector2(3f, 1f);
        }

    }
    void HitAdelante()
    {
        AdelanteQ hit_1 = Instantiate(hit_q, firePoint.position, firePoint.rotation);
        hit_1.transform.parent = transform;
        hit_1.setParent(GetInstanceID());
        hit_1.collision.size = new Vector2(3f, 2f);
        hit_1.par = this;
        hit_1.coly = 3f;
        hit_1.colx = 6f;
    }
    void HitCrouch()
    {
        CrouchHit hit_1 = Instantiate(hit_c, firePoint.position, firePoint.rotation);
        hit_1.transform.parent = transform;
        hit_1.setParent(GetInstanceID());
        hit_1.collision.size = new Vector2(3f, 2f);
        hit_1.par = this;
        if (automata != null)
        {
            CrouchHit hit_12 = Instantiate(hit_c, automata.transform.position, firePoint.rotation);
            hit_12.par = automata.par;
            hit_12.collision.size = new Vector2(3f, 1f);
        }
    }
    void hardHit()
    {
        GreaterHit hit_g = Instantiate(hitg, firePoint.position, firePoint.rotation);
        hit_g.transform.parent = transform;
        hit_g.setParent(GetInstanceID());
        hit_g.collision.size = new Vector2(3f, 2f);
        hit_g.par = this;
        if (automata != null)
        {
            GreaterHit hit_g2 = Instantiate(hitg, automata.transform.position, firePoint.rotation);
            hit_g2.par = automata.par;
            hit_g2.collision.size = new Vector2(3f, 1f);
        }
    }

    void Ult()
    {
        godsbless = 3;
        blesshp = 9f;
        blessactive = true;
        pinkstrike c = Instantiate(pinks, firePoint.position, firePoint.rotation);
        c.estetica = true;
        c.auxilio = 10f;
        c.godsbles = true;
        c.par = this;
        c.enemy = this; 
        if (automata != null)
        {
            pinkstrike hit_g2 = Instantiate(pinks, automata.transform.position, firePoint.rotation);
            hit_g2.par = automata.par;
            hit_g2.godsbles = true;
            hit_g2.enemy = this;
        }
    }

    void setInvulernable()
    {
        move.x = 0f;
        gravityModifier = 1.5f;
        invulnerable = true;
    }

    void setVulnerable()
    {
        invulnerable = false;
    }

    public bool getRotation()
    {
        return right;
    }/*
    void SpawnFireAlongOutline()
    {
        PolygonCollider2D col = GetComponent<PolygonCollider2D>();

        int pathIndex = Random.Range(0, col.pathCount);

        Vector2[] points = col.GetPath(pathIndex);

        int pointIndex = Random.Range(0, points.Length);

        Vector2 pointA = points[pointIndex];
        pointA.x /= 7;
        pointA.y /= 7;
        Vector2 pointB = points[(pointIndex + 1) % points.Length];
        pointB.x /= 7;
        pointB.y /= 7;
        if (right)
        {
            pointA.x = -pointA.x;
            pointB.x = -pointB.x;
        }

        Vector2 spawnPoint = Vector2.Lerp(pointA, pointB, Random.Range(0f, 1f));

        SpawnFireAtPosition(spawnPoint + (Vector2)this.transform.position);
    }

    void SpawnFireAtPosition(Vector2 position)
    {
        GameObject pp = SimplePool.Spawn(ParticlePrefab, position, Quaternion.identity);
        pp.transform.parent = transform;
        GameObject pp2 = SimplePool.Spawn(ParticlePrefab2, position, Quaternion.identity);
        pp2.transform.parent = transform;
    }*/

    void buffer()
    {
        aura.Play();
        buff = true;
    }

    void setInv()
    {
        if (grounded)
        {
            invulnerable = true;
        }
    }
    void meditation()
    {
        if (buff)
            hp2 += 0.3f;
        hp2 += 0.5f;
    }

    void stopper()
    {
        move.x = 0;
        velocity.x = 0;
    }
    void velset()
    {
        gravityModifier = 1.5f;
        rb2d.velocity = transform.right * 0f;
    }
    void Dead()
    {
        animator.SetBool("dying", true);
        cap.size = new Vector2(0f, 0f);
        gravityModifier = 0f;
        velocity = new Vector2(0f, 0f);
        rb2d.velocity = new Vector2(0f, 0f);
    }
    void releaseData()
    {
        ParticleSystem hit_g = Instantiate(deadsystem, transform.position, new Quaternion(0f, 0f, 0f, 0f));
        hit_g.transform.parent = transform;
        hit_g.Play();
        if (right)
            hit_g.transform.position = transform.position + new Vector3(0f, -0.7f, 0f);
        else
            hit_g.transform.position = transform.position + new Vector3(-0.2f, -0.7f, 0f);
        hit_g.transform.localScale = transform.localScale;
        dead = true;
    }
    void damaged()
    {
        disable = true;
    }
    void gravitySet()
    {
        gravityModifier = 1.5f;
    }
    void rectify()
    {
        status = 0;
    }
    void ultstrike()
    {
        pinkstrike c = Instantiate(pinks, firePoint.position, firePoint.rotation);
        c.godsbles = true;
        c.par = this;
        c.enemy = enemycounterspell;
        if (automata != null)
        {
            pinkstrike hit_g2 = Instantiate(pinks, automata.transform.position, firePoint.rotation);
            hit_g2.par = automata.par;
            hit_g2.godsbles = true;
            hit_g2.enemy = this;
        }
    }
}
