using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteHook : Spell
{
    public int parent;
    public float speed = 25f;
    public float initPosition;
    public Cuerda cuerda;
    public float initSpeed;
    float timerhook = 0.05f;
    public bool auto = false;
    float hp;

    void Update()
    {
        if(par.hp2 != hp)
        {
            Destroy(gameObject);
        }
        if(!auto)
            transform.position = new Vector3(transform.position.x, par.transform.position.y);
        if (initSpeed < 0)
        {
            if (initPosition + speed / 3.5f >= transform.position.x)
            {
                speed = -initSpeed;
            }
            if (timeleft <= 0f)
            {
                if (transform.position.x > initPosition)
                {
                    if(!auto)
                        par.animator.SetTrigger("atk3end");
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (initPosition + speed / 3.5f <= transform.position.x)
            {
                speed = -initSpeed;
            }
            if (timeleft <= 0f)
            {
                if (transform.position.x < initPosition)
                {
                    if(!auto)
                        par.animator.SetTrigger("atk3end");
                    Destroy(gameObject);
                }
            }
        }
        timeleft -= Time.deltaTime;/*
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }*/
        rb.velocity = new Vector2(speed, 0f);
    }
    // Use this for initialization
    void Start()
    {
        destroyable = false;
        bouncable = false;
        hp = par.hp2;
        Type = true;
        initSpeed = speed;
        destroyOnLeaveScreen = true;
        cuerda = Instantiate(cuerda, transform.position, transform.rotation);
        cuerda.hook = this;
        cuerda.par = par;
        initPosition = transform.position.x;
        damage = 6f;
        timeleft = 0.2f;
        Vector2 veler;
        veler.x = speed;
        veler.y = 0f;
        rb.velocity = veler;
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.invulnerable == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.magicalReflect)
                {
                    enemy.enemycounterspell = par;
                    enemy.magicalCounter = damage * 2f;
                    par.animator.SetTrigger("atk3end");
                    destroy();
                }
                else
                {
                    timerhook -= Time.deltaTime;
                    if (timerhook <= 0f)
                    {
                        speed = -initSpeed / enemy.constMaxSpeed;
                        enemy.disable = true;
                        enemy.absorbed = true;
                        enemy.gameObject.layer = 11;
                        enemy.move.x = speed / 0.7f;
                        enemy.gravityModifier = 0f;
                        enemy.velocity.y = 0f;
                    }
                }
            }
            else
            {
                par.animator.SetTrigger("atk3end");
                destroy();
                enemy.enemycounterspell = par;
                enemy.blesshp -= damage * par.magic / enemy.magic;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.godsbless <= 0)
            {
                float dmg = damage * par.physical / enemy.armor;
                if (enemy.magicalReflect)
                {
                    enemy.magicalCounter = dmg * 2f;
                    if (par.transform.position.x > enemy.transform.position.x && !enemy.right)
                    {
                        enemy.flip();
                    }
                    if (par.transform.position.x < enemy.transform.position.x && enemy.right)
                        enemy.flip();
                    Destroy(gameObject);
                }
                else
                {
                    enemy.damaged(dmg, false, true, 0, 0);
                    par.addMana(dmg);
                }
                enemy.gravityModifier = 1.5f;
                enemy.absorbed = false;
                enemy.gameObject.layer = 1;
                enemy.externalGravity = false;
            }
            else
            {
                destroy();
            }
        }
    }

    public void setParent(int par)
    {
        parent = par;
    }
    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}