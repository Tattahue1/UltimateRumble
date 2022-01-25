using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikingStriker : Spell
{
    public BoxCollider2D collision;

    public int parent;
    private float hp;

    void Start()
    {
        destroyable = false;
        Type = true;
        timeleft = 0.38f;
        hp = par.hp2;
        damage = 5f;
    }

    void Update()
    {
        if (par.hp2 != hp)
            Destroy(gameObject);
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.physicalImmune == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
                {
                    damage = damage * par.physical + enemy.stingmonStacks * 2;
                    float dmg = damage / enemy.armor;
                    if (enemy.physicalReflect)
                    {
                        enemy.physicalCounter = dmg * 2f;
                        if (par.transform.position.x > enemy.transform.position.x && !enemy.right)
                            enemy.flip();
                        if (par.transform.position.x < enemy.transform.position.x && enemy.right)
                            enemy.flip();
                        Destroy(gameObject);
                    }
                    else
                    {
                        Vector3 posEnemy = enemy.transform.position;
                        Vector3 posthis = transform.position;
                        enemy.damaged(dmg, true, true, 0f, 0f);
                        enemy.stingmonStacks += 1;
                        enemy.stingmonStacksDuration = 5f;
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

    public void setParent(int par)
    {
        parent = par;
    }
}
