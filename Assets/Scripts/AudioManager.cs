using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource, sfxSource;
    [SerializeField] AudioClip[] musicList, sfxList;
    // Start is called before the first frame update
    void Start()
    {
        if (musicSource != null)
        {
            playSongs(0);
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void playSongs(int id)
    {
        musicSource.clip = musicList[id];
        musicSource.Play();
    }
    public void PlaySFX(int id)
    {
        
            sfxSource.clip = sfxList[id];
            sfxSource.Play();
    }
}
