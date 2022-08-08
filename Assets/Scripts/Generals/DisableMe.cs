using UnityEngine;
using System.Collections;

public class DisableMe : MonoBehaviour {

    public void DisableObjects()
    {
        transform.gameObject.SetActive(false);
    }
}
