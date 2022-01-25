using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdelanteQ : MonoBehaviour
{
    public BoxCollider2D collision;

    public int parent;
    private float timeleft = 0.2f;
    public float damage = 4f;
    public PhysicsObject par;
    public float colx = 2f;
    public float coly = 2f;
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
        if (enemy != null && enemy != par)
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
                if (enemy.transform.position.x < transform.position.x)
                    colx = -colx;
                float dmg = damage*par.physical/enemy.armor;
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
    }

    public void setParent(int par)
    {
        parent = par;
    }
}
