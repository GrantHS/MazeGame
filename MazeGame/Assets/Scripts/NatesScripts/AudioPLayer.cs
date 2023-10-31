using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPLayer : MonoBehaviour
{
    public AudioSource ChickenNoise;

    private bool isPlayingSound = false;
   
    public AudioClip keyPressSound;

    private void Start()
    {


        if (ChickenNoise == null)
        {
            ChickenNoise = GetComponent<AudioSource>();
        }

        if (keyPressSound == null)
        {
            Debug.LogError("Key press sound is not assigned.");
            enabled = false;
        }
    }

    private void Update()
    {
      
            if (!isPlayingSound)
            {
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
                {
                    isPlayingSound = true;
                    ChickenNoise.PlayOneShot(keyPressSound);
                    StartCoroutine(TurnOffSoundAfterDelay(3f)); // Turn off sound after 0.5 seconds.
                }
            }
        
    }

    private IEnumerator TurnOffSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPlayingSound = false;
    }
}
