using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaInForce : MonoBehaviour
{
    public BoxCollider2D collision;

    public int parent;
    private float timeleft = 6.5f;
    public PhysicsObject par;
    private float hp;
    Vector2 vel;
    Vector2 vel2;

    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            par.constMaxSpeed = 6.3f;
            par.jumpTakeOffSpeed = 12.5f;
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        level.alpha();
        hp = par.hp2;
        par.constMaxSpeed = 7.5f;
        par.jumpTakeOffSpeed = 14f;
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        Stunner stun = hitInfo.GetComponent<Stunner>();
        if (stun != null)
        {
            stun.timeleft += 5f;
        }
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            enemy.invulnerable = false;
            enemy.defended = false;
            enemy.defendedCrouch = false;
            enemy.alphed = true;
            enemy.ultbuff += 5f;
            enemy.buffcooldown += 5f;
            enemy.buffDuration += 5f;
            enemy.MusicSource.Stop();
            enemy.aura.Pause();
            Vector2 vel0;
            vel0.x = vel0.y = 0f;
            enemy.disable = true;
            enemy.animator.enabled = false;
            enemy.gravityModifier = 0f;
            enemy.velocity.y = 0f;
            enemy.initVel.x = enemy.move.x;
            enemy.initVel.y = enemy.velocity.y;
            enemy.move.x = 0f;
            enemy.velocity.x = 0f;
            enemy.rb2d.velocity = vel0;
        }
        Spell b = hitInfo.GetComponent<Spell>();
        if (b != null && b.GetComponent<SoulDigitalization>() == false)
        {
            b.timeStopped = true;
            Vector2 vel0;
            vel0.x = vel0.y = 0f;
            b.animator.enabled = false;
            b.alpha();
            b.initVel = b.rb.velocity;
            b.rb.velocity = vel0;
        }
        testament test = hitInfo.GetComponent<testament>();
        if(test != null)
        {
            test.stopping = true;
        }
        divermon d = hitInfo.GetComponent<divermon>();
        if(d != null)
        {
            d.animator.enabled = false;
            d.gravity = 0f;
            d.speed.x = d.speed.y = 0f;
            d.rb.velocity = new Vector2(0f, 0f);
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            enemy.alphed = false;
            enemy.MusicSource.Play();
            if (enemy.buff)
                enemy.aura.Play();
            enemy.animator.enabled = true;
            enemy.gravityModifier = 1.5f;
            enemy.move.x = enemy.initVel.x;
            enemy.velocity.y = enemy.initVel.y;
        }
        Spell b = hitInfo.GetComponent<Spell>();
        if (b != null)
        {
            b.timeStopped = false;
            b.animator.enabled = true;
            b.rb.velocity = b.initVel;
        }
        testament test = hitInfo.GetComponent<testament>();
        if (test != null)
        {
            test.stopping = false;
        }
        divermon d = hitInfo.GetComponent<divermon>();
        if (d != null)
        {
            d.animator.enabled = true;
            d.gravity = 1.5f;
            if (d.right)
                d.speed.x = 5f;
            else
                d.speed.x = -5f;
        }
    }

    public void setParent(int par)
    {
        parent = par;
    }
}
