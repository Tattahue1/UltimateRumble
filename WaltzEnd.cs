using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaltzEnd : Spell
{
    public BoxCollider2D collision;
    private float hp;
    public float colx = 1f;
    public float coly = 0f;

    void Start()
    {
        destroyable = false;
        Type = false;
        hp = par.hp2;
        damage = 5f;
    }

    void Update()
    {
        transform.position = par.transform.position;
        if (par.hp2 != hp)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.invulnerable == false && enemy.magicImmune == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false)
                {
                    if (enemy.transform.position.x < transform.position.x)
                        colx = -colx;
                    float dmg = damage * par.magical / enemy.magical;
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
                        enemy.damaged(dmg, true, true, coly, colx);
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

    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}
