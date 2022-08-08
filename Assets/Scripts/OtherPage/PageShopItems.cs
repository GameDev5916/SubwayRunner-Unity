using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PageShopItems : MonoBehaviour {

    public Text textKey, textCoin, textHighScore;
    public GameObject panelBuyItem, panelUpgrades;

	public void CallStart () {
        ChangeAllLanguage();
        UpdateCoins();
        UpdateKeys();
        textHighScore.GetComponent<EffectUpScore>().StartEffect(Modules.totalScore);
	}

    public void UpdateCoins()
    {
        textCoin.text = Mathf.RoundToInt(Modules.totalCoin).ToString();
    }

    public void UpdateKeys()
    {
        textKey.text = Mathf.RoundToInt(Modules.totalKey).ToString();
    }

    public void ButtonBuyItemClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        panelBuyItem.SetActive(true);
        panelUpgrades.SetActive(false);
    }

    public void ButtonUpgradesClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        panelBuyItem.SetActive(false);
        panelUpgrades.SetActive(true);
    }

    public void ButtonPlayClick()
    {
        Modules.PlayAudioClipFree(Modules.audioTapPlay);
        Modules.autoTapPlay = true;
        Modules.containMainGame.SetActive(true);
        Modules.containShopItem.SetActive(false);
        if (Modules.statusGame == StatusGame.over)
            Modules.containMainGame.transform.Find("CamContent").GetComponentInChildren<PageMainGame>().ResetGame();
        else
            Modules.containMainGame.transform.Find("CamContent").GetComponentInChildren<PageMainGame>().CheckTapNow();
    }

    public void ButtonGoHomeClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.containMainGame.SetActive(true);
        Modules.containShopItem.SetActive(false);
        if (Modules.statusGame == StatusGame.over)
            Modules.containMainGame.transform.Find("CamContent").GetComponentInChildren<PageMainGame>().ResetGame();
    }

    public void ButtonRankClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.containAchievement.SetActive(true);
        Modules.containShopItem.SetActive(false);
        Modules.containAchievement.transform.Find("MainCamera").GetComponent<PageAchievement>().CallStart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonGoHomeClick();
        }
    }

    //xu ly ngon ngu
    public Text textBuyItemA, textBuyItemB, textUpgradeA, textUpgradeB;
    public Text textBuyCoins, textBuyKeys, textSingleUse;
    public Text textPlay;
    public List<Text> textBuy;
    public Text textHoverboard, textHoverboardNote, textChestBox, textChestBoxNote, textHeadStart, textHeadStartNote, textScoreBooster, textScoreBoosterNote;
    public Text textJetpack, textJetpackNote, textSuperSneaker, textSuperSneakerNote, textMagnet, textMagnetNote, textMultiplier, textMultiplierNote, textHoverbike, textHoverbikeNote, textHoverboardU, textHoverboardUNote;
    public List<GameObject> listSingleItems;

    public void ChangeAllLanguage()
    {
        int iLang = Modules.indexLanguage;
        textBuyItemA.font = AllLanguages.listFontLangA[iLang];
        textBuyItemA.text = AllLanguages.shopBuyItem[iLang];
        textBuyItemB.font = AllLanguages.listFontLangA[iLang];
        textBuyItemB.text = AllLanguages.shopBuyItem[iLang];
        textUpgradeA.font = AllLanguages.listFontLangA[iLang];
        textUpgradeA.text = AllLanguages.shopUpgrades[iLang];
        textUpgradeB.font = AllLanguages.listFontLangA[iLang];
        textUpgradeB.text = AllLanguages.shopUpgrades[iLang];
        textBuyCoins.font = AllLanguages.listFontLangA[iLang];
        textBuyCoins.text = AllLanguages.shopBuyCoin[iLang];
        textBuyKeys.font = AllLanguages.listFontLangA[iLang];
        textBuyKeys.text = AllLanguages.shopBuyKey[iLang];
        textSingleUse.font = AllLanguages.listFontLangA[iLang];
        textSingleUse.text = AllLanguages.shopSingleUse[iLang];
        textPlay.font = AllLanguages.listFontLangA[iLang];
        textPlay.text = AllLanguages.menuPlay[iLang];
        foreach (Text txt in textBuy) {
            txt.font = AllLanguages.listFontLangA[iLang];
            txt.text = AllLanguages.shopButtonBuy[iLang]; 
        }
        textHoverboard.font = AllLanguages.listFontLangA[iLang];
        textHoverboard.text = AllLanguages.shopHoverboard[iLang];
        textHoverboardNote.font = AllLanguages.listFontLangB[iLang];
        textHoverboardNote.text = AllLanguages.shopDetailHoverboard[iLang];
        textChestBox.font = AllLanguages.listFontLangA[iLang];
        textChestBox.text = AllLanguages.shopChestBox[iLang];
        textChestBoxNote.font = AllLanguages.listFontLangB[iLang];
        textChestBoxNote.text = AllLanguages.shopDetailChestbox[iLang];
        textHeadStart.font = AllLanguages.listFontLangA[iLang];
        textHeadStart.text = AllLanguages.shopHeadStart[iLang];
        textHeadStartNote.font = AllLanguages.listFontLangB[iLang];
        textHeadStartNote.text = AllLanguages.shopDetailHeadstart[iLang];
        textScoreBooster.font = AllLanguages.listFontLangA[iLang];
        textScoreBooster.text = AllLanguages.shopScoreBooster[iLang];
        textScoreBoosterNote.font = AllLanguages.listFontLangB[iLang];
        textScoreBoosterNote.text = AllLanguages.shopDetailScoreBooster[iLang];
        textJetpack.font = AllLanguages.listFontLangA[iLang];
        textJetpack.text = AllLanguages.shopJetpack[iLang];
        textJetpackNote.font = AllLanguages.listFontLangB[iLang];
        textJetpackNote.text = AllLanguages.shopDetailUpgrades[iLang] + " " + AllLanguages.shopJetpack[iLang];
        textSuperSneaker.font = AllLanguages.listFontLangA[iLang];
        textSuperSneaker.text = AllLanguages.shopSneaker[iLang];
        textSuperSneakerNote.font = AllLanguages.listFontLangB[iLang];
        textSuperSneakerNote.text = AllLanguages.shopDetailUpgrades[iLang] + " " + AllLanguages.shopSneaker[iLang];
        textMagnet.font = AllLanguages.listFontLangA[iLang];
        textMagnet.text = AllLanguages.shopCoinMagnet[iLang];
        textMagnetNote.font = AllLanguages.listFontLangB[iLang];
        textMagnetNote.text = AllLanguages.shopDetailUpgrades[iLang] + " " + AllLanguages.shopCoinMagnet[iLang];
        textMultiplier.font = AllLanguages.listFontLangA[iLang];
        textMultiplier.text = AllLanguages.shop2xMultiplier[iLang];
        textMultiplierNote.font = AllLanguages.listFontLangB[iLang];
        textMultiplierNote.text = AllLanguages.shopDetailUpgrades[iLang] + " " + AllLanguages.shop2xMultiplier[iLang];
        textHoverbike.font = AllLanguages.listFontLangA[iLang];
        textHoverbike.text = AllLanguages.shopHoverbike[iLang];
        textHoverbikeNote.font = AllLanguages.listFontLangB[iLang];
        textHoverbikeNote.text = AllLanguages.shopDetailUpgrades[iLang] + " " + AllLanguages.shopHoverbike[iLang];
        textHoverboardU.font = AllLanguages.listFontLangA[iLang];
        textHoverboardU.text = AllLanguages.shopHoverboard[iLang];
        textHoverboardUNote.font = AllLanguages.listFontLangB[iLang];
        textHoverboardUNote.text = AllLanguages.shopDetailUpgrades[iLang] + " " + AllLanguages.shopHoverboard[iLang];
        foreach (GameObject go in listSingleItems) go.GetComponent<BuyItemController>().LoadDataNow();
    }
}
