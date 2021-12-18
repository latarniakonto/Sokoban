using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;

    [SerializeField] [Range(0f, 1f)] private float volume;

    [SerializeField] private float pitch;

    private AudioSource source;

    private bool wasPlayed = false;
    public void SetSource(AudioSource source) => this.source = source;

    public AudioSource GetSource() => this.source;

    public void SetClip() => this.source.clip = clip;
    public void SetVolume() => this.source.volume = volume;
    public void SetPitch() => this.source.pitch = pitch;

    public string GetName() => this.name;

    public void SetWasPlayed(bool played) => wasPlayed = played;

    public bool GetWasPlayed() => wasPlayed;
    
    
}
