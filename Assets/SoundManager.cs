using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;
    public AudioSource audioSource;
    public AudioClip shoot;

	private void Awake()
	{
        instance = this;
	}

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(shoot);
    }
}
