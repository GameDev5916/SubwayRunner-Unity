using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PageConstructHero : MonoBehaviour {

    public Text textKey, textCoin, textHighScore;
    public GameObject panelBuyHero, panelBuySkis;
    public Text textValueHero, textValueSkis, textNoteHero, textNoteSkis;
    public GameObject contentHero, contentSkis;
    public GameObject itemTempHero, itemTempSkis;
    public Vector3 pointShowHero, pointShowSkis, pointShowThumHero, pointShowThumSkis;
    public Vector3 scaleShowHero, scaleShowSkis, scaleShowThumHero, scaleShowThumSkis;
    public Color colorError = Color.red;
    public List<float> valueScrollHero = new List<float>();
    public List<float> valueScrollSkis = new List<float>();
    public List<AudioClip> audioHero = new List<AudioClip>();
    public Image iconHeroSale, iconSkisSale;
    private Color colorOriginButBuyHero = Color.white;
    private Color colorOriginButBuySkis = Color.white;
    private string valueOriginButBuyHero = "10000";
    private string valueOriginButBuySkis = "10000";

    public void CallStart()
    {
        ChangeAllLanguage();
        colorOriginButBuyHero = textValueHero.color;
        colorOriginButBuySkis = textValueSkis.color;
        textKey.text = Mathf.RoundToInt(Modules.totalKey).ToString();
        textCoin.text = Mathf.RoundToInt(Modules.totalCoin).ToString();
        codeHeroChoose = Modules.codeHeroUse;
        codeSkisChoose = Modules.codeSkisUse;
        textHighScore.GetComponent<EffectUpScore>().StartEffect(Modules.totalScore);
        ButtonBuyHeroClick(false);
    }

    private void PlayAudioHero(AudioClip audioPlay)
    {
        GetComponent<AudioSource>().clip = audioPlay;
        GetComponent<AudioSource>().Play();
    }

    private GameObject heroChooseShow, skisChooseShow;
    private string codeHeroChoose = "001", codeSkisChoose = "001";
    void LoadHeroChoose()
    {
        if (heroChooseShow != null) Destroy(heroChooseShow);
        for (int i = 0; i < Modules.listHeroUse.Count; i++)
        {
            if (Modules.listHeroUse[i].GetComponent<HeroController>().idHero == codeHeroChoose)
            {
                heroChooseShow = Instantiate(Modules.listHeroUse[i], pointShowHero, Quaternion.identity) as GameObject;
                Modules.SetLayer(heroChooseShow.gameObject, "MCG-Hero");
                heroChooseShow.transform.parent = Modules.containHeroConstruct.transform;
                heroChooseShow.transform.localScale = scaleShowHero;
                heroChooseShow.transform.eulerAngles = new Vector3(0, 180, 0);
                HeroController heroNow = heroChooseShow.GetComponent<HeroController>();
                heroNow.SetupShowMenu(2);
                heroNow.CallAniMenu(heroNow.aniIdleMenu, 1f);
                if (heroNow.iconSale != null)
                {
                    iconHeroSale.sprite = heroNow.iconSale;
                    iconHeroSale.color = new Color(1, 1, 1, 1);
                }
                else iconHeroSale.color = new Color(1, 1, 1, 0);
                textNoteHero.text = AllLanguages.heroInfoHero[heroNow.noteHero][Modules.indexLanguage];
                textValueHero.text = heroNow.costHero.ToString();
                if (Modules.codeHeroUse == codeHeroChoose)
                    textValueHero.text = AllLanguages.heroSelected[Modules.indexLanguage];
                else if (Modules.listHeroUnlock.Contains(codeHeroChoose))
                    textValueHero.text = AllLanguages.heroUnlocked[Modules.indexLanguage];
                valueOriginButBuyHero = textValueHero.text;
                PlayAudioHero(audioHero[i]);
                break;
            }
        }
    }

    void LoadSkisChoose()
    {
        for (int i = 0; i < Modules.listSkisUse.Count; i++)
        {
            SkisController skisCon = Modules.listSkisUse[i].GetComponent<SkisController>();
            if (skisCon.idSkis == codeSkisChoose)
            {
                heroChooseShow.transform.parent = Modules.containHeroConstruct.transform;
                heroChooseShow.transform.position = pointShowSkis;
                heroChooseShow.transform.localScale = scaleShowSkis;
                heroChooseShow.transform.eulerAngles = new Vector3(20, 180, 0);
                HeroController heroNow = heroChooseShow.GetComponent<HeroController>();
                heroNow.mySkis = Modules.listSkisUse[i];
                heroNow.SetupShowMenu(2);
                heroNow.CallAniMenu(heroNow.aniRunSkis, 1f);
                if (skisCon.iconSale != null)
                {
                    iconSkisSale.sprite = skisCon.iconSale;
                    iconSkisSale.color = new Color(1, 1, 1, 1);
                }
                else iconSkisSale.color = new Color(1, 1, 1, 0);
                Modules.RemoveModelUseItem(heroChooseShow.transform, "Skis");
                Modules.SetModelUseItem(heroChooseShow.transform, heroNow.codeBody, heroNow.mySkis, "Skis");
                Modules.SetLayer(heroChooseShow.gameObject, "MCG-Hero");
                foreach (GameObject go in heroNow.listObjectHide) go.gameObject.SetActive(false);
                //skisCon.CallAniMenu(skisCon.aniRun, 1f);
                textNoteSkis.text = AllLanguages.heroInfoHoverboard[skisCon.noteSkis][Modules.indexLanguage];
                textValueSkis.text = skisCon.costSkis.ToString();
                if (Modules.codeSkisUse == codeSkisChoose)
                    textValueSkis.text = AllLanguages.heroSelected[Modules.indexLanguage];
                else if (Modules.listSkisUnlock.Contains(codeSkisChoose))
                    textValueSkis.text = AllLanguages.heroUnlocked[Modules.indexLanguage];
                valueOriginButBuySkis = textValueSkis.text;
                break;
            }
        }
    }

    void LoadListHero()
    {
        foreach (Transform tran in contentHero.transform) Destroy(tran.gameObject);
        mesLoadListHero = true;
        runLoadListHero = 0;
    }

    void LoadListSkis()
    {
        foreach (Transform tran in contentSkis.transform) Destroy(tran.gameObject);
        mesLoadListSkis = true;
        runLoadListSkis = 0;
    }

    private int indexChooseHero = 0;
    private bool mesLoadListHero = false;
    private int runLoadListHero = 0;
    private int indexChooseSkis = 0;
    private bool mesLoadListSkis = false;
    private int runLoadListSkis = 0;
    void FixedUpdate()
    {
        //load list hero tuan tu
        if (mesLoadListHero)
        {
            if (runLoadListHero < Modules.listHeroUse.Count)
            {
                string idNow = Modules.listHeroUse[runLoadListHero].GetComponent<HeroController>().idHero;
                GameObject newItem = Instantiate(itemTempHero.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
                newItem.transform.SetParent(contentHero.transform, false);
                newItem.GetComponent<ChangeImageClick>().codeObject = idNow;
                GameObject heroChoose = Instantiate(Modules.listHeroUse[runLoadListHero], pointShowThumHero, Quaternion.identity) as GameObject;
                heroChoose.transform.localScale = scaleShowThumHero;
                heroChoose.transform.eulerAngles = new Vector3(0, 180, 0);
                HeroController heroNow = heroChoose.GetComponent<HeroController>();
                heroNow.SetupShowMenu(2);
                heroNow.CallAniMenu(heroNow.aniIdleMenu, 1f);
                Image iconSaleSmall = newItem.transform.GetChild(0).GetComponent<Image>();
                if (heroNow.iconSale != null)
                {
                    iconSaleSmall.sprite = heroNow.iconSale;
                    iconSaleSmall.color = new Color(1, 1, 1, 1);
                }
                else iconSaleSmall.color = new Color(1, 1, 1, 0);
                heroChoose.transform.SetParent(newItem.transform, false);
                if (idNow == codeHeroChoose) { newItem.GetComponent<ChangeImageClick>().OpenImageFront(); indexChooseHero = runLoadListHero; }
                else newItem.GetComponent<ChangeImageClick>().CloseImageFront();
                runLoadListHero++;
            }
            else
            {
                mesLoadListHero = false;
                //dieu chinh scroll bar toi nhan vat
                Vector2 oldPoint = contentHero.GetComponent<RectTransform>().anchoredPosition;
                contentHero.GetComponent<RectTransform>().anchoredPosition = new Vector3(valueScrollHero[indexChooseHero], oldPoint.y);
            }
        }
        //load list skis tuan tu
        if (mesLoadListSkis)
        {
            if (runLoadListSkis < Modules.listSkisUse.Count)
            {
                string idNow = Modules.listSkisUse[runLoadListSkis].GetComponent<SkisController>().idSkis;
                GameObject newItem = Instantiate(itemTempSkis.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
                newItem.transform.SetParent(contentSkis.transform, false);
                newItem.GetComponent<ChangeImageClick>().codeObject = idNow;
                GameObject skisChoose = Instantiate(Modules.listSkisUse[runLoadListSkis], pointShowThumSkis, Quaternion.identity) as GameObject;
                skisChoose.transform.localScale = scaleShowThumSkis;
                skisChoose.transform.eulerAngles = new Vector3(50, -215, -10);
                SkisController skisNow = skisChoose.GetComponent<SkisController>();
                RotateZModels rotateZ = skisChoose.AddComponent<RotateZModels>();
                rotateZ.originAngle = skisChoose.transform.eulerAngles;
                //skisNow.CallAniMenu(skisNow.aniRun, 1f);
                Image iconSaleSmall = newItem.transform.GetChild(0).GetComponent<Image>();
                if (skisNow.iconSale != null)
                {
                    iconSaleSmall.sprite = skisNow.iconSale;
                    iconSaleSmall.color = new Color(1, 1, 1, 1);
                }
                else iconSaleSmall.color = new Color(1, 1, 1, 0);
                skisNow.transform.SetParent(newItem.transform, false);
                if (idNow == codeSkisChoose) { newItem.GetComponent<ChangeImageClick>().OpenImageFront(); indexChooseSkis = runLoadListSkis; }
                else newItem.GetComponent<ChangeImageClick>().CloseImageFront();
                runLoadListSkis++;
            }
            else
            {
                mesLoadListSkis = false;
                //dieu chinh scroll bar toi hoverboard
                Vector2 oldPoint = contentSkis.GetComponent<RectTransform>().anchoredPosition;
                contentSkis.GetComponent<RectTransform>().anchoredPosition = new Vector3(valueScrollSkis[indexChooseSkis], oldPoint.y);
            }
        }
    }

    public void ButtonHeroClick(string codeHero)
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        codeHeroChoose = codeHero;
        LoadHeroChoose();
        ResetButtonListHero(codeHero);
    }

    public void ButtonSkisClick(string codeSkis)
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        codeSkisChoose = codeSkis;
        LoadSkisChoose();
        ResetButtonListSkis(codeSkis);
    }

    private void ResetButtonListHero(string codeActive)
    {
        foreach (Transform tran in contentHero.transform)
        {
            if (tran.GetComponent<ChangeImageClick>().codeObject != codeActive)
                tran.GetComponent<ChangeImageClick>().CloseImageFront();
        }
    }

    private void ResetButtonListSkis(string codeActive)
    {
        foreach (Transform tran in contentSkis.transform)
        {
            if (tran.GetComponent<ChangeImageClick>().codeObject != codeActive)
                tran.GetComponent<ChangeImageClick>().CloseImageFront();
        }
    }

    private bool showErrorHero = false;
    void ShowMessageErrorHero(string textError)
    {
        showErrorHero = true;
        textValueHero.text = textError;
        textValueHero.color = colorError;
        Invoke("ResetValueOriginHero", 1f);
    }

    void ResetValueOriginHero()
    {
        showErrorHero = false;
        textValueHero.text = valueOriginButBuyHero;
        textValueHero.color = colorOriginButBuyHero;
    }

    private bool showErrorSkis = false;
    void ShowMessageErrorSkis(string textError)
    {
        showErrorSkis = true;
        textValueSkis.text = textError;
        textValueSkis.color = colorError;
        Invoke("ResetValueOriginSkis", 1f);
    }

    void ResetValueOriginSkis()
    {
        showErrorSkis = false;
        textValueSkis.text = valueOriginButBuySkis;
        textValueSkis.color = colorOriginButBuySkis;
    }

    public void ButtonCoinHeroClick()
    {
        if (showErrorHero) return;
        Modules.PlayAudioClipFree(Modules.audioButton);
        if (!Modules.listHeroUnlock.Contains(codeHeroChoose))
        {
            int cost = Modules.IntParseFast(textValueHero.text);
            if (Modules.totalCoin >= cost)//neu du tien mua
            {
                Modules.totalCoin -= cost;
                Modules.SaveCoin();
                textCoin.text = Modules.totalCoin.ToString();
                textValueHero.text = AllLanguages.heroSelected[Modules.indexLanguage];
                Modules.listHeroUnlock.Add(codeHeroChoose);
                Modules.SaveListHeroUnlock();
                Modules.codeHeroUse = codeHeroChoose;
                Modules.statusGame = StatusGame.over;
            }
            else
                ShowMessageErrorHero(AllLanguages.heroNotEnough[Modules.indexLanguage]);
        }
        else
        {
            textValueHero.text = AllLanguages.heroSelected[Modules.indexLanguage];
            Modules.codeHeroUse = codeHeroChoose;
            Modules.SaveBodyHero();
            Modules.statusGame = StatusGame.over;
        }
    }

    public void ButtonCoinSkisClick()
    {
        if (showErrorSkis) return;
        Modules.PlayAudioClipFree(Modules.audioButton);
        if (!Modules.listSkisUnlock.Contains(codeSkisChoose))
        {
            int cost = Modules.IntParseFast(textValueSkis.text);
            if (Modules.totalCoin >= cost)//neu du tien mua
            {
                Modules.totalCoin -= cost;
                Modules.SaveCoin();
                textCoin.text = Modules.totalCoin.ToString();
                textValueSkis.text = AllLanguages.heroSelected[Modules.indexLanguage];
                Modules.listSkisUnlock.Add(codeSkisChoose);
                Modules.SaveListSkisUnlock();
                Modules.codeSkisUse = codeSkisChoose;
                Modules.statusGame = StatusGame.over;
            }
            else
                ShowMessageErrorSkis(AllLanguages.heroNotEnough[Modules.indexLanguage]);
        }
        else
        {
            textValueSkis.text = AllLanguages.heroSelected[Modules.indexLanguage];
            Modules.codeSkisUse = codeSkisChoose;
            Modules.SaveSkisHero();
            Modules.statusGame = StatusGame.over;
        }
    }

    public void ButtonBuyHeroClick(bool playAudio = true)
    {
        if (playAudio) Modules.PlayAudioClipFree(Modules.audioButton);
        panelBuyHero.SetActive(true);
        panelBuySkis.SetActive(false);
        LoadListHero();
        LoadHeroChoose();
    }

    public void ButtonBuySkisClick(bool playAudio = true)
    {
        if (playAudio) Modules.PlayAudioClipFree(Modules.audioButton);
        panelBuyHero.SetActive(false);
        panelBuySkis.SetActive(true);
        LoadListSkis();
        LoadSkisChoose();
    }

    public void ButtonPlayClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.autoTapPlay = true;
        Modules.containMainGame.SetActive(true);
        Modules.containHeroConstruct.SetActive(false);
        if (Modules.statusGame == StatusGame.over)
            Modules.containMainGame.transform.Find("CamContent").GetComponentInChildren<PageMainGame>().ResetGame();
        else Modules.containMainGame.transform.Find("CamContent").GetComponentInChildren<PageMainGame>().CheckTapNow();
    }

    public void ButtonGoHomeClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.containMainGame.SetActive(true);
        Modules.containHeroConstruct.SetActive(false);
        if (Modules.statusGame == StatusGame.over)
            Modules.containMainGame.transform.Find("CamContent").GetComponentInChildren<PageMainGame>().ResetGame();
    }

    public void ButtonRankClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.containAchievement.SetActive(true);
        Modules.containHeroConstruct.SetActive(false);
        Modules.containAchievement.transform.Find("MainCamera").GetComponent<PageAchievement>().CallStart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ButtonGoHomeClick();
    }

    //xu ly ngon ngu
    public Text textCharactersA, textCharactersB, textHoverboardA, textHoverboardB;
    public Text textPlay;
    public void ChangeAllLanguage()
    {
        int iLang = Modules.indexLanguage;
        textPlay.font = AllLanguages.listFontLangA[iLang];
        textPlay.text = AllLanguages.menuPlay[iLang];
        textCharactersA.font = AllLanguages.listFontLangA[iLang];
        textCharactersA.text = AllLanguages.heroCharacters[iLang];
        textCharactersB.font = AllLanguages.listFontLangA[iLang];
        textCharactersB.text = AllLanguages.heroCharacters[iLang];
        textHoverboardA.font = AllLanguages.listFontLangA[iLang];
        textHoverboardA.text = AllLanguages.heroHoverboard[iLang];
        textHoverboardB.font = AllLanguages.listFontLangA[iLang];
        textHoverboardB.text = AllLanguages.heroHoverboard[iLang];
        //update font khac
        textNoteHero.font = AllLanguages.listFontLangB[iLang];
        textNoteSkis.font = AllLanguages.listFontLangB[iLang];
        textValueHero.font = AllLanguages.listFontLangA[iLang];
        textValueSkis.font = AllLanguages.listFontLangA[iLang];
    }
}
