using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackEffect : MonoBehaviour
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioCont.instance.playSound("PlayertoBug");
            bug.takeDamage = true;
            bug.HP -= Player.PInstance.damage;
            if (bug.HP <= 100)
            {
                bug.CD4 = 20f;
                bug.moveCD = 0f;
                bug.skillCD = 0f;
                bug.damage = 2;
            }
            EnableEffect(Random.Range(0, 3), collision.transform);
        }
    }

    private void EnableEffect(int a, Transform posi)
    {
        bugEf_BeAttacked[a].transform.position = posi.position;
        Instantiate(bugEf_BeAttacked[a]);
    }

    private void DestroyEffects()
    {
        Destroy(gameObject);
    }
}
