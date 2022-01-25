using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaClawBlue : Spell
{
    public int parent;
    public float speed = -4.5f;
    public bool dramonMult = false;
    bool stopTransform = false;
    void Update()
    {
        transform.GetComponent<SpriteRenderer>().color -= new Color(0f, 0f, 0f, 0.01f);
        if (!stopTransform)
            transform.localScale += new Vector3(5f, 5f, 0f)*Time.deltaTime;
        if (transform.localScale.x >= 2f)
            stopTransform = true;
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {//0.5-1.5
        destroyable = false;
        Type = true;
        damage = 3f;
        bouncable = true;
        timeleft = 1f;
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        Vector2 veler = rb.velocity;
        veler.x = speed;
        veler.y = 0;
        rb.velocity = veler * 0.8f;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.physicalImmune == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
                {
                    if (enemy.dramon)
                        damage = damage * 1.5f;
                    if (dramonMult)
                        damage = damage * 1.5f;
                    float colx = 1f;
                    Vector3 posEnemy = enemy.transform.position;
                    Vector3 posthis = transform.position;
                    if (posEnemy.x >= posthis.x)
                        colx = 2f;
                    else
                        colx = -2f;
                    float dmg = damage * par.physical / enemy.armor;
                    if (enemy.magicalReflect)
                    {
                        enemy.enemycounterspell = par;
                        enemy.magicalCounter = damage * 2f;
                    }
                    else
                    {
                        enemy.damaged(dmg, true, true, 4f, colx);
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
    public void destr()
    {
        Destroy(gameObject);
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