using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0, 256)]
    public int priority;
    [Range(0f, 5f)]
    public float volume;
    [Range(-3f, 3f)]
    public float pitch;
    [Range(-1, 1)]
    public float stereoPan;
    [Range(0, 1)]
    public float spatialBlend;
    [Range(0, 1.1f)]
    public float reverbZoneMix;

    public bool loop;
    public bool playOnAwake;
    public bool bypassReverbZones;



    [HideInInspector]
    public AudioSource source;
}
