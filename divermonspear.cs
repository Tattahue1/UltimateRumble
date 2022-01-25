using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class divermonspear : Spell
{
    public Vector2 speed;
    public float gravity = 1.5f;
    public divermon parent;
    private bool left;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
        speed += Physics2D.gravity * Time.deltaTime * gravity;
        rb.velocity = new Vector2(speed.x / 1.3f, speed.y);
        if (left)
            transform.eulerAngles = new Vector3(0f, 0f, -speed.y * 6f);
        else
            transform.eulerAngles = new Vector3(0f, 180f, -speed.y * 6f);
    }
    // Use this for initialization
    void Start()
    {
        Type = true;
        damage = 2f;
        speed.y = 2.5f;
        if (parent.right)
        {
            left = false;
            speed.x = 10f;
        }
        else
        {
            left = true;
            speed.x = -10f;
        }
        bouncable = true;
        timeleft = 4f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.physicalImmune == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.magicalReflect)
                {
                    enemy.magicalCounter = damage * 2f;
                }
                else
                {
                    if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false && !enemy.physicalReflect)
                    {
                        float dmg = (damage + enemy.divermonStacks / 2) / enemy.armor;
                        if (enemy.divermonStacks >= 3)
                        {
                            enemy.damaged(dmg, true, true, 0, 0);
                            enemy.divermonStacks = -1;
                        }
                        else
                            enemy.damaged(dmg, false, true, 0, 0);
                        par.addMana(dmg);
                        enemy.divermonStacks += 1;
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
    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}