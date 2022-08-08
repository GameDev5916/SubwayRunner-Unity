using UnityEngine;
using System.Collections;

public class MessageBuyKeys : MonoBehaviour
{
    public void StartShowMessage()
    {
        Camera.main.GetComponent<PageMainGame>().UpdateNeedKeys((int)Mathf.Pow(2, Modules.timeSaveMe));
        Camera.main.GetComponent<PageMainGame>().UpdateKeys();
    }

    public void ButtonCancel()
    {
        if (Modules.bonusFirst > 0) Modules.HandleGameOver();
        else Modules.ShowBonusFirst();
        transform.gameObject.SetActive(false);
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonApply()
    {
        if (!Modules.HandleReborn())
        {
            if (Modules.bonusFirst > 0) Modules.HandleGameOver();
            else Modules.ShowBonusFirst();
            Modules.PlayAudioClipFree(Modules.audioButton);
        }
        transform.gameObject.SetActive(false);
    }
}
