using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeopardStrike : Spell
{
    public int parent;
    public float speed = -25f;

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
        Type = false;
        damage = 2.5f;
        bouncable = true;
        timeleft = 8f;
        rb.velocity = new Vector2(speed * 1.5f, 0f);
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
                    destroy();
                }
                else
                {
                    if (enemy.defended == false && enemy.defendedCrouch == false)
                    {
                        float dmg = damage * par.magic / enemy.magic;
                        enemy.damaged(dmg, false, false, 0f, 0f);
                        par.addMana(dmg);
                        destroy();
                    }
                    else
                        destroy();
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
