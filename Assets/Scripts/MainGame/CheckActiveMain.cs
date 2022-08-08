using UnityEngine;
using System.Collections;

public class CheckActiveMain : MonoBehaviour {

    void OnEnable()
    {
        if (Modules.poolTerrains) Modules.poolTerrains.SetActive(true);
    }

    void OnDisable()
    {
        if (Modules.poolTerrains) Modules.poolTerrains.SetActive(false);
    }
}
