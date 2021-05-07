using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public static NPC instance;

    public AudioSource BGM;
    public Transform player_Teleport_USB;
    public Transform bug_Teleportation_White;
    public Transform[] NPCs;

    private AudioClip[] BGMs;
    private bool nextMusic;

    private void Awake()
    {
        instance = this;
        NPCs = transform.GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        nextMusic = false;
        BGMs = Resources.LoadAll<AudioClip>("BGM/Bug");
    }

    private void Update()
    {
        if (nextMusic && !BGM.isPlaying)
        {
            nextMusic = false;
            BGM.loop = true;
            BGM.PlayOneShot(BGMs[2]);
        }
    }


    public void EnableAllNPCs()
    {
        StartCoroutine(WaitForSecondsEnableNPC());
    }

    private IEnumerator WaitForSecondsEnableNPC()
    {

        yield return new WaitForSeconds(4f);

        bug_Teleportation_White.gameObject.SetActive(true);
        bug_Teleportation_White.gameObject.GetComponent<Animator>().SetTrigger("start");
        
        float p = Player.PInstance.transform.position.x;
        float b = Bug.instance.transform.position.x;

        for (int i = 1; i < NPCs.Length; i += 2)
        {
            yield return new WaitForSeconds(0.9f);

            NPCs[i].position = new Vector3(Random.Range(p > b ? b + 10f : p + 2f, p > b ? p - 2f : b - 10f), -6.189f);
            NPCs[i].gameObject.SetActive(true);
            NPCs[i].gameObject.GetComponent<Animator>().SetTrigger("start");
            if (p > b)
            {
                NPCs[i+1].rotation = Quaternion.Euler(0f,0f,0f);
            }
            NPCs[i+1].gameObject.SetActive(true);
        }
        BGM.loop = false;

        BGM.PlayOneShot(BGMs[1]);
        nextMusic = true;

        UI_PlayerHP.Instance.GoodEndSteps = 10000000;
        UI_Dialog.Instance.dialog(GameManagerLevelTwo.Instance.GetDialogConf_Bug(4), 0);
    }

    public void DisableAllNPCs()
    {
        StartCoroutine(WaitForSecondsDisableNPC());
    }

    private IEnumerator WaitForSecondsDisableNPC()
    {
        
        for (int i = 1; i < NPCs.Length; i += 2)
        {
            NPCs[i].GetComponent<Animator>().SetTrigger("start");
            yield return new WaitForSeconds(0.7f);
        }

        bug_Teleportation_White.GetComponent<Animator>().SetTrigger("start");

        player_Teleport_USB.gameObject.SetActive(true);

        UI_PlayerHP.Instance.inBattle = false;
        UI_PlayerHP.Instance.EnableSonicHeal(0.1f, 0.05f);
        
        // Start Battle
        Player.PInstance.damage = 5;
        Player.PInstance.flashDamage = 2.5f;
        Player.PInstance.enablePlayer = true;

        yield return new WaitForSeconds(1.3f);

        Player.PInstance.EnablePlayer(1);
        
        CamCont.instance.ChangeTarget(Player.PInstance.transform);
    }
}
