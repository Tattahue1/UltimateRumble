using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterHit : MonoBehaviour
{
    public BoxCollider2D collision;

    public int parent;
    public float timeleft = 0.1f;
    private float damage = 3;
    public PhysicsObject par;
    public bool dramonMult = false;
    public bool stingmonStack = false;
    public float coly = 2f;
    public float colx = 2f;

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
        if (enemy != null && enemy != par && enemy.invulnerable == false)
        {
            if (enemy.godsbless <= 0)
            {
                bool aux = false;
                bool aux2 = false;
                if (par.lordknightmon && par.buff)
                    aux = true;
                if (!enemy.defended)
                    aux2 = true;
                if (aux2 || aux)
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
                    float colx2 = 2f;
                    Vector3 posEnemy = enemy.transform.position;
                    Vector3 posthis = transform.position;
                    if (posEnemy.x >= posthis.x)
                        colx2 = colx;
                    else
                        colx2 = -colx;
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
                        enemy.damaged(dmg, true, true, coly, colx2);
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
