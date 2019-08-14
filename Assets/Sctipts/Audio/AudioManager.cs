using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    

    public Sound[] sounds;

    private void Awake() {
        Instance = this;

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    private void Start() {
        if(SceneManager.GetActiveScene().buildIndex != 0)
            Play("Theme");
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        s.isPlaying = true;
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        s.isPlaying = false;
    }

    public bool isPlaying(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.isPlaying;
    }

    
}
