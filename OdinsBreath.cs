using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinsBreath : Spell
{
    SpriteRenderer spriteRenderer;
    public freezer freeze;
    ParticleSystem particlesystem;
    // Start is called before the first frame update
    void Start()
    {
        ultimate = true;
        destroyable = false;
        Type = false;
        bouncable = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        particlesystem = GetComponent<ParticleSystem>();
        timeleft = 6.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft <= 0f)
        {
            spriteRenderer.color -= new Color(0f, 0f, 0f, Time.deltaTime * 1f);
            particlesystem.Stop();
        }
        if (spriteRenderer.color.a < 0.6f && timeleft > 0)
            spriteRenderer.color += new Color(0f, 0f, 0f, Time.deltaTime * 2f);
        //     if (spriteRenderer.color.a <= 0 && timeleft <= 0)
        //            destroy();
    }
    void OnTriggerStay2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.invulnerable == false && enemy.hp2 > 0)
            {
                if(enemy.godsbless <= 0)
                    enemy.hp2 -= 3f * Time.deltaTime;
                else
                {
                    enemy.enemycounterspell = par;
                    enemy.blesshp -= 3f * Time.deltaTime * par.magic / enemy.magic;
                }
                par.hp2 += 2f * Time.deltaTime;
                if(enemy.animator.speed >= 0)
                    enemy.animator.speed -= Time.deltaTime * 0.5f;
                if(enemy.maxSpeed >= 0)
                    enemy.maxSpeed -= enemy.constMaxSpeed* Time.deltaTime*0.8f;
                if(enemy.jumpTakeOffSpeed >= 0)
                    enemy.jumpTakeOffSpeed -= enemy.constJump * Time.deltaTime *0.35f;
                if(enemy.maxSpeed <= 0f && !enemy.frozen)
                {
                    enemy.animator.speed = 1f;
                    freezer hit_1 = Instantiate(freeze, enemy.transform.position, enemy.transform.rotation);
                    hit_1.par = enemy;
                    enemy.frozen = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            enemy.animator.speed = 1f;
            enemy.jumpTakeOffSpeed = enemy.constJump;
            enemy.maxSpeed = enemy.constMaxSpeed;
        }
    }
}
