using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparky : Spell
{
    public int parent;
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
        Type = false;
        damage = 0f;
        bouncable = true;
        timeleft = 0.4f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.invulnerable == false && enemy.magicImmune == false)
            {
                enemy.damaged(0f, false, false, 0f, 0f);
            }
        }
    }
 
    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}