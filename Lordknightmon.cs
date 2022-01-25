using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lordknightmon : PhysicsObject
{
    public Stunner stun;
    Vector2 aux;
    public AudioClip audioattack1;
    public AudioClip audioult;
    public AudioClip audiohit_1;
    public AudioClip audiohit_2;
    public AudioClip audioattack2;
    public AudioClip audiobuff;
    public AudioClip audioattack3;

    public Transform firePoint;
    public CrimsonTempest bul;
    public counter count;
    public Spin sp;
    public Hit hit_i;
    public AdelanteQ hit_q;
    public CrouchHit hit_c;
    public GreaterHit hitg;
    public slice terminalJ;

    protected float hitTimeLeft = 1f;
    protected bool flipSprite;
    protected SpriteRenderer spriteRenderer;
    protected int jumpArrow = 1;
    protected float cooldownAtk1 = 0f;
    protected float cooldownAtk2 = 0f;
    protected float cooldownAtk3 = 0f;
    public static float energybar = 0f;
    protected int hitcount = 0;
    Vector3 health = new Vector3(1.0f, 1.0f, 1.0f);
    Vector3 ultb = new Vector3(1.0f, 1.0f, 1.0f);

    void Awake()
    {
        //ruthless - despiadado
        lordknightmon = true;
        weight = 1.7f;
        constMaxSpeed = 7.7f;
        staminaCharge = 1.3f;
        buffcooldown = 25f;
        buffDuration = 10f;
        constJump = 13.5f;
        jumpTakeOffSpeed = constJump;
        magic = 0.94f;
        armor = 1.17f;
        maxSpeed = constMaxSpeed;
        MusicSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cap = GetComponent<CapsuleCollider2D>();
    }

    protected override void ComputeVelocity()
    {
        buffcooldown -= Time.deltaTime;
        if (physicalCounter > 0.1f)
            animator.SetBool("counter", true);
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
                GetComponent<CapsuleCollider2D>().size = new Vector2(GetComponent<CapsuleCollider2D>().size.x, 6.5f / 1.5f);
                GetComponent<CapsuleCollider2D>().offset = new Vector2(GetComponent<CapsuleCollider2D>().offset.x, -2.2f);
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

                if (Input.GetKeyDown(ult) && grounded && ultBarCharge >= 100)
                {
                    stopper();
                    MusicSource.PlayOneShot(audioult, 0.7F);
                    disable = true;
                    animator.SetTrigger("ult");
                    gravityModifier = 0f;
                    velocity.y = 0f;
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
                if (adrenaline >= 15f)
                {
                    if (Input.GetKeyDown(atk3))
                    {
                        stopper();
                        adrenaline -= 15f;
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
            hit_1.addtime = 1.3f;
            hit_1.par = this;
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
        CrimsonTempest bull = Instantiate(bul, firePoint.position, firePoint.rotation);
        if (right)
            bull.speed = 20f;
        else
            bull.speed = -20f;
        bull.setParent(GetInstanceID());
        bull.par = this;
        if (automata != null)
        {
            CrimsonTempest bull2 = Instantiate(bul, automata.transform.position, firePoint.rotation);
            if (right)
                bull2.speed = 15f;
            else
                bull2.speed = -15f;
            bull2.par = automata.par;
        }
    }
    void Counter()
    {
        counter c = Instantiate(count, firePoint.position, firePoint.rotation);
        c.setParent(GetInstanceID());
        c.par = this;
    }
    void setCounter()
    {
        physicalReflect = false;
        animator.SetBool("counter", false);
        physicalCounter = 0f;
    }
    void enableCounter()
    {
           timerCounter = 3f;
           physicalReflect = true;
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
        hit_1.coly = 3f;
        hit_1.colx = 7f;
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

    void Ult1()
    {/*
        move.x = 0f;
        velocity.y = 0f;
        aux = cap.size;
        cap.size = new Vector2(0f, 0f);
        invulnerable = true;
        gravityModifier = 0f;
        if (right)
            transform.position = new Vector3(transform.position.x - 4f, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x + 4f, transform.position.y, transform.position.z);
        //TerminalJudgement ult = Instantiate(terminalJ, firePoint.position, firePoint.rotation);
        //ult.setParent(GetInstanceID());
        float vel;
        if (right)
            vel = 3f;
        else
            vel = -3f;
        velocity.y = 2f;
        move.x = vel;*/
        move.x = 0f;
        velocity.y = 0f;
        gravityModifier = 0f;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        slice ult = Instantiate(terminalJ, transform.position, transform.rotation);
        ult.par = this;
        float vel;
        if (right)
            vel = 5.5f;
        else
            vel = -5.5f;
        velocity.y = 3.5f;
        move.x = vel;
    }
    void Ult2()
    {
        gravityModifier = 0f;
        move.x = 0f;
        velocity.y = 0f;
        right = !right;
        transform.Rotate(0f, 180f, 0f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        slice ult = Instantiate(terminalJ, transform.position, transform.rotation);
        ult.par = this;
        //ult.setParent(GetInstanceID());
        float vel;
        if (right)
            vel = 5.5f;
        else
            vel = -5.5f;
        velocity.y = 3.5f;
        move.x = vel;
    }
    void Ult3()
    {
        gravityModifier = 0f;
        move.x = 0f;
        velocity.y = 0f;
        right = !right;
        transform.Rotate(0f, 180f, 0f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        slice ult = Instantiate(terminalJ, transform.position, transform.rotation);
        ult.par = this;
        float vel;
        if (right)
            vel = 5.5f;
        else
            vel = -5.5f;
        velocity.y = 3.5f;
        move.x = vel;
    }
    void Ult4()
    {
        move.x = 0f;
        velocity.y = 0f;
        right = !right;
        transform.Rotate(0f, 180f, 0f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        slice ult = Instantiate(terminalJ, transform.position, transform.rotation);
        ult.preultimate = true;
        ult.par = this;
        //ult.setParent(GetInstanceID());
        float vel;
        if (right)
            vel = 5.5f;
        else
            vel = -5.5f;
        velocity.y = 3.5f;
        move.x = vel;
    }
    void Ult5()
    {
        move.x = 0f;
        if (right)
            transform.position = new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z);
        velocity.y = 0f;
    }
    void ultimateSlash()
    {
        slice ult = Instantiate(terminalJ, transform.position, transform.rotation);
        ult.ulti = true;
        ult.transform.localScale = new Vector3(3f, 3f, 0f);
        ult.transform.Rotate(0f, 0f, 135f);
        ult.par = this;
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
        Spin hit_1 = Instantiate(sp, transform.position, firePoint.rotation);
        hit_1.par = this;
        if (automata != null)
        {
            Spin hit_12 = Instantiate(sp, automata.transform.position, firePoint.rotation);
            hit_12.par = automata.par;
        }
    }

    void stopper()
    {
        move.x = 0;
        velocity.x = 0;
    }
    void velset()
    {
        gravityModifier = 1.5f;
        rb2d.velocity = new Vector2(0f, 0f);
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
        ParticleSystem hit_g = Instantiate(deadsystem, transform.position, new Quaternion(0f, 0f,0f,0f));
        hit_g.transform.parent = transform;
        hit_g.Play();
        if (right)
            hit_g.transform.position = transform.position + new Vector3(0.3f, -0.7f, 0f);
        else
            hit_g.transform.position = transform.position + new Vector3(-0.3f, -0.7f, 0f);
        hit_g.transform.localScale = transform.localScale;
        dead = true;
    }
    void setUltOff()
    {
        cap.size = aux;
        setVulnerable();
    }
    void damaged()
    {
        disable = true;
    }
    void gravitySet()
    {
        gravityModifier = 1.5f;
    }
    void setUntouchable()
    {
        absorbed = true;
        gameObject.layer = 12;
    }
    void setTouchable()
    {
        absorbed = false;
        gameObject.layer = 1;
    }

    void counterDisabler()
    {
        Hit hit_1 = Instantiate(hit_i, firePoint.position, firePoint.rotation);
        hit_1.transform.parent = transform;
        hit_1.damage = 0f;
        hit_1.setParent(GetInstanceID());
        hit_1.par = this;
    }
}
