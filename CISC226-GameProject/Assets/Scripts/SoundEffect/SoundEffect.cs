using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private void AE_DestroyHPsystemAlarm()
    {
        AudioCont.instance.playSound("DestroyHPSys");
    }
    private void AE_BossHPAttack()
    {
        AudioCont.instance.playSound("BugHPAttack");
    }

    private void AE_TeleportSoundEffect()
    {
        AudioCont.instance.playSound("Teleport");
    }

    private void AE_Bug_Twitch()
    {
        AudioCont.instance.playSound("BugTwich");
    }

    private void AE_Bug_Voice()
    {
        AudioCont.instance.playSound("Bug_Voice");
    }
}
