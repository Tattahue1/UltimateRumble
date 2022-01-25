using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public BoxCollider2D collision;

    public int parent;
    public float timeleft = 0.1f;
    public float damage = 3f;
    public PhysicsObject par;
    public bool dramonMult = false;
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
                        damage = damage + enemy.stingmonStacks;
                        enemy.stingmonStacks += 1;
                        enemy.stingmonStacksDuration = 5f;
                    }
                    if (enemy.dramon && dramonMult)
                    {
                        damage = damage * 2f;
                    }
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
                        enemy.damaged(dmg, false, true, 0, 0);
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
