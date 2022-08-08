using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunEffectViewEnemy : MonoBehaviour {

    public Text textMessage;
    public Color colorWin = Color.green, colorLose = Color.red;
    public AudioClip audioEffect;
    public Image avatarEnemy;
    public Image imageCheck;
    public GameObject imageLoading;
    private Animator myAni;
    private bool statusRun = false;
    private GameObject modelResultOnline;

    public void ResetView()
    {
        avatarEnemy.sprite = Modules.fbEnemyAvatar;
        if (avatarEnemy.sprite == null) avatarEnemy.sprite = Modules.iconAvatarNull;
        Animator myAni = transform.GetComponent<Animator>();
        myAni.ResetTrigger("TriWin");
        myAni.ResetTrigger("TriLose");
        myAni.SetTrigger("TriClose");
        imageCheck.sprite = null;
        imageLoading.SetActive(true);
        statusRun = false;
    }

    public void StartView(bool gameWin)
    {
        if (statusRun) return;
        statusRun = true;
        Animator myAni = transform.GetComponent<Animator>();
        if (gameWin)
        {
            myAni.ResetTrigger("TriLose");
            myAni.ResetTrigger("TriClose");
            myAni.SetTrigger("TriWin");
            textMessage.text = "YOU\nWIN";
            textMessage.color = colorWin;
            Modules.winNow++;
            mesWin = true;
            stringTitle = "";
            bonusCoins = 0;
            if (Modules.totalWin < 100)
            {
                Modules.totalWin++;
                if (Modules.totalWin >= 30)//so lan win dot 3
                {
                    int totalNow = 0;
                    if (Modules.bonusWin < 1)//chua thuong dot nao
                        totalNow = 16000;
                    else if (Modules.bonusWin < 2)//neu da thuong dot 1
                        totalNow = 15000;
                    else if (Modules.bonusWin < 3)//neu da thuong dot 1, 2
                        totalNow = 10000;
                    if (totalNow > 0)
                    {
                        Modules.bonusWin = 3;
                        stringTitle = AllLanguages.playWinXTimes[Modules.indexLanguage].Replace("?", "30");
                        bonusCoins = totalNow;
                    }
                }
                else if (Modules.totalWin >= 10)
                {
                    int totalNow = 0;
                    if (Modules.bonusWin < 1)//chua thuong dot nao
                        totalNow = 6000;
                    else if (Modules.bonusWin < 2)//neu da thuong dot 1
                        totalNow = 5000;
                    if (totalNow > 0)
                    {
                        Modules.bonusWin = 2;
                        stringTitle = AllLanguages.playWinXTimes[Modules.indexLanguage].Replace("?", "10");
                        bonusCoins = totalNow;
                    }
                }
                else if (Modules.totalWin >= 3)
                {
                    int totalNow = 0;
                    if (Modules.bonusWin < 1)//chua thuong dot nao
                        totalNow = 1000;
                    if (totalNow > 0)
                    {
                        Modules.bonusWin = 1;
                        stringTitle = AllLanguages.playWinXTimes[Modules.indexLanguage].Replace("?", "3");
                        bonusCoins = totalNow;
                    }
                }
                Modules.SaveBonusWinFirst();
            }
        }
        else
        {
            myAni.ResetTrigger("TriWin");
            myAni.ResetTrigger("TriClose");
            myAni.SetTrigger("TriLose");
            textMessage.text = "YOU\nLOSE";
            textMessage.color = colorLose;
            Modules.loseNow++;
            mesWin = false;
        }
        Modules.PlayAudioClipFree(audioEffect);
    }

    private bool mesWin = true;
    private string stringTitle = "";
    private int bonusCoins = 0;
    public void HideEffect()
    {
        gameObject.SetActive(false);
        Modules.panelHighScoreNow.SetActive(true);
        //hien thi hieu ung va dung game
        if (mesWin)
        {
            Modules.mainCharacter.GetComponent<HeroController>().SetStopGame();
            Camera.main.GetComponent<PageMainGame>().containUIPlay.SetActive(false);
            Modules.ShowMessageWinLose(0, stringTitle, bonusCoins);
            if (modelResultOnline != null) Destroy(modelResultOnline);
            modelResultOnline = Instantiate(Modules.mainCharacter.GetComponent<HeroController>().modelIdelWin, Modules.parentResultOnline.transform) as GameObject;
            modelResultOnline.transform.localPosition = Vector3.zero;
            modelResultOnline.transform.localEulerAngles = Vector3.zero;
            modelResultOnline.transform.localScale = new Vector3(1, 1, 1);
            Modules.parentResultOnline.GetComponent<ColorBGWinLose>().SetColor(true);
            Modules.parentResultOnline.SetActive(true);
            Modules.resultOnlineBox.SetActive(true);
            Modules.resultOnlineBox.GetComponent<MessageResultOnline>().ShowMessageBox();
        }
        else
        {
            Camera.main.GetComponent<PageMainGame>().containUIPlay.SetActive(false);
            Modules.ShowMessageWinLose(1);
            if (modelResultOnline != null) Destroy(modelResultOnline);
            modelResultOnline = Instantiate(Modules.mainCharacter.GetComponent<HeroController>().modelIdelLose, Modules.parentResultOnline.transform) as GameObject;
            modelResultOnline.transform.localPosition = Vector3.zero;
            modelResultOnline.transform.localEulerAngles = Vector3.zero;
            modelResultOnline.transform.localScale = new Vector3(1, 1, 1);
            Modules.parentResultOnline.GetComponent<ColorBGWinLose>().SetColor(false);
            Modules.parentResultOnline.SetActive(true);
            Modules.resultOnlineBox.SetActive(true);
            Modules.resultOnlineBox.GetComponent<MessageResultOnline>().ShowMessageBox();
        }
        Modules.startViewOnline = false;
    }
}
