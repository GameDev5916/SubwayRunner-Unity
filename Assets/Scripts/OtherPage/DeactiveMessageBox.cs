using UnityEngine;
using System.Collections;

public class DeactiveMessageBox : MonoBehaviour {

	public void DeactiveBox()
    {
        transform.gameObject.SetActive(false);
    }

    public void PlayAudioShow()
    {
        Modules.PlayAudioClipFree(Modules.audioShowMessage);
    }
}
