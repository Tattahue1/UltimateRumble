using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Spell
{
    public CircleCollider2D collision;
    private float hp;
    public Transform sp;
    public float colx = 3f;

    void Start()
    {
        destroyable = false;
        Type = true;
        sp = Instantiate(sp, transform.position, par.transform.rotation);
        sp.transform.parent = transform;
        hp = par.hp2;
        damage = 3f;
        timeleft = 0.3f;
    }

    void Update()
    {
        transform.position = par.transform.position;
        if (par.hp2 != hp)
        {
            Destroy(gameObject);
        }
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
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
                bool aux = false;
                if (par.lordknightmon && par.buff)
                    aux = true;
                if (enemy.defended == false && enemy.defendedCrouch == false || aux)
                {
                    if (enemy.transform.position.x < transform.position.x)
                        colx = -colx;
                    float dmg = damage * par.physical / enemy.armor;
                    if (enemy.physicalReflect)
                    {
                        enemy.physicalCounter = dmg * 2f;
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
                        enemy.damaged(dmg, true, true, 0f, colx);
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
        if (par.buff)
        {
            Spell sedf = hitInfo.GetComponent<Spell>();
            if (sedf != null && sedf.destroyable && sedf.par != par)
            {
                sedf.destroy();
            }
        }
    }

    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}

