using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform LevelOneBGM;
    public Transform LinkBGM;
    public AudioSource levelOneBGM;
    public AudioSource linkBGM;
    public Animator linkAnim;

    private DialogConf[] dialogConfs;
    private Animator globalLightAnim;
    private Animator freeformLightAnim;

    void Awake()
    {
        Instance = this;
        dialogConfs = Resources.LoadAll<DialogConf>("DialogConf");
        globalLightAnim = GameObject.Find("LightSystem/Global Light 2D").GetComponent<Animator>();
        freeformLightAnim = GameObject.Find("LightSystem/Freeform Light 2D").GetComponent<Animator>();
        levelOneBGM = LevelOneBGM.GetComponent<AudioSource>();
        linkBGM = LinkBGM.GetComponent<AudioSource>();
    }

    // Start First Talk
    private void Start()
    {
        PlayerLevelOne.Instance.talking = true; // Player cannot move until all conversations end
        StartCoroutine(FirstTalk());
    }
    private IEnumerator FirstTalk()
    {
        yield return new WaitForSeconds(1.8f);
        UI_Dialog.Instance.dialog(dialogConfs[0], 0);
    }
    
    // Finish First Talk, turning the global light on and stand up
    public void FirstSection()
    {
        globalLightAnim.SetTrigger("turnOn");
        freeformLightAnim.SetTrigger("turnOff");
        StartCoroutine(StandUp());
    }
    private IEnumerator StandUp()
    {
        yield return new WaitForSeconds(4.2f);
        PlayerLevelOne.Instance.getPlayerAnimator().SetTrigger("Start");
        StartCoroutine(TalkToLinkOne());
    }

    // After standing up, directly talk to Link.
    private IEnumerator TalkToLinkOne()
    {
        yield return new WaitForSeconds(3.5f);
        UI_Dialog.Instance.dialog(dialogConfs[1], 0);
    }

    // Display USB information UI
    public void SecondSection()
    {
        UI_USBInfor.USBInstance.displayUSBUI();
    }

    // After displaying USB UI, continue to talk.
    public void ThirdSection()
    {
        UI_Dialog.Instance.dialog(dialogConfs[2], 0);
    }

    public void ExtraSection()
    {
        PlayerLevelOne.Instance.talking = false;
    }
    // End of Conversations, player could move around
    public void ForthSection()
    {
        SceneName.instance.gameObject.SetActive(true);
        //PlayerLevelOne.Instance.talking = false;
        StartCoroutine(turnDownVolume(0.03f));
        linkAnim.SetTrigger("music");
        linkBGM.Play();
    }

    private IEnumerator turnDownVolume(float a)
    {
        while (levelOneBGM.volume > a)
        {
            levelOneBGM.volume -= 0.01f;
            yield return new WaitForSeconds(0.07f);
        }
    }



    // End the game
    public void EndGame(float seconds)
    {
        StartCoroutine(endGame(seconds));
    }
    private IEnumerator endGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.Quit();
    }




    public DialogConf GetDialogConf(int index)
    {
        return dialogConfs[index];
    }
    
    
}
