using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private List<AudioClip> _clips;


    public void EnemyDieSound()
    {
        _audioSource.PlayOneShot(_clips[0]);
    }

    public void PlayerDieSound()
    {
        _audioSource.PlayOneShot(_clips[1]);
    }

    public void IcePotionCrashSound()
    {
        _audioSource.PlayOneShot(_clips[2]);
    }

    public void HealUsed() {
        _audioSource.PlayOneShot(_clips[3]);
    }

    public void PlayerDamaged() {
        _audioSource.PlayOneShot(_clips[4]);
    }
}
