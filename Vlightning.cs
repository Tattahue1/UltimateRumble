using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vlightning : Spell
{
    public int parent;
    public float speed = -20f;
    public spark ls;

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
        Type = false;
        damage = 4.5f;
        bouncable = true;
        timeleft = 8f;
        rb.velocity = transform.right * speed*1.2f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.magicalReflect)
                {
                    enemy.magicalCounter = damage * 2f;
                    enemy.enemycounterspell = par;
                }
                else
                {
                    if (enemy.invulnerable == false && enemy.magicImmune == false)
                    {
                        if (enemy.defended == false && enemy.defendedCrouch == false)
                        {
                            float dmg = damage / enemy.magic;
                            enemy.damaged(dmg, false, false, 0, 0);
                            par.addMana(dmg);
                        }
                        spark lightning = Instantiate(ls, enemy.transform.position, transform.rotation);
                        lightning.setParent(parent);
                        lightning.par = par;
                        lightning.enemy = enemy;
                        Destroy(gameObject);
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
    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}