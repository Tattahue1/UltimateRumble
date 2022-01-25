using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilAntenna : Spell
{
    public CircleCollider2D collision;

    public int parent;
    private float hp;

    void Update()
    {
        if (par.hp2 != hp && par.buff == false)
            Destroy(gameObject);
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
        }
    }
    // Use this for initialization
    void Start()
    {
        ultimate = true;
        Type = false;
        damage = 27f;
        destroyable = false;
        timeleft = 2.4f;
        hp = par.hp2;
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy.GetInstanceID() != parent)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.invulnerable == false)
                {
                    enemy.disable = true;
                    enemy.animator.enabled = false;
                    enemy.gravityModifier = 0f;
                    enemy.move.x = 0f;
                    enemy.velocity.y = 0f;
                    enemy.stingmonStacksDuration += 3f;
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

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
            {
                float colx;
                if (enemy.transform.position.x >= par.transform.position.x)
                    colx = 2f;
                else
                    colx = -2f;
                damage = damage + enemy.stingmonStacks*5f;
                enemy.animator.enabled = true;
                enemy.stingmonStacks = 0;
                float dmg = damage/enemy.magic;
//                enemy.velocity.x = colx/2f;
//                enemy.velocity.y = 13f;
                enemy.damaged(dmg, true, false, 5f, colx*2f);
                par.addHealth(dmg/1.3f);
                enemy.gravityModifier = 1.5f;
             //   enemy.disable = false;
             //   enemy.animator.enabled = true;
            }
        }
    }

    public void setParent(int par)
    {
        parent = par;
    }

    void destoy()
    {
        Destroy(gameObject);
    }
}