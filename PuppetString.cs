using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetString : MonoBehaviour
{
    public PhysicsObject par;
    public PhysicsObject enemy;
    float timeleft;
    // Start is called before the first frame update
    void Start()
    {
        enemy.jump = par.jump;
        enemy.moveleft = par.moveleft;
        enemy.moveright = par.moveright;
        enemy.down = par.down;
        enemy.up = par.up;
        enemy.atk1 = par.atk1;
        enemy.atk3 = par.atk3;
        enemy.atk2 = par.atk2;
        enemy.hit = par.hit;
        enemy.defend = KeyCode.None;
        enemy.ult = KeyCode.None;
        enemy.boost = KeyCode.None;

        timeleft = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        if(timeleft <= 0f)
        {
            par.stringed = null;
            par.buff = false;
            enemy.jump = enemy.Sjump;
            enemy.moveleft = enemy.Smoveleft;
            enemy.moveright = enemy.Smoveright;
            enemy.down = enemy.Sdown;
            enemy.up = enemy.Sup;
            enemy.atk1 = enemy.Satk1;
            enemy.atk3 = enemy.Satk3;
            enemy.atk2 = enemy.Satk2;
            enemy.hit = enemy.Shit;
            enemy.defend = enemy.Sdefend;
            enemy.ult = enemy.Sult;
            enemy.boost = enemy.Sboost;
            Destroy(gameObject);
        }
    }
}
