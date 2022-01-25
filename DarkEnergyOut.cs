using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnergyOut : Spell
{
    public CircleCollider2D collision;

    public int parent;
    private float hp;
    public bool state = false;

    private void Awake()
    {
        timeleft = 3f;
    }
    void Start()
    {
        timeleft = 3f;
        hp = par.hp2;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft <= 0)
        {
            Destroy(gameObject);
        }
        if (par.hp2 != hp)
        {
            timeleft = 0f;
            Destroy(gameObject);
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            bool right;
            if (enemy.transform.position.x >= transform.position.x)
                right = false;
            else
                right = true;
            enemy.darkp(right, timeleft);
        }
    }*/

    private void OnTriggerStay2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        bool auxd = false;
        if (enemy.statusResHero && enemy.buff)
        {
            auxd = true;
        }
        if (enemy != null && enemy != par && !auxd)
        {
            bool right;
            if (enemy.transform.position.x >= transform.position.x)
                right = false;
            else
                right = true;
            enemy.darkp(right, timeleft);
            enemy.absorbed = true;
            enemy.gameObject.layer = 11;
            float aux;
            float aux2;
            if (enemy.transform.position.x > transform.position.x)
                aux2 = 1f;
            else
                aux2 = -1f;
            float aux3;
            if (enemy.transform.position.y > transform.position.y)
                aux3 = 1f;
            else
                aux3 = -1f;
            aux = -0.3f;
            enemy.move.x = aux2 * aux;
            enemy.velocity.y = 5f*aux * aux3;
        }
    }

    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            enemy.absorbed = false;
            enemy.gameObject.layer = 1;
            enemy.externalGravity = false;
            enemy.maxSpeed = enemy.constMaxSpeed;
        }
    }

    public void setParent(int par)
    {
        parent = par;
    }

    public void setPar(PhysicsObject parent)
    {
        par = parent;
    }
}

