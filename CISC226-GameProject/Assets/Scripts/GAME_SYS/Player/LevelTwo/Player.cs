using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Public values, which can be given values outside
    //Velocities about player's movement
    public float playerVelocity;
    public float jumpUpVelocity;
    public float jumpDownVelocity;
    public float attackVelocity;
    public float takeInUSBVelocity;
    public float takeInUSBVelocity_Delta;
    public static Player PInstance;
    public float damage;
    public float flashDamage;
    public Transform flashEffect1;
    public Transform flashEffect2;
    public bool talking;
    public bool enableAttack;
    public bool isAttack;
    public bool enableJump;
    public bool isFlash;
    public bool enablePlayer;
    public Animator animationCont;
    public Transform attackEffectsParent;
    public Animator camShake;

    //Private values, which are only used in this script
    //Mainly used for controlling player's body and checking triggers
    private Rigidbody2D playerBody;
    private EdgeCollider2D groundCheck;
    private bool groundTouch;
    private bool doubleJump;
    private AudioCont player_auCont;
    private Transform[] attackEffects;
    private Quaternion left = Quaternion.Euler(0f,180f,0f);
    private Quaternion right = Quaternion.Euler(0f,0f,0f);

    private bool takeInUSB_Move;
    private bool takeInUSB_SlowDown;
    private Vector3 takeInUSB_Distance;
    private float takeInUSBVelocity_Copy;

    private void Awake()
    {
        PInstance = this;
        talking = false;
        enableAttack = true;
        isAttack = false;
        isFlash = false;
        enableJump = true;
        takeInUSB_Move = false;
        takeInUSB_SlowDown = false;

        if (damage == 0)
        {
            damage = 1f;
        }
        if (flashDamage == 0)
        {
            flashDamage = 0.5f;
        }

        attackEffects = Resources.LoadAll<Transform>("PlayerEffect_Attack");
    }
    
    void Start()
    {
        takeInUSBVelocity_Copy = takeInUSBVelocity;
        animationCont = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<EdgeCollider2D>();
        player_auCont = AudioCont.instance;
        enablePlayer = false;
    }
    
    void Update()
    {
        // Is player touching ground?
        groundTouch = groundCheck.IsTouchingLayers(LayerMask.GetMask("ground"));

        run();

        if (Input.GetKeyDown("space") && enableJump)
        {
            jump();
        }

        // Reset doubleJump value when player touchs ground again
        if (playerBody.velocity.y == 0)
        {
            doubleJump = false;
        }

        // Different Speed for falling
        if (playerBody.velocity.y < -5f)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, -jumpDownVelocity);
        }

        // Animation Control Part

        // Flash
        FlashAnim();
        // attack
        if (groundTouch && Input.GetMouseButtonDown(0) && enableAttack) {
            attack();
        }

        // Run and Idle
        if (Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon)
        {
            animationCont.SetInteger("run", 1);
        }
        else if (Mathf.Abs(playerBody.velocity.x) < 5f)
        {
            animationCont.SetInteger("run", 0);
        }

        // Jump
        if (!groundTouch)
        { 
            if (playerBody.velocity.y < -0.5f)
            {
                animationCont.SetInteger("Jump", 3); // Air Drop
            }
            else if (Input.GetKey("a") || Input.GetKey("d"))
            {
                animationCont.SetInteger("Jump", 1); // Angled jump
            }
            else
            {
                animationCont.SetInteger("Jump", 2); // Vertical jump
            }
        }
        // back to run or idle
        else
        {
            if (Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon)
            {
                animationCont.SetInteger("run", 1); // run
            }
            else
            {
                animationCont.SetInteger("run", 0); //idle (runend)
            }
            animationCont.SetInteger("Jump", 0); // idle (landing)
        }
        
        if (isFlash)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x * 0f, playerBody.velocity.y * 0f);
        }

        if (takeInUSB_Move)
        {
            transform.position = Vector3.Lerp(transform.position, takeInUSB_Distance, Time.deltaTime * takeInUSBVelocity);
            if (takeInUSB_SlowDown)
            {
                takeInUSBVelocity = Mathf.Lerp(takeInUSBVelocity, takeInUSBVelocity_Copy - takeInUSBVelocity_Delta, Time.deltaTime);
            }
        }

        animDire();
    }

    // Action Control -------------------------------------------------------------------------------------------------------------
    private void run()
    {
        // Horizontal Movement
        if (isAttack)
        {
            playerBody.velocity = new Vector2(
                transform.localEulerAngles.y == 0 ? Mathf.Abs(Input.GetAxis("Horizontal") * attackVelocity) : -Mathf.Abs(Input.GetAxis("Horizontal") * attackVelocity),
                0.0f);
        }
        else {
            playerBody.velocity = new Vector2(Input.GetAxis("Horizontal") * playerVelocity, playerBody.velocity.y);
        }

        if (talking)
        {
            playerBody.velocity = new Vector2(Input.GetAxis("Horizontal") * 0, playerBody.velocity.y);
        }

        // Bug Correcting
        if ((Input.GetKey("a") && Input.GetKey("d"))|| (Input.GetKey("left") && Input.GetKey("right")))
        {
            if (!groundTouch)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, playerBody.velocity.y);
            }
            else playerBody.velocity = new Vector2(0.0f, playerBody.velocity.y);
        } 
    }

    private void jump() 
    {
        // Second Jump
        if (doubleJump)
        {
            animationCont.SetTrigger("DoubleJump");
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpUpVelocity);
            doubleJump = false;
        }
        // First Jump
        else if (groundTouch)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpUpVelocity);
            doubleJump = true;
        }
    }

    private void attack()
    {

        switch (animationCont.GetCurrentAnimatorClipInfo(0)[0].clip.name)
        {
            // First Attack to Second Attack
            case "FirstAttack":
                animationCont.SetInteger("attack", 3);
                enableAttack = false;
                break;
            // Second Attack to Third Attack
            case "SecondAttack":
                animationCont.SetInteger("attack", 4);
                enableAttack = false;
                break;
            // Third Attack to Forth Attack
            case "ThirdAttack":
                animationCont.SetInteger("attack", 5);
                enableAttack = false;
                break;
            // Forth Attack back to ReFirst Attack
            case "ForthAttack":
                animationCont.SetInteger("attack", 6);
                enableAttack = false;
                break;
            // ReFirst Attack to Second Attack
            case "ReFirstAttack":
                animationCont.SetInteger("attack", 7);
                enableAttack = false;
                break;
            // Idle or run to First Attack
            default:
                animationCont.SetInteger("attack", 2);
                enableAttack = false;
                break;
        }
    }

    private void FlashAnim()
    {
        if (!isAttack && Input.GetKey("left shift"))
        {
            if (Input.GetMouseButton(0))
            {
                animationCont.SetInteger("flash", 1);
            }
            else
            {
                animationCont.SetInteger("flash", 2);
            }
            camShake.SetTrigger("shake");
        }
    }

    private void animDire()
    {
        // Flip Animation
        if (playerBody.velocity.x > 0.15f)
        {
            transform.rotation = right;
        }
        else if (playerBody.velocity.x < -0.15f)
        {
            transform.rotation = left;
        }
    }


    // Animation Events ---------------------------------------------------------------------------------------------------
    private void Flash()
    {
        if (transform.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            transform.position = new Vector3(transform.position.x + 12f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - 12f, transform.position.y, transform.position.z);
        }
    }

    private void flashFin()
    {
        enableAttack = true;
        animationCont.SetInteger("flash", 0);
        isFlash = false;
        isAttack = false;
        animationCont.SetInteger("attack", 0);
    }

    private void flashStart()
    {
        animationCont.SetInteger("attack", 0);
        enableAttack = false;
        isFlash = true;
        isAttack = true;
    }
    private void EnableFlashEffec1()
    {
        flashEffect1.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        flashEffect1.localRotation = transform.localRotation;
        flashEffect1.gameObject.SetActive(true);
    }

    private void EnableFlashEffec2()
    {
        flashEffect2.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        flashEffect2.localRotation = transform.localRotation;
        flashEffect2.gameObject.SetActive(true);
    }
    
    private void attackFin()
    {
        isAttack = false;

        if (Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon)
        {
            animationCont.SetInteger("attack", 1);  // run
        }
        else
        {
            animationCont.SetInteger("attack", 0);  // idle
        }
    }

    private void EnableAttack()
    {
        enableAttack = true;
    }

    private void IsAttack()
    {
        isAttack = true;
    }

    private void EnableAttackEffectOne()
    {
        attackEffects[1].position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        attackEffects[1].localRotation = transform.localRotation;
        Instantiate(attackEffects[1], attackEffectsParent);
    }
    private void EnableAttackEffectTwo()
    {
        attackEffects[4].position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        attackEffects[4].localRotation = transform.localRotation;
        Instantiate(attackEffects[4], attackEffectsParent);
    }
    private void EnableAttackEffectThree()
    {
        attackEffects[3].position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        attackEffects[3].localRotation = transform.localRotation;
        Instantiate(attackEffects[3], attackEffectsParent);
    }
    private void EnableAttackEffectFour()
    {
        attackEffects[0].position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        attackEffects[0].localRotation = transform.localRotation;
        Instantiate(attackEffects[0], attackEffectsParent);
    }
    private void EnableAttackEffectRe()
    {
        attackEffects[2].position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        attackEffects[2].localRotation = transform.localRotation;
        Instantiate(attackEffects[2], attackEffectsParent);
    }

    // Helper Function for Disabling players' control
    public void DisablePlayer()
    {
        talking = true;
        enableAttack = false;
        isAttack = true;
        enableJump = false;
        if (Bug.instance != null)
        {
            Bug.instance.TargetActive = false;
        }
    }

    private void EnablePlayer()
    {
        if (enablePlayer)
        {
            talking = false;
            enableAttack = true;
            isAttack = false;
            enableJump = true;
            if (Bug.instance != null)
            {
                Bug.instance.TargetActive = true;
            }
        }
    }

    private void DeathFin()
    {
        animationCont.SetBool("death", false);
    }

    private void RebirthFin()
    {
        animationCont.SetBool("rebirth", false);
    }

    // Helper Function for giving players' control back
    public void EnablePlayer(int a)
    {
        talking = false;
        enableAttack = true;
        isAttack = false;
        enableJump = true;
        if (Bug.instance != null)
        {
            Bug.instance.TargetActive = true;
        }
    }

    private void TakeInUSB_Move()
    {
        if (transform.position.x - Bug.instance.transform.position.x >= 0)
        {
            takeInUSB_Distance = new Vector3(transform.position.x - 5f, transform.position.y);
        }
        else
        {
            takeInUSB_Distance = new Vector3(transform.position.x + 5f, transform.position.y);
        }
        takeInUSB_Move = true;
    }
    private void TakeInUSB_SlowDown()
    {
        takeInUSB_SlowDown = true;
    }
    private void TakeInUSB_StopMove()
    {
        takeInUSB_Move = false;
    }

    private void EnableUSB_Tip()
    {
        UI_USBTip.instance.gameObject.SetActive(true);
    }

    private void EnableFinalHit()
    {
        Bug.instance.anim.SetBool("final", true);
    }
    private void FinalScene()
    {
        SceneFadeInOut.instance.FinalEndScene();
    }
    
    
    
    // SoundEffect ------------------------------------------------------------------------------------
    void AE_FootStep()
    {
        player_auCont.playSound("grassRun");
    }
    void AE_JumpStart()
    {
        player_auCont.playSound("jumpStart");
    }
    void AE_Land()
    {
        player_auCont.playSound("land");
    }
    void AE_Attack1()
    {
        player_auCont.playSound("PlayerAttackOne");

    }
    void AE_Attack2()
    {
        player_auCont.playSound("PlayerAttackTwo");

    }
    void AE_Attack3()
    {
        player_auCont.playSound("PlayerAttackThree");

    }
    void AE_Attack4()
    {
        player_auCont.playSound("PlayerAttackFour");

    }
    void AE_AttackRe1()
    {
        player_auCont.playSound("PlayerAttackFive");
    }
    void AE_Flash()
    {
        player_auCont.playSound("Flash");
    }
    void AE_Teleport()
    {
        player_auCont.playSound("Teleport");
    }
    void AE_Teleport_PlayerSet()
    {
        player_auCont.playSound("Teleport_PlayerSet");
    }
    void AE_Flash_Sword_Begin()
    {
        player_auCont.playSound("Flash_attack_sword_begin");
    }

    void AE_FinalHit_Many()
    {
        player_auCont.playSound("FinalHit_Many");
    }
}
