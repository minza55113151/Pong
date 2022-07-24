using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitAudioClip;
    [SerializeField] private AudioClip TNTAudioClip;

    private PhotonView pv;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        pv = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
    }
    [PunRPC]
    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitAudioClip);
    }
    [PunRPC]
    public void PlayTNTSound()
    {
        audioSource.PlayOneShot(TNTAudioClip);
    }


}
