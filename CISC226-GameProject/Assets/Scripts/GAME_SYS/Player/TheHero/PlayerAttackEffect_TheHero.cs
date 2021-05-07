using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffect_TheHero : MonoBehaviour
{

    private GameObject[] bugEf_BeAttacked;

    private void Awake()
    {
        bugEf_BeAttacked = Resources.LoadAll<GameObject>("TheHero_PlayerAttackEffect/BugEffects_BeAttacked");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioCont.instance.playSound("PlayertoEnemy");
            Train.instance.ChangeDirection(Player_TheHero.PInstance.transform.position.x);
            EnableEffect(Random.Range(0, 3), collision.transform);
        }
    }

    private void EnableEffect(int a, Transform posi)
    {
        Instantiate(bugEf_BeAttacked[a]);
    }

    private void DestroyEffects()
    {
        Destroy(gameObject);
    }
}
