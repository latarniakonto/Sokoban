using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private Sound[] sounds;
    private AudioSource managerAudio = null;

    private static SoundManager instance;
    void Awake() 
    {     
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(var sound in sounds)
        {
            sound.SetSource(gameObject.AddComponent<AudioSource>());
            sound.SetClip();
            sound.SetVolume();
            sound.SetPitch();
        }
    }

    void Update() 
    {
        AudioSource mainSong = sounds[7].GetSource();
        if(!mainSong.isPlaying)   
        {
            mainSong.Play();
        }
    }
    public void Play(string name)
    {
        if(name == "Walking")
        {
            foreach(var sound in sounds)
            {
                if(sound.GetName() == name && !sound.GetWasPlayed())
                {
                    if(managerAudio == null || (!managerAudio.isPlaying))
                    {
                        managerAudio = sound.GetSource();
                        Debug.Log(managerAudio.clip.name);
                        managerAudio.Play();
                        sound.SetWasPlayed(true);
                    }                    
                    return;
                }
            }
            foreach(var sound in sounds)
            {
                if(sound.GetName() == name)
                {                    
                    sound.SetWasPlayed(false);                    
                }
            }
        }

        foreach(var sound in sounds)
        {
            if(sound.GetName() == name)
            {
                if(managerAudio == null || (!managerAudio.isPlaying || name == "Acknowledge"))
                {
                    managerAudio = sound.GetSource();
                    Debug.Log(managerAudio.clip.name);
                    managerAudio.Play();
                }
                return;
            }
        }        
    }    
}
