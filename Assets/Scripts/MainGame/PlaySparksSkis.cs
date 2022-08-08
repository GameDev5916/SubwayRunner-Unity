using UnityEngine;
using System.Collections;

public class PlaySparksSkis : MonoBehaviour {

    public ParticleSystem parLeft, parRight;

    public void PlayParticle(bool leftSide)
    {
        if (leftSide)
        {
            parLeft.Stop();
            parLeft.Play();
        }
        else
        {
            parRight.Stop();
            parRight.Play();
        }
    }
}
