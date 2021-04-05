using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class DoorSoundCaller : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.bypassReverbZones = s.bypassReverbZones;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.priority = s.priority;
            s.source.panStereo = s.stereoPan;
            s.source.spatialBlend = s.spatialBlend;
            s.source.reverbZoneMix = s.reverbZoneMix;
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

    public void DoorOpenSFX()
    {
        Play("DoorOpen");
    }

    public void DoorCloseSFX()
    {
        Play("DoorClose");
        Debug.Log("hej");
    }

    public void DoorSlamSFX()
    {
        Play("DoorSlam");
    }
}
