using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip onHover, onClick;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover()
    {
        audioSource.PlayOneShot(onHover);
    }

    public void Click()
    {
        audioSource.PlayOneShot(onClick);
    }
}
