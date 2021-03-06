﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private bool isOn;

    private int currentScene;





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

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {

        
        if (PlayerPrefs.HasKey("GameSound")) {
            isOn = PlayerPrefs.GetInt("GameSound") == 1 ? true : false;
        }
        else {
            isOn = true;
        }

        if (isOn)
            Play("Theme_" + SceneManager.GetActiveScene().buildIndex);
        else
            AudioListener.volume = 0;
        
        currentScene = SceneManager.GetActiveScene().buildIndex;

        //if (SceneManager.GetActiveScene().buildIndex != 0)
       //     Play("Theme");
    }

    private void Update() {
        if(SceneManager.GetActiveScene().buildIndex != currentScene) {
            Play("Theme_" + SceneManager.GetActiveScene().buildIndex);
            Stop("Theme_" + currentScene);
            currentScene = SceneManager.GetActiveScene().buildIndex;
        }
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

    public void Mute(string name, bool isMute) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.mute = isMute;
        
    }

    
}
