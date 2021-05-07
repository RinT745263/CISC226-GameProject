using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_PlayerHP : MonoBehaviour
{
    public static UI_PlayerHP Instance;
    private Transform[] disableHPs;
    public Transform[] HPs;
    public Transform[] currHPs;
    public bool inBattle;
    public int SonicHealChance;
    public int GoodEndSteps;

    private float time;
    private Player player;
    private Bug bug;

    private void Awake()
    {
        Instance = this;
        inBattle = false;
        time = 0.0f;
        GoodEndSteps = 1;
        SonicHealChance = 1;
        HPs = transform.GetComponentsInChildren<Transform>();
        if (SceneManager.GetActiveScene().name == "Village")
        {
            for (int i = 1; i < HPs.Length; i++)
            {
                HPs[i].gameObject.SetActive(false);
            }
        }
    }
    private void Start()
    {
        player = Player.PInstance;
        bug = Bug.instance;
    }

    // Taking Damage
    // Disable HPenable
    public void DisableHP(int num)
    {
        currHPs = transform.GetComponentsInChildren<Transform>();

        if (num >= currHPs.Length - 1)
        {
            // Player dies

            AudioListener.volume = 1f;
            GoodEndSteps -= 1;
            bug.TargetActive = false;
            player.talking = true;
            player.enableAttack = false;
            player.isAttack = true;
            player.enableJump = false;
            if (GoodEndSteps < 0)
            {
                if (player.transform.position.x - Bug.instance.transform.position.x >= 0)
                {
                    player.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // left
                }
                else
                {
                    player.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // right
                }
                StopAllCoroutines();
                StartCoroutine(bug.WaitForSkills(1));
            }

            for (int i = 1; i < currHPs.Length; i++)
            {
                currHPs[i].gameObject.SetActive(false);
            }

            player.animationCont.SetInteger("attack", 0);
            player.animationCont.SetBool("death", true);
            player.animationCont.SetBool("beAttacked", false);

            UI_Death.instance.gameObject.SetActive(true);

        }
        else if (currHPs.Length > 2)
        {
            for (int i = currHPs.Length - 1; i > currHPs.Length - num - 1; i--)
            {
                currHPs[i].gameObject.SetActive(false);
            }
        }

        int len = transform.GetComponentsInChildren<Transform>().Length;

        if (GoodEndSteps > 10)
        {
            EnableSonicHeal(0.15f, 0.005f);
        }
        else if (len == 2 && SonicHealChance > 0)
        {
            EnableSonicHeal();
        }
    }

    public void EnableSonicHeal(float percent = 0.01f, float waitTime = 0.05f)
    {
        UI_SonicHeal.Instance.gameObject.SetActive(true);
        StartCoroutine(StartHeal(percent, waitTime));
    }

    private IEnumerator StartHeal(float percent, float waitTime)
    {
        currHPs = transform.GetComponentsInChildren<Transform>();

        if (!inBattle)
        {
            for (int i = currHPs.Length; i < HPs.Length; i++)
            {
                Image pic = HPs[i].GetComponent<Image>();
                pic.fillAmount = 0;
                HPs[i].gameObject.SetActive(true);
                while (pic.fillAmount < 1)
                {
                    pic.fillAmount = Mathf.Lerp(0f, 1f, time / 1f);
                    time = time + percent;
                    yield return new WaitForSeconds(waitTime);
                }
                time = 0f;
            }
        }
        else
        {
            SonicHealChance -= 1;
            Image pic = HPs[currHPs.Length].GetComponent<Image>();
            pic.fillAmount = 0;
            HPs[currHPs.Length].gameObject.SetActive(true);
            while (pic.fillAmount < 1)
            {
                pic.fillAmount = Mathf.Lerp(0f, 1f, time / 1f);
                time = time + 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
            time = 0f;
        }
        UI_SonicHeal.Instance.gameObject.SetActive(false);

    }

    // Enable HP at the first level
    // Enable both HPenable (only two) and HPdisable (four of them)
    public void EnableHPFirstLevel()
    {
        for (int i = 1; i < HPs.Length; i++)
        {
            HPs[i].gameObject.SetActive(true);
        }
        UI_PlayerHPDisable.Instance.EnableAll();
        DisableHP(2);
    }
    
}
