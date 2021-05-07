using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginPage : MonoBehaviour, IPointerClickHandler
{
    public AudioSource BGM;
    public float beginPageFadeOutSpeed;
    public float BGMFadeOutSpeed;

    private Image beginPage;
    private bool fadeToWhite;

    public void Awake()
    {
        fadeToWhite = false;
    }

    private void Start()
    {
        beginPage = transform.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!fadeToWhite)
        {
            StartLoading();
        }
    }

    private void Update()
    {
        if (fadeToWhite)
        {
            beginPage.color = Color.Lerp(beginPage.color, Color.clear, beginPageFadeOutSpeed * Time.deltaTime);
            BGM.volume = Mathf.Lerp(BGM.volume, 0, BGMFadeOutSpeed * Time.deltaTime);
        }
    }

    private void StartLoading()
    {
        fadeToWhite = true;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitUntil(() => beginPage.color.a < 0.05f);
        SceneManager.LoadScene("Village");
    }
}
