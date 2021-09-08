using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip dead, hitStone, whooze1, whooze2, whooze3, choppingWood;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        hitStone = Resources.Load<AudioClip>("hitStone");
        dead = Resources.Load<AudioClip>("Boss hit 1");
        whooze1 = Resources.Load<AudioClip>("whoozeMalee1");
        whooze2 = Resources.Load<AudioClip>("whoozeMalee2");
        whooze3 = Resources.Load<AudioClip>("whoozeMalee3");
        choppingWood = Resources.Load<AudioClip>("AxeChopWood");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "dead":
                audioSrc.PlayOneShot(dead);
                break;
            case "whooze":
                var ranWhooze = Random.Range(1, 3);
                if (ranWhooze == 1)
                {
                    audioSrc.PlayOneShot(whooze1);
                }
                else
                {
                    audioSrc.PlayOneShot(whooze3);
                }
                
                break;
            case "hitStone":
                audioSrc.PlayOneShot(hitStone);
                break;
            case "choppingWood":
                audioSrc.PlayOneShot(choppingWood);
                break;
        }


    }
}
