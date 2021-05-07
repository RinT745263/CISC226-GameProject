using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelOne : MonoBehaviour
{
    // Public values, which can be given values outside
    //Velocities about player's movement
    public float playerVelocity;
    public static PlayerLevelOne Instance;
    public bool talking;

    //Private values, which are only used in this script
    //Mainly used for controlling player's body and checking triggers
    private Rigidbody2D playerBody;
    private Animator animationCont;
    private AnimatorStateInfo animStat;
    private AudioCont player_auCont;

    private void Awake()
    {
        Instance = this;
        talking = false;
    }
    
    void Start()
    {
        animationCont = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        player_auCont = AudioCont.instance;
    }
    
    void Update()
    {
        // Accessing to Current Animation State
        animStat = animationCont.GetCurrentAnimatorStateInfo(0);

        animDire();
        run();
        
        // Animation Control Part

        // Run and Idle
        if (Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon)
        {
            animationCont.SetInteger("run", 1);
        }
        else if (Mathf.Abs(playerBody.velocity.x) < 5f)
        {
            animationCont.SetInteger("run", 0);
        }

    }

    private void run()
    {

        playerBody.velocity = new Vector2(Input.GetAxis("Horizontal") * playerVelocity, playerBody.velocity.y);

        if (talking || animStat.IsName("getup1") || animStat.IsName("getup2") || animStat.IsName("getup3") || animStat.IsName("getup4"))
        {
            playerBody.velocity = new Vector2(Input.GetAxis("Horizontal") * 0, playerBody.velocity.y);
        }

        // Bug Correcting
        if ((Input.GetKey("a") && Input.GetKey("d")) || (Input.GetKey("left") && Input.GetKey("right")))
        {
            playerBody.velocity = new Vector2(0.0f, playerBody.velocity.y);
        }
    }

    private void animDire()
    {
        if (!(animStat.IsName("FirstAttack") || animStat.IsName("SecondAttack")))
        {
            // Flip Animation
            if (playerBody.velocity.x > 0.15f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (playerBody.velocity.x < -0.15f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    public Animator getPlayerAnimator()
    {
        return animationCont;
    }

    public void Teleportation()
    {
        animationCont.Play("Teleport");
        StartCoroutine(disablePlayer());
    }

    private IEnumerator disablePlayer()
    {
        yield return new WaitForSeconds(2.3f);
        gameObject.SetActive(false);
        SceneManager.LoadScene("Loading2");
    }

    private void AE_FootStep()
    {
        player_auCont.playSound("grassRun");
    }

    private void AE_Teleport()
    {
        player_auCont.playSound("Teleport");
    }
}
