using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Spell
{
    public int parent;
    public float speed = -20f;
    public int direction = 0;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        destroyable = false;
        Type = false;
        float acceleration = 0f;
        if(direction == 1)
        {
            speed = speed / 1.2f;
            acceleration = 15f;
            transform.Rotate(0f, 0f, -30f);
        }
        if (direction == 2)
        {
            transform.Rotate(0f, 0f, 30f);
            speed = speed / 1.2f;
            acceleration = -15f;
        }
        damage = 5.5f;
        bouncable = true;
        timeleft = 8f;
        rb.velocity = new Vector2(speed*1.5f, acceleration);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.magicImmune == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.magicalReflect)
                {
                    enemy.enemycounterspell = par;
                    enemy.magicalCounter = damage * 2f;
                }
                else
                {
                    if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
                    {
                        float colx;
                        if (enemy.transform.position.x >= transform.position.x)
                            colx = 1f;
                        else
                            colx = -1f;
                        float dmg = damage * par.magic / enemy.magic;
                        enemy.damaged(dmg, true, false, 2f, colx);
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

    public void setParent (int par)
    {
        parent = par;
    }
    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}