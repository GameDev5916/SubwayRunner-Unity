using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageResultOnline : MonoBehaviour {

    public Text textButOkay;

    public void UpdateLanguages()
    {
        int iLang = Modules.indexLanguage;
        textButOkay.font = AllLanguages.listFontLangA[iLang];
        textButOkay.text = AllLanguages.playBonusButton[iLang];
    }
    public void ShowMessageBox()
    {
        UpdateLanguages();
    }
    public void ButtonOkayClick()
    {
        Modules.CheckShowAds();
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
}
