using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezebreath : Spell
{
    public freezer freeze;
    public float hp;
    // Start is called before the first frame update
    void Start()
    {
        float aux;
        if (par.right)
            aux = 3.1f;
        else
            aux = -3.1f;
        float aux1 = 0.26f;
        transform.position = par.transform.position + new Vector3(aux, aux1);
        hp = par.hp2;
        destroyable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (par.hp2 != hp)
            Destroy(gameObject);
        float aux;
        if (par.right)
            aux = 3.1f;
        else
            aux = -3.1f;
        float aux1 = 0.26f;
        transform.position = par.transform.position + new Vector3(aux, aux1);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.magicImmune == false)
        {
            if (enemy.godsbless <= 0)
            {
                if (enemy.invulnerable == false)
                {
                    if (enemy.magicalReflect)
                    {
                        enemy.enemycounterspell = par;
                        enemy.magicalCounter = 2f * 2f;
                        destroy();
                    }
                    else
                    {
                        freezer hit_1 = Instantiate(freeze, enemy.transform.position, enemy.transform.rotation);
                        hit_1.par = enemy;
                        enemy.frozen = true;
                    }
                }
            }

            else
            {
                destroy();
                enemy.enemycounterspell = par;
                enemy.blesshp -= 2f * par.magic / enemy.magic;
            }
        }
    }
}
