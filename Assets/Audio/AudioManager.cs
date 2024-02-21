using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource testSound;

    void Start()
    {

        testSound.Play();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
