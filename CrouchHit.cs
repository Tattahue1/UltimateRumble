using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchHit : MonoBehaviour
{
    public BoxCollider2D collision;

    public int parent;
    private float timeleft = 0.1f;
    private float damage = 5;
    public bool dramonMult = false;
    public PhysicsObject par;
    public bool stingmonStack = false;

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
                    if (stingmonStack)
                    {
                        damage = damage + enemy.stingmonStacks * 1.5f;
                        enemy.stingmonStacks += 1;
                        enemy.stingmonStacksDuration = 5f;
                    }
                    if (enemy.dramon && dramonMult)
                    {
                        damage = damage * 2f;
                    }
                    float colx = 2f;
                    Vector3 posEnemy = enemy.transform.position;
                    Vector3 posthis = transform.position;
                    if (posEnemy.x >= posthis.x)
                        colx = 2f;
                    else
                        colx = -2f;
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
                        enemy.damaged(dmg, true, true, 2, colx);
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
