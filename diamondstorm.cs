using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamondstorm : Spell
{
    public int parent;
    public float speed = -20f;
    public float acc;

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
        damage = 0.7f;
        bouncable = true;
        timeleft = 2f;
        Vector2 veler = rb.velocity;
        veler.x = speed;
        veler.y = acc;
        rb.velocity = veler;
        if(par.buff)
        {
            destroyable = false;
        }
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
                    bool aux = false;
                    if (par.lordknightmon && par.buff)
                        aux = true;
                    if (enemy.defended == false && enemy.defendedCrouch == false || aux)
                    {
                        float dmg = damage / enemy.magic;
                        enemy.damaged(dmg, false, false, 0, 0);
                        par.addMana(dmg);
                    }
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
        if (sp != null && !sp.GetComponent<Bouncer>() && !sp.GetComponent<DarkEnergyOut>())
        {
            if (!par.buff)
            {
                Destroy(gameObject);
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