using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioClip menuMusic;
    public AudioClip stage1Music;
    public AudioClip stage2Music;
    public AudioClip stage3Music;
    public AudioClip enemyDestroySound;
    public AudioClip stageClearSound;
    
    public void PlayBackgroundMusic(AudioClip music)
    {
        backgroundMusic.clip = music;
        backgroundMusic.Play();
    }

    public void PlayEnemyDestroySound(AudioSource source)
    {
        source.PlayOneShot(enemyDestroySound, 0.5f);
    }

    public void PlayStageClearSound(AudioSource source)
    {
        source.PlayOneShot(stageClearSound, 0.2f);
    }
}
