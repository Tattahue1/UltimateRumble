using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bifrost : Spell
{
    public int parent;
    public float speed = -20f;
    float acceleration = 5f;
    public float factor;
    
    void Update()
    {
        float x = (acceleration) / (Mathf.Abs(speed) + Mathf.Abs(acceleration)) * 90f;
        acceleration -= Time.deltaTime * factor;
        if (speed >= 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, -x);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, -x);
        }
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
        rb.velocity = new Vector2(speed, acceleration);
    }
    // Use this for initialization
    void Start()
    {
        destroyable = false;
        Type = false;
        damage = 3f;
        bouncable = true;
        timeleft = 8f;
        rb.velocity = new Vector2(speed*1.3f, acceleration);
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
                        if (enemy.transform.position.x >= transform.position.x)
                            colx = 1f;
                        else
                            colx = -1f;
                        float dmg = damage * par.magic / enemy.magic;
                        enemy.damaged(dmg, true, false, 2f, colx);
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

    public void setParent(int par)
    {
        parent = par;
    }
    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}