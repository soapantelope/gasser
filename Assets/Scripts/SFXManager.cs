using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private Dictionary<string, AudioSource> sounds;

    void Awake()
    {
        sounds = new Dictionary<string, AudioSource>();

        foreach (AudioSource sound in gameObject.GetComponents<AudioSource>()) {
            sounds.Add(sound.clip.name, sound);
        }
    }

    public void play(string name) {
        sounds[name].Play();
    }

    public void stop(string name) {
        sounds[name].Pause();
    }
}
