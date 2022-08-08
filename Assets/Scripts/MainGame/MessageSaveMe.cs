using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageSaveMe : MonoBehaviour {

    public Image iconCooldown;
    public Text textKey, totalTextKey;
    public int timeCooldown = 3;//tinh bang giay
    private bool mesShow = false;
    private int totalTime = 0;
    private int tunTime = 0;
    public GameObject butShareVideo;
    //xu ly ngon ngu
    public Text textSaveMe, textButUpVideo;
    
    public void StartShowMessage()
    {
        Modules.SetStatusButShareVideo(butShareVideo);
        int iLang = Modules.indexLanguage;
        textSaveMe.font = AllLanguages.listFontLangA[iLang];
        textSaveMe.text = AllLanguages.playSaveMe[iLang];
        textButUpVideo.font = AllLanguages.listFontLangA[iLang];
        textButUpVideo.text = AllLanguages.menuUpVideo[iLang];
        totalTextKey.font = AllLanguages.listFontLangA[iLang];
        totalTextKey.text = Modules.totalKey.ToString();
        textKey.text = Mathf.Pow(2, Modules.timeSaveMe).ToString();
        iconCooldown.fillAmount = 1f;
        totalTime = Modules.SecondsToTimePerFrame(timeCooldown);
        tunTime = 0;
        mesShow = true;
    }

    public void ButtonSaveMeClick()
    {
        if (tunTime >= totalTime) return;
        Modules.PlayAudioClipFree(Modules.audioButton);
        //xu ly kiem tra du key chua, neu du thi thuc thi hoi sinh, khong thi hien bang mua
        if (!Modules.HandleReborn())//neu khong du thi hien thi bang mua key
        {
            Modules.mesNotEnoughKey.SetActive(true);
            Modules.mesNotEnoughKey.GetComponent<MessageBuyKeys>().StartShowMessage();
        }
        transform.gameObject.GetComponent<Animator>().SetTrigger("TriClose");
        tunTime = 0;
        mesShow = false;
    }

    public void PanelOutsideClick()
    {
        if (Modules.bonusFirst > 0) Modules.HandleGameOver();
        else Modules.ShowBonusFirst();
        transform.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (mesShow)
        {
            if (tunTime >= totalTime)
            {
                mesShow = false;
                if (Modules.bonusFirst > 0) Modules.HandleGameOver();
                else Modules.ShowBonusFirst();
                transform.gameObject.SetActive(false);
            }
            else
            {
                tunTime++;
                iconCooldown.fillAmount = 1f - (float)tunTime / (float)totalTime;
            }
        }
    }
}
