using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dust1side : MonoBehaviour
{
    public PhysicsObject par;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = par.transform.position + new Vector3(0f, -1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = par.transform.position + new Vector3(0f, -1f, 0f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
