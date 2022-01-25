using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automata : MonoBehaviour
{
    public PhysicsObject par;
    public PhysicsObject enemy;
    float timeleft;

    void Start()
    {
        timeleft = 15f;
        enemy.automata = this;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft <= 0f || enemy.hp2 <= 0f)
        {
            Destroy(gameObject);
        }
        if (enemy.right)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.position = enemy.transform.position - new Vector3(1.5f, 0f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.position = enemy.transform.position + new Vector3(1.5f, 0f, 0f);
        }
    }
}
