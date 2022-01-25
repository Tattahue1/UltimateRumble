using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : Spell
{
    public BoxCollider2D collision;

    public int parent;

    void Start()
    {
        destroyable = false;
        bouncable = false;
        ultimate = true;
        Type = false;
        damage = 20f;
        destroyable = false;
        animator = GetComponent<Animator>();
        timeleft = 0.65f;
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
        if (enemy != null && enemy != par)
        {
            if (enemy.godsbless <= 0)
            {
                float dmg = damage * par.magic / enemy.magic;
                enemy.damaged(dmg, true, false, 0, 0);
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