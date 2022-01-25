using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trumpswords : Spell
{
    public int parent;
    public float speed = 10f;
    public KeyCode up;
    public KeyCode down;
    public float acceleration = 0f;

    void Update()
    {/*
        if (Input.GetKeyDown(swap) && par.disable == false)
        {
            Vector3 aux = par.transform.position;
            par.transform.position = transform.position;
            transform.position = aux;
        }*/
        if (Input.GetKey(up) && acceleration <= 7f)
        {
            acceleration += Time.deltaTime*50;
        }
        if(Input.GetKey(down) && acceleration >= -7f)
        {
            acceleration -= Time.deltaTime*50; 
        }
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
        if (speed >= 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, -acceleration * 10f);
            if (acceleration >= 0)
                speed = 10f - acceleration;
            else
                speed = 10f + acceleration;
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, -acceleration * 10f);
            if (acceleration >= 0)
                speed = -10f + acceleration;
            else
                speed = -10f - acceleration;
        }
        Vector2 veler = rb.velocity;
        veler.x = speed;
        veler.y = acceleration;
        rb.velocity = veler;
    }
    // Use this for initialization 
    void Start()
    {
        destroyOnLeaveScreen = false;
        Type = false;
        damage = 5.5f;
        up = par.up;
        down = par.down;
        timeleft = 100f;
        Vector2 veler = rb.velocity;
        veler.x = speed;
        veler.y = acceleration;
        rb.velocity = veler;
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
                        float dmg = damage * par.magic / enemy.magic;
                        enemy.damaged(dmg, false, false, 0, 0);
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
