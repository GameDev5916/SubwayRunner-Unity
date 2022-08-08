using UnityEngine;
using System.Collections;

public class CallAniRandom : MonoBehaviour {

    public Animator myAnimator;
    public string nameTrigger = "TriPlay";
    public Vector2 timeRandom = new Vector2(1, 5);//tinh bang giay

    void Start()
    {
        Invoke("PlayAnimation", Random.Range(timeRandom.x, timeRandom.y));
    }

    void PlayAnimation()
    {
        if (transform.gameObject.activeSelf && transform.parent.gameObject.activeSelf && Modules.containMainGame.activeSelf)
            myAnimator.SetTrigger(nameTrigger);
        Invoke("PlayAnimation", Random.Range(timeRandom.x, timeRandom.y));
    }
}
