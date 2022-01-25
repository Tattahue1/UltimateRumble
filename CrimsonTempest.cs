using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimsonTempest : Spell
{
    public diamondstorm crimson;

    public int parent;
    private Quaternion r;
    private Vector3 pos;
    public float speed;
    public float acceleration = 0f;
    public float cd = 0f;
    public bool puppet = false;
    public int cont = 0;

    // Start is called before the first frame update
    void Start()
    {
        r.w = 0;
        r.x = 0;
        r.y = 0;
        r.z = 180f;
        pos.x = 0f;
        pos.z = 0f;
        timeleft = 1f;
        if (par.lordknightmon)
        {
            for (int i = 0; i < 40; i++)
            {
                pos.y = (i - 20) / 20f;
                pos.x = pos.x = Random.Range(-1f, 1f);
                diamondstorm diamond = Instantiate(crimson, transform.position + pos, r);
                diamond.speed = speed;
                if (par.lordknightmon)
                    diamond.acc = acceleration + (i - 20) / 15f;
                diamond.parent = parent;
                diamond.par = par;
                if (par.buff)
                {
                    diamond.damage = 1.25f;
                }
            }
            Destroy(gameObject);
        }
        else
        {
            puppet = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        if(puppet)
        {
            if (cont <= 6)
            {
                if (cd <= 0f)
                {
                    pos.y = (cont - 1) / 10f;
                    diamondstorm diamond = Instantiate(crimson, transform.position + pos, r);
                    diamond.speed = speed;
                    diamond.parent = parent;
                    diamond.par = par;
                    diamond.damage = 2f;
                    diamond.acc = acceleration + (cont - 3)*2f;
                    cd = 0.05f;
                    cont += 1;
                }
            }
            else
                Destroy(gameObject);
        }
    }

    public void setParent(int par)
    {
        parent = par;
    }
}
