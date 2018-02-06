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
	
	// Update is called once per frame
	void Update () {
		
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
}
