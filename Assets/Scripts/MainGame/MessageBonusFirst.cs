using UnityEngine;
using System.Collections;

public class MessageBonusFirst : MonoBehaviour {

    public void ButtonCloseBox()
    {
        transform.gameObject.SetActive(false);
        Modules.HandleGameOver();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }
}
