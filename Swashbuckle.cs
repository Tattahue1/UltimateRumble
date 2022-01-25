using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swashbuckle : Spell
{
    public int parent;
    public bool exploder = false;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0 && !exploder)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        timeleft = 0.02f;
        Type = true;
        damage = 0.5f;
        bouncable = true;
        if (exploder)
        {
            Type = false;
            bouncable = false;
            destroyable = false;
            damage = 5f;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.magicImmune == false && enemy.invulnerable == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
                {
                    damage = damage * par.physical + enemy.stingmonStacks * 2;
                    float dmg = damage*par.physical/ enemy.armor;
                    if (enemy.physicalReflect)
                    {
                        enemy.physicalCounter = dmg * 2f;
                        if (par.transform.position.x > enemy.transform.position.x && !enemy.right)
                            enemy.flip();
                        if (par.transform.position.x < enemy.transform.position.x && enemy.right)
                            enemy.flip();
                        Destroy(gameObject);
                    }
                    else
                    {
                        bool aux = false;
                        Vector2 auxd = new Vector2(0f,0f);
                        if (exploder)
                        {
                            float colx;
                            if (enemy.transform.position.x >= par.transform.position.x)
                                colx = 1f;
                            else
                                colx = -1f;
                            auxd.y = 3f;
                            auxd.x = 5f*colx;
                            aux = true;
                        }
                        enemy.damaged(dmg, aux, true, auxd.y, auxd.x);
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
}
