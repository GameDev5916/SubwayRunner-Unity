using UnityEngine;
using System.Collections;

public class RotateZModels : MonoBehaviour {

    public float speedRotate = 100f;
    public bool startStatus = false;
    public Vector3 originAngle = Vector3.zero;

    public void StartRotate()
    {
        startStatus = true;
    }

    public void StopRotate()
    {
        startStatus = false;
        transform.eulerAngles = originAngle;
    }

    void FixedUpdate()
    {
        if (!startStatus) return;
        transform.Rotate(0, 0, speedRotate * Time.fixedDeltaTime);
    }
}
