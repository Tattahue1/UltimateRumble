using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class barassitant : MonoBehaviour
{
    public int playerNumber;
    public PhysicsObject player;
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.replacement != null)
            {
                player = player.replacement;
            }
            float hp = 0, mana = 0, stamina = 0f;
            if (player != null)
            {
                hp = player.hp2;
                mana = player.ultBarCharge;
                stamina = player.adrenaline;
            }
            transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 1f - hp / 100f;
            transform.GetChild(0).GetChild(1).GetComponent<Slider>().value = 1f - mana / 100f;
            transform.GetChild(0).GetChild(2).GetComponent<Slider>().value = 1f - stamina / 100f;
            if (hp == 0)
            {
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject);
    }
}
