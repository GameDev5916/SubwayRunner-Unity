using UnityEngine;
using System.Collections;

public class PlayAudioOnEnable : MonoBehaviour {

    void OnEnable()
    {
        Modules.PlayAudioClipFree(Modules.audioBonusText);
    }
}
