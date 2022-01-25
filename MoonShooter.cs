using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonShooter : Spell
{
    public int parent;
    public float speed = -20f;
    public float acc = 0f;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            animator.SetTrigger("destr");
        }
    }
    // Use this for initialization
    void Start()
    {
        Type = false;
        transform.Rotate(0f, 0f, speed * 6f-acc*3f*speed/15f);
        damage = 2f;
        bouncable = true;
        timeleft = 0.5f;
        Vector2 veler = rb.velocity;
        if (acc < 0f)
            veler.x = speed;
        else
            veler.x = speed / 1.5f;
        veler.y = acc;
        rb.velocity = veler*1.5f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.magicImmune == false && enemy.invulnerable == false)
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
                    if (enemy.defended == false && enemy.defendedCrouch == false)
                    {
                        damage = damage + enemy.stingmonStacks * 2;
                        float dmg = damage / enemy.magic;
                        enemy.damaged(dmg, false, false, 0, 0);
                        enemy.stingmonStacksDuration = 5f;
                        enemy.stingmonStacks += 2;
                        par.addMana(dmg);
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
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
        Spell sp = hitInfo.GetComponent<Spell>();
        if (sp != null && sp.par != par && !sp.GetComponent<Bouncer>() && !sp.GetComponent<DarkEnergyOut>())
        {
            Destroy(gameObject);
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
    public void destr()
    {
        Destroy(gameObject);
    }
}