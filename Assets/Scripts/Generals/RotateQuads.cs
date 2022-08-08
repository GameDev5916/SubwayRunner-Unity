using UnityEngine;
using System.Collections;

public class RotateQuads : MonoBehaviour {

    public float speedRotate = 100f;
    void FixedUpdate()
    {
        transform.Rotate(0, 0, speedRotate * Time.fixedDeltaTime);
    }
}
