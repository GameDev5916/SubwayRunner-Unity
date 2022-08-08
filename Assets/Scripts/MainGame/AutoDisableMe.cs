using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoDisableMe : MonoBehaviour {

    public Image imageCheck;
    void FixedUpdate()
    {
        if (imageCheck.sprite != null)
            gameObject.SetActive(false);
    }
}
