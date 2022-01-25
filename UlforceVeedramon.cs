using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UlforceVeedramon : PhysicsObject
{
    public Stunner stun;
    public AudioClip audioattack1;
    public AudioClip audioult;
    public AudioClip audiohit_1;
    public AudioClip audiohit_2;
    public AudioClip audioattack2;
    public AudioClip audiobuff;
    public AudioClip audioattack3;
    public bool ultbuff2;


    public Transform firePoint;

    public DragonImpulse di;
    public Vlightning bul;
    public Tensegrity darke;
    public Hit hit_i;
    public AdelanteQ hit_q;
    public CrouchHit hit_c;
    public GreaterHit hitg;

    protected float hitTimeLeft = 1f;
    // protected float hp = 100.0f;
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
        weight = 1.8f;
        constMaxSpeed = 7.4f;
        staminaCharge = 1.1f;
        ultbuff2 = false;
        ultbuff = 0f;
        buffcooldown = 25f;
        buffDuration = 15f;
        constJump = 13.5f;
        jumpTakeOffSpeed = constJump;
        maxSpeed = constMaxSpeed;
        magic = 1f;
        armor = 1.12f;
        MusicSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void ComputeVelocity()
    {
        ultbuff -= Time.deltaTime;
        if (buff)
        {
            maxSpeed = constMaxSpeed * 1.5f;
            jumpTakeOffSpeed = 17f;
            armor = 1.5f;
            buffDuration -= Time.deltaTime;
            if (buffDuration < 0)
            {
                buff = false;
            }
        }
        else
        {
            jumpTakeOffSpeed = 13.5f;
            maxSpeed = constMaxSpeed;
            buffDuration = 15f;
        }
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

        if (ultbuff2)
        {
            animator.SetBool("buffbool", true);
            physical = 3f;
            if (ultbuff <= 0)
            {
                animator.SetTrigger("buffout");
                animator.SetBool("buffbool", false);
                ultbuff2 = false;
            }
        }
        else
        {
            animator.SetBool("buffbool", false);
            physical = 1f;
            ultbuff = 25f;
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
                    platformCooldown = 2f* 0.18f / weight;
                    gameObject.layer = 9;
                }
                stopper();
                animator.SetBool("crouch", true);
                crouch = true;
                GetComponent<CapsuleCollider2D>().size = new Vector2(GetComponent<CapsuleCollider2D>().size.x, 8f / 1.5f);
                GetComponent<CapsuleCollider2D>().offset = new Vector2(GetComponent<CapsuleCollider2D>().offset.x, -2f);
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
                if (Input.GetKeyDown(moveright) == false && Input.GetKeyDown(moveleft) == false && darkenergy == false)
                    move.x = 0f;
                if (Input.GetKey(moveright) && darkenergy == false)
                {
                    move.x = 1f;
                }
                if (Input.GetKey(moveleft) && darkenergy == false)
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
                        velocity.y = 0f;
                        gravityModifier = 0f;
                        stopper();
                        MusicSource.PlayOneShot(audioattack1, 0.7F);
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
                    if (Input.GetKeyDown(atk2))
                    {
                        stopper();
                        MusicSource.PlayOneShot(audioattack2, 0.7F);
                        animator.SetTrigger("attack2");
                        disable = true;
                        gravityModifier = 0f;
                        velocity.y = 0f;
                        adrenaline -= 30f;
                    }
                }

                if (Input.GetKeyDown(ult) && grounded && ultBarCharge >= 100)
                {
                    //aura.Play();
                    stopper();
                    //MusicSource.PlayOneShot(audiobuff, 0.7F);
                    disable = true;
                    animator.SetTrigger("buff");
                    animator.SetBool("buffbool", true);
                    ultbuff = 25f;
                    stopper();
                    ultBarCharge = 0f;
                }

                if (adrenaline >= 70f)
                {
                    if (Input.GetKeyDown(boost) && grounded)
                    {
                        stopper();
                        //MusicSource.PlayOneShot(audiobuff, 0.7F);
                        disable = true;
                        animator.SetTrigger("ult");
                        adrenaline -= 80f;
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
                        animator.SetTrigger("attack3");
                        velocity.y = 0f;
                        gravityModifier = 0f;
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
        if (hitcount == 1)
        {
            animator.SetInteger("hitcount", 1);
            animator.SetTrigger("hit2");
            hitTimeLeft = 3f;
        }
        if (hitcount == 2 && !ultbuff2)
        {
            animator.SetInteger("hitcount", 2);
            animator.SetTrigger("hit3");
            hitTimeLeft = 3f;
            hitcount = -1;
        }
        if (hitcount == 2 && ultbuff2)
        {
            animator.SetInteger("hitcount", 0);
            animator.SetTrigger("hit1");
            hitTimeLeft = 3f;
        }
        if (hitcount == 3 && ultbuff2)
        {
            animator.SetInteger("hitcount", 1);
            animator.SetTrigger("hit2");
            hitTimeLeft = 3f;
        }
        if (hitcount == 4 && ultbuff2)
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
            hit_1.par = this;
        }
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
        animator.SetTrigger("hurt 0");
        disable = true;
        damagedP = false;
        hp2 += damage;
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
        animator.SetTrigger("highdamaged");
        disable = true;
    }
    void Shoot()
    {
        Vlightning bull = Instantiate(bul, firePoint.position, firePoint.rotation);
        bull.setParent(GetInstanceID());
        bull.setPar(this);
        if (automata != null)
        {
            Vlightning bull2 = Instantiate(bul, automata.transform.position, firePoint.rotation);
            bull2.par = automata.par;
        }
    }
    void Shoot2()
    {
        if (right)
            move.x = 2.4f;
        else
            move.x = -2.4f;
        DragonImpulse bull = Instantiate(di, firePoint.position, firePoint.rotation);
        bull.transform.parent = transform;
        bull.setParent(GetInstanceID());
        bull.par = this;
        if (automata != null)
        {
            DragonImpulse bull2 = Instantiate(di, automata.transform.position, firePoint.rotation);
            bull2.par = automata.par;
        }
    }
    void DarkPrison()
    {
        Vector3 poser;
        if (right)
            poser = new Vector3(-0.3f, 0f, 0f);
        else
            poser = new Vector3(0.3f, 0f, 0f);
        Tensegrity de = Instantiate(darke, transform.position+poser, firePoint.rotation);
        de.setParent(GetInstanceID());
        de.setPar(this);
        if (automata != null)
        {
            Tensegrity bull2 = Instantiate(darke, automata.transform.position, firePoint.rotation);
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
        hit_1.coly = 4f;
        hit_1.colx = 5f;
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
        aura.Play();
        buff = true;
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
        ultbuff2 = true;
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
        Vector3 rotation;
        rotation.x = rotation.y = rotation.z = 0f;
        rotation.z = 360 - transform.eulerAngles.z;
        transform.Rotate(rotation);
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
        hit_g.transform.position = transform.position + new Vector3(-0.2f, -0.7f, 0f);
        hit_g.transform.localScale = transform.localScale;
        dead = true;
    }
    void damaged()
    {
        disable = true;
    }
    void rotateE()
    {
        transform.Rotate(0f, 0f, 15f);
    }
    void gravitySet()
    {
        gravityModifier = 1.5f;
    }
}