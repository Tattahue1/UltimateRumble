using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healinganim : Spell
{
    public int parent;
    public float speed = -20f;
    public float acc = 0f;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        bouncable = true;
        timeleft = 4f;
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
            if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
            {
                float dmg = damage / enemy.magic;
                enemy.damaged(dmg, false, false, 0, 0);
                par.addMana(dmg);
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