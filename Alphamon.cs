using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphamon : PhysicsObject
{
    bool timeJump = false;

    public Stunner stun;
    public AudioClip audioattack1;
    public AudioClip audioult;
    public AudioClip audiohit_1;
    public AudioClip audiohit_2;
    public AudioClip audioattack2;
    public AudioClip audiobuff;
    public AudioClip audioattack3;

    public Transform firePoint;

    public Transform ouryuken;
    public SoulDigitalization bul;
    public Timewalk timewalk;
    public Hit hit_i;
    public AdelanteQ hit_q;
    public CrouchHit hit_c;
    public GreaterHit hitg;
    public AlphaInForce alpha;

    protected float hitTimeLeft = 1f;
    // protected float hp = 100.0f;
    protected bool flipSprite;
    protected SpriteRenderer spriteRenderer;
    protected int jumpArrow = 1;
    protected float cooldownAtk1 = 0f;
    protected float cooldownAtk2 = 0f;
    protected float cooldownAtk3 = 0f;
    public static float energybar = 0f;
    public GameObject ParticlePrefab;
    public GameObject ParticlePrefab2;
    protected int hitcount = 0;
    //private Rigidbody body;
    Vector3 health = new Vector3(1.0f, 1.0f, 1.0f);
    Vector3 ultb = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 capsaver;

    // Use this for initialization
    void Awake()
    {
        dramon = true;
        weight = 1.8f;
        constMaxSpeed = 6.3f;
        staminaCharge = 1.1f;
        capsaver = cap.transform.localScale;
        buffcooldown = 25f;
        buffDuration = 15f;
        constJump = 12.5f;
        jumpTakeOffSpeed = constJump;
        maxSpeed = constMaxSpeed;
        magic = 1.11f;
        armor = 1.18f;
        MusicSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void ComputeVelocity()
    {
        if (timeJump)
        {
            if (Input.GetKey(down))
                velocity.y = -10f;
            if (Input.GetKey(up))
                velocity.y = 10f;
            if (Input.GetKey(moveleft))
                move.x = -3f;
            if (Input.GetKey(moveright))
                move.x =3f;
            if (!Input.GetKey(moveright) && !Input.GetKey(moveleft))
                move.x = 0f;
            if (!Input.GetKey(up) && !Input.GetKey(down))
                velocity.y = 0f;
        }
        buffcooldown -= Time.deltaTime;
        if (damagedP)
        {
            timeJump = false;
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
            animator.SetBool("buffbool", true);
            magic = 1.7f;
            physical = 2.2f;
            buffDuration -= Time.deltaTime;
            if (buffDuration < 0)
            {
                animator.SetTrigger("buffout");
                animator.SetBool("buffbool", false);
                buff = false;
            }
        }
        else
        {
            animator.SetBool("buffbool", false);
            magic = 1.12f;
            physical = 1f;
            buffDuration = 15f;
        }
        if (hp2 <= 0)
        {
            animator.SetBool("dead", true);
        }
        if (colora.a <= 0f)
            Destroy(gameObject);
        if (dead)
        {
            colora.a -= 0.002f;
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
                GetComponent<CapsuleCollider2D>().size = new Vector2(GetComponent<CapsuleCollider2D>().size.x, 5.5f / 1.2f);
                GetComponent<CapsuleCollider2D>().offset = new Vector2(GetComponent<CapsuleCollider2D>().offset.x, -0.7f);
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
                if (Input.GetKeyDown(atk3) && adrenaline >= 12f)
                {
                    stopper();
                    gravityModifier = 0f;
                    velocity.y = 0f;
                    adrenaline -= 12f;
                    MusicSource.PlayOneShot(audioattack3, 0.7F);
                    disable = true;
                    animator.SetTrigger("attack3");
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
                        adrenaline -= 30f;
                        velocity.y = 0f;
                        gravityModifier = 0f;
                        stopper();
                        MusicSource.PlayOneShot(audioattack1, 0.7F);
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
                        gravityModifier = 0f;
                        velocity.y = 0f;
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
                    ultBarCharge = 0f;
                }

                if (adrenaline >= 100f)
                {
                    if (Input.GetKeyDown(boost) && grounded)
                    {
                        aura.Play();
                        stopper();
//                        MusicSource.PlayOneShot(audiobuff, 0.7F);
                        disable = true;
                        animator.SetTrigger("buff");
                        animator.SetBool("buffbool", true);
                        adrenaline -= 100f;
                    }
                }

                if (Input.GetKeyDown(atk3) && adrenaline >= 20f)
                {
                    stopper();
                    gravityModifier = 0f;
                    velocity.y = 0f;
                    adrenaline -= 20f;
                    MusicSource.PlayOneShot(audioattack3, 0.7F);
                    disable = true;
                    animator.SetTrigger("attack3");
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
        Vector3 pos;
        pos.y = pos.z = 0f;
        pos.x = -2f;
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
        if (hitcount == 2)
        {
            animator.SetInteger("hitcount", 2);
            animator.SetTrigger("hit3");
            hitTimeLeft = 3f;
        }
        if (hitcount == 3)
        {
            velocity.y = 0f;
            gravityModifier = 0f;
            if (right)
                pos.y = -2f;
            else
                pos.y = 2f;
            if (right)
                transform.position -= pos;
            else
                transform.position += pos;
            flip();
            animator.SetInteger("hitcount", 3);
            animator.SetTrigger("hit4");
            hitTimeLeft = 3f;
            hitcount = -1;
            gravityModifier = 0f;
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
        Vector3 poser;
        poser = firePoint.position;
        if (right)
        {
            poser += new Vector3(1.2f, 0.2f, 0f);
        }
        else
        {
            poser += new Vector3(-1.2f, 0.2f, 0f);
        }
        if (buff)
        {
            if (right)
                poser = ouryuken.position + new Vector3(2f, 0f, 0f);
            else
                poser = ouryuken.position + new Vector3(-2f, 0f, 0f);
        }
        SoulDigitalization bull = Instantiate(bul, poser, firePoint.rotation);
        poser = firePoint.position;
        if (right)
        {
            bull.speed = 30f;
        }
        else
        {
            bull.speed = -30f;
        }
        bull.setParent(GetInstanceID());
        bull.setPar(this);
        if (automata != null)
        {
            SoulDigitalization bull2 = Instantiate(bul, automata.transform.position, firePoint.rotation);
            bull2.par = automata.par;
            if (right)
                bull2.speed = 30f;
            else
                bull2.speed = -30f;
        }
    }
    void Shoot2()
    {
        Vector3 poser;
        poser = firePoint.position;
        if (right)
        {
            poser += new Vector3(1.2f, 0.2f, 0f);
        }
        else
        {
            poser += new Vector3(-1.2f, 0.2f, 0f);
        }
        if (buff)
        {
            if (right)
                poser = ouryuken.position + new Vector3(2f, 0f, 0f);
            else
                poser = ouryuken.position + new Vector3(-2f, 0f, 0f);
        }
        SoulDigitalization bull = Instantiate(bul, poser, firePoint.rotation);
        poser = firePoint.position;
        bull.acc = -5f;
        if (right)
        {
            bull.speed = 30f;
        }
        else
        {
            bull.speed = -30f;
        }
        bull.setParent(GetInstanceID());
        bull.setPar(this);
        if (automata != null)
        {
            SoulDigitalization bull2 = Instantiate(bul, automata.transform.position, firePoint.rotation);
            bull2.par = automata.par;
            bull2.acc = -5f;
            if (right)
                bull2.speed = 30f;
            else
                bull2.speed = -30f;
        }
    }
    void Shoot3()
    {
        Vector3 poser;
        poser = firePoint.position;
        if (right)
        {
            poser += new Vector3(1.2f, 0.2f, 0f);
        }
        else
        {
            poser += new Vector3(-1.2f, 0.2f, 0f);
        }
        if (buff)
        {
            if (right)
                poser = ouryuken.position + new Vector3(2f, 0f, 0f);
            else
                poser = ouryuken.position + new Vector3(-2f, 0f, 0f);
        }
        SoulDigitalization bull = Instantiate(bul, poser, firePoint.rotation);
        poser = firePoint.position;
        bull.acc = 5f;
        if (right)
        {
            bull.speed = 30f;
        }
        else
        {
            bull.speed = -30f;
        }
        bull.setParent(GetInstanceID());
        bull.setPar(this);
        if (automata != null)
        {
            SoulDigitalization bull2 = Instantiate(bul, automata.transform.position, firePoint.rotation);
            bull2.par = automata.par;
            bull2.acc = 5f;
            if (right)
                bull2.speed = 30f;
            else
                bull2.speed = -30f;
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
    void disabler2()
    {
        disable = true;
    }
    void disablerHit()
    {
        move.x = 0;
        velocity.x = 0;
        disable = true;
    }
    void disabler3()
    {
        move.x = 0;
        velocity.x = 0;
        disable = true;
        gravityModifier = 0f;
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
        Vector3 poser;
        poser = firePoint.position;
        Hit hit_1 = Instantiate(hit_i, poser, firePoint.rotation);
        hit_1.transform.parent = transform;
        if (buff)
            hit_1.GetComponent<BoxCollider2D>().size = new Vector2(4f, 1f);
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
        Vector3 poser;
        poser = firePoint.position;
        AdelanteQ hit_1 = Instantiate(hit_q, poser, firePoint.rotation);
        hit_1.transform.parent = transform;
        if (buff)
            hit_1.GetComponent<BoxCollider2D>().size = new Vector2(4f, 1f);
        hit_1.setParent(GetInstanceID());
        hit_1.par = this;
        hit_1.coly = 3f;
        hit_1.colx = 3f;
    }
    void HitCrouch()
    {
        Vector3 poser;
        poser = firePoint.position;
        CrouchHit hit_1 = Instantiate(hit_c, poser, firePoint.rotation);
        hit_1.transform.parent = transform;
        if (buff)
            hit_1.GetComponent<BoxCollider2D>().size = new Vector2(4f, 1f);
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
        Vector3 poser;
        poser = firePoint.position;
        GreaterHit hit_g = Instantiate(hitg, poser, firePoint.rotation);
        hit_g.transform.parent = transform;
        if (buff)
            hit_g.GetComponent<BoxCollider2D>().size = new Vector2(4f, 1f);
        hit_g.setParent(GetInstanceID());
        hit_g.par = this;
        if (hitcount == 3)
        {
            hit_g.colx = 0f;
            hit_g.coly = 8f;
        }
        if (hitcount == 4)
        {
            hit_g.coly = -2f;
        }
        if (automata != null)
        {
            GreaterHit hit_g2 = Instantiate(hitg, automata.transform.position, firePoint.rotation);
            hit_g2.par = automata.par;
            hit_g2.collision.size = new Vector2(3f, 1f);
        }
    }

    void Ult()
    {
        Vector3 pos;
        pos.x = pos.y = 0f;
        pos.z = 3f;
        AlphaInForce ult = Instantiate(alpha, pos, firePoint.rotation);
        ult.setParent(GetInstanceID());
        ult.par = this;
    }
    void timewalkdmg()
    {
        Timewalk ult = Instantiate(timewalk, transform.position, firePoint.rotation);
        ult.setParent(GetInstanceID());
        ult.par = this;
        if (automata != null)
        {
            Timewalk ult2 = Instantiate(timewalk, automata.transform.position, firePoint.rotation);
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
    }

    void buffer()
    {
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
        cap.transform.localScale = capsaver;
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
    
    void timewalkMove1()
    {
        if (right)
            transform.position += new Vector3(3f, 0f, 0f);
        else
            transform.position -= new Vector3(3f, 0f, 0f);
    }

    void setTimeJump()
    {
        //        cap.transform.localScale = new Vector3(0f, 0f, 0f);
        disable = true;
        timeJump = true;
    }
    void UnsetTimeJump()
    {
        //      cap.transform.localScale = capsaver;
        maxSpeed = constMaxSpeed;
        timeJump = false;
        velocity.y = 0.2f;
        gravityModifier = 1.5f;
    }
    void gravitySet()
    {
        gravityModifier = 1.5f;
    }
}
