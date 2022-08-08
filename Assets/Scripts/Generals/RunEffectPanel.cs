using UnityEngine;
using System.Collections;

public class RunEffectPanel : MonoBehaviour {

    public bool startOpen = true;
    public float timeRun = 1.1f;
    void Start()
    {
        if (startOpen)
        {
            transform.GetComponent<Animator>().SetTrigger("TriOpen");
            Destroy(gameObject, timeRun);
        }
    }

    public void CallClose()
    {
        transform.GetComponent<Animator>().SetTrigger("TriClose");
    }
}