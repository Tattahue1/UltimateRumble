using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xboomerang : Spell
{
    public int parent;
    public float speed;
    public float initPosition;
    public float initSpeed;
    float hp;

    void Update()
    {
        if (par.hp2 != hp)
        {
            Destroy(gameObject);
        }
        transform.position = new Vector3(transform.position.x, par.transform.position.y);
        if (initSpeed < 0)
        {
            if (initPosition + speed / 3f >= transform.position.x)
            {
                speed = -initSpeed;
            }
            if (timeleft <= 0f)
            {
                if (transform.position.x > initPosition)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (initPosition + speed / 3f <= transform.position.x)
            {
                speed = -initSpeed;
            }
            if (timeleft <= 0f)
            {
                if (transform.position.x < initPosition)
                {
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
        hp = par.hp2;
        Type = true;
        initSpeed = speed;
        destroyOnLeaveScreen = true;
        initPosition = transform.position.x;
        damage = 5f;
        bouncable = true;
        timeleft = 0.2f;
        Vector2 veler;
        veler.x = speed;
        veler.y = 0f;
        rb.velocity = veler;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.physicalImmune == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
                {
                    float dmg = damage * par.physical / enemy.armor;
                    if (enemy.physicalReflect)
                    {
                        enemy.physicalCounter = dmg * 2f;
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
                }
            }
            else
            {
                destroy();
                enemy.enemycounterspell = par;
                enemy.blesshp -= damage * par.magic / enemy.magic;
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