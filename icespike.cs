using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icespike : Spell
{
    // Start is called before the first frame update
    void Start()
    {
        destroyable = false;
        Type = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.magicImmune == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
                {
                    if (enemy.magicalReflect)
                    {
                        enemy.enemycounterspell = par;
                        enemy.magicalCounter = damage * 2f;
                    }
                    else
                    {
                        float colx;
                        float posEnemy = enemy.transform.position.x;
                        float posthis = transform.position.x;
                        if (posEnemy >= posthis)
                        {
                            colx = 3f;
                        }
                        else
                        {
                            colx = -3f;
                        }
                        float dmg = damage / enemy.magic;
                        enemy.damaged(dmg, true, false, 3f, colx);
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
}
