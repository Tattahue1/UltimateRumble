using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starterAvalon : Spell
{
    float hp;
    void Start()
    {
        destroyOnLeaveScreen = true;
        destroyable = false;
        bouncable = false;
        ultimate = true;
        hp = par.hp2;
        rb.velocity = new Vector2(-3f, -0.9f);
        if (par.right)
            rb.velocity = new Vector2(3f, -0.9f);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp < par.hp2)
        {
            animator.SetTrigger("deste");
        }
    }
}
