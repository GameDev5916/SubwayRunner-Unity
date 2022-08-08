using UnityEngine;
using System.Collections;

public class RotateSkyBox : MonoBehaviour {

    public Material myMatSky;
    public float speed = 1f;

    void Update()
    {
        if (Modules.reducedEffect < 2)
        {
            myMatSky.SetFloat("_Rotation", Time.time * speed);
        }
    }
}
