using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wargreymon : PhysicsObject
{
    bool flipper = false;

    public AudioClip audioattack1;
    public AudioClip audioult;
    public AudioClip audiohit_1;
    public AudioClip audiohit_2;
    public AudioClip audioattack2;
    public AudioClip audiobuff;
    public AudioClip audioattack3;


    private int direction = 0;
    public Transform firePoint;
    public Transform terra;
    public GreatTornado great;
    public MegaClawRed mcr;
    public MegaClawBlue mcb;
    public Terraforce ult3;

    public Hit hit_i;
    public AdelanteQ hit_q;
    public CrouchHit hit_c;
    public GreaterHit hitg;
    public Stunner stun;

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
    Vector3 health = new Vector3(1.0f, 1.0f, 1.0f);
    Vector3 ultb = new Vector3(1.0f, 1.0f, 1.0f);

    private bool terramove = false;

    // Use this for initialization
    void Awake()
    {
        weight = 1.85f;
        constMaxSpeed = 7.3f;
        staminaCharge = 1.1f;
        jumpArrow = 2;
        cooldownAtk3 = 10f;
        buffcooldown = 25f;
        buffDuration = 10f;
        constJump = 12.5f;
        jumpTakeOffSpeed = constJump;
        maxSpeed = constMaxSpeed;
        magic = 1.07f;
        armor = 1.2f;
        MusicSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        if(braveShieldStacks <= 0)
            cooldownAtk3 -= Time.deltaTime;
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
            physical = 1.8f;
            buffDuration -= Time.deltaTime;
            if (buffDuration < 0)
            {
                buff = false;
            }
        }
        else
        {
            physical = 1.8f;
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

        if (disable == false)
        {
            if(signalHit)
            {
                signalHit = false;
                normalHit();
                hitcooldown = 0f;
            }
            if(signalHardhit)
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
                    platformCooldown =2f* 0.18f / weight;
                    gameObject.layer = 9;
                }
                stopper();
                animator.SetBool("crouch", true);
                crouch = true;
                GetComponent<CapsuleCollider2D>().size = new Vector2(GetComponent<CapsuleCollider2D>().size.x, 7.5f / 1.5f);
                GetComponent<CapsuleCollider2D>().offset = new Vector2(GetComponent<CapsuleCollider2D>().offset.x, -1f);
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
                if (adrenaline >= 30f)
                {
                    direct();
                    if (Input.GetKeyDown(atk2))
                    {
                        gravityModifier = 0f;
                        rotateDirection();
                        greatTornadomovement();
                        MusicSource.PlayOneShot(audioattack2, 0.7F);
                        animator.SetTrigger("attack2");
                        disable = true;
                        adrenaline -= 30f;
                    }
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
                    if (Input.GetKeyDown(atk1) && grounded)
                    {
                        stopper();
                        MusicSource.PlayOneShot(audioattack1, 0.7F);
                        adrenaline -= 30f;
                        animator.SetTrigger("attack1");
                        disable = true;
                    }
                }
                
                if (Input.GetKeyDown(hit)&& Input.GetKey(moveright) == false && Input.GetKey(moveleft) == false)
                {
                    normalHit();
                }
                if (Input.GetKeyDown(hit) && grounded == false)
                    normalHit();
                if (adrenaline >= 35f)
                {
                    direct();
                    if (Input.GetKeyDown(atk2))
                    {
                        gravityModifier = 0f;
                        rotateDirection();
                        greatTornadomovement();
                        MusicSource.PlayOneShot(audioattack2, 0.7F);
                        animator.SetTrigger("attack2");
                        disable = true;
                        velocity.y = 0f;
                        adrenaline -= 35f;
                    }
                }

                if (Input.GetKeyDown(ult) && ultBarCharge >= 100)
                {
                    gravityModifier = 0f;
                    velocity.x = 0f;
                    velocity.y = 0f;
                    if (grounded)
                        terraforceMovement1();
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
                if (cooldownAtk3 <= 0 && adrenaline >= 20f)
                {
                    if (Input.GetKeyDown(atk3) && grounded)
                    {
                        stopper();
                        MusicSource.PlayOneShot(audioattack3, 0.7F);
                        braveShieldStacks = 5;
                        adrenaline -= 20f;
                        cooldownAtk3 = 5f;
                        //disable = true;
                        //animator.SetTrigger("meditation");
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
        maxSpeed = constMaxSpeed;
        transform.Rotate(0f, 0f, 0f);
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
        Vector3 addpos = new Vector3(0.82f, -0.2f, 0f);
        if (!right)
            addpos.x = -0.87f;
        MegaClawBlue bull = Instantiate(mcb, firePoint.position+addpos, firePoint.rotation);
        bull.setParent(GetInstanceID());
        bull.setPar(this);
        if (buff)
            bull.dramonMult = true;
        if (right)
            bull.speed = 4.5f;
        if (automata != null)
        {
            MegaClawBlue bull2 = Instantiate(mcb, automata.transform.position, firePoint.rotation);
            if (right)
                bull2.speed = 4.5f;
            bull2.par = automata.par;
        }
    }
    void Shoot2()
    {
        Vector3 addpos = new Vector3(0.82f, -0.2f, 0f);
        if (!right)
            addpos.x = -0.87f;
        MegaClawRed bull = Instantiate(mcr, firePoint.position+addpos, firePoint.rotation);
        bull.setParent(GetInstanceID());
        bull.setPar(this);
        if (buff)
            bull.dramonMult = true;
        if (right)
            bull.speed = 4.5f;
        if (automata != null)
        {
            MegaClawRed bull2 = Instantiate(mcr, automata.transform.position, firePoint.rotation);
            if (right)
                bull2.speed = 4.5f;
            bull2.par = automata.par;
        }
    }

    void DarkPrison()
    {
        GreatTornado de = Instantiate(great, firePoint.position, firePoint.rotation);
        de.setParent(GetInstanceID());
        de.par = this;
        if (buff)
            de.dramonMult = true;
        if (automata != null)
        {
            GreatTornado de2 = Instantiate(great, firePoint.position, firePoint.rotation);
            de2.par = automata.par;
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
        maxSpeed = constMaxSpeed;
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
        if(buff)
            hit_1.dramonMult = true;
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
    void hitad()
    {
            float vel;
            flipper = true;
            if (right)
                vel = -0.4f;
            else
                vel = 0.4f;
            move.x = vel;
            velocity.y = 2.5f;
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
            hit_1.coly = 2.5f;
            hit_1.colx = 0.4f;
        }
    }
    void HitCrouch()
    {
        CrouchHit hit_1 = Instantiate(hit_c, firePoint.position, firePoint.rotation);
        hit_1.transform.parent = transform;
        hit_1.setParent(GetInstanceID());
        hit_1.par = this;
        if (buff)
            hit_1.dramonMult = true;
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
            hit_g.dramonMult = true;
        if (automata != null)
        {
            GreaterHit hit_g2 = Instantiate(hitg, automata.transform.position, firePoint.rotation);
            hit_g2.par = automata.par;
            hit_g2.collision.size = new Vector2(3f, 1f);
        }
    }

    void Ult()
    {
        Terraforce ult2 = Instantiate(ult3, terra.position, firePoint.rotation);
        if (right)
            ult2.speed = 12f;
        ult2.setParent(GetInstanceID());
        ult2.par = this;
        if (automata != null)
        {
            Terraforce ult22 = Instantiate(ult3, automata.transform.position, firePoint.rotation);
            if (right)
                ult22.speed = 12f;
            ult22.par = automata.par;
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
        Vector3 rotation;
        rotation.x = rotation.y = rotation.z = 0f;
        rotation.z = 360 - transform.eulerAngles.z;
        transform.Rotate(rotation);
        gravityModifier = 1.5f;
        rb2d.velocity = transform.right * 0f;
        maxSpeed = constMaxSpeed;
        flipper = false;
    }
    void direct()
    {
        if(Input.GetKey(up))
            direction = 0;
        else if (Input.GetKey(down))
            direction = 4;
        if (Input.GetKey(moveright))
        {
            direction = 2;
            if (Input.GetKey(up))
                direction = 1;
            else if (Input.GetKey(down))
                direction = 3;
        }
        else if (Input.GetKey(moveleft))
        {
            direction = 6;
            if (Input.GetKey(up))
                direction = 7;
            else if (Input.GetKey(down))
                direction = 5;
        }
    }
    void rotateDirection()
    {
        Vector3 rotation;
        rotation.x = rotation.y = rotation.z = 0f;
        if (direction == 0)
            rotation.z = 270f - transform.eulerAngles.z;
        if (direction == 1)
            rotation.z = 135f - transform.eulerAngles.z - transform.eulerAngles.y;
        if (direction == 2)
            rotation.z = 180f - transform.eulerAngles.z - transform.eulerAngles.y;
        if (direction == 3)
            rotation.z = 225f - transform.eulerAngles.z - transform.eulerAngles.y;
        if (direction == 4)
            rotation.z = 90f - transform.eulerAngles.z;
        if (direction == 5)
            rotation.z = 45f - transform.eulerAngles.z - transform.eulerAngles.y;
        if (direction == 6)
            rotation.z = 0f - transform.eulerAngles.z - transform.eulerAngles.y;
        if (direction == 7)
            rotation.z = -45f - transform.eulerAngles.z - transform.eulerAngles.y;
        transform.Rotate(rotation);
    }
    void terraforceMovement1()
    {
        right = !right;
        transform.Rotate(0f, 180f, 0f);
        Vector2 vel;
        if (right)
            vel.x = -0.1f;
        else
            vel.x = 0.1f;
        velocity.y = 2f;
        move.x = vel.x;
        terramove = true;
    }
    void terraforceMovement2()
    {
        if (terramove)
        {
            velocity.y = 0.5f;
            terramove = false;
        }
    }
    void greatTornadomovement()
    {
        Vector2 veloc;
        veloc.x = veloc.y = 0f;
        if (direction == 0)
        {
            veloc.y = 5f;
            veloc.x = 0f;
        }
        if (direction == 1)
        {
            veloc.y = 3.2f;
            veloc.x = 3.2f;
        }
        if (direction == 2)
        {
            veloc.y = 0f;
            veloc.x = 5f;
        }
        if (direction == 3)
        {
            veloc.y = -3.2f;
            veloc.x = 3.2f;
        }
        if (direction == 4)
        {
            veloc.y = -5f;
            veloc.x = 0f;
        }
        if (direction == 5)
        {
            veloc.y = -3.2f;
            veloc.x = -3.2f;
        }
        if (direction == 6)
        {
            veloc.y = 0f;
            veloc.x = -5f;
        }
        if (direction == 7)
        {
            veloc.y = 3.2f;
            veloc.x = -3.2f;
        }
        velocity.y = veloc.y * 1.25f;
        move.x = veloc.x/3f;
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
            hit_g.transform.position = transform.position + new Vector3(0.15f, -0.7f, 0f);
        else
            hit_g.transform.position = transform.position + new Vector3(-0.2f, -0.7f, 0f);
        hit_g.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
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
}
