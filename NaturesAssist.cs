using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturesAssist : Spell
{
    public int parent;
    private float timestep = 2f;
    private bool activate = false;
    public float acceleration = 0f;
    public float speed = 0f;
    public finder finder;
    bool aux = false;
    bool aux2 = false;
    bool deacti = false;

    void Update()
    {
        if (timeleft <= 15f && !aux2)
        {
            aux = true;
            xdero(aux);
        }
        timestep -= Time.deltaTime;
        if (timestep <= 0f)
        {
            activate = true;
        }
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            animator.SetTrigger("destroy");
        }
        Vector2 veler;
        veler.x = speed;
        veler.y = acceleration;
        rb.velocity = veler;
        if (finder.enem != null)
        {
            float x = (acceleration) / (Mathf.Abs(speed) + Mathf.Abs(acceleration)) * 90f;
            if (speed < 0)
            {
                transform.eulerAngles = new Vector3(0f, 180f, -x);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, -x);
            }
        }
    }
    // Use this for initialization
    void Start()
    {
        destroyable = false;
        bouncable = true;
        Type = false;
        damage = 4f;
        timeleft = 25f;
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        if (activate)
        {
            PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
            if (enemy != null && enemy != par && enemy.magicImmune == false)
            {
                if (enemy.godsbless <= 0)
                {
                    if (enemy.magicalReflect)
                    {
                        enemy.enemycounterspell = par;
                        enemy.magicalCounter = damage * 2f;
                    }
                    else
                    {
                        if (enemy.defended == false && enemy.defendedCrouch == false && enemy.invulnerable == false)
                        {
                            float colxe;
                            Vector3 posEnemy = enemy.transform.position;
                            Vector3 posthis = transform.position;
                            if (posEnemy.x >= posthis.x)
                                colxe = 3f;
                            else
                                colxe = -3f;
                            animator.SetTrigger("destroy");
                            damage = damage + enemy.stingmonStacks * 1;
                            float dmg = damage / enemy.magic;
                            enemy.damaged(dmg, true, false, 1f, colxe);
                            enemy.stingmonStacksDuration = 5f;
                            enemy.stingmonStacks += 1;
                            par.addMana(dmg);
                        }
                    }
                }
                else
                {
                    animator.SetTrigger("destroy");
                    enemy.enemycounterspell = par;
                    enemy.blesshp -= damage * par.magic / enemy.magic;
                }
            }

            Spell sedf = hitInfo.GetComponent<Spell>();
            if (sedf != null && sedf.destroyable == false && sedf.par != par)
            {
                animator.SetTrigger("destroy");
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

    void delete()
    {
        Destroy(gameObject);
    }
    void deactivate()
    {
        deacti = true;
        GetComponent<CapsuleCollider2D>().size = new Vector2(0f, 0f);
    }
    
    void xdero(bool xd)
    {
        if(xd)
        {
            finder = Instantiate(finder, transform.position, transform.rotation);
            finder.mother = this;
            finder.nature = true;
            finder.transform.localScale = new Vector3(3f, 3f, 1f);
        }
        aux2 = true;
    }
}