using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebreath : Spell
{
    public CapsuleCollider2D collision;
    public bool machine = false;
    public int parent;
    private float hp;
    public bool auto = false;

    void Update()
    {//1 0 1 0.4 - 0 0.25 3 1.5
        if (collision.size.x <= 3f)
        {
            float aux2 = 0.25f;
            if (machine)
                aux2 = -0.3f;
            collision.offset += new Vector2(-1f, aux2) * Time.deltaTime*5f;
            collision.size += new Vector2(2f, 1.1f) * Time.deltaTime*5f;
        }
        if (par.hp2 != hp)
            Destroy(gameObject);
        float aux;
        if (!machine)
        {
            if (par.right)
                aux = 3.35f;
            else
                aux = -3.35f;
        }
        else
        {
            if (par.right)
                aux = 3.3f;
            else
                aux = -3.3f;
        }
        float aux1 = 0.36f;
        if (machine)
            aux1 = 1.25f;
        if(!auto)
            transform.position = par.transform.position + new Vector3(aux, aux1);
        timeleft -= Time.deltaTime;
    }
    // Use this for initialization
    void Start()
    {
        float aux;
        if (!machine)
        {
            if (par.right)
                aux = 3.35f;
            else
                aux = -3.35f;
        }
        else
        {
            if (par.right)
                aux = 3.3f;
            else
                aux = -3.3f;
        }
        float aux1 = 0.36f;
        if (machine)
            aux1 = 1.25f;
        Type = false;
        bouncable = true;
        damage = 6f;
        destroyable = false;
        timeleft = 5f;
        hp = par.hp2;
        transform.position = par.transform.position + new Vector3(aux, aux1);
        if (machine)
        {
            transform.Rotate(0f, 0f, -25f);
        }
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.invulnerable == false && !enemy.defendedCrouch && enemy.damagedby != GetInstanceID())
                {
                    if (enemy.magicalReflect)
                    {
                        enemy.enemycounterspell = par;
                        enemy.magicalCounter = damage * 2f;
                        destroy();
                    }
                    else
                    {
                        float colx;
                        if (enemy.transform.position.x >= par.transform.position.x)
                            colx = 2f;
                        else
                            colx = -2f;
                        float dmg = damage * par.magic / enemy.magic;
                        enemy.damaged(dmg, true, false, 0f, colx);
                        par.addMana(dmg);
                        enemy.timerDamagedby = 0.5f;
                        enemy.damagedby = GetInstanceID();
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

    void destoy()
    {
        Destroy(gameObject);
    }
}