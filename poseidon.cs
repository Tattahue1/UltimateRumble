using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poseidon : Spell
{
    public int parent;
    public float speed = -20f;

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
        damage = 6f;
        bouncable = true;
        timeleft = 10f;
        Vector2 veler = rb.velocity;
        veler.x = speed;
        veler.y = 0f;
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
                        float dmg = damage / enemy.magic;
                        enemy.damaged(dmg, false, false, 0, 0);
                        par.addMana(dmg);
                    }
                    else
                    {
                        enemy.addMana(damage);
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