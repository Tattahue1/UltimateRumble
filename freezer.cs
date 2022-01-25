using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezer : MonoBehaviour
{
    public PhysicsObject par;
    float timeleft = 4.6f;
    // Start is called before the first frame update
    void Start()
    {
        par.defended = false;
        par.invulnerable = false;
        par.disable = true;
        par.animator.enabled = false;
        par.move.x = 0f;
        par.jumpTakeOffSpeed = par.constJump;
    }

    // Update is called once per frame
    void Update()
    {
        par.defended = false;
        par.disable = true;
        par.animator.enabled = false;
        timeleft -= Time.deltaTime;
        transform.position = par.transform.position;
        if(timeleft <= 0f)
        {
            par.animator.speed = 1f;
            par.jumpTakeOffSpeed = par.constJump;
            par.maxSpeed = par.constMaxSpeed;
            par.animator.enabled = true;
            par.frozen = false;
            Destroy(gameObject);
        }
    }
}
