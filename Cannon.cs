using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Machinedramon par;
    public Transform firepoint;
    public float hp;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 auxil;
        auxil.z = 0f;
        if (par.right)
            auxil.x = 1.306f;
        else
            auxil.x = -1.306f;
        auxil.y = 0.444f;
        transform.position = new Vector3(par.transform.position.x, transform.position.y, 0f) + auxil;
        par.ultFirepoint = firepoint;
        hp = par.hp2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!par.enableUlt)
            Destroy(gameObject);
        float rot;
        Vector3 auxil;
        rot = transform.rotation.z;
        auxil.z = 0f;
        auxil.x = -1.306f;
        if (par.right)
        {
            rot = -transform.rotation.z;
            auxil.x = 1.306f;
        }
        else
        {
            rot = transform.rotation.z;
            auxil.x = -1.306f;
        }
        auxil.y = 0.444f;
        if (hp != par.hp2)
        {
            Destroy(gameObject);
        }
        if(par.right)
            transform.eulerAngles = new Vector3(0f, 0f, par.aim - 40f);
        else
            transform.eulerAngles = new Vector3(0f, 180f, par.aim - 40f);
        if (transform.rotation.z > 0f)
        {
            if(par.right)
                transform.position = new Vector3(par.transform.position.x +rot, par.transform.position.y+ transform.rotation.z, 0f) + auxil;
            else
                transform.position = new Vector3(par.transform.position.x - rot, par.transform.position.y - transform.rotation.z, 0f) + auxil;
        }
        else
        {
            if (par.right)
                transform.position = new Vector3(par.transform.position.x + rot, par.transform.position.y + transform.rotation.z, 0f) + auxil;
            else
                transform.position = new Vector3(par.transform.position.x - rot, par.transform.position.y - transform.rotation.z, 0f) + auxil;
        }
    }
}
