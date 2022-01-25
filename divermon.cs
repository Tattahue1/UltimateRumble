using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class divermon : MonoBehaviour
{
    public divermonspear spear;
    public Rigidbody2D rb;
    public CapsuleCollider2D cap;
    public Animator animator;
    public PhysicsObject par;
    public Vector2 speed;
    public bool grounded;
    public float circleRadius = 0.2f;
    public LayerMask whatIsGround;
    public Transform feetPos;
    public Transform jumppos;
    public bool right;
    public float timerJump = 0f;
    public bool jumper = false;
    public int jumpcount = 1;
    private float platformcooldown = 0f;
    public float hp;
    public float gravity = 1f;
    public bool swimmer = false;
    private bool vertDirection;
    private bool attacker = false;
    public float timeleft = 30f;
    public float timero = 2f;
    public float possaver;
    private bool dead = false;
    private float deadTimer = 10f;
    public radar radar;

    void Start()
    {
        radar = Instantiate(radar, transform.position, transform.rotation);
        radar.transform.localScale = new Vector3(0.22f, 0.22f, 0f);
        radar.transform.parent = transform;
        radar.parent = this;
        hp = 10f;
    }

    void Update()
    {
        if(dead)
        {
            deadTimer -= Time.deltaTime;
        }
        if (deadTimer <= 0f)
            Destroy(gameObject);
        timero -= Time.deltaTime;
        if(timero <= 0f)
        {
            possaver = transform.position.x;
//            timero = 2f;
        }
        timeleft -= Time.deltaTime;
        if(timeleft <= 0f || hp <= 0f)
        {
            animator.SetTrigger("dead");
        }
        if (swimmer)
        {
            print("XDDDD");
            gameObject.layer = 11;
            animator.SetBool("water", true);
            if (transform.position.y >= 6.5f)
            {
                vertDirection = false;
            }
            if (transform.position.y <= -5.3f)
            {
                vertDirection = true;
            }
            if (!attacker)
            {
                if (vertDirection)
                    speed.y = 2f;
                else
                    speed.y = -2f;
            }
        }
        else
        {
            animator.SetBool("water", false);
        }
        platformcooldown -= Time.deltaTime;
        if (platformcooldown <= 0f && !swimmer)
            gameObject.layer = 10;
        if(grounded && timerJump <= 0f)
        {
            jumpcount = 1;
        }
        timerJump -= Time.deltaTime;
        if (speed.x > 0 && !right)
        {
            flip();
        }
        else if (speed.x < 0 && right)
        {
            flip();
        }
        
        if(rb.velocity.x == 0f)
        {
            if (timero <= 0f)
            {
                timero = 2f;
                speed.x = -speed.x;
            }
        }
        animator.SetBool("grounded", grounded);
        if (jumper && timerJump < 0f && jumpcount > 0 && swimmer == false)
        {
            jumpcount = 0;
            timerJump = 1f;
            jump();
        }
        if (Input.GetKeyDown(KeyCode.O))
            godown();
        if(transform.position.x <= -18.3f)
        {
            speed.x = 5f;
        }
        if(transform.position.x >= 11.2f)
        {
            speed.x = -5f;
        }
        if(!grounded)
            speed += 2f * Physics2D.gravity * Time.deltaTime * gravity;
        if (swimmer == false)
            grounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
        else
            grounded = true;
        if (Physics2D.OverlapCircle(jumppos.position, circleRadius, whatIsGround))
            jumper = false;
        else
            jumper = true;
        rb.velocity = new Vector2(speed.x/1.1f, speed.y);
    }

    public void jump()
    {
        speed.y = 10f;
        if (timeleft <= 12.5f)
            speed.y = 15f;
    }

    public void flip()
    {
        right = !right;
        transform.Rotate(0f, 180f, 0f);
    }

    public void godown()
    {
        gameObject.layer = 9;
        platformcooldown = 0.8f;
        speed.y = -5f;
    }

    void attack()
    {
        speed.x = speed.x*0.01f;
        speed.y = 0f;
        gravity = 0f;
    }

    void undisabler()
    {

        gravity = 1f;
        if (right)
            speed.x = 5f;
        else
            speed.x = -5f;
    }
    void setAttack()
    {
        attacker = true;
        speed.y = 0f;
    }
    void unsetAttack()
    {
        attacker = false;
    }
    void shoot()
    {
        Vector3 aux;
        if(right)
             aux = new Vector3(1.2f, 0.43f, 0f);
        else
            aux = new Vector3(-1.2f, 0.43f, 0f);
        divermonspear hit_1 = Instantiate(spear, transform.position + aux, transform.rotation);
        hit_1.parent = this;
        hit_1.damage = 1f;
        hit_1.par = par;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Spell enemy = hitInfo.GetComponent<Spell>();
        if (enemy != null && enemy.par != par)
            hp -= enemy.damage*enemy.par.magic;

        Hit hit = hitInfo.GetComponent<Hit>();
        if (hit != null && hit.par != par)
            hp -= hit.damage * hit.par.physical;

        GreaterHit ghit = hitInfo.GetComponent<GreaterHit>();
        if (ghit != null && ghit.par != par)
            hp -= hit.damage * ghit.par.physical;

        CrouchHit chit = hitInfo.GetComponent<CrouchHit>();
        if (chit != null && chit.par != par)
            hp -= hit.damage * chit.par.physical;

        AdelanteQ ahit = hitInfo.GetComponent<AdelanteQ>();
        if (ahit != null && ahit.par != par)
            hp -= hit.damage * ahit.par.physical;

        spikingStriker sp = hitInfo.GetComponent<spikingStriker>();
        if (sp != null)
            hp -= sp.damage * sp.par.physical;

        Timewalk tw = hitInfo.GetComponent<Timewalk>();
        if (tw != null)
            hp -= tw.damage * tw.par.physical;

        DragonImpulse di = hitInfo.GetComponent<DragonImpulse>();
        if (di != null)
            hp -= di.damage * di.par.physical;

        GreatTornado gt = hitInfo.GetComponent<GreatTornado>();
        if (gt != null)
            hp -= gt.damage * gt.par.physical;

        AlphaInForce ai = hitInfo.GetComponent<AlphaInForce>();
        if(ai != null)
        {
            animator.enabled = false;
            gravity = 0f;
            speed.x = speed.y = 0f;
        }
    }
    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        AlphaInForce ai = hitInfo.GetComponent<AlphaInForce>();
        if (ai != null)
        {
            print("buggedalphainforcelmfao");
            animator.enabled = true;
            gravity = 1.5f;
            if (right)
                speed.x = 5f;
            else
                speed.x = -5f;
        }
    }

    void damaged()
    {
        speed.x = speed.x * 0.01f;
        speed.y = 0f;
    }

    void die()
    {
        Destroy(gameObject);
    }

    void setdiying()
    {
        cap.transform.localScale = new Vector3(0f, 0f, 0f);
        dead = true;
    }
}
