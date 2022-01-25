using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spark : Spell
{
    public BoxCollider2D collision;
    public sparky ls;
    public PhysicsObject enemy;

    public int parent;

    // Start is called before the first frame update
    void Start()
    {
        Type = false;
        bouncable = true;
        timeleft = 1.5f;
        timestrike = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.transform.position;
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
        if (timestrike <= 0)
        {
            sparky lightning = Instantiate(ls, transform.position, transform.rotation);
            timestrike = 0.5f;
            lightning.par = par;
        }
        else
        {
            timestrike -= Time.deltaTime;
        }

    }
    public void setParent(int par)
    {
        parent = par;
    }
}