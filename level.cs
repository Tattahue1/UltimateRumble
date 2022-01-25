using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level : MonoBehaviour
{
    const int playernumbers = 4;
    public BoxCollider2D col;
    public static Color colora;
    public static Transform ta;
    public static float duration = 11.5f;
    public static Color colorc;
    public static bool getDark = false;

    public PhysicsObject[] players;
    public int tam = 0;
    public int cont = 0;
    float thetime = 5f;
    public float auxtime = 2f;

    void Start()
    {
        players = new PhysicsObject[playernumbers];
        colora.a = colorc.r = colorc.g = colorc.b = colora.r = colora.g = colora.b = 1f;
        colorc.r = colorc.g = colorc.b = 0.7f;
        ta = transform.Find("Sky");
    }

    // Update is called once per frame
    void Update()
    {
        thetime -= Time.deltaTime;
        cont = 0;
        for (int i = 0; i < tam; i++)
        {
            if(players[i] != null)
            {
                cont++;
            }
        }
        if (cont <= 1 && thetime <= 0)
        {
            auxtime -= Time.deltaTime;
        }
        else
            auxtime = 1f;
        if(auxtime <= 0f)
        {
            cont = 0;
            for (int i = 0; i < tam; i++)
            {
                players[i] = null;
            }
            tam = 0;
            SceneManager.LoadScene("PickChars");
        }
        if (duration < 0)
        {
            getDark = false;
        }
        if (getDark)
        {
            //           colora.a = 0.19f;
            if (colora.g > 0.7f)
            {
                colora.g -= 0.007f;
                colora.r -= 0.007f;
                colora.b -= 0.007f;
            }
            duration -= Time.deltaTime;
            for (int i = 0; i < 5; i++)
            {
                Transform te = ta.transform.GetChild(i);
                SpriteRenderer spriteRenderer = te.GetComponent<SpriteRenderer>();
                spriteRenderer.color = colora;
            }
        }
        else
        {
            if (colora.g < 1f)
            {
                colora.g += 0.007f;
                colora.r += 0.007f;
                colora.b += 0.007f;
            }
            for (int i = 0; i < 5; i++)
            {
                Transform te = ta.transform.GetChild(i);
                SpriteRenderer spriteRenderer = te.GetComponent<SpriteRenderer>();
                spriteRenderer.color = colora;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PhysicsObject enemy = hitInfo.GetComponent<PhysicsObject>();
        if (enemy != null)
        {/*
            bool isin = false;
            int tamer = -1;
            do
            {
                tamer++;
                if (players[tamer] == null)
                {
                    players[tamer] = enemy;
                    tam++;
                    break;
                }
            } while (players[tamer+1] != null && tamer + tam <= 8);*/
            for (int i = 0; i < playernumbers; i++)
            {
                if (players[i] == null)
                {
                    players[i] = enemy;
                    tam++;
                    break;
                }
            }/*
            if (!isin)
            {
                players[tamer] = enemy;
                tam++;
            }*/
        }
    }

    public static void ChangeColor()
    {
        duration = 11.5f;
        getDark = true;
    }
    public static void alpha()
    {
        if(getDark)
            duration += 5f;
    }
}
