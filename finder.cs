using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finder : MonoBehaviour
{
    public PhysicsObject par;
    public PhysicsObject enem = null;
    public HechizoFinal father;
    public NaturesAssist mother;
    float acceleration = 0f;
    float speed;
    public bool nature = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!nature)
            par = father.par;
        else
            par = mother.par;
    }

    // Update is called once per frame
    void Update()
    {
        if (!nature)
        {
            if (father != null)
                transform.position = father.transform.position;
            else
                Destroy(gameObject);
        }
        else
        {
            if(mother != null)
                transform.position = mother.transform.position;
            else
                Destroy(gameObject);
        }
        if (enem != null)
        {
            if (enem.transform.position.y >= transform.position.y && acceleration <= 10f)
            {
                acceleration += Time.deltaTime * 35f;
            }
            if (enem.transform.position.y < transform.position.y && acceleration >= -10f)
            {
                acceleration -= Time.deltaTime * 35f;
            }/*
            if (speed >= 0)
            {
                transform.eulerAngles = new Vector3(0f, 180f, -acceleration * 10f);
                if (acceleration >= 0)
                    speed = 10f - acceleration;
                else
                    speed = 10f + acceleration;
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, -acceleration * 10f);
                if (acceleration >= 0)
                    speed = -10f + acceleration;
                else
                    speed = -10f - acceleration;
            }*/
            if (enem.transform.position.x >= transform.position.x && speed <= 10f)
            {
                speed += Time.deltaTime * 35f;
            }
            if (enem.transform.position.x < transform.position.x && speed >= -10f)
            {
                speed -= Time.deltaTime * 35f;
            }
            if (!nature)
            {
                father.acc = acceleration;
                father.speed = speed;
            }
            else
            {
                mother.acceleration = acceleration/7f;
                mother.speed = speed/7f;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)// && enem != null)
        {
            enem = enemy;
        }
    }
    void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy == enem)
        {
            enem = null;
        }
    }
}
