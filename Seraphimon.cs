using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seraphimon : PhysicsObject
{
    bool flipper = false;

    public Stunner stun;
    public AudioClip audioattack1;
    public AudioClip audioult;
    public AudioClip audiohit_1;
    public AudioClip audiohit_2;
    public AudioClip audioattack2;
    public AudioClip audiobuff;
    public AudioClip audioattack3;

    public Transform firePoint;
    public Bullet bul;
    public DarkEnergy darke;
    public SevenHeavens heavens;
    public Hit hit_i;
    public AdelanteQ hit_q;
    public CrouchHit hit_c;
    public GreaterHit hitg;
    public testament terminalJ;

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


    //    public float Rate = 500; // per second

    //float timeSinceLastSpawn = 0;

    // Use this for initialization
    void Awake()
    {
        weight = 1.7f;
        constMaxSpeed = 7.5f;
        maxSpeed = constMaxSpeed;
        staminaCharge = 1.22f;
        physical = 1f;
        buffcooldown = 25f;
        buffDuration = 10f;
        constJump = 13.5f;
        jumpTakeOffSpeed = constJump;
        magic = 1.15f;
        armor = 1.15f;
        MusicSource = GetComponent<AudioSource>();
        //      body = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        cooldownAtk1 -= Time.deltaTime;
        cooldownAtk2 -= Time.deltaTime;
        cooldownAtk3 -= Time.deltaTime;
        buffcooldown -= Time.deltaTime;
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
            magic = 1.8f;
            buffDuration -= Time.deltaTime;
            if (buffDuration < 0)
            {
                buff = false;
            }/*
            timeSinceLastSpawn += Time.deltaTime;

            float correctTimeBetweenSpawns = 1f / Rate;

            while (timeSinceLastSpawn > correctTimeBetweenSpawns)
            {
                // Time to spawn a particle
                SpawnFireAlongOutline();
                timeSinceLastSpawn -= correctTimeBetweenSpawns;
            }*/
        }
        else
        {
            magic = 1.3f;
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
                GetComponent<CapsuleCollider2D>().size = new Vector2(GetComponent<CapsuleCollider2D>().size.x, 7 / 1.5f);
                GetComponent<CapsuleCollider2D>().offset = new Vector2(GetComponent<CapsuleCollider2D>().offset.x, -2.3f);
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
                GetComponent<CapsuleCollider2D>().size = saver;
                GetComponent<CapsuleCollider2D>().offset = saver2;
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
                        animator.SetTrigger("attack1");
                        disable = true;
                    }
                }

                if (Input.GetKeyDown(hit) && Input.GetKey(moveright) == false && Input.GetKey(moveleft) == false)
                {
                    normalHit();
                }
                if (Input.GetKeyDown(hit) && grounded == false)
                    normalHit();

                if (adrenaline >= 30f)
                {
                    if (Input.GetKeyDown(atk2) && grounded)
                    {
                        stopper();
                        MusicSource.PlayOneShot(audioattack2, 0.7F);
                        animator.SetTrigger("attack2");
                        disable = true;
                        adrenaline -= 30f;
                    }
                }


                if (Input.GetKeyDown(ult) && ultBarCharge >= 100)
                {
                    gravityModifier = 0f;
                    velocity.y = 0f;
                    move.x = 0f;
                    stopper();
                    MusicSource.PlayOneShot(audioult, 0.7F);
                    disable = true;
                    animator.SetTrigger("ult");
                    ultBarCharge = 0f;
                    hp2 -= 10f;
                }

                if (adrenaline >= 100f)
                {
                    if (Input.GetKeyDown(boost) && grounded)
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
                    if (Input.GetKeyDown(atk3) && grounded)
                    {
                        stopper();
                        adrenaline -= 30f;
                        MusicSource.PlayOneShot(audioattack3, 0.7F);
                        disable = true;
                        animator.SetTrigger("meditation");
                    }
                }
            }
        }
        if (move.x > 0 && !right && !flipper)
        {
            flip();
        }
        else if (move.x < 0 && right && !flipper)
        {
            flip();
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

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

    void hitad()
    {
        float vel;
        flipper = true;
        if (right)
            vel = -0.4f;
        else
            vel = 0.4f;
        move.x = vel;
        velocity.y = 5f;
        gravityModifier = 0f;
    }
    void hitad1()
    {
        if (hitcooldown <= 0f)
        {
            Stunner hit_1 = Instantiate(stun, firePoint.position, firePoint.rotation);
            hit_1.transform.parent = transform;
            hit_1.setParent(GetInstanceID());
            hit_1.par = this;
            hit_1.addtime = 1.5f;
            hit_1.coly = 5.2f;
            hit_1.colx = 0.5f;
        }
    }

    public void takeDamage(float damage)
    {
        if (grounded == false)
            activeInvulernable = true;
        if (alphed == false)
            gravityModifier = 1.5f;
        animator.SetTrigger("hurt 0");
        disable = true;
        damagedP = false;
        hp2 += damage;
    }

    public void takeHighdamage(float damage)
    {
        activeInvulernable = true;
        if (alphed == false)
            gravityModifier = 1.5f;
        energybar += damage;
        animator.SetTrigger("highdamaged");
        disable = true;
    }
    void Shoot()
    {
        Bullet bull = Instantiate(bul, firePoint.position, firePoint.rotation);
        bull.setParent(GetInstanceID());
        if (buff)
            bull.damage = 18f;
        else
            bull.damage = 12f;
        bull.setPar(this);
    }

    void DarkPrison()
    {
        DarkEnergy de = Instantiate(darke, firePoint.position, firePoint.rotation);
        de.setParent(GetInstanceID());
        de.setPar(this);
        if (buff)
            de.damage = 0.75f;
    }
    void darkrpsionOut()
    {
        SevenHeavens de2 = Instantiate(heavens, firePoint.position, firePoint.rotation);
        if (grounded)
            de2.acc = 10f;
        else
            de2.acc = -10f;
        if (right)
            de2.speed = 10f;
        else
            de2.speed = -10f;
        de2.par = this;
        if (automata != null)
        {
            SevenHeavens bull2 = Instantiate(heavens, automata.transform.position, firePoint.rotation);
            bull2.acc = de2.acc;
            bull2.speed = de2.speed;
            bull2.par = automata.par;
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
        hit_1.par = this;
        hit_1.coly = -4f;
        hit_1.colx = 5f;
        if (buff)
            hit_1.dramonMult = true;
    }
    void HitCrouch()
    {
        CrouchHit hit_1 = Instantiate(hit_c, firePoint.position, firePoint.rotation);
        hit_1.transform.parent = transform;
        hit_1.setParent(GetInstanceID());
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
        hit_g.par = this;
        if (automata != null)
        {
            GreaterHit hit_g2 = Instantiate(hitg, automata.transform.position, firePoint.rotation);
            hit_g2.par = automata.par;
            hit_g2.collision.size = new Vector2(3f, 1f);
        }
    }
    void hitxd()
    {
        GreaterHit hit_g = Instantiate(hitg, firePoint.position, firePoint.rotation);
        hit_g.transform.parent = transform;
        hit_g.setParent(GetInstanceID());
        hit_g.coly = 6f;
        hit_g.par = this;
        if (automata != null)
        {
            GreaterHit hit_g2 = Instantiate(hitg, automata.transform.position, firePoint.rotation);
            hit_g2.par = automata.par;
            hit_g2.collision.size = new Vector2(3f, 1f);
        }
    }

    void hitxd2()
    {
        GreaterHit hit_g = Instantiate(hitg, firePoint.position, firePoint.rotation);
        hit_g.transform.parent = transform;
        hit_g.setParent(GetInstanceID());
        hit_g.coly = 1f;
        hit_g.colx = 7f;
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
        testament ult = Instantiate(terminalJ, transform.position, firePoint.rotation);
        ult.par = this;
        if (automata != null)
        {
            testament bull2 = Instantiate(terminalJ, automata.transform.position, firePoint.rotation);
            bull2.par = automata.par;
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
        hp2 += 1;
    }

    void stopper()
    {
        move.x = 0;
        velocity.x = 0;
    }
    void velset()
    {
        flipper = false;
        gravityModifier = 1.5f;
        move.x = 0f;
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
    void sevenheavensMove()
    {
        gravityModifier = 1.5f;
        flipper = true;
        if (right)
            move.x = -0.45f;
        else
            move.x = 0.45f;
        velocity.y = 2.5f;
    }
    void divineBreakermove()
    {
        if (right)
            move.x = 1.8f;
        else
            move.x = -1.8f;
    }
    void divineBreakermove2()
    {
        gravityModifier = 0f;
        if (right)
            move.x = 0.5f;
        else
            move.x = -0.5f;
        velocity.y = 3f;
    }
    void divineBreakermove3()
    {
        gravityModifier = 0f;
        if (right)
            move.x = 0.5f;
        else
            move.x = -0.5f;
        velocity.y = 6f;
    }
    void gravitySet()
    {
        gravityModifier = 1.5f;
    }
}
