using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinctionWave : Spell
{
    public int parent;
    public float speed = -10f;
    void Update()
    {
    }
    // Use this for initialization
    void Start()
    {
        ultimate = true;
        destroyOnLeaveScreen = true;
        Type = false;
        damage = 30f;
        destroyable = false;
        Vector2 veler = rb.velocity;
        veler.x = speed;
        veler.y = 0f;
        rb.velocity = veler;
        initVel = veler;
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.invulnerable == false && enemy.damagedby != GetInstanceID())
                {
                    float colx;
                    Vector3 posEnemy = enemy.transform.position;
                    Vector3 posthis = transform.position;
                    if (posEnemy.x >= posthis.x)
                        colx = 3f;
                    else
                        colx = -3f;
                    float dmg = damage / enemy.magic;
                    enemy.damaged(dmg, true, false, 3f, colx);
                    enemy.timerDamagedby = 2f;
                    enemy.damagedby = GetInstanceID();
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
    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            enemy.timerDamagedby = 0f;
        }
    }
}
