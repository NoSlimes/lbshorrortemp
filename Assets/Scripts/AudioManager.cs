using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if (LevelHandler.GameIsPaused == true)
            {
                s.source.volume = 0.5f;
            } 
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: \"" + name + "\" was not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop()
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "was stopped!");
            return;
        }
        s.source.Stop();
    }

    private void Start()
    {
        Play("Theme");
        Play("carDoorStart");
    }

    private void Update()
    {
        
       
    }
}
