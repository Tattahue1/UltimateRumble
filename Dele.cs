using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dele : MonoBehaviour
{
    public BoxCollider2D collision;

    public int parent;
    public float timeleft = 1f;
    public PhysicsObject par;
    public float colx = 0f;
    public float coly = 0f;

    void Start()
    {
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            Destroy(gameObject);
        }
        transform.position = par.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par && enemy.invulnerable == false)
        {
            if (enemy.physicalReflect)
            {
                enemy.physicalCounter = 5f * par.physical * 2f;
                if (par.transform.position.x > enemy.transform.position.x && !enemy.right)
                {
                    enemy.flip();
                }
                if (par.transform.position.x < enemy.transform.position.x && enemy.right)
                    enemy.flip();
                Destroy(gameObject);
            }
            else
            {
                par.leopardone = true;
                enemy.MusicSource.Stop();
                enemy.disable = true;
                enemy.gravityModifier = 0f;
                enemy.velocity.y = coly;
                if (par.right)
                    enemy.move.x = -colx;
                else
                    enemy.move.x = colx;
                enemy.animator.enabled = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy.GetInstanceID() != parent)
        {
            enemy.MusicSource.Play();
            enemy.gravityModifier = 1.5f;
            enemy.animator.enabled = true;
            Destroy(gameObject);
        }
    }

    public void setParent(int par)
    {
        parent = par;
    }
}
