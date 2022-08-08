using UnityEngine;
using System.Collections;

public class CheckUseSkis : MonoBehaviour {

    public GameObject boneControllSkis;
    public GameObject meshShowSkis;
    void FixedUpdate()
    {
        //if (Modules.statusGame != StatusGame.play) return;
        if (boneControllSkis.transform.childCount > 0 || Modules.totalSkis <= 0)
        {
            if (meshShowSkis.activeSelf)
                meshShowSkis.SetActive(false);
        }
        else
        {
            if (!meshShowSkis.activeSelf)
                meshShowSkis.SetActive(true);
        }
    }
}
