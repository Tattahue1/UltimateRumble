using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringProyectile : MonoBehaviour
{
    public PhysicsObject par;
    public float speed = -20f;
    public PuppetString ls;
    float timeleft;
    public Rigidbody2D rb;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        timeleft = 1f;
        rb.velocity = transform.right * speed * 1.2f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.invulnerable == false && enemy.magicImmune == false)
            {
                PuppetString lightning = Instantiate(ls, enemy.transform.position, transform.rotation);
                lightning.par = par;
                lightning.enemy = enemy;
                par.stringed = enemy;
                par.buff = true;
                Destroy(gameObject);
            }
        }
    }
}