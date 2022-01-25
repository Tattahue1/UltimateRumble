using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riopoderoso : Spell
{
    public int parent;
    float hp;

    void Update()
    {
        if (hp != par.hp2)
            Destroy(gameObject);
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
        transform.Rotate(0f, 0f, -30f * Time.deltaTime);
        transform.position += new Vector3(0f, -15f * Time.deltaTime, 0f);
    }
    // Use this for initialization
    void Start()
    {
        ultimate = true;
        hp = par.hp2;
        Type = false;
        if (par.right)
            transform.eulerAngles = new Vector3(0f, 0f, 12f);
        else
            transform.eulerAngles = new Vector3(0f, 180f, 12f);
        damage = 35f;
        timeleft = 0.8f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.godsbless <= 0)
            {
                float dmg = damage / enemy.magic;
                enemy.damaged(dmg, true, false, 0, 0);
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