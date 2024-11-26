using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSoundManager : MonoBehaviour
{
    public static csSoundManager instance;

    AudioSource myAudio;
    public AudioClip halloweenHitSound;
    public AudioClip spaceShipHitSound;
    public AudioClip seaHitSound;
    public AudioClip caveHitSound;
    public AudioClip bearHitSound;
    public AudioClip wolfHitSound;
    public AudioClip stagHitSound;
    public AudioClip pigHitSound;
    public AudioClip birdHitSound;
    public AudioClip dragonHitSound;
    public AudioClip butterflyHitSound;
    public AudioClip moleHitSound;
    public AudioClip zombieHitSound;
    public AudioClip[] dinosaurHitSound;

    void Awake()
    {
        if (csSoundManager.instance == null)
        {
            csSoundManager.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudio = gameObject.GetComponent<AudioSource>();
    }

    public void PlayHalloweenHitSound()
    {
        myAudio.PlayOneShot(halloweenHitSound);
    }

    public void PlaySpaceShipHitSound()
    {
        myAudio.PlayOneShot(spaceShipHitSound);
    }

    public void PlaySeaHitSound()
    {
        myAudio.PlayOneShot(seaHitSound);
    }

    public void PlayCaveHitSound()
    {
        myAudio.PlayOneShot(caveHitSound);
    }

    public void PlayBearHitSound()
    {
        myAudio.PlayOneShot(bearHitSound);
    }

    public void PlayWolfHitSound()
    {
        myAudio.PlayOneShot(wolfHitSound);
    }

    public void PlayStagHitSound()
    {
        myAudio.PlayOneShot(stagHitSound);
    }

    public void PlayPigHitSound()
    {
        myAudio.PlayOneShot(pigHitSound);
    }

    public void PlayBirdHitSound()
    {
        myAudio.PlayOneShot(birdHitSound);
    }

    public void PlayDragonHitSound()
    {
        myAudio.PlayOneShot(dragonHitSound);
    }

    public void PlayButterflyHitSound()
    {
        myAudio.PlayOneShot(butterflyHitSound);
    }

    public void PlayMoleHitSound()
    {
        myAudio.PlayOneShot(moleHitSound);
    }

    public void PlayZombieHitSound()
    {
        myAudio.PlayOneShot(zombieHitSound);
    }

    public void PlayDinosaurHitSound()
    {
        int ran = Random.Range(0, 5);

        myAudio.PlayOneShot(dinosaurHitSound[ran]);
    }
}
