using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyReminder : MonoBehaviour
{
    public static KeyReminder instance;
    private Animator anim;
    private Image img;
    private bool firstStep;
    private bool secondStep;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        firstStep = false;
        secondStep = false;
        anim = transform.GetComponent<Animator>();
        img = transform.GetComponent<Image>();
        img.color = new Color(1f,1f,1f,0f);

        StartCoroutine(FirstStep());
    }

    private void Update()
    {
        if (firstStep)
        {
            img.color = Color.Lerp(img.color, Color.white, 4 * Time.deltaTime);
        }
        else if (secondStep)
        {
            img.color = Color.Lerp(img.color, new Color(1f, 1f, 1f, 0f), 4 * Time.deltaTime);
        }
    }


    private IEnumerator FirstStep()
    {
        yield return new WaitForSeconds(3f);
        firstStep = true;
        yield return new WaitUntil(() => img.color.a > 0.95f);
        firstStep = false;
        img.color = Color.white;
        yield return new WaitForSeconds(1f);
        anim.SetInteger("mode", 1);
        yield return new WaitUntil(() => Input.GetKeyDown("space"));
        anim.SetInteger("mode", 0);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("mode", 2);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        anim.SetInteger("mode", 0);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("mode", 3);
        yield return new WaitUntil(() => Input.GetKeyDown("left shift"));
        anim.SetInteger("mode", 0);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("mode", 4);
        yield return new WaitUntil(() => Input.GetKey("left shift") && Input.GetMouseButton(0));
        anim.SetInteger("mode", 0);
        yield return new WaitForSeconds(1.5f);
        secondStep = true;
        yield return new WaitUntil(() => img.color.a < 0.05f);
        gameObject.SetActive(false);
    }
}
