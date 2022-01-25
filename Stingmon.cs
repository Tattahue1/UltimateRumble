using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stingmon : PhysicsObject
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
    public spikingStriker ss;
    public MoonShooter moonS;
    public NaturesAssist na;
    public Hit hit_i;
    public AdelanteQ hit_q;
    public CrouchHit hit_c;
    public GreaterHit hitg;
    public EvilAntenna evilant;

    public float naturesCooldown = 25f;
    protected int NaturesCount = 0;
    protected float hitTimeLeft = 1f;
    protected bool flipSprite;
    protected SpriteRenderer spriteRenderer;
    protected int jumpArrow = 1;
    protected float cooldownAtk1 = 0f;
    protected float cooldownAtk2 = 0f;
    protected float cooldownAtk3 = 0f;
    public static float energybar = 0f;
    protected int hitcount = 0;
    //private Rigidbody body;
    Vector3 health = new Vector3(1.0f, 1.0f, 1.0f);
    Vector3 ultb = new Vector3(1.0f, 1.0f, 1.0f);


    // Use this for initialization
    void Awake()
    {
        statusResHero = true;
        weight = 1.8f;
        constMaxSpeed = 8f;
        maxSpeed = constMaxSpeed;
        staminaCharge = 1.4f;
        buffcooldown = 25f;
        buffDuration = 10f;
        constJump = 13f;
        jumpTakeOffSpeed = constJump;
        NaturesCount = 3;
        armor = 1.05f;
        magic = 0.9f;
        MusicSource = GetComponent<AudioSource>();
        //      body = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        cooldownAtk2 -= Time.deltaTime;
        buffcooldown -= Time.deltaTime;
        if (damagedP)
        {
            if(!buff)
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
        naturesCooldown -= Time.deltaTime;
        if (naturesCooldown < 0)
        {
            naturesCooldown = 15f;
            if(NaturesCount < 3)
                NaturesCount++;
        }
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
            buff = false;
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
                GetComponent<CapsuleCollider2D>().size = new Vector2(GetComponent<CapsuleCollider2D>().size.x,  5f);
                GetComponent<CapsuleCollider2D>().offset = new Vector2(GetComponent<CapsuleCollider2D>().offset.x, -1f);
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
                if (adrenaline >= 25f)
                {
                    if (Input.GetKeyDown(atk2))
                    {
                        MusicSource.PlayOneShot(audioattack2, 0.7F);
                        velocity.y = 0f;
                        gravityModifier = 0;
                        adrenaline -= 25f;
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
                if (adrenaline >= 25f)
                {
                    if (Input.GetKeyDown(atk1) && cooldownAtk2 <= 0f)
                    {
                        velocity.y = 0f;
                        if (grounded == false)
                            gravityModifier = 0f;
                        stopper();
                        adrenaline -= 25f;
                        MusicSource.PlayOneShot(audioattack1, 0.7F);
                        animator.SetTrigger("attack2");
                        disable = true;
                        cooldownAtk2 = 1f;
                    }
                }

                if (Input.GetKeyDown(ult) && grounded && ultBarCharge >= 100)
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
                        MusicSource.PlayOneShot(audiobuff, 0.7F);
                        disable = true;
                        animator.SetTrigger("buff");
                        adrenaline -= 100f;
                    }
                }

                if (Input.GetKeyDown(atk3) && NaturesCount > 0 && adrenaline >= 35f)
                {
                    naturesAssist();
                    adrenaline -= 35f;
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

    void hitad1()
    {
        if (hitcooldown <= 0f)
        {
            Stunner hit_1 = Instantiate(stun, firePoint.position, firePoint.rotation);
            hit_1.transform.parent = transform;
            hit_1.setParent(GetInstanceID());
            hit_1.par = this;
            hit_1.addtime = 0.2f;
        }
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

    public void takeDamage(float damage)
    {
        Vector3 rotation;
        rotation.x = rotation.y = rotation.z = 0f;
        rotation.z = 360 - transform.eulerAngles.z;
        transform.Rotate(rotation);
        if (grounded == false)
            activeInvulernable = true;
        if (alphed == false)
            gravityModifier = 1.5f;
        if (buff == false)
        {
            animator.SetTrigger("hurt 0");
            disable = true;
        }
        damagedP = false;
    }

    public void takeHighdamage(float damage)
    {
        Vector3 rotation;
        rotation.x = rotation.y = rotation.z = 0f;
        rotation.z = 360 - transform.eulerAngles.z;
        transform.Rotate(rotation);
        activeInvulernable = true;
        if (alphed == false)
            gravityModifier = 1.5f;
        energybar += damage;
        if (buff == false)
        {
            animator.SetTrigger("highdamaged");
            disable = true;
        }
    }
    void Shoot()
    {
        if(grounded == false)
        {
            transform.Rotate(0f, 0f, 20f);
            gravityModifier = 0f;
            velocity.y = -9f;
        }
        if (right)
            move.x = 2f/0.7f;
        else
            move.x = -2f/0.7f;
        spikingStriker bull = Instantiate(ss, firePoint.position, firePoint.rotation);
        bull.transform.parent = transform;
        bull.setParent(GetInstanceID());
        bull.par = this;
        if (automata != null)
        {
            spikingStriker bull2 = Instantiate(ss, automata.transform.position, firePoint.rotation);
            bull2.transform.parent = automata.transform;
            bull2.par = automata.par;
        }
    }

    void DarkPrison()
    {
        MoonShooter bull = Instantiate(moonS, firePoint.position, firePoint.rotation);
        if (right)
            bull.speed = 15f;
        else
            bull.speed = -15f;
        if (grounded == false)
        {
            bull.acc = -7.5f;
            bull.speed = bull.speed * 0.7f;
        }
        bull.setParent(GetInstanceID());
        bull.par = this;
        if (automata != null)
        {
            MoonShooter bull2 = Instantiate(moonS, automata.transform.position, firePoint.rotation);
            if (grounded == false)
            {
                bull2.acc = -10f;
            }
            if (right)
                bull2.speed = 15f;
            else
                bull2.speed = -15f;
            bull2.par = automata.par;
        }
    }

    void naturesAssist()
    {
        NaturesAssist nat = Instantiate(na, firePoint.position, firePoint.rotation);
        nat.setParent(GetInstanceID());
        nat.par = this;
        NaturesCount -= 1;
        if (automata != null)
        {
            NaturesAssist nat2 = Instantiate(na, automata.transform.position, firePoint.rotation);
            nat2.par = automata.par;
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
    void damaged()
    {
        disable = true;
    }
    void disablerHit()
    {
        disable = true;
    }

    void undisabler()
    {
        gravityModifier = 1.5f;
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
        if (buff)
            hit_1.stingmonStack = true;
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
        hit_1.colx = 0f;
        if (buff)
            hit_1.stingmonStack = true;
    }
    void HitCrouch()
    {
        CrouchHit hit_1 = Instantiate(hit_c, firePoint.position, firePoint.rotation);
        hit_1.transform.parent = transform;
        hit_1.setParent(GetInstanceID());
        hit_1.par = this;
        if (buff)
            hit_1.stingmonStack = true;
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
        if (buff)
            hit_g.stingmonStack = true;
        if (automata != null)
        {
            GreaterHit hit_g2 = Instantiate(hitg, automata.transform.position, firePoint.rotation);
            hit_g2.par = automata.par;
            hit_g2.collision.size = new Vector2(3f, 1f);
        }
    }

    void Ult()
    {
        Vector3 pos2 = firePoint.position;
        if (right)
            pos2.x = firePoint.position.x + 3f;
        else
            pos2.x = firePoint.position.x - 3f;
        pos2.y = firePoint.position.y;
        EvilAntenna ult = Instantiate(evilant, pos2, firePoint.rotation);
        ult.setParent(GetInstanceID());
        ult.par = this;
        if (automata != null)
        {
            EvilAntenna hit_g2 = Instantiate(evilant, automata.transform.position, firePoint.rotation);
            hit_g2.par = automata.par;
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
    }

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
    
    void gravitySet()
    {
        gravityModifier = 1.5f;
    }
    void rotateBack()
    {
        Vector3 rotation;
        rotation.x = rotation.y = rotation.z = 0f;
        rotation.z = 360 - transform.eulerAngles.z;
        transform.Rotate(rotation);
    }
}
