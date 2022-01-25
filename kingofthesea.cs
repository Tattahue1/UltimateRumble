using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingofthesea : MonoBehaviour
{
    public BoxCollider2D boxcollider;
    private float timeleft = 20f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        if(timeleft <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy.seadramon)
        {
            enemy.kingofthesea = true;
            enemy.velocity.y = 0f;
            enemy.constMaxSpeed = 16f;
            enemy.maxSpeed = 16f;
        }
        divermon d = hitInfo.GetComponent<divermon>();
        if (d != null)
        {
            d.swimmer = true;
            d.timeleft += 20f;
        }
    }
    private void OnTriggerStay2D(Collider2D hitInfo)
    {
        divermon enemy = hitInfo.GetComponent<divermon>();
        if (enemy != null)
        {
            enemy.swimmer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy.seadramon)
        {
            enemy.kingofthesea = false;
            enemy.gravityModifier = 1.5f;
            enemy.maxSpeed = 8f;
            enemy.gameObject.layer = 1;
            enemy.constMaxSpeed = 8f;
        }
        divermon diver = hitInfo.GetComponent<divermon>();
        if (diver != null)
        {
            diver.swimmer = false;
        }
    }
}
