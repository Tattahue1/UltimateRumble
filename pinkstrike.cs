using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinkstrike : Spell
{
    public PhysicsObject enemy;
    public bool godsbles = false;
    public float auxilio = 15.8f;
    public bool estetica = false;

    void Start()
    {
        if (godsbles && !estetica)
            auxilio = 7f;
        Type = false;
        destroyable = false;
        damage = 5f;
        transform.position = enemy.transform.position + new Vector3(0f, auxilio);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.transform.position + new Vector3(0f, auxilio);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (!godsbles)
            {
                if (enemy.magicalReflect)
                {
                    enemy.enemycounterspell = par;
                    enemy.magicalCounter = damage * 2f;
                }
                else
                {
                    if (!enemy.invulnerable)
                    {
                        float dmg = damage * par.magic / enemy.magic;
                        enemy.damaged(dmg, true, false, 0, 0);
                    }
                }
            }
            else
            {
                if (!enemy.invulnerable)
                {
                    float dmg = 10f * par.magic / enemy.magic;
                    enemy.damaged(dmg, true, false, 0, 0);
                }
            }
        }
    }
}
