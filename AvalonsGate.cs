using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvalonsGate : Spell
{
    public int parent;
    public float speed = -5f;
//    bool explosive = false;
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
        ultimate = true;
        Type = false;
        damage = 40f;
        destroyable = false;
        timeleft = 23f;
        Vector2 veler;
        veler.x = speed;
        veler.y = -3f;
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
                    float colx = 4f;
                    Vector3 posEnemy = enemy.transform.position;
                    Vector3 posthis = transform.position;
                    if (posEnemy.x >= posthis.x)
                        colx = 5f;
                    else
                        colx = -5f;
                    float dmg = damage / enemy.magic;
                    enemy.damaged(dmg, true, false, 4f, colx);
                    //  explosive = true;
                    enemy.timerDamagedby = 3f;
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

    public void setParent(int par)
    {
        parent = par;
    }
    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}