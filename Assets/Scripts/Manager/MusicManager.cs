using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public AudioSource musicAudioSource;
    public AudioSource soundAudioSource;

    public AudioClip stageBgm;
    public AudioClip getBuff;
    public AudioClip clickButton;

    // Start is called before the first frame update
    void Start()
    {
        stageBgm = Resloader.Load<AudioClip>("Music/stageBgm");
        getBuff = Resloader.Load<AudioClip>("Music/getBuff");
        clickButton = Resloader.Load<AudioClip>("Music/clickButton");
        clickButton = Resloader.Load<AudioClip>("Music/getHeal");
        clickButton = Resloader.Load<AudioClip>("Music/playerOpenFire");
        clickButton = Resloader.Load<AudioClip>("Music/enemyOpenFire");
        clickButton = Resloader.Load<AudioClip>("Music/getHit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(string name)
    {
        AudioClip clip = Resloader.Load<AudioClip>(name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlayMusic : {0} not existed", name);
            return;
        }
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }

        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarningFormat("PlayMusic : {0} not existed", clip.name);
            return;
        }
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }

        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
    public void StopMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
    }

    public void PlaySound(string name)
    {
        AudioClip clip = Resloader.Load<AudioClip>(name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlaySound : {0} not existed", name);
            return;
        }
        soundAudioSource.PlayOneShot(clip);
    }

}
