using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bug : MonoBehaviour
{
    public Transform camera;
    public int damage;
    public float HP;
    public Transform target;
    public static Bug instance;
    public bool takeDamage;
    public float leftMost;
    public float rightMost;
    public bool TargetActive;
    public Transform bossBlood;
    public Transform[] bossBloods;
    public Transform bossBloodAttack;
    public Transform bossBloodAttackRightToLeft;

    public Animator anim;
    public AudioClip[] battleMusics;
    private Transform[] attackOneEfs;
    private Transform[] attackTwoEfs;
    private Transform[] attackThreeEfs;
    private Transform[] attackFourEfs;
    private Transform[] attackToHero;
    private float distance;
    private bool isFlipped;
    private bool isSkill;
    private bool enableEscape;
    private bool enableChase;
    private bool enableSkill;
    private bool finalHit;

    public float skillCD;
    private float floatSkillCD;
    public float CD4;
    private float floatCD4;
    public float moveCD;
    private float floatMoveCD;

    private string[] skills;
    
    private void Awake()
    {
        instance = this;

        if (damage == 0)
        {
            damage = 1;
        }
        if (CD4 == 0.0f)
        {
            CD4 = 70.0f;
        }
        if (moveCD == 0.0f)
        {
            moveCD = 0.6f;
        }
        if (skillCD == 0.0f)
        {
            skillCD = 0.3f;
        }
        if (HP == 0)
        {
            HP = 120f;
        }
        skills = new string[] { "attack1", "attack2", "attack3" };
        isFlipped = false;
        isSkill = false;
        takeDamage = false;
        enableEscape = true;
        enableChase = true;
        enableSkill = true;
        TargetActive = true;
        finalHit = true;
        bossBloods = bossBlood.GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        battleMusics = Resources.LoadAll<AudioClip>("BGM/Bug");

        anim = GetComponent<Animator>();

        // Start Fight !!!
        UI_PlayerHP.Instance.inBattle = true;

        // But wait for dialog finishing
        TargetActive = false;

        bossBloodAttack.gameObject.SetActive(false);
        bossBloodAttackRightToLeft.gameObject.SetActive(false);

        attackOneEfs = transform.Find("AttackOneEffects").GetComponentsInChildren<Transform>();
        attackTwoEfs = transform.Find("AttackTwoEffects").GetComponentsInChildren<Transform>();
        attackThreeEfs = transform.Find("AttackThreeEffects").GetComponentsInChildren<Transform>();
        attackFourEfs = transform.Find("AttackFourEffects").GetComponentsInChildren<Transform>();
        attackToHero = transform.Find("AttackToTheHero").GetComponentsInChildren<Transform>();

        for (int i = 1; i < attackOneEfs.Length; i++)
        {
            attackOneEfs[i].gameObject.SetActive(false);
        }
        for (int i = 1; i < attackTwoEfs.Length; i++)
        {
            attackTwoEfs[i].gameObject.SetActive(false);
        }
        for (int i = 1; i < attackThreeEfs.Length; i++)
        {
            attackThreeEfs[i].gameObject.SetActive(false);
        }
        for (int i = 1; i < attackFourEfs.Length; i++)
        {
            attackFourEfs[i].gameObject.SetActive(false);
        }
        for (int i = 1; i < attackToHero.Length; i++)
        {
            attackToHero[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < bossBloods.Length; i++)
        {
            bossBloods[i].gameObject.SetActive(false);
        }
        
    }

    private void Update()
    {
        if (HP <= 0 && finalHit)
        {
            finalHit = false;
            StopAllCoroutines();

            UI_FinalHitTip.instance.gameObject.SetActive(true);
            Time.timeScale = 0.06f;
        }

        if (!isSkill) { AnimDire(); }

        distance = Mathf.Abs(transform.position.x - target.position.x);

        // Player is close to the Boss
        // And Being attacked by the player
        if (takeDamage)
        {
            takeDamage = false;
            if (enableEscape)
            {
                enableEscape = false;
                StartCoroutine(WaitForSkills(1));
            }
        }

        // Player starts to run away
        else if (distance > 18.0f && TargetActive)
        {
            if (enableChase && !takeDamage && !isSkill)
            {
                enableChase = false;
                StartCoroutine(WaitForSkills(2));
            }
        }

        // Attack when not being attacked and player is within a proper distance
        else if (!isSkill && enableChase && enableEscape && TargetActive)
        {
            if (enableSkill)
            {
                enableSkill = false;
                isSkill = true;
                if (floatCD4 <= 0)
                {
                    anim.SetBool("attack4", true);
                    floatCD4 = CD4;
                }
                else
                {
                    StartCoroutine(WaitForCD());
                }
            }
        }

        if (floatSkillCD > 0.0f)
        {
            floatSkillCD -= Time.deltaTime;
        }
        if (floatCD4 > 0.0f)
        {
            floatCD4 -= Time.deltaTime;
        }
        if (floatMoveCD > 0.0f)
        {
            floatMoveCD -= Time.deltaTime;
        }

    }

    private IEnumerator WaitForCD()
    {
        floatSkillCD = skillCD;
        yield return new WaitUntil(() => floatSkillCD <= 0);
        anim.SetBool(skills[Random.Range(0, 3)], true);
    }

    public IEnumerator WaitForSkills(int action)
    {
        switch (action)
        {
            case 1:
                yield return new WaitUntil(() => !isSkill);
                floatMoveCD = moveCD;
                yield return new WaitUntil(() => floatMoveCD <= 0);
                anim.SetBool("move", true);
                break;
            case 2:
                anim.SetBool("move", true);
                break;
        }
    }


    // Facing Character
    private void AnimDire()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > target.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < target.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    


    // Animation Events
    private void exitSkill()
    {
        isSkill = false;
        enableSkill = true;
        anim.SetBool("attack1", false);
        anim.SetBool("attack2", false);
        anim.SetBool("attack3", false);
        anim.SetBool("attack4", false);
    }
    private void exitMove()
    {
        enableEscape = true;
        enableChase = true;
        anim.SetBool("move", false);
    }

    private void move()
    {
        if (UI_PlayerHP.Instance.GoodEndSteps < 0)
        {
            if (target.position.x - transform.position.x > 0)
            {
                transform.position = new Vector3(target.position.x - Random.Range(20f, 20f),
                    transform.position.y,
                    transform.position.z);
            }
            else
            {
                transform.position = new Vector3(target.position.x + Random.Range(20f, 20f),
                    transform.position.y,
                    transform.position.z);
            }
            MidPoint.instance.ChangePosition();
            CamCont.instance.ChangeTarget(MidPoint.instance.transform);
        }
        else
        {
            // on the right
            if (rightMost - target.position.x < target.position.x - leftMost)
            {
                transform.position = new Vector3(target.position.x - Random.Range(8.5f, 12.5f),
                    transform.position.y,
                    transform.position.z);
            }
            // on the left
            else
            {
                transform.position = new Vector3(target.position.x + Random.Range(8.5f, 12.5f),
                    transform.position.y,
                    transform.position.z);
            }
        }
    }
    

    public void TakeInBegin()
    {
        StartCoroutine(Wait(anim, "takein", true, 0.19f));
    }

    private IEnumerator Wait(Animator a, string s, bool b, float f)
    {
        yield return new WaitForSeconds(f);
        a.SetBool(s, b);
    }

    public void TakeInFin()
    {
        camera.GetComponent<AudioSource>().PlayOneShot(battleMusics[0]);
        CamCont.instance.ChangeTarget(Player.PInstance.transform);
        Player.PInstance.EnablePlayer(1);
        Player.PInstance.enablePlayer = true;
        for (int i = 0; i < bossBloods.Length; i++)
        {
            bossBloods[i].gameObject.SetActive(true);
        }
        TargetActive = true;
    }

    private void DialogAfterTakein()
    {
        anim.SetBool("takein", false);
        UI_Dialog.Instance.dialog(GameManagerLevelTwo.Instance.GetDialogConf_Bug(0), 0);
    }

    // Story Mode

    public void DestroyHPSystem()
    {
        bossBlood.gameObject.SetActive(false);
        bossBloodAttack.gameObject.SetActive(true);
        bossBloodAttack.GetComponent<Animator>().SetTrigger("disappear");

        StartCoroutine(WaitNextAnim(0.05f));
    }

    private IEnumerator WaitNextAnim(float f = 0.0f, Animator a = null, string s = "")
    {
        yield return new WaitForSeconds(f);
        if (a != null)
        {
            a.SetTrigger(s);
        }
        if (bossBloodAttackRightToLeft.gameObject.activeSelf == false)
        {
            StartCoroutine(Wait(1.2f));
        }
    }

    private IEnumerator Wait(float f)
    {
        yield return new WaitForSeconds(f);

        bossBloodAttack.gameObject.SetActive(false);
        bossBloodAttackRightToLeft.gameObject.SetActive(true);
        StartCoroutine(WaitNextAnim(1.4f, bossBloodAttackRightToLeft.GetComponent<Animator>(), "attack"));
    }

    public void TakeInUSB()
    {
        anim.SetTrigger("takinUSB");
    }

    private void PlayerAnim_TakeInUSB()
    {
        Player.PInstance.animationCont.SetTrigger("takeInUSB");
    }








    // -------------------------------------------------------------------------------------------------------
    private void AttackOneEffects()
    {
        StartCoroutine(StartAttackOneEffect());
    }

    private IEnumerator StartAttackOneEffect()
    {
        for (int i = 1; i < attackOneEfs.Length; i++)
        {
            attackOneEfs[i].position = new Vector3(target.position.x, transform.position.y - 3.687f, transform.position.z);
            attackOneEfs[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1.2f);
        }
    }
    
    private void AttackFourSoundEffect()
    {
        AudioCont.instance.playSound("BugAttackFour");
    }

    private void AttackThreeSoundEffect()
    {
        AudioCont.instance.playSound("BugAttackThree");
    }

    private void AttackTwoSoundEffect()
    {
        AudioCont.instance.playSound("BugAttackTwo");
    }

    private void AttackFourSoundEffectBody()
    {
        AudioCont.instance.playSound("BugAttackFourBody");
    }
    private void AE_Flash_Sword_Begin()
    {
        AudioCont.instance.playSound("Flash_attack_sword_begin");
    }

    private void AttackTwoEffectsEnable1()
    {
        attackTwoEfs[1].gameObject.SetActive(true);
    }
    private void AttackTwoEffectsDisable1()
    {
        attackTwoEfs[1].gameObject.SetActive(false);
    }
    private void AttackTwoEffectsEnable2()
    {
        attackTwoEfs[2].gameObject.SetActive(true);
    }
    private void AttackTwoEffectsDisable2()
    {
        attackTwoEfs[2].gameObject.SetActive(false);
    }
    private void AttackTwoEffectsEnable3()
    {
        attackTwoEfs[3].gameObject.SetActive(true);
    }
    private void AttackTwoEffectsDisable3()
    {
        attackTwoEfs[3].gameObject.SetActive(false);
    }
    private void AttackThreeEffectsEnable1()
    {
        attackThreeEfs[1].gameObject.SetActive(true);
    }
    private void AttackThreeEffectsDisable1()
    {
        attackThreeEfs[1].gameObject.SetActive(false);
    }
    private void AttackThreeEffectsEnable2()
    {
        attackThreeEfs[2].gameObject.SetActive(true);
    }
    private void AttackThreeEffectsDisable2()
    {
        attackThreeEfs[2].gameObject.SetActive(false);
    }
    private void AttackThreeEffectsEnable3()
    {
        attackThreeEfs[3].gameObject.SetActive(true);
    }
    private void AttackThreeEffectsDisable3()
    {
        attackThreeEfs[3].gameObject.SetActive(false);
    }
    private void AttackThreeEffectsEnable4()
    {
        attackThreeEfs[4].gameObject.SetActive(true);
    }
    private void AttackThreeEffectsDisable4()
    {
        attackThreeEfs[4].gameObject.SetActive(false);
    }
    private void AttackThreeEffectsEnable5()
    {
        attackThreeEfs[5].gameObject.SetActive(true);
    }
    private void AttackThreeEffectsDisable5()
    {
        attackThreeEfs[5].gameObject.SetActive(false);
    }
    private void AttackThreeEffectsEnable6()
    {
        attackThreeEfs[6].gameObject.SetActive(true);
    }
    private void AttackThreeEffectsDisable6()
    {
        attackThreeEfs[6].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable1()
    {
        attackFourEfs[1].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable1()
    {
        attackFourEfs[1].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable2()
    {
        attackFourEfs[2].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable2()
    {
        attackFourEfs[2].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable3()
    {
        attackFourEfs[3].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable3()
    {
        attackFourEfs[3].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable4()
    {
        attackFourEfs[4].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable4()
    {
        attackFourEfs[4].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable5()
    {
        attackFourEfs[5].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable5()
    {
        attackFourEfs[5].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable6()
    {
        attackFourEfs[6].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable6()
    {
        attackFourEfs[6].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable7()
    {
        attackFourEfs[7].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable7()
    {
        attackFourEfs[7].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable8()
    {
        attackFourEfs[8].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable8()
    {
        attackFourEfs[8].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable9()
    {
        attackFourEfs[9].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable9()
    {
        attackFourEfs[9].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable10()
    {
        attackFourEfs[10].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable10()
    {
        attackFourEfs[10].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable11()
    {
        attackFourEfs[11].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable11()
    {
        attackFourEfs[11].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable12()
    {
        attackFourEfs[12].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable12()
    {
        attackFourEfs[12].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable13()
    {
        attackFourEfs[13].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable13()
    {
        attackFourEfs[13].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable14()
    {
        attackFourEfs[14].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable14()
    {
        attackFourEfs[14].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable15()
    {
        attackFourEfs[15].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable15()
    {
        attackFourEfs[15].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable16()
    {
        attackFourEfs[16].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable16()
    {
        attackFourEfs[16].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable17()
    {
        attackFourEfs[17].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable17()
    {
        attackFourEfs[17].gameObject.SetActive(false);
    }
    private void AttackFourEffectsEnable18()
    {
        attackFourEfs[18].gameObject.SetActive(true);
    }
    private void AttackFourEffectsDisable18()
    {
        attackFourEfs[18].gameObject.SetActive(false);
    }
    public void EnableAttackToTheHero()
    {
        attackToHero[1].gameObject.SetActive(true);
    }

}
