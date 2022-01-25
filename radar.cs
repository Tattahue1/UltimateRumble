using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radar : MonoBehaviour
{
    public PhysicsObject par;
    public divermon parent;
    public BoxCollider2D col;
    public Vector2 aux;
//    private float jumpTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        par = parent.par;
        aux = col.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {/*
        if (enemy.transform.position.x > transform.position.x)
            parent.speed.x = 5f;
        else
            parent.speed.x = -5f;/*
        if (enemy.transform.position.y > transform.position.y)
        {
            if (parent.speed.x == 0f)
            {
                parent.jump();
            }
        }
        if (enemy.transform.position.y <= transform.position.y)
            parent.godown();*/
        transform.position = parent.transform.position;
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.physicalImmune == false)
        {
            if (enemy.invulnerable == false && enemy.defended == false && enemy.defendedCrouch == false)
            {/*
                if (enemy.transform.position.x > transform.position.x && !parent.right)
                {
                    parent.flip();
                    parent.speed.x = 5f;
                }
                if (parent.transform.position.x < transform.position.x && parent.right)
                {
                    parent.flip();
                    parent.speed.x = -5f;
                }*/
                parent.animator.SetTrigger("attack");
            }
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.physicalImmune == false)
        {
//            col.transform.localScale = aux;
        }
    }*/
}
