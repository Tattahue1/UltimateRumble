using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metalseadramon : PhysicsObject
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
    public poseidon bul;
    public divermon diver;
    public kingofthesea seaking;
    public megahorn horn;
    public Hit hit_i;
    public AdelanteQ hit_q;
    public CrouchHit hit_c;
    public GreaterHit hitg;
    public riopoderoso terminalJ;

    public Transform ultfirepoint;        

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
    private float jumpcooldown =1f;

    //    public float Rate = 500; // per second

    //float timeSinceLastSpawn = 0;

    // Use this for initialization
    void Awake()
    {
        dramon = true;
        seadramon = true;
        weight = 1.9f;
        constMaxSpeed = 8f;
        maxSpeed = constMaxSpeed;
        staminaCharge = 1.4f;
        physical = 1.1f;
        buffcooldown = 25f;
        buffDuration = 10f;
        constJump = 13f;
        jumpTakeOffSpeed = constJump;
        magic = 1.22f;
        armor = 1.22f;
        MusicSource = GetComponent<AudioSource>();
        //      body = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        if (kingofthesea)
        {
            animator.SetBool("crouch", false);
            crouch = false;
        }
        jumpcooldown -= Time.deltaTime;
        if (kingofthesea)
        {
            gravityModifier = 0f;
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
            if (Input.GetKey(down) && grounded && Input.GetKeyDown(moveleft) == false && Input.GetKeyDown(moveright) == false && kingofthesea == false)
            {
                if (grounded == true && Input.GetKeyDown(jump))
                {
                    platformCooldown = 2f* 0.18f / weight;
                    gameObject.layer = 9;
                }
                stopper();
                animator.SetBool("crouch", true);
                crouch = true;
                GetComponent<CapsuleCollider2D>().size = new Vector2(9f, 1.7f);
                GetComponent<CapsuleCollider2D>().offset = new Vector2(GetComponent<CapsuleCollider2D>().offset.x, -2.1f);
                if (Input.GetKey(defend))
                {
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
                if(kingofthesea)
                {
                    if (Input.GetKeyDown(up) == false && Input.GetKeyDown(down) == false)
                        velocity.y = 0f;
                    if (Input.GetKey(up))
                    {
                        velocity.y = 8f;
                    }
                    if (Input.GetKey(down))
                    {
                        velocity.y = -8f;
                    }
                }
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
                        hitcooldown = 1f;
                    }
                }
                if (Input.GetKeyDown(jump))
                {
                    if (kingofthesea)
                    {
                        if (jumpcooldown <= 0f)
                        {
                            animator.SetTrigger("burst");
                            jumpcooldown = 1f;
                        }
                    }
                    else
                    {
                        if (jumpCount > 0)
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
                    }
                }

                if (Input.GetKeyDown(hit) && Input.GetKey(moveright) == false && Input.GetKey(moveleft) == false)
                {
                    normalHit();
                }
                if (Input.GetKeyDown(hit) && grounded == false)
                    normalHit();

                if (adrenaline >= 20f)
                {
                    if (Input.GetKeyDown(atk2))
                    {
                        stopper();
                        MusicSource.PlayOneShot(audioattack2, 0.7F);
                        animator.SetTrigger("attack2");
                        disable = true;
                        gravityModifier = 0f;
                        velocity.y = 0f;
                        adrenaline -= 20f;
                    }
                }

                if (Input.GetKeyDown(ult) && ultBarCharge >= 100)
                {
                    stopper();
                    MusicSource.PlayOneShot(audioult, 0.7F);
                    disable = true;
                    animator.SetTrigger("ult");
                    ultBarCharge = 0f;
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
                if (adrenaline >= 40f)
                {
                    if (Input.GetKeyDown(atk3) && grounded)
                    {
                        stopper();
                        adrenaline -= 40f;
                        MusicSource.PlayOneShot(audioattack3, 0.7F);
                        disable = true;
                        animator.SetTrigger("meditation");
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
            hit_1.addtime = 2f;
            hit_1.par = this;
        }
    }

    public void takeDamage(float damage)
    {
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
        activeInvulernable = true;
        if (alphed == false)
            gravityModifier = 1.5f;
        energybar += damage;
        animator.SetTrigger("highdamaged");
        disable = true;
    }
    void Shoot()
    {
        float aux;
        float velaux;
        float rotation;
        if (right)
        {
            rotation = 0f;
            velaux = 20f;
            aux = -30f;
        }
        else
        {
            rotation = 180f;
            velaux = -20f;
            aux = 25f;
        }
        poseidon bull = Instantiate(bul, new Vector3(aux, transform.position.y, 0f), new Quaternion(0f, rotation, 0f, 0f));
        bull.speed = velaux;
        if (kingofthesea)
            bull.transform.localScale = bull.transform.localScale * 1.5f;
        bull.setParent(GetInstanceID());
        bull.setPar(this);
        if (automata != null)
        {
            poseidon bull2 = Instantiate(bul, new Vector3(aux, transform.position.y, 0f), new Quaternion(0f, rotation, 0f, 0f));
            bull2.speed = velaux;
            if (kingofthesea)
                bull2.transform.localScale = bull.transform.localScale * 1.5f;
            bull2.par = automata.par;
        }
    }

    void DarkPrison()
    {
        for (int i = 0; i < 4; i++)
        {
            divermon de = Instantiate(diver, transform.position + new Vector3(2f - 2* Mathf.Abs(i), 0f ,0f), diver.transform.rotation);
            if (right)
                de.speed.x = 5f;
            else
                de.speed.x = -5f;
            if(kingofthesea)
                de.timeleft += 20f;
            de.par = this;
            if (automata != null)
            {
                divermon de2 = Instantiate(diver, automata.transform.position + new Vector3(2f - 2 * Mathf.Abs(i), transform.position.y, 0f), diver.transform.rotation);
                de2.par = automata.par;
            }
        }
    }
    void buffer()
    {
        kingofthesea de2 = Instantiate(seaking, seaking.transform.position, seaking.transform.rotation);
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
        hit_1.coly = 5f;
        hit_1.colx = 3f;
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

    void Ult()
    {
        Vector3 auxiliar;
        if (right)
        {
            auxiliar = new Vector3(23.8f, 5.2f, 0f);
        }
        else
            auxiliar = new Vector3(-23.8f, 5.2f, 0f);
        riopoderoso ult = Instantiate(terminalJ, ultfirepoint.position +auxiliar , terminalJ.transform.rotation);
        ult.par = this;
        if (automata != null)
        {
            riopoderoso ult2 = Instantiate(terminalJ, automata.transform.position, terminalJ.transform.rotation);
            ult2.par = automata.par;
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
        gravityModifier = 1.5f;
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
    void burst()
    {
        if (right)
            move.x = 2.1f;
        else
            move.x = -2.1f;
        Hit hit_1 = Instantiate(hit_i, firePoint.position, firePoint.rotation);
        hit_1.transform.parent = transform;
        hit_1.par = this;
        hit_1.damage = 3f;
        hit_1.timeleft = 0.5f;
        if (automata != null)
        {
            Hit hit_12 = Instantiate(hit_i, automata.transform.position, firePoint.rotation);
            hit_12.par = automata.par;
            hit_12.collision.size = new Vector2(3f, 1f);
        }
    }
    void HornAttack()
    {
        Vector3 addpos = new Vector3(0.82f, -0.2f, 0f);
        if (!right)
            addpos.x = -0.87f;
        megahorn bull = Instantiate(horn, firePoint.position + addpos, firePoint.rotation);
        bull.setPar(this);
        if (right)
            bull.speed = 4.5f;
        if (automata != null)
        {
            megahorn bull2 = Instantiate(horn, automata.transform.position, firePoint.rotation);
            bull2.setPar(automata.par);
            if (right)
                bull2.speed = 4.5f;
        }
    }
}
