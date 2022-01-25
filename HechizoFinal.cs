using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HechizoFinal : Spell
{
    public int parent;
    public float speed = -10f;
    public float acc = 0f;
    public finder finder;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
        Vector2 veler = rb.velocity;
        veler.x = speed;
        veler.y = acc;
        rb.velocity = veler;
    }
    // Use this for initialization
    void Start()
    {
        ultimate = true;
        destroyable = false;
        Type = true;
        destroyOnLeaveScreen = true;
        finder = Instantiate(finder, transform.position, transform.rotation);
        finder.father = this;
        damage = 35f;
        timeleft = 10f;
        Vector2 veler = rb.velocity;
        veler.x = speed;
        veler.y = acc;
        rb.velocity = veler;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.magicImmune == false)
        {
            if (enemy.invulnerable == false)
            {
                if (enemy.godsbless <= 0)
                {
                    float colx;
                    if (enemy.transform.position.x >= transform.position.x)
                        colx = 2f;
                    else
                        colx = -2f;
                    float dmg = damage * par.magic / enemy.magic;
                    enemy.damaged(dmg, true, false, 5f, colx);
                    Destroy(gameObject);
                }
                else
                {
                    destroy();
                    enemy.enemycounterspell = par;
                    enemy.blesshp -= damage * par.magic / enemy.magic;
                }
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