using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiqueManager : MonoBehaviour {

    public AudioClip[] sounds;
    public static musiqueManager instance;
    
    // Use this for initialization
    void Start () {
        musiqueManager.playSound("GloomyForest");
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
        instance.GetComponent<AudioSource>().Play();
    }
}
