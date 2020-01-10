using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsEffectHelper : MonoBehaviour
{
    public static SoundsEffectHelper instance;
    public AudioClip explosionSound;
    public AudioClip playerShotSound;
    public AudioClip enemyShotSound;

    void Awake()
    {
      if(instance != null)
        {
            Debug.Log("too much objects");
        }
        instance = this;  
    }

    public void makeExplosionSound()
    {
        makeSound(explosionSound);
    }

    public void makePlayerShotSound()
    {
        makeSound(playerShotSound);
    }

    public void makeEnemyShotSound()
    {
        makeSound(enemyShotSound);
    }

    private void makeSound(AudioClip sound)
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }
}
