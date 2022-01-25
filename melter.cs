using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melter : MonoBehaviour
{
    public Rigidbody2D rb;
    float timer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(0f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<0f)
        {
            rb.velocity = new Vector2(0f, -1f);
        }
    }
}
