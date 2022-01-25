using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaBlaster : Spell
{
    public int parent;
    public Vector2 speed;
    public float gravity = 1.5f;

    void Update()
    {
        float x = (speed.y) / (Mathf.Abs(speed.x) + Mathf.Abs(speed.y)) * 90f;
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
        if (speed.x >= 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, -x);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, -x);
        }
        speed += 2f * Physics2D.gravity * Time.deltaTime * gravity;
        rb.velocity = new Vector2(speed.x, speed.y);
    }
    // Use this for initialization
    void Start()
    {
        ultimate = true;
        destroyable = false;
        Type = false;
        if (speed.x >= 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, -speed.y*2f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, -speed.y*2f);
        }
        destroyOnLeaveScreen = true;
        damage = 12f;
        timeleft = 10f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.invulnerable == false)
                {
                    float dmg = damage / enemy.magic;
                    enemy.damaged(dmg, false, false, 0f, 0f);
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

    public void setParent(int par)
    {
        parent = par;
    }
    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}