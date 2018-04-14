using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiqueManager : MonoBehaviour {
    public AudioSource audioS; 
    public AudioClip[] sounds;
    public static musiqueManager instance;
    
    // Use this for initialization
    void Start () {
       playSound("GloomyForestWav");
    }

    void Awake()
    {
        instance = this;
    }

    public static AudioClip getSound(string name)
    {
        for (int i = 0; i < instance.sounds.Length; i++)
        {
            if (instance.sounds[i].name == name)
            {
                return instance.sounds[i];
            }
        }
        return new AudioClip();
    }

    public static void playSound(string name)
    {
        AudioClip clip = getSound(name);
        instance.GetComponent<AudioSource>().clip = clip;
        instance.GetComponent<AudioSource>().Play();
    }

    public static void stopSound(string name)
    {
        AudioClip clip = getSound(name);
        instance.GetComponent<AudioSource>().clip = clip;
        instance.GetComponent<AudioSource>().Stop();
    }
}
