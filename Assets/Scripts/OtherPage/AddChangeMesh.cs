using UnityEngine;
using System.Collections;

public class AddChangeMesh : MonoBehaviour {

    public GameObject objectCheck;
    void Start()
    {
        Instantiate(objectCheck, transform);
    }
}
