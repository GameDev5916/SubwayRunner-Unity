using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CooldownKey : MonoBehaviour {

    public int secondsCooldown = 15;
    public Image runProgress;
    public Text textNumMenu;
    public GameObject messageBox;
    private bool allowClick = false;
    //xu ly ngon ngu
    public Text textTitle, textInfo, textStatus;

    public void ShowMessageBox()
    {
        //xu ly ngon ngu
        int iLang = Modules.indexLanguage;
        textTitle.font = AllLanguages.listFontLangA[iLang];
        textTitle.text = AllLanguages.menuGetKeys[iLang];
        textInfo.font = AllLanguages.listFontLangB[iLang];
        textInfo.text = AllLanguages.menuNoteKeys[iLang];
        textStatus.font = AllLanguages.listFontLangA[iLang];
        if (runProgress.fillAmount >= 1)
            textStatus.text = AllLanguages.menuClickView[Modules.indexLanguage];
        else
            textStatus.text = AllLanguages.menuPleaseWait[Modules.indexLanguage];
        //xu ly khac
        InvokeRepeating("UpdateTime", 0, 1);
    }

    void UpdateTime()
    {
        DateTime dateTimeNow = DateTime.Now;
        DateTime dateTimeOld = Modules.LoadOldDateTime("GetKey");
        double totalSeconds = (dateTimeNow - dateTimeOld).TotalSeconds;
        if (totalSeconds < 0)//neu thoi gian am, chung to nguoi dung doi gio nguoc lai.
        {
            //thuc hien xoa sach keys hien co
            Modules.totalKey = 0;
            Modules.SaveKey();
            textNumMenu.text = Modules.totalKey.ToString();
            Modules.SaveNewDateTime("GetKey");
            ButtonCloseClick();
            return;
        }
        float percent = (float)totalSeconds / (float)secondsCooldown;
        if (percent >= 1)
        {
            percent = 1;
            allowClick = true;
            textStatus.text = AllLanguages.menuClickView[Modules.indexLanguage];
        }
        else
        {
            allowClick = false;
            textStatus.text = AllLanguages.menuPleaseWait[Modules.indexLanguage];
        }
        runProgress.fillAmount = percent;
    }

    public void ButtonCloseClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        CancelInvoke("UpdateTime");
        messageBox.GetComponent<Animator>().SetTrigger("TriClose");
    }

    public void ButtonViewAdsClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        if (!allowClick) return;
        Modules.SaveNewDateTime("GetKey");
        Modules.itemBonusViewAds = "Key";
#if (UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
        ADSController.Instance.RequestRewardBasedVideo(true, CallReward);
#endif
    }

    private void CallReward(bool obj)
    {
        if (obj) Modules.RewardKeysSkis();
    }
}
