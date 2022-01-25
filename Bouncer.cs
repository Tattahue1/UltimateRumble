using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Spell
{
    public int parent;
    public reflecter refl;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            animator.SetTrigger("out");
        }
    }
    // Use this for initialization
    void Start()
    {
        destroyable = false;
        swappable = false;
        bouncable = true;
        timeleft = 2f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Spell enemy = hitInfo.GetComponent<Spell>();
        if (enemy != null && enemy.bouncable == true && enemy.par != par)
        {
            if (enemy.par.right)
            {
                if (enemy.rb.velocity.x <= 0f)
                    enemy.rb.velocity = new Vector2(-enemy.rb.velocity.x, enemy.rb.velocity.y);
                enemy.transform.eulerAngles = enemy.par.transform.eulerAngles;
                reflecter c = Instantiate(refl, enemy.par.transform.position + new Vector3(-3f, 0f, 0f), transform.rotation);
                c.transform.eulerAngles = new Vector3(0f, 0f, 90f);
                enemy.transform.position = enemy.par.transform.position + new Vector3(-3f, 0f, 0f);
            }
            else
            {
                if (enemy.rb.velocity.x >= 0f)
                    enemy.rb.velocity = new Vector2(-enemy.rb.velocity.x, enemy.rb.velocity.y);
                enemy.transform.eulerAngles = enemy.par.transform.eulerAngles;
                reflecter c = Instantiate(refl, enemy.par.transform.position + new Vector3(3f, 0f, 0f), transform.rotation);
                c.transform.eulerAngles = new Vector3(0f, 0f, -90f);
                enemy.transform.position = enemy.par.transform.position + new Vector3(3f, 0f, 0f);
            }
            enemy.swap = swap;
            enemy.par = par;
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