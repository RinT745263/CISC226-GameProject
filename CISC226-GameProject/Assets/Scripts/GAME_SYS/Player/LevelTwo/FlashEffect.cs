using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    private GameObject[] bugEf_BeAttacked;
    private Bug bug;

    private void Awake()
    {
        bugEf_BeAttacked = Resources.LoadAll<GameObject>("BugEffects_BeAttacked");
    }

    private void Start()
    {
        bug = Bug.instance;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioCont.instance.playSound("PlayertoBug");
            bug.takeDamage = true;
            bug.HP -= Player.PInstance.flashDamage;
            if (bug.HP <= 100)
            {
                bug.CD4 = 20f;
                bug.moveCD = 0f;
                bug.skillCD = 0f;
                bug.damage = 3;
            }
            EnableEffect(Random.Range(0, 3), collision.transform);
        }
    }
    private void EnableEffect(int a, Transform posi)
    {
        bugEf_BeAttacked[a].transform.position = posi.position;
        Instantiate(bugEf_BeAttacked[a]);
    }

    private void DisableEffect()
    {
        gameObject.SetActive(false);
    }
}
