using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public int numbDestroyer;



    public PhysicsObject alphamon;
    public PhysicsObject lordknightmon;
    public PhysicsObject cherubimon;
    public PhysicsObject stingmon;
    public PhysicsObject wargreymon;
    public PhysicsObject ulforceveedramon;
    public PhysicsObject piedmon;
    public PhysicsObject seraphimon;
    public PhysicsObject metalgarurumon;
    public PhysicsObject examon;
    public PhysicsObject metalseadramon;
    public PhysicsObject machinedramon;
    public PhysicsObject puppetmon;
    public PhysicsObject sleipmon;
    public PhysicsObject craniummon;
    public PhysicsObject leopardmon;

    public int digimonNumber;

    public int playerNumber = 1;
    public int position;
    public int vertical = 0;
    public PhysicsObject digimon;

    private float horConst = -10f;
    private float vertConst = -1f;
    public bool choose = false;

    public KeyCode moveup;
    public KeyCode moveleft;
    public KeyCode moveright;
    public KeyCode movedown;
    public KeyCode select;
    public KeyCode unselect;


    public float aux = 0f;
    public float aux2 = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //if (playerNumber == 1)
        //{
            position = 1;
            movedown = KeyCode.S;
            moveup = KeyCode.W;
            moveleft = KeyCode.A;
            moveright = KeyCode.D;
            select = KeyCode.G;
            unselect = KeyCode.T;
            /*}
            if (playerNumber == 2)
            {
                position = 9;
                movedown = KeyCode.DownArrow;
                moveup = KeyCode.UpArrow;
                moveleft = KeyCode.LeftArrow;
                moveright = KeyCode.RightArrow;
                select = KeyCode.Keypad2;
                unselect = KeyCode.Keypad5;
            }
            if (playerNumber == 3)
            {
                position = 18;
                movedown = KeyCode.K;
                moveup = KeyCode.I;
                moveleft = KeyCode.J;
                moveright = KeyCode.L;
                select = KeyCode.Quote;
            }
            if (playerNumber == 4)
            {
                position = 10;
                movedown = KeyCode.E;
                moveup = KeyCode.V;
                moveleft = KeyCode.I;
                moveright = KeyCode.O;
                select = KeyCode.M;
            }*/
    }

        // Update is called once per frame
        void Update()
    {
        if(playerNumber > numbDestroyer)
        {
            Destroy(gameObject);
        }
        digimonNumber = position;
        if (choose == false)
        {
            if (position > 11)
            {
                vertConst = -2f;
                horConst = -33.6f;
                if(position > 22)
                {
                    vertConst = -4f;
                    horConst = -56.7f;
                    if(position > 33)
                    {
                        vertConst = -6f;
                        horConst = -79.8f;
                        if (position > 44)
                        {
                            vertConst = -8f;
                            horConst = -102.9f;
                            if(position > 55)
                            {
                                vertConst = -10f;
                                horConst = -126f;
                            }
                        }
                    }
                }
            }
            else
            {
                vertConst = 0f;
                horConst = -10.5f;
            }
            transform.position = new Vector3(horConst + (position -1) * 2.1f, vertConst+5, 1);
            if (Input.GetKeyDown(moveright) && position < 66)
                position++;
            if (Input.GetKeyDown(moveleft) && position > 1)
                position--;
            if (Input.GetKeyDown(movedown) && position < 56)
                position += 11;
            if (Input.GetKeyDown(moveup) && position > 11)
                position -= 11;
            if (Input.GetKey(moveright) && position < 66)
            {
                aux2 -= Time.deltaTime;
                aux -= Time.deltaTime * 0.5f;
                if (aux <= 0f && aux2 <= 0f)
                {
                    position += 1;
                    aux = 0.01f;
                }
            }
            if (Input.GetKey(moveleft) && position > 1)
            {
                aux2 -= Time.deltaTime;
                aux -= Time.deltaTime * 0.5f;
                if (aux <= 0f && aux2 <= 0f)
                {
                    position -= 1;
                    aux = 0.01f;
                }
            }
            if (Input.GetKey(movedown) && position < 56)
            {
                aux2 -= Time.deltaTime;
                aux -= Time.deltaTime * 0.5f;
                if (aux <= 0f && aux2 <= 0f)
                {
                    position += 11;
                    aux = 0.01f;
                }
            }
            if (Input.GetKey(moveup) && position > 11)
            {
                aux2 -= Time.deltaTime;
                aux -= Time.deltaTime * 0.5f;
                if (aux <= 0f && aux2 <= 0f)
                {
                    position -= 11;
                    aux = 0.01f;
                }
            }
            if (Input.GetKeyUp(movedown) || Input.GetKeyUp(moveup) || Input.GetKeyUp(moveleft) || Input.GetKeyUp(moveright))
                aux2 = 0.5f;
            if (Input.GetKey(select))
            {
                choose = true;
                switch(position)
                {
                    case 1:
                        digimon = alphamon;
                        break;
                    case 14:
                        digimon = lordknightmon;
                        break;
                    case 5:
                        digimon = cherubimon;
                        break;
                    case 23:
                        digimon = stingmon;
                        break;
                    case 26:
                        digimon = wargreymon;
                        break;
                    case 17:
                        digimon = craniummon;
                        break;
                    case 18:
                        digimon = ulforceveedramon;
                        break;
                    case 20:
                        digimon = leopardmon;
                        break;
                    case 6:
                        digimon = seraphimon;
                        break;
                    case 58:
                        digimon = piedmon;
                        break;
                    case 9:
                        digimon = metalgarurumon;
                        break;
                    case 12:
                        digimon = examon;
                        break;
                    case 57:
                        digimon = metalseadramon;
                        break;
                    case 56:
                        digimon = machinedramon;
                        break;
                    case 59:
                        digimon = puppetmon;
                        break;
                    case 13:
                        digimon = sleipmon;
                        break;
                }
            }
        }
        else
        {
            if(Input.GetKeyDown(unselect))
            {
                choose = false;
                digimon = null;
            }
        }
    }
}
