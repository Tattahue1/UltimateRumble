using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SevenHeavens : MonoBehaviour
{
    public LightOrb heaven;
    public float speed;
    public float acc;
    public PhysicsObject par;

    private Quaternion r;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        r.w = 0;
        r.x = 0;
        r.y = 0;
        r.z = 180f;
        pos.x = 0f;
        pos.z = 0f;
        for (int i = 0; i < 7; i++)
        {
            if(i < 2)
            {
                pos.x = -1f;
                if (i == 0)
                    pos.y = 0.5f;
                else
                    pos.y = -0.5f;
            }
            else if(i > 4)
            {
                pos.x = 1f;
                if (i == 5)
                    pos.y = 0.5f;
                else
                    pos.y = -0.5f;
            }
            else
            {
                pos.x = 0f;
                if (i == 2)
                    pos.y = 1f;
                if (i == 3)
                    pos.y = 0f;
                if (i == 4)
                    pos.y = -1f;
            }
            LightOrb diamond = Instantiate(heaven, transform.position + pos/1.3f, r);
            if (par.grounded)
            {
                diamond.speed = speed/1.2f - pos.x*0.8f;
                diamond.acc = acc/1.2f - pos.y * 0.6f;
            }
            else
            {
                diamond.speed = speed/1.2f - pos.x*0.6f;
                diamond.acc = acc/1.2f + pos.y * 0.06f*acc;
            }
            diamond.par = par;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
