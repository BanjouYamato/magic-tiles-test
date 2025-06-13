using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseSingleton<AudioManager>
{
    [SerializeField] AudioSource source;

    public AudioClip LoseClip, TileClip;

    public void PlaySoundEffect(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    public AudioSource GetSource() => source;
}
