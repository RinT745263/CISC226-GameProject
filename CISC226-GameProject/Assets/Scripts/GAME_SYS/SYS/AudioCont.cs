using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound {

    public string typeName;
    public AudioClip[] m_clips;

    [Range(0f, 1f)] public float volume = 1.0f;
    [Range(0f, 1f)] public float pitch = 1.0f;

    private AudioSource audSour;

    public void setAS(AudioSource AS) {
        audSour = AS;
        audSour.clip = m_clips[Random.Range(0, m_clips.Length - 1)];
    }

    public void Play() {
        if (m_clips.Length > 1) {
            audSour.clip = m_clips[Random.Range(0, m_clips.Length-1)];
        }
        audSour.volume = volume;
        audSour.pitch = pitch;
        audSour.PlayOneShot(audSour.clip); ;
    }
}

public class AudioCont : MonoBehaviour
{
    public static AudioCont instance;

    [SerializeField] Sound[] sounds;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++) {
            GameObject obj = new GameObject(sounds[i].typeName);
            obj.transform.SetParent(transform);
            sounds[i].setAS(obj.AddComponent<AudioSource>());
        }

    }

    public void playSound(string n) {

        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].typeName == n) {
                sounds[i].Play();
                return;
            }
        }

    }

}
