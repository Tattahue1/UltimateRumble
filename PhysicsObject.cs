using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhysicsObject : MonoBehaviour
{
    PhotonView view;

    public static int num;
    public static float hp;
    public static float mana;
    public static float stamina;

    public PhysicsObject enemycounterspell;

    public int playerNumber = 0;

    public bool magicImmune = false;
    public bool physicalImmune = false;
    public float hitcooldown = 0f;

    public float testamentTimeOut;
    public int damagedby;
    public float timerDamagedby = 1f;
    public bool darkenergy = false;
    public bool darkeDirection = false;
    public float darkeTimeleft = 0f;

    public int braveShieldStacks = 0;

    public float staminaCharge = 1f;

    public PhysicsObject stringed = null;

    public KeyCode Sjump;
    public KeyCode Smoveleft;
    public KeyCode Smoveright;
    public KeyCode Sdown;
    public KeyCode Sup;
    public KeyCode Satk1;
    public KeyCode Satk2;
    public KeyCode Satk3;
    public KeyCode Shit;
    public KeyCode Sboost;
    public KeyCode Sdefend;
    public KeyCode Sult;


    public bool alphed = false;
    public KeyCode jump;
    public KeyCode moveleft;
    public KeyCode moveright;
    public KeyCode down;
    public KeyCode up;
    public KeyCode atk1;
    public KeyCode atk2;
    public KeyCode atk3;
    public KeyCode hit;
    public KeyCode boost;
    public KeyCode defend;
    public KeyCode ult;

    public PhysicsObject replacement = null; 
    
    public int returnType = 0;

    public float ultbuff;
    public Transform feetPos;       //this variable will store reference to transform from where we will create a circle
    public float circleRadius;      //radius of circle
    public LayerMask whatIsGround;  //layer our ground will have.
    public ParticleSystem deadsystem;
    public float gravityModifier = 1.5f;
    public Animator animator;
    public bool crouch = false;
    public int stingmonStacks = 0;
    public float stingmonStacksDuration = 5f;
    public float armor = 1f;
    public float physical = 1f;
    public float magical = 1f;
    public float magic = 1f;
    public float buffDuration = 10f;
    public float buffcooldown = 25f;
    public ParticleSystem aura;
    public Vector3 initialPos;
    public AudioSource MusicSource;
    public bool physicalReflect = false;
    public CapsuleCollider2D cap;
    public bool magicalReflect = false;
    public bool dead = false;


    public bool enableReturn = false;

    public Color colora;
    public float physicalCounter = 0f;
    public float magicalCounter = 0f;

    public float constJump;
    public bool buff = false;
    public bool dramon = false;
    public bool damagedP = false;
    protected Vector2 targetVelocity;
    public bool grounded;
    protected Vector2 groundNormal;
    public bool disable = false;
    public Rigidbody2D rb2d;
    public Vector2 velocity;
    public bool right = false;
    public bool defended = false;
    public bool defendedCrouch = false;
    public bool invulnerable = false;

    public float maxSpeed = 5.0f;
    public float jumpTakeOffSpeed = 10.5f;
    public float weight = 1.5f;

    public float hp2 = 100.0f;
    public float ultBarCharge = 0.0f;
    public float adrenaline = 100f;
    protected bool highD = false;
    protected bool typeD = false;
    protected float colX = 0;
    protected float colY = 0;
    public Vector2 move = Vector2.zero;
    public float platformCooldown = 0f;
    public Vector2 initVel;

    public bool signalHardhit = false;
    public bool signalHit = false;

    public float timeInvulnerable = 0.1f;
    public bool activeInvulernable = false;
    public bool swapper = false;

    public float constMaxSpeed = 10f;

    public float jumpTimer = 0f;
    public int jumpCount = 2;
    public int jumpTaker = 1;
    public bool jumper = false;

    public bool kingofthesea = false;
    public bool seadramon = false;
    public bool lordknightmon = false;

    public bool absorbed = false;

    public bool externalGravity = false;

    public float slicetimer = 0f;

    public float timerCounter = 0f;

    public bool statusResHero = false;

    public automata automata;

    public int liartype = 0;

    public bool enableType = false;

    public int heavenStacks = 0;
    public float timerHeaven = 0f;
    public int divermonStacks = 0;

    public Vector2 saver;
    public Vector2 saver2;

    public float jumpPlatformTimer = 0f;

    public bool test;

    public bool frozen = false;

    public bool leopardone = false;

    public int godsbless = 0;
    public float blesshp = 0f;

    public bool predeterminedPosition = false;

    float replacementTimer = 0.1f;

    void OnEnable()
    {
        saver = GetComponent<CapsuleCollider2D>().size;
        saver2 = GetComponent<CapsuleCollider2D>().offset;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        view = GetComponent<PhotonView>();
        num = playerNumber;

        flip();
        jump = KeyCode.G;
        moveleft = KeyCode.A;
        moveright = KeyCode.D;
        down = KeyCode.S;
        up = KeyCode.W;
        atk1 = KeyCode.F;
        atk3 = KeyCode.H;
        atk2 = KeyCode.T;
        hit = KeyCode.Y;
        boost = KeyCode.Q;
        defend = KeyCode.E;
        ult = KeyCode.R;
        initialPos.x = -6.5f;
        initialPos.y = 6f;
        initialPos.z = 0f;
        
        //if (playerNumber == 2)
        //{
        //    jump = KeyCode.Keypad2;
        //    moveleft = KeyCode.LeftArrow;
        //    moveright = KeyCode.RightArrow;
        //    down = KeyCode.DownArrow;
        //    up = KeyCode.UpArrow;
        //    atk1 = KeyCode.Keypad1;
        //    atk2 = KeyCode.Keypad5;
        //    atk3 = KeyCode.Keypad3;
        //    hit = KeyCode.Keypad6;
        //    boost = KeyCode.Keypad4;
        //    defend = KeyCode.KeypadPeriod;
        //    ult = KeyCode.Keypad0;
        //    initialPos.x = 10f;
        //    initialPos.y = 5.4f;
        //    initialPos.z = 0f;
        //}
        //if (playerNumber == 3)
        //{
        //    flip();
        //    jump = KeyCode.B;
        //    moveleft = KeyCode.Z;
        //    moveright = KeyCode.C;
        //    down = KeyCode.X;
        //    up = KeyCode.S;
        //    atk1 = KeyCode.V;
        //    atk2 = KeyCode.G;
        //    atk3 = KeyCode.N;
        //    hit = KeyCode.H;
        //    boost = KeyCode.A;
        //    defend = KeyCode.D;
        //    ult = KeyCode.F;
        //    initialPos.x = 3f;
        //    initialPos.y = 2.9f;
        //    initialPos.z = 0f;
        //}
        
        //if (playerNumber == 4)
        //{
        //    jump = KeyCode.Keypad2;
        //    moveleft = KeyCode.LeftArrow;
        //    moveright = KeyCode.RightArrow;
        //    down = KeyCode.DownArrow;
        //    up = KeyCode.UpArrow;
        //    atk1 = KeyCode.Keypad1;
        //    atk2 = KeyCode.Keypad5;
        //    atk3 = KeyCode.Keypad3;
        //    hit = KeyCode.Keypad6;
        //    boost = KeyCode.Keypad4;
        //    defend = KeyCode.KeypadPeriod;
        //    ult = KeyCode.Keypad0;
        //    initialPos.x = -4.5f;
        //    initialPos.y = 3.5f;
        //    initialPos.z = 0f;
        //}

        //if (playerNumber == 5)
        //{/*
        //    flip();
        //    jump = KeyCode.G;
        //    moveleft = KeyCode.A;
        //    moveright = KeyCode.D;
        //    down = KeyCode.S;
        //    up = KeyCode.W;
        //    atk1 = KeyCode.F;
        //    atk2 = KeyCode.T;
        //    atk3 = KeyCode.H;
        //    hit = KeyCode.Y;
        //    boost = KeyCode.Q;
        //    defend = KeyCode.E;
        //    ult = KeyCode.R;*/
        //    initialPos.x = 10f;
        //    initialPos.y = -4.5f;
        //    initialPos.z = 0f;
        //}

        //if (playerNumber == 6)
        //{/*
        //    jump = KeyCode.G;
        //    moveleft = KeyCode.A;
        //    moveright = KeyCode.D;
        //    down = KeyCode.S;
        //    up = KeyCode.W;
        //    atk1 = KeyCode.F;
        //    atk2 = KeyCode.T;
        //    atk3 = KeyCode.H;
        //    hit = KeyCode.Y;
        //    boost = KeyCode.Q;
        //    defend = KeyCode.E;
        //    ult = KeyCode.R;*/
        //    initialPos.x = -16f;
        //    initialPos.y = -4.5f;
        //    initialPos.z = 0f;
        //}

        Sjump = jump;
        Smoveleft = moveleft;
        Smoveright = moveright;
        Sdown = down;
        Sup = up;
        Satk1 = atk1;
        Satk3 = atk3;
        Satk2 = atk2;
        Shit = hit;
        Sboost = boost;
        Sdefend = defend;
        Sult = ult;

        colora.b = colora.g = colora.r = 1f;
        colora.a = 1f;
        if (!predeterminedPosition)
            transform.position = initialPos;
    }

    public void addMana(float mana)
    {
        ultBarCharge += mana * 3;
    }

    public void addHealth(float health)
    {
        hp2 += health;
    }

    void respawn()
    {
        if (transform.position.y < -14f && hp2 > 0f)
        {
            transform.position = initialPos;
            damaged(0f, true, false, 0, 0);
            hp2 -= 4.5f;
        }
        if (transform.position.x <= -22f || transform.position.x >= 18f)
        {
            if(!disable)
            {
                transform.position = initialPos;
                damaged(0f, true, false, 0, 0);
                hp2 -= 8f;
            }
        }
    }

    void Update()
    {
        if (view.IsMine)
        {
            if (hp2 >= 100)
                hp2 = 100;
            if (ultBarCharge >= 100f)
                ultBarCharge = 100f;
            if (ultBarCharge <= 0f)
                ultBarCharge = 0f;
            if (replacement != null)
            {
                replacementTimer -= Time.deltaTime;
            }
            if (replacementTimer <= 0)
                destroyer();
            timerHeaven -= Time.deltaTime;
            if (timerHeaven <= 0f)
            {
                heavenStacks = 0;
            }
            if (invulnerable)
            {
                gameObject.layer = 10;
            }
            timerCounter -= Time.deltaTime;
            if (physicalReflect)
            {
                if (timerCounter >= 0f && timerCounter <= 1f)
                {
                    physicalCounter = 0f;
                    physicalReflect = false;
                }
            }
            if (magicalReflect)
            {
                if (timerCounter >= 0f && timerCounter <= 1f)
                {
                    print("XDDDDD");
                    physicalCounter = 0f;
                    physicalReflect = false;
                }
            }
            slicetimer -= Time.deltaTime;
            if (slicetimer >= 0f && slicetimer < 1f)
            {
                absorbed = false;
                gameObject.layer = 1;
            }
            jumpTimer -= Time.deltaTime;
            if (grounded && velocity.y < 0)
            {
                jumpCount = 2;
            }
            if (grounded && jumpTimer <= 0f)
            {
                jumper = false;
            }
            if (jumper)
            {
                jumpTaker = 2;
            }
            else
            {
                jumpTaker = 1;
            }
            if (activeInvulernable)
            {
                timeInvulnerable -= Time.deltaTime;
                if (timeInvulnerable <= 0f)
                {
                    if (grounded && disable)
                    {
                        invulnerable = true;
                        activeInvulernable = false;
                        timeInvulnerable = 0.1f;
                    }
                }
            }
            timerDamagedby -= Time.deltaTime;
            if (timerDamagedby <= 0f)
            {
                damagedby = 0;
            }
            adrenaline += Time.deltaTime * staminaCharge * 4.5f;
            if (adrenaline >= 100f)
                adrenaline = 100f;
            if (adrenaline <= 0f)
                adrenaline = 0f;
            hitcooldown -= Time.deltaTime;
            testamentTimeOut -= Time.deltaTime;
            darkeTimeleft -= Time.deltaTime;
            if (darkeTimeleft > 0)
            {
                darkenergy = true;
                if (darkeDirection && !disable)
                {
                    move.x = 0.35f;
                }
                if (!darkeDirection && !disable)
                {
                    move.x = -0.35f;
                }
            }
            else
                darkenergy = false;
            mana = ultBarCharge;
            hp = hp2;
            stamina = adrenaline;
            platformCooldown -= Time.deltaTime;
            if (platformCooldown <= 0f && !absorbed && !invulnerable)
            {
                if (!kingofthesea)
                    gameObject.layer = 1;
                else
                    gameObject.layer = 11;
            }
            if (hp2 <= 0f)
            {
                move.x = 0f;
                hp2 = 0f;
            }
            if (velocity.y <= -7.5f * weight)
                velocity.y = -7.5f * weight;
            //here we set the isGrounded
            if (!kingofthesea)
            {
                test = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
                if (test)
                {

                    jumpPlatformTimer -= Time.deltaTime;
                    if (jumpPlatformTimer <= 0f)
                    {
                        grounded = true;
                    }
                }
                else
                {
                    grounded = false;
                    jumpPlatformTimer = 0.08f;
                }
            }
            else
                grounded = true;
            respawn();
            targetVelocity = Vector2.zero;
            ComputeVelocity();
        }
    }

    public void damaged(float damage, bool high, bool type, float collisionY, float collisionX)
    {
        physicalReflect = false;
        magicalReflect = false;
        if (type)
            liartype = 1;
        if (!type)
            liartype = 2;
        bool xd = false;
        if (lordknightmon && buff && Random.Range(0f, 1f) <= 0.25f)
            xd = true;
        if (!xd)
        {
            defended = false;
            animator.SetBool("defended", false);
            bool blockPhysical = false;
            if (type && braveShieldStacks > 0)
                blockPhysical = true;
            if (blockPhysical == false)
            {
                MusicSource.Stop();
                if (statusResHero && buff)
                    colY = 0f;
                if (seadramon && kingofthesea)
                    colY = 0f;
                else
                    colY = collisionY * 2f;
                colX = collisionX;
                if (!kingofthesea)
                    velocity.y = colY;
                move.x = collisionX / (5f*0.7f);

                typeD = type;
                highD = high;
                damagedP = true;
                if (stringed != null)
                    stringed.hp2 -= damage;
                else
                    hp2 -= damage;
                ultBarCharge += damage;
            }
            else
                braveShieldStacks -= 1;
        }
    }
    private void LateUpdate()
    {
        if (darkeTimeleft > 0)
        {
            darkenergy = true;
            if (darkeDirection)
            {
                move.x = 0.35f;
            }
            if (!darkeDirection)
            {
                move.x = -0.35f;
            }
        }
        else
            darkenergy = false;
    }

    protected virtual void ComputeVelocity()
    {
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(velocity.x, velocity.y);

        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime * weight;
        //       velocity.y += gravityModifier * Time.deltaTime;
        velocity.x = targetVelocity.x*0.7f;
    }
    public static float Lifebar(int playerNum)
    {
        if (playerNum == num)
            return hp;
        else
            return 100f;
    }
    public static float Mana(int playerNum)
    {
        if (playerNum == num)
            return mana;
        else
            return 100f;
    }
    public static float Stamina(int playerNum)
    {
        if (playerNum == num)
            return stamina;
        else
            return 100f;
    }
    public void darkp(bool dir, float timeleft)
    {
        darkeTimeleft = timeleft;
        darkeDirection = dir;
    }
    public void flip()
    {
        right = !right;
        transform.Rotate(0f, 180f, 0f);
    }
    public void destroyer()
    {
        Destroy(gameObject);
    }
}