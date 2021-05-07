using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffects : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = Player.PInstance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0.175f;
            player.animationCont.SetBool("beAttacked", true);

            AudioListener.volume = 0.5f;
            player.isFlash = false;
            player.isAttack = false;
            player.enableAttack = true;

            player.animationCont.SetInteger("attack", 0);
            player.animationCont.SetInteger("flash", 0);
            PlayerEffects_BeAtteckeds.instance.EnableRandomEffect();

            UI_PlayerHP.Instance.DisableHP(Bug.instance.damage);
        }
    }


    void SoundEffect()
    {
        AudioCont.instance.playSound("BugAttackOne");
    }

    private void SelfDisable()
    {
        gameObject.SetActive(false);
    }
}
