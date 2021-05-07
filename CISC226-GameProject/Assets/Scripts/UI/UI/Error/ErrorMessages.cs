using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMessages : MonoBehaviour
{
    public static ErrorMessages instance;

    private Transform[] errorMessages;

    private void Awake()
    {
        instance = this;
        errorMessages = transform.GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        for (int i = 1; i < errorMessages.Length; i++)
        {
            errorMessages[i].gameObject.SetActive(false);
        }
    }

    public void EnableErrorMessages()
    {
        StartCoroutine(EnableOneByOne());
    }
    private IEnumerator EnableOneByOne()
    {
        float waitTime = 0.19f;
        for (int i = 1; i < errorMessages.Length; i += 2)
        {
            AudioCont.instance.playSound("Info_Open");
            errorMessages[i].gameObject.SetActive(true);
            errorMessages[i + 1].gameObject.SetActive(true);

            yield return new WaitForSeconds(waitTime);
            waitTime = Mathf.Lerp(waitTime, 0.04f, 30f * Time.deltaTime);
        }
    }

    public void DisableErrorMessages()
    {
        StartCoroutine(DisableOneByOne());
    }
    private IEnumerator DisableOneByOne()
    {
        for (int i = 1; i < errorMessages.Length; i += 2)
        {
            AudioCont.instance.playSound("Info_Close");
            errorMessages[i].gameObject.SetActive(false);
            errorMessages[i + 1].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.03f);
        }
    }
}
