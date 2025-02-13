using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------------")] [SerializeField]
    private AudioSource SFXSource;

    public AudioClip getEle;
    public AudioClip walk;
    public AudioClip getHit;
    public AudioClip heal;
    public AudioClip jump;
    public AudioClip enemyDead;
    public AudioClip shoot;
    public AudioClip enemyHit;
    public AudioClip button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
       SFXSource.PlayOneShot(clip);
    }
}
