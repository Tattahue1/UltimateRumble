using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter : MonoBehaviour
{
    public BoxCollider2D collision;

    public int parent;
    private float timeleft = 0.5f;
    private float damage;
    public PhysicsObject par;
    public float colx = 7f;
    public float coly = 7f;

    void Start()
    {
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
                if (enemy.invulnerable == false)
                {
                    damage = par.physicalCounter;
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
                        enemy.damaged(dmg, true, true, coly, colx);
                        par.addMana(dmg);
                    }
                }
            }
            else
            {
                Destroy(gameObject);
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

