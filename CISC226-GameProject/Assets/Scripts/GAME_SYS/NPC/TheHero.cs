using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHero : MonoBehaviour
{
    public float leftPosition;
    public float velocity;
    public static TheHero instance;
    public Animator camShake;

    private Animator animCont;
    private bool startTalk;
    private float wait;
    private bool playSoundOnce;

    private Vector3 a;

    private void Awake()
    {
        instance = this;
        startTalk = true;
        playSoundOnce = true;
        wait = 4f;
    }

    private void Start()
    {
        animCont = transform.GetComponent<Animator>();
        a = new Vector3(leftPosition, transform.position.y);
        // Player has been disabled in teleportation animation
    }

    private void Update()
    {
        // Move to target position
        if (wait <= 0)
        {
            if (transform.position.x > leftPosition + 0.3f)
            {
                transform.position = Vector3.Lerp(transform.position, a, velocity * Time.deltaTime);
            }
            else if (startTalk)
            {
                startTalk = false;
                animCont.SetBool("idle", true);
                UI_Dialog.Instance.dialog(GameManagerLevelTwo.Instance.GetDialogConf_TheHero(0), 0);
            }
        }


        if (wait > 0)
        {
            wait -= Time.deltaTime;
        }
        if (wait < 1f && playSoundOnce)
        {
            playSoundOnce = false;
            AudioCont.instance.playSound("Hero_HitOne");
            camShake.SetTrigger("shake");
        }
    }

    // Skill One is written in UI_Dialog.

    // Teleport Animation is set in UI_Dialog as a dialog event;
    // Black Light is also set in UI_Dialog.
    // This gameObject is closed by the blackAnimation

    public void Teleport()
    {
        animCont.SetBool("idle", false);

        // Player Teleportation animation of black light
        StartCoroutine(WaitForSeconds_BlackLight());
    }

    private IEnumerator WaitForSeconds_BlackLight()
    {
        yield return new WaitForSeconds(1.5f);
        Teleport_BlackLigth.instance.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    public bool GetStartTalk() { return startTalk; }
}
