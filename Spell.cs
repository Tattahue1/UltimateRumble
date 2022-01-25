using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public bool bouncable = false;
    public bool timeStopped = false;
    public Animator animator;
    public float timeleft;
    public Rigidbody2D rb;
    public float timestrike;
    public Vector2 initVel;
    public PhysicsObject par;
    public KeyCode swap;
    public bool swappable = true;
    public trickster trickster;
    public bool destroyable = true;
    public float damage = 0f;
    public bool destroyOnLeaveScreen = false;
    public bool ultimate = false;
    public bool Type;


    private void Awake()
    {
    }
    void Start()
    {
        timestrike = 1f;
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(swap) && par.disable == false && par.adrenaline >= 15f && swappable)
        {
            par.adrenaline -= 15f;
            Vector3 aux = par.transform.position;
            par.transform.position = transform.position;
            transform.position = aux;
            trickster trick = Instantiate(trickster, par.transform.position, par.transform.rotation);
            trick.par = par;
        }
    }

    // Update is called once per frame
    void Update()
    {

     
    }

    public void alpha()
    {
        timeleft = timeleft + 5f;
        timestrike = timestrike + 5f;
    }
    public virtual void destroy()
    {
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        if(!destroyOnLeaveScreen)
            Destroy(gameObject);
    }
}
