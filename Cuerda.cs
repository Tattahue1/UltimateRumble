using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuerda : MonoBehaviour
{
    public InfiniteHook hook;
    public float aux = 1f;
    float hp;
    public PhysicsObject par;
    // Start is called before the first frame update
    void Start()
    {
        hp = par.hp2;
    }

    // Update is called once per frame
    void Update()
    {
        if (par.hp2 != hp)
        {
            Destroy(gameObject);
        }
        if (hook != null)
        {
            transform.position = new Vector3(transform.position.x, hook.transform.position.y);
            if (hook.initSpeed > 0f)
            {
                transform.localScale = new Vector2(0.35f + 0.35f * 1.2f * (-hook.initPosition + hook.transform.position.x), 0.35f);
                transform.position = new Vector3(hook.initPosition + (-2f + 0.795f) * 1.2f * (-hook.initPosition + hook.transform.position.x) - 0.2f, transform.position.y, transform.position.z);

            }
            else
            { 
                transform.localScale = new Vector2(0.35f + 0.35f * 1.2f * (hook.initPosition - hook.transform.position.x), 0.35f);
                transform.position = new Vector3(hook.initPosition + (2f - 0.795f) * 1.2f * (hook.initPosition - hook.transform.position.x)+0.2f, transform.position.y, transform.position.z);

            }
        }
        else
            Destroy(gameObject);
        //2-0.795
    }
}
