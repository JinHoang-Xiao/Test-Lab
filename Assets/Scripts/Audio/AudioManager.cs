using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource defaultAudioSource;
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip energyClip;

    public void PlayShootSound()
    {
        effectAudioSource.PlayOneShot(shootClip);
    }
    public void PlayReloadSound()
    {
        effectAudioSource.PlayOneShot(reloadClip);
    }
    public void PlayEnergySound()
    {
        effectAudioSource.PlayOneShot(energyClip);
    }    
    public void PlayDefaultAudio()
    {
        bossAudioSource.Stop();
        defaultAudioSource.Play();
    }   
    public void PlayBossAudio()
    {
        bossAudioSource.Play();
        defaultAudioSource.Stop();
    }
    public void StopAudio()
    {
        bossAudioSource.Stop();
        defaultAudioSource.Stop();
        effectAudioSource.Stop();
    }    
}
