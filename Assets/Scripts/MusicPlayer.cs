using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips; 
    private AudioSource audioSource;
    private int currentClipIndex = -1; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomClip();
    }

    void FixedUpdate()
    {
        if (audioSource && !audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    void PlayRandomClip()
    {
        if (clips.Length == 0) 
            return; 
        currentClipIndex = Random.Range(0, clips.Length);
        audioSource.clip = clips[currentClipIndex];
        audioSource.Play();
    }

    void PlayNextClip()
    {
        if (clips.Length == 0) 
            return;

        currentClipIndex = (currentClipIndex + 1) % clips.Length;
        audioSource.clip = clips[currentClipIndex];
        audioSource.Play();
    }
}
