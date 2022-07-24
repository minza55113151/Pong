using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        audioSource.Play();
        audioSource.loop = true;
    }

}
