using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slice : Spell
{
    public CapsuleCollider2D collision;
    public bool ulti = false;
    public bool preultimate = false;

    void Start()
    {
        ultimate = true;
        Type = true;
        damage = 8f;
        timeleft = 0.4f;
    }

    void Update()
    {
        transform.position = par.transform.position;
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.godsbless <= 0)
            {
                float auxiliar = timeleft * 1.5f;
                if (ulti || !enemy.invulnerable || par.buff)
                {
                    enemy.slicetimer = 3.2f;
                    enemy.absorbed = true;
                    enemy.gameObject.layer = 13;
                    float aux = 3f * enemy.weight * auxiliar * 2f;
                    if (preultimate)
                        aux = 5f * enemy.weight;
                    if (ulti)
                    {
                        aux = -10f * enemy.weight;
                        damage = 13f;
                    }
                    float dmg = damage * par.physical / enemy.magic;
                    enemy.damaged(dmg, true, true, aux, 0f);
                }
            }
            else
            {
                destroy();
                enemy.enemycounterspell = par;
                enemy.blesshp -= damage * par.magic / enemy.magic;
            }
            //  }
        }
    }
    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (!ulti)
                enemy.slicetimer = 2.5f;
            else
                enemy.slicetimer = 1.5f;
        }
    }


    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}

