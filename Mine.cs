using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Spell
{
    private bool activate = false;
    float triggertime = 2f;

    void Update()
    {
        if(par.replacement != null)
        {
            par = par.replacement;
        }
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            animator.SetTrigger("destroy");
        }
        triggertime -= Time.deltaTime;
        if (triggertime <= 0f)
            activate = true;
    }
    // Use this for initialization
    void Start()
    {
        destroyable = false;
        ultimate = true;
        bouncable = false;
        Type = false;
        damage = 10f;
        timeleft = 25f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (activate)
        {
            PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
            if (enemy != null && enemy != par)
            {
                if (enemy.godsbless <= 0)
                {
                    float colxe;
                    Vector3 posEnemy = enemy.transform.position;
                    Vector3 posthis = transform.position;
                    if (posEnemy.x >= posthis.x)
                        colxe = 3f;
                    else
                        colxe = -3f;
                    animator.SetTrigger("destroy");
                    float dmg = damage / enemy.magic;
                    enemy.damaged(dmg, true, false, 2f, colxe*1.5f);
                    
                }
                else
                {
                    animator.SetTrigger("destroy");
                    enemy.enemycounterspell = par;
                    enemy.blesshp -= damage * par.magic / enemy.magic;
                }
            }
        }
    }

}
