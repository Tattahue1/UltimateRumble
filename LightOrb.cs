using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrb : Spell
{
    public int parent;
    public float speed = -20f;
    public float acc = 0f;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            //            animator.SetTrigger("destr");
        }
    }
    // Use this for initialization
    void Start()
    {
        Type = false;
        transform.Rotate(0f, 0f, speed * 6f - acc * 3f * speed / 15f);
        damage = 0.3f;
        bouncable = true;
        timeleft = 0.5f;
        Vector2 veler = rb.velocity;
        if (acc < 0f)
            veler.x = speed;
        else
            veler.x = speed / 1.5f;
        veler.y = acc;
        rb.velocity = veler * 1.5f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.magicImmune == false && enemy.invulnerable == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false)
                {
                    if (enemy.magicalReflect)
                    {
                        enemy.enemycounterspell = par;
                        enemy.magicalCounter = damage * 2f;
                    }
                    else
                    {
                        damage = damage + Mathf.Pow(1.3f, enemy.heavenStacks);
                        float dmg = damage / enemy.magic;
                        if (enemy.heavenStacks >= 6)
                            enemy.damaged(dmg, true, false, 3f, 0f);
                        else
                            enemy.damaged(dmg, false, false, 0f, 0f);
                        enemy.timerHeaven = 0.2f;
                        enemy.heavenStacks += 1;
                        par.addMana(dmg);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Destroy(gameObject);
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
}