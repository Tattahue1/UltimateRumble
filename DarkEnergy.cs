using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnergy : Spell
{
    public CircleCollider2D collision;

    public int parent;

    void Start()
    {
        destroyable = false;
        Type = false;
        damage = 0.2f;
        timeleft = 0.2f;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.invulnerable == false && enemy.magicImmune == false)
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
                    float dmg = damage * par.magic / enemy.magic;
                    enemy.adrenaline -= damage * par.magic*2f;
                    par.adrenaline += damage * par.magic*2f;
                    enemy.damaged(dmg, false, false, 0, 0);
                    par.addMana(dmg);
                }
            }
            else
            {
                destroy();
                enemy.enemycounterspell = par;
                enemy.blesshp -= damage * par.magic / enemy.magic;
            }
        }
        Spell sedf = hitInfo.GetComponent<Spell>();
        if (sedf != null && sedf.destroyable)
        {
            sedf.destroy();
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

