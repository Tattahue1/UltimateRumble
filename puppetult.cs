using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puppetult : MonoBehaviour
{
    public PhysicsObject par;
    public automata auto;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null && enemy != par)
        {
            if (enemy.godsbless <= 0)
            {
                automata bull = Instantiate(auto, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                bull.enemy = enemy;
                bull.par = par;
            }
            else
            {
                automata bull = Instantiate(auto, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
                bull.enemy = par;
                bull.par = enemy;
            }
        }
    }
}
