using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound [] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;

            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.Loop;
        }
    }
    private void Start()
    {
        Play("DiveAttack");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
