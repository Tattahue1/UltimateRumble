using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraniumSpear : Spell
{
    public int parent;
    public float speed = -20f;

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
        destroyable = false;
        bouncable = false;
        Type = false;
        damage = 5f;
        timeleft = 1f;
        Vector2 veler;
        veler.x = speed;
        veler.y = 0f;
        rb.velocity = veler;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.invulnerable == false)
        {
            if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
            {
                if (enemy.magicalReflect)
                {
                    enemy.enemycounterspell = par;
                    enemy.magicalCounter = damage * 2f;
                    destroy();
                }
                else
                {
                    enemy.animator.enabled = false;
                    enemy.disable = true;
                    enemy.absorbed = true;
                    enemy.move.x = speed / 7.2f;
                    enemy.gravityModifier = 0f;
                    enemy.velocity.y = 0f;
                    float dmg = damage * par.magic / enemy.magic;
                    enemy.hp2 -= dmg;
                    par.addMana(dmg);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.invulnerable == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false && !enemy.magicalReflect)
                {
                    enemy.damaged(0f, false, false, 0f, 0f);
                    enemy.absorbed = false;
                    enemy.gravityModifier = 1.5f;
                    enemy.animator.enabled = true;
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
    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}