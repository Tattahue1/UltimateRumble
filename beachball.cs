using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beachball : Spell
{
    public int parent;
    public float speed = -3f;
    public CircleCollider2D circ;

    void Update()
    {
        transform.Rotate(0f, 0f, -rb.velocity.x*Time.deltaTime*100f);
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
        damage = 4f;
        timeleft = 20f;
        Vector2 veler;
        veler.x = speed;
        veler.y = 0f;
        rb.velocity = veler;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.magicImmune == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
                {
                    if (enemy.magicalReflect)
                    {
                        enemy.enemycounterspell = par;
                        enemy.magicalCounter = damage * 2f;
                    }
                    else
                    {
                        float colx;
                        float posEnemy = enemy.transform.position.x;
                        float posthis = transform.position.x;
                        if (posEnemy >= posthis)
                        {
                            colx = 3f;
                            rb.velocity = new Vector2(-5f, rb.velocity.y);
                        }
                        else
                        {
                            colx = -3f;
                            rb.velocity = new Vector2(5f, rb.velocity.y);
                        }
                        float dmg = damage / enemy.magic;
                        enemy.damaged(dmg, true, false, 3f, colx);
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
        Hit hit = hitInfo.GetComponent<Hit>();
        if (hit != null)
        {
            Vector2 aux;
            aux.y = rb.velocity.y;
            if (hit.transform.position.x >= transform.position.x)
            {
                aux.x = rb.velocity.x - 3f;
            }
            else
                aux.x = rb.velocity.x + 3f;
            rb.velocity = aux;
        }
        GreaterHit ghit = hitInfo.GetComponent<GreaterHit>();
        if (ghit != null)
        {
            Vector2 aux;
            aux.y = rb.velocity.y;
            if (ghit.transform.position.x >= transform.position.x)
            {
                aux.x = rb.velocity.x - 5f;
            }
            else
                aux.x = rb.velocity.x + 5f;
            rb.velocity = aux;
        }
        CrouchHit chit = hitInfo.GetComponent<CrouchHit>();
        if (chit != null)
        {
            Vector2 aux;
            aux.y = rb.velocity.y;
            if (chit.transform.position.x >= transform.position.x)
            {
                aux.x = rb.velocity.x - 4f;
            }
            else
                aux.x = rb.velocity.x + 4f;
            rb.velocity = aux;
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