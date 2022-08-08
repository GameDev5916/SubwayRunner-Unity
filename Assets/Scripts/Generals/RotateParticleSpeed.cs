using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParticleSpeed : MonoBehaviour {

    private float numChange = 15f;
	void Update () {
        float num = Screen.width / Screen.height;
        float angle = (-120 - numChange) + (1 / 0.6f * num * numChange);
        if (angle < -135) angle = -135;
        if (angle > -110) angle = -110;
        transform.eulerAngles = new Vector3(0, angle, 0);
	}
}
