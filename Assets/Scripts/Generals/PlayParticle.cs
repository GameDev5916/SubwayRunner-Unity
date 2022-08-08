using UnityEngine;
using System.Collections;

public class PlayParticle : MonoBehaviour {

    public AudioClip audioPlay;
    
    public bool IsPlay()
    {
        bool result = false;
        result =  transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying;
        return result;
    }

    public void Play(bool isCoins = false)
    {
        Modules.PlayAudioClipFree(audioPlay, isCoins);
        foreach(Transform tran in transform)
            tran.GetComponent<ParticleSystem>().Play();
    }
}
