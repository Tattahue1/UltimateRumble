using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class picking : MonoBehaviour
{
    static Gamestart gamestart;
    void Start()
    {
        for (int i = 0; i < gamestart.transform.childCount+2; i++)
        {
            PhysicsObject xd = Instantiate(gamestart.transform.GetChild(i).GetComponent<controller>().digimon, transform.position, transform.rotation);
            xd.playerNumber = gamestart.transform.GetChild(i).GetComponent<controller>().playerNumber;
            transform.GetChild(0).GetChild(i).GetComponent<barassitant>().player = xd;
            int playernumb, posNum;
            posNum = gamestart.transform.GetChild(i).GetComponent<controller>().position;
            playernumb = gamestart.transform.GetChild(i).GetComponent<controller>().playerNumber;
            switch (playernumb)
            {
                case 1:
                    transform.GetChild(1).GetChild(posNum - 1).transform.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    transform.GetChild(1).GetChild(posNum - 1).position = new Vector3(-18.16f, -7.19312f, 0f);
                    break;
                case 2:
                    transform.GetChild(1).GetChild(posNum - 1).transform.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    transform.GetChild(1).GetChild(posNum - 1).position = new Vector3(12.385f, -7.19312f, 0f);
                    break;
                case 3:
                    transform.GetChild(1).GetChild(posNum - 1).transform.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    transform.GetChild(1).GetChild(posNum - 1).position = new Vector3(-18.16f, 8.23f, 0f);
                    break;
                case 4:
                    transform.GetChild(1).GetChild(posNum - 1).transform.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    transform.GetChild(1).GetChild(posNum - 1).position = new Vector3(12.385f, 8.23f, 0f);
                    break;
            }            
        }
        Destroy(gamestart);
    }

    // Update is called once per frame
    void Update()
    {
    }
    static public void addPlayer(Gamestart game)
    {
        gamestart = game; 
    }
}
