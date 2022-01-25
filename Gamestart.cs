using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Gamestart : MonoBehaviour
{
    int count = 0;
    float timer = 0.1f;
    bool destroyer = false;
    public int auxiliar = 2;
    static int numba = 3;
    // Start is called before the first frame update
    void Start()
    {
        numba = auxiliar;
        for (int i = 0; i < transform.childCount-1; i++)
        {
            transform.GetChild(i).GetComponent<controller>().numbDestroyer = numba;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyer)
            timer -= Time.deltaTime;
        if (timer <= 0f)
            Destroy(gameObject);
        if (transform.GetChild(count).GetComponent<controller>().choose)
        {
            print(count);
            print("XDDDDDDDDDD");
            count++;
            if (transform.childCount <= count)
                count = transform.childCount;
        }
        if(count >= transform.childCount)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = -20;
            }
            DontDestroyOnLoad(this);
            SceneManager.LoadScene("StageScene");
            destroyer = true;
            picking.addPlayer(this);
        }
    }

    public PhysicsObject getDigimons()
    {
        return transform.GetChild(1).GetComponent<controller>().digimon;
    }

    public static void numbPlayers(int numb)
    {
        numba = numb;
    }
}
