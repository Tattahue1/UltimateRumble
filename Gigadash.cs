using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gigadash : Spell
{
    public BoxCollider2D collision;
    
    void Start()
    {
        Type = true;
        damage = 6f;
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
        if (enemy != null && enemy != par && enemy.invulnerable == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false)
                {
                    float colx;
                    if (enemy.transform.position.x >= transform.position.x)
                        colx = 2f;
                    else
                        colx = -2f;
                    float dmg = damage * par.physical / enemy.armor;
                    if (enemy.magicalReflect)
                    {
                        enemy.magicalCounter = dmg * 2f;
                        if (par.transform.position.x > enemy.transform.position.x && !enemy.right)
                        {
                            enemy.flip();
                        }
                        if (par.transform.position.x < enemy.transform.position.x && enemy.right)
                            enemy.flip();
                        Destroy(gameObject);
                    }
                    else
                    {
                        enemy.damaged(dmg, true, true, 6f, colx * 2f);
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
