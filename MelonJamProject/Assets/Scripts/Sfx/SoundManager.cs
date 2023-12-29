using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _EnemyDieSound;

    [SerializeField] private AudioSource _playerDieSound;

    [SerializeField] private AudioSource _IcePotionCrashSound;

    public void EnemyDieSound()
    {
        _EnemyDieSound.Play();
    }

    public void PlayerDieSound()
    {
        _playerDieSound.Play();  
    }

    public void IcePotionCrashSound()
    {
        _IcePotionCrashSound.Play();
    }
}
