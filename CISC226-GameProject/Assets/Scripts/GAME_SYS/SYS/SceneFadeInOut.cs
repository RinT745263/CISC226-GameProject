using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFadeInOut : MonoBehaviour
{

    public float fadeOutSpeed;
    public float fadeInSpeed;
    public bool sceneStart;
    public Camera cam;
    public static SceneFadeInOut instance;
        
    private RawImage rawImage;
    private bool fadeToWhite;
    private bool turnDownVoice;
    private AudioSource BGM;

    private void Awake()
    {
        instance = this;
        rawImage = transform.GetComponent<RawImage>();
        sceneStart = false;
        fadeToWhite = false;
        turnDownVoice = false;
    }

    private void Start()
    {
        BGM = cam.GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (fadeToWhite)
        {
            rawImage.color = Color.Lerp(rawImage.color, Color.white, fadeOutSpeed * Time.deltaTime);
        }

        else if (sceneStart)
        {
            rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeInSpeed * Time.deltaTime);
        }

        if (turnDownVoice)
        {
            BGM.volume = Mathf.Lerp(BGM.volume, 0f, 2f * Time.deltaTime);
        }
    }

    public void StartScene()
    {
        if (SceneManager.GetActiveScene().name == "Bug")
        {
            cam.orthographicSize = 9f;
        }
        gameObject.SetActive(true);
        StartCoroutine(WaitForFadeIn());
        sceneStart = true;
    }
    private IEnumerator WaitForFadeIn()
    {
        yield return new WaitUntil(() => rawImage.color.a < 0.01f);
        rawImage.color = Color.clear;
        sceneStart = false;
        gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Bug")
        {
            cam.orthographicSize = 9f;
            NPC.instance.EnableAllNPCs();
        }
    }

    public void EndScene()
    {
        rawImage.color = new Color(1f,1f,1f,0f);
        gameObject.SetActive(true);
        StartCoroutine(WaitForFadeOut());
        fadeToWhite = true;
        if (SceneManager.GetActiveScene().name == "Bug")
        {
            turnDownVoice = true;
        }
    }
    

    private IEnumerator WaitForFadeOut()
    {
        yield return new WaitUntil(() => rawImage.color.a > 0.97f);
        if (SceneManager.GetActiveScene().name == "Bug")
        {
            turnDownVoice = false;
            BGM.Stop();
            BGM.volume = 0.5f;
        }
        rawImage.color = Color.white;
        fadeToWhite = false;
        if (SceneManager.GetActiveScene().name == "TheHero")
        {
            KeyReminder.instance.gameObject.SetActive(false);
            SceneManager.LoadScene("Scenes/Bug");
        }
        if (SceneManager.GetActiveScene().name == "Bug")
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        USB.instance.gameObject.SetActive(false);
        Bug.instance.anim.SetTrigger("twitch");
        Player.PInstance.animationCont.SetTrigger("takeInUSBFin");
        StartScene();
    }

    public void FinalEndScene()
    {
        rawImage.color = new Color(1f, 1f, 1f, 0f);
        gameObject.SetActive(true);
        StartCoroutine(NextScene());
        fadeToWhite = true;
    }

    public IEnumerator NextScene()
    {
        yield return new WaitUntil(() => rawImage.color.a > 0.97f);
        rawImage.color = Color.white;
        fadeToWhite = false;
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
}
