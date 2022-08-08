using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessagePauseGame : MonoBehaviour {

    public GameObject countTimeResume;
    public GameObject effectCount;
    public GameObject butShareVideo;
    //xu ly ngon ngu
    public Text textSetting, textGoHome, textButUpVideo;

    public void UpdateLanguages()
    {
        int iLang = Modules.indexLanguage;
        textSetting.font = AllLanguages.listFontLangA[iLang];
        textSetting.text = AllLanguages.playSetting[iLang];
        textGoHome.font = AllLanguages.listFontLangA[iLang];
        textGoHome.text = AllLanguages.playGoHome[iLang];
        textButUpVideo.font = AllLanguages.listFontLangA[iLang];
        textButUpVideo.text = AllLanguages.menuUpVideo[iLang];
    }
    public void ShowMessageBox()
    {
        Modules.SetStatusButShareVideo(butShareVideo);
        UpdateLanguages();
        Time.timeScale = 0;
    }
    public void ButtonCloseClick()
    {
        transform.gameObject.GetComponent<Animator>().SetTrigger("TriClose");
        countTimeResume.SetActive(true);
        MessageTimeCount mesCountTime = countTimeResume.GetComponent<MessageTimeCount>();
        mesCountTime.effectTimeShow = effectCount;
        mesCountTime.StartCount();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonHomeClick()
    {
        Modules.CheckShowAds();
		Time.timeScale = 1;
        Modules.UpdateIndexRunTerrain();
		if (Modules.scorePlayer > Modules.totalScore)
		{
			Modules.totalScore = Modules.scorePlayer;
			Modules.SaveScore();
		}
        Modules.totalCoin += Modules.coinPlayer;
        Modules.SaveCoin();
        transform.gameObject.SetActive(false);
        Modules.autoTapPlay = false;
        Camera.main.GetComponent<PageMainGame>().ResetGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (transform.gameObject.activeSelf) ButtonCloseClick();
        }
    }
}
