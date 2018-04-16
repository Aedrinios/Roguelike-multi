using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip[] sounds;
    public static SoundManager instance;


    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
       

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

    public static void playSound(string name) { 
        instance.GetComponent<AudioSource>().PlayOneShot(getSound(name));
    }
}
