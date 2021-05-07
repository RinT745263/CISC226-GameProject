using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffects_ToHero : MonoBehaviour
{
    private Player player;
    public Animator camShake;

    private void Start()
    {
        player = Player.PInstance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            TheHero.instance.Teleport();
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

    private void CamShake()
    {
        camShake.SetTrigger("shake");
    }
    private void AE_SoundEffect()
    {
        AudioCont.instance.playSound("Bug_AttackOne_ToHero");
    }
}
