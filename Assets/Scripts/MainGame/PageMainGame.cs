using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PageMainGame : MonoBehaviour {

    public GameObject containMainGame, containHeroConstruct, containAchievement, containShopItem, containHighScore, containOpenBox;
    public GameObject containUIMenu, containUIPlay;
    public Text textTotalSkis, textTotalKeys, textCoinPlay, textScorePlay, textXPointPlay, textHighScore;
    public GameObject containMesItems, containButtonBuy, containEffectAddPoint;
    public GameObject panelShowEatItemsLeft, panelShowEatItemsRight, panelShowBuyItems, panelEffectAddPoint, panelHighScoreNow, panelViewEnemy, panelGameGuide;
    public GameObject panelMissions, panelChallenge, panelBonus, panelCrackGlass, panelFakeCity;
    public GameObject enemyLeft;
    public GameObject itemShoeLeft, itemShoeRight, itemMagnet, itemRocket, itemCable;
    public Vector3 pointStartEnemyLeft = Vector3.zero, pointStartEnemyRight = Vector3.zero;
    public GameObject mesSaveMeBox, mesPauseGame, mesCountTime,
        mesNotEnoughKey, mesSetting, getSkisBox,
        getKeysBox, missionsBox, challengeBox,
        listLanguageBox, rateBox, shareFBBox,
        inviteFBBox, networkBox, roomBox,
        bonusFirstBox, findOpponentsBox, resultOnlineBox;
    public GameObject panelBGEffectBonus, panelTextEffectBonus, panelTextEffectWinLose;
    public GameObject parSpeedFly, parReborn, parSkisCollider;
    public GameObject parentResultOnline;
    //xu ly intro game
    public GameObject parentCam;
    public GameObject policeCar;
    public Vector3 pointPoliceCar = Vector3.zero;
    //xu ly hieu ung tang thuong skis va keys
    public GameObject parentSkis, parentKey;
    public Text textSkis, textKey, textNeedKeys, textTotalKeySave;
    public GameObject butShareVideo;
    //xu ly online mode
    public GameObject buttonPause;

    void Awake()
    {
        originSwipeDistX = minSwipeDistX;
        originSwipeDistY = minSwipeDistY;
        //gan lai cac thiet lap vao modules
        Modules.containMainGame = containMainGame;
        Modules.containHeroConstruct = containHeroConstruct;
        Modules.containAchievement = containAchievement;
        Modules.containShopItem = containShopItem;
        Modules.containHighScore = containHighScore;
        Modules.containOpenBox = containOpenBox;
        Modules.textCoinPlay = textCoinPlay;
        Modules.textScorePlay = textScorePlay;
        Modules.textXPointPlay = textXPointPlay;
        Modules.textHighScore = textHighScore;
        Modules.containMesItems = containMesItems;
        Modules.containButtonBuy = containButtonBuy;
        Modules.containEffectAddPoint = containEffectAddPoint;
        Modules.panelShowEatItemsLeft = panelShowEatItemsLeft;
        Modules.panelShowEatItemsRight = panelShowEatItemsRight;
        Modules.panelEffectAddPoint = panelEffectAddPoint;
        Modules.panelHighScoreNow = panelHighScoreNow;
        Modules.panelViewEnemy = panelViewEnemy;
        Modules.panelGameGuide = panelGameGuide;
        Modules.panelMissions = panelMissions;
        Modules.panelChallenge = panelChallenge;
        Modules.panelBonus = panelBonus;
        Modules.panelCrackGlass = panelCrackGlass;
        Modules.panelBGEffectBonus = panelBGEffectBonus;
        Modules.panelTextEffectBonus = panelTextEffectBonus;
        Modules.panelTextEffectWinLose = panelTextEffectWinLose;
        Modules.parSpeedFly = parSpeedFly;
        Modules.parReborn = parReborn;
        Modules.parSkisCollider = parSkisCollider;
        Modules.mesSaveMeBox = mesSaveMeBox;
        Modules.mesNotEnoughKey = mesNotEnoughKey;
        Modules.itemShoeLeft = itemShoeLeft;
        Modules.itemShoeRight = itemShoeRight;
        Modules.itemMagnet = itemMagnet;
        Modules.itemRocket = itemRocket;
        Modules.itemCable = itemCable;
        Modules.bonusFirstBox = bonusFirstBox;
        Modules.resultOnlineBox = resultOnlineBox;
        Modules.parentResultOnline = parentResultOnline;
        Modules.panelFakeCity = panelFakeCity;
        ChangeAllLanguage();
    }

    void Start()
    {
        InvokeRepeating("EffectAddSkisKeys", 2, 2);
        ResetGame();
        Modules.UpdateValueSensitivity();
    }

    public void ResetGame()
    {
        //System.GC.Collect();//giai thoat ram
#if (UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
        ADSController.Instance.RequestInterstitial(false);
#endif
        Modules.parSpeedFly.GetComponent<ParticleSystem>().Stop();
        Modules.SetStatusButShareVideo(butShareVideo);
        ButtonStopRecord();
        rateBox.SetActive(false);
        containUIMenu.SetActive(true);
        containUIPlay.SetActive(false);
        Modules.textCoinPlay.text = "0";
        Modules.PlayAudioClipLoop(Modules.audioBackgrond, transform);
        //chinh lai camera
        Animator ani = transform.GetComponent<Animator>();
        ani.SetTrigger("TriReset");
        ani.enabled = true;
        //xu ly an hien cac page trong game main
        Modules.containMainGame.SetActive(true);
        Modules.containHeroConstruct.SetActive(false);
        Modules.containAchievement.SetActive(false);
        Modules.containShopItem.SetActive(false);
        Modules.containHighScore.SetActive(false);
        Modules.containOpenBox.SetActive(false);
        transform.GetComponent<MissionsController>().StartShowMessage(false);
        transform.GetComponent<ChallengeController>().StartShowMessage(false);
        //dieu khien trang thai trong man game
        textTotalSkis.text = Modules.totalSkis.ToString();
        textTotalKeys.text = Modules.totalKey.ToString();
        Modules.ResetGame();
        Camera.main.GetComponent<TerrainController>().ResetTerrain();
        Camera.main.GetComponent<CameraController>().CallStart();
        Camera.main.GetComponent<ChangeHeightFog>().ResetValue();
        Camera.main.GetComponent<ChangeHeightFog>().enabled = false;
        CreateCharacters();
        CheckTapNow();
    }

    public void CheckTapNow()
    {
        if (Modules.autoTapPlay)
        {
            Modules.autoTapPlay = false;
            ClickPlayGame(false);
        }
        else
        {
            if (Modules.totalUseGame >= 3 && Modules.clickRate == 0)
                Invoke("ShowRateBox", 1);
        }
    }

    void ShowRateBox()
    {
        if (Modules.statusGame == StatusGame.menu
             && Modules.containMainGame.activeSelf
             && containUIMenu.activeSelf
             && CheckNoMessageShow())
            ButtonRateClick();
    }

    private Vector2 startPos;
    private Vector2 oldTap = new Vector2(-1000, -1000);
    public float minSwipeDistY = 50f;
    public float minSwipeDistX = 50f;
    [HideInInspector]
    public float originSwipeDistY = 50f;
    [HideInInspector]
    public float originSwipeDistX = 50f;
    private bool firstSwipe = false;
    void ResetDoubleTap()
    {
        oldTap = new Vector2(-1000, -1000);
    }

    public void InputButtonDown()
    {
        //xu ly click hoverboard
        if (Vector2.Distance(oldTap, Input.mousePosition) <= 10 * (Screen.width / Modules.KTCScenes.x))
        {
            Modules.mainCharacter.GetComponent<HeroController>().UseSkis();
            ResetDoubleTap();
        }
        else
        {
            oldTap = Input.mousePosition;
            Invoke("ResetDoubleTap", 0.25f);
        }
        //xu ly swipe
        startPos = Input.mousePosition;
        firstSwipe = true;
    }

    public void InputButtonUp()
    {
        oldSwipe = "";
        firstSwipe = false;
    }

    private string oldSwipe = "";
    public void InputButtonStay()
    {
        if (!firstSwipe) return;
        float swipeDistHorizontal = Mathf.Abs(Input.mousePosition.x - startPos.x);
        float swipeDistVertical = Mathf.Abs(Input.mousePosition.y - startPos.y);
        if (swipeDistHorizontal > swipeDistVertical)//vuot ngang
        {
            if (swipeDistHorizontal > minSwipeDistX * (float)Screen.width / Modules.KTCScenes.x)
            {
                float swipeValue = Input.mousePosition.x - startPos.x;
                if (swipeValue > 0)
                {
                    bool checkMoreMove = false;
                    if (oldSwipe == "") checkMoreMove = true;
                    oldSwipe = "right";
                    Modules.mainCharacter.GetComponent<HeroController>().MoveRight(checkMoreMove);
                    firstSwipe = false;
                }
                else if (swipeValue < 0)
                {
                    bool checkMoreMove = false;
                    if (oldSwipe == "") checkMoreMove = true;
                    oldSwipe = "left";
                    Modules.mainCharacter.GetComponent<HeroController>().MoveLeft(checkMoreMove);
                    firstSwipe = false;
                }
            }
        }
        else//vuot doc
        {
            if (swipeDistVertical > minSwipeDistY * (float)Screen.height / Modules.KTCScenes.y)
            {
                float swipeValue = Input.mousePosition.y - startPos.y;
                if (swipeValue > 0)
                {
                    bool checkMoreMove = false;
                    if (oldSwipe == "") checkMoreMove = true;
                    oldSwipe = "up";
                    Modules.mainCharacter.GetComponent<HeroController>().MoveUp(checkMoreMove);
                    firstSwipe = false;
                }
                else if (swipeValue < 0)
                {
                    bool checkMoreMove = false;
                    if (oldSwipe == "") checkMoreMove = true;
                    oldSwipe = "down";
                    Modules.mainCharacter.GetComponent<HeroController>().MoveDown(checkMoreMove);
                    firstSwipe = false;
                }
            }
        }
    }

    private bool CheckNoMessageShow()
    {
        bool result = true;
        if (mesSetting.activeSelf || mesPauseGame.activeSelf || mesSaveMeBox.activeSelf || mesNotEnoughKey.activeSelf
               || getSkisBox.activeSelf || getKeysBox.activeSelf || missionsBox.activeSelf || challengeBox.activeSelf
               || listLanguageBox.activeSelf || mesCountTime.activeSelf || rateBox.activeSelf || shareFBBox.activeSelf
               || inviteFBBox.activeSelf || networkBox.activeSelf || roomBox.activeSelf || findOpponentsBox.activeSelf
               || bonusFirstBox.activeSelf | resultOnlineBox.activeSelf) result = false;
        return result;
    }

    void Update()
    {
        if (Time.timeScale == 0) { InputButtonUp(); return; }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!CheckNoMessageShow())
                return;
            if (Modules.statusGame == StatusGame.play)
                ButtonPauseGameClick();
            else if (Modules.statusGame == StatusGame.menu)
                ButtonSettingClick();
        }
        //xu ly su kien hieu ung play menu
        if (mesAniStart)
        {
            if (runTimeAniStart >= timeAniStart)
            {
                //hoan thanh viec nhay;
                mesAniStart = false;
                Modules.mainCharacter.transform.position = pointHeroTarget;
                //createEnemyLeft.transform.position = pointEnemyLeftTarget;
                //if (!Modules.versionExpress) createEnemyRight.transform.position = pointEnemyRightTarget;
                //khoi tao cac nhan vat trong game o day
                Modules.statusGame = StatusGame.play;
                containUIPlay.SetActive(true);
                if (Modules.startViewOnline) buttonPause.GetComponent<ButtonStatus>().Disable();
                else buttonPause.GetComponent<ButtonStatus>().Enable();
                //#if (UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
                //                ADSController.Instance.RequestBanner();
                //#endif
                CreateButtonItemBuys();
                SetupCharacterPlay();
                Invoke("CheckAvatarEnemy", 1f);
                panelMissions.SetActive(false);
                panelChallenge.SetActive(false);
                //xu ly guide
                if (Modules.gameGuide == "YES")
                {
                    if (Modules.startViewOnline)
                        Modules.gameGuide = "NO";
                    else
                    {
                        Modules.SetAllowHoverbike(false);
                        Modules.panelGameGuide.SetActive(true);
                        Transform textGuide = Modules.panelGameGuide.transform.Find("TextGuide");
                        textGuide.GetComponent<Text>().text = AllLanguages.playSwipeLeft[Modules.indexLanguage];
                        Modules.distanceEnemy = 2;
                    }
                }
                else
                {
                    ShowMissionsChallenge();
                }
            }
            else
            {
                Modules.mainCharacter.transform.position = Vector3.MoveTowards(Modules.mainCharacter.transform.position, pointHeroTarget, 7.5f * Time.deltaTime);
                createEnemyLeft.transform.position = Vector3.MoveTowards(createEnemyLeft.transform.position, pointEnemyLeftTarget, 7.5f * Time.deltaTime);
                runTimeAniStart += Time.deltaTime;
            }
        }
    }

    public void ShowMissionsChallenge()
    {
        //xu ly nhiem vu ca thu thach
        if (Modules.dataMissionsUse != "")
        {
            Modules.UpdateValueMissions();
            Modules.panelMissions.SetActive(true);
        }
        if (Modules.dataChallengeUse != "")
        {
            Modules.UpdateValueChallenge();
            Modules.panelChallenge.SetActive(true);
            Invoke("AudoHideChallenge", 2f);
        }
    }

    private int oldValueSkis = 0;
    private int oldValueKeys = 0;
    void EffectAddSkisKeys()
    {
        if (Modules.statusGame != StatusGame.menu) return;
        if (Modules.totalSkis != oldValueSkis)
        {
            int pointBonus = Modules.totalSkis - oldValueSkis;
            Modules.ShowPanelEffectAddPoint(pointBonus, Vector3.zero, parentSkis, 0.3f);
            Invoke("UpdateNewTotalSkis", 0.3f);
            oldValueSkis = Modules.totalSkis;
        }
        if (Modules.totalKey != oldValueKeys)
        {
            int pointBonus = Modules.totalKey - oldValueKeys;
            Modules.ShowPanelEffectAddPoint(pointBonus, Vector3.zero, parentKey, 0.3f);
            Invoke("UpdateNewTotalKey", 0.3f);
            oldValueKeys = Modules.totalKey;
        }
    }

    void UpdateNewTotalSkis()
    {
        textSkis.text = Modules.totalSkis.ToString();
    }

    void UpdateNewTotalKey()
    {
        textKey.text = Modules.totalKey.ToString();
    }

    IEnumerator LoadImage(string url)
    {
        WWW www = new WWW(url);
        while (!www.isDone && string.IsNullOrEmpty(www.error))
            yield return new WaitForSeconds(0.1f);
        if (string.IsNullOrEmpty(www.error) && url != "Null" && www.texture != null)
        {
            int width = www.texture.width;
            int height = www.texture.height;
            if (width > 128) width = 128;
            if (height > 128) height = 128;
            Transform tranAvatar = Modules.panelHighScoreNow.transform.Find("Avatar");
            Image fbAvatar = tranAvatar.GetComponent<Image>();
            fbAvatar.sprite = Sprite.Create(www.texture, new Rect(0, 0, width, height), new Vector2(0, 0));
        }
        www.Dispose();
        //yield return Resources.UnloadUnusedAssets();
        yield break;
    }

    private int maxTime = 5, Runtime = 0;
    void CheckAvatarEnemy()
    {
        if (Modules.fbHighScore.Count <= 0)
        {
            Runtime++;
            if (Runtime > maxTime)
            {
                Runtime = 0;
                UpdateAvatarEnemy();
                return;
            }
            Invoke("CheckAvatarEnemy", 1f);
            return;
        }
        Runtime = 0;
        UpdateAvatarEnemy();
    }

    void UpdateAvatarEnemy()
    {
        if (!Modules.startViewOnline) 
            Modules.panelHighScoreNow.SetActive(true);
        else
        {
            Modules.panelViewEnemy.SetActive(true);
            Modules.panelViewEnemy.GetComponent<RunEffectViewEnemy>().ResetView();
        }
        //cap nhat avatar cho panel high score
        Modules.totalScoreNow = Modules.totalScore;
        Transform tranAvatar = Modules.panelHighScoreNow.transform.Find("Avatar");
        Image fbAvatar = tranAvatar.GetComponent<Image>();
        Transform tranName = Modules.panelHighScoreNow.transform.Find("TextHighScore");
        Text fbName = tranName.GetComponent<Text>();
        if (Modules.fbHighScore.Count > 0)//neu co doi thu thi lay thang cuoi cung
        {
            StartCoroutine(LoadImage(Modules.fbAvatarEnemy[Modules.fbAvatarEnemy.Count - 1]));
            fbName.text = Modules.fbNameEnemy[Modules.fbNameEnemy.Count - 1].ToUpper();
            Modules.totalScoreNow = Modules.fbHighScore[Modules.fbHighScore.Count - 1];
        }
        else
        {
            if (Modules.fbMyAvatar != null)
            {
                fbAvatar.sprite = Modules.fbMyAvatar;
                fbName.text = Modules.fbName.ToUpper();
            }
            else
            {
                fbAvatar.sprite = Modules.iconAvatarNull;
                fbName.text = AllLanguages.playHighScore[Modules.indexLanguage];
            }
        }
    }

    //private IEnumerator coroutine;
    //void OnDisable()
    //{
    //    if (coroutine != null)
    //        StopCoroutine(coroutine);
    //}

    void AudoHideChallenge()
    {
        Modules.panelChallenge.SetActive(false);
    }

    public void ClickPlayGame(bool playAudio = true)
    {
        //thuc hien chay xu ly start
        if (playAudio) Modules.PlayAudioClipFree(Modules.audioTapPlay);
        Modules.PlayAudioClipFree(Modules.audioPoStart);
        transform.GetComponent<Animator>().SetTrigger("TriPlay");
        rateBox.SetActive(false);
        containUIMenu.SetActive(false);
    }

    public void StartMainGame()
    {
        Modules.statusGame = StatusGame.start;
        SetupCharacterStart();
    }

    void CreateCharacters()
    {
        parentCam.transform.position = new Vector3(0, Modules.pointHeroTerrain, 0);
        //tao nhan vat chinh va thiet lap ban dau cho nhan vat
        Vector3 pointHero = new Vector3(0, Modules.pointHeroTerrain + 0.3f, 0);
        if (Modules.mainCharacter != null)
        {
            Modules.mainCharacter.GetComponent<ShadowFixed>().RemoveShadow();
            Destroy(Modules.mainCharacter.gameObject);
        }
        foreach (GameObject go in Modules.listHeroUse)
        {
            HeroController heroCon = go.GetComponent<HeroController>();
            if (heroCon.idHero == Modules.codeHeroUse)
            {
                Modules.mainCharacter = Instantiate(go, Modules.containMainGame.transform) as GameObject;
                Modules.mainCharacter.transform.position = pointHero;
                HeroController heroNow = Modules.mainCharacter.GetComponent<HeroController>();
                heroNow.CallAniMenu(heroNow.aniActionMenu, 1f);
                break;
            }
        }
        //tao van truot cho hero da luu
        foreach (GameObject go in Modules.listSkisUse)
        {
            SkisController skisCon = go.GetComponent<SkisController>();
            if (skisCon.idSkis == Modules.codeSkisUse)
            {
                Modules.mainCharacter.GetComponent<HeroController>().mySkis = go;
                break;
            }
        }
        //tao xe canh sat, van truot
        Vector3 pointPoliceCarNew = new Vector3(pointPoliceCar.x, pointPoliceCar.y + Modules.pointHeroTerrain, pointPoliceCar.z);
        CancelInvoke("HideCarPolice");
        if (policeCar != null)
        {
            if (carPolice) Destroy(carPolice);
            carPolice = Instantiate(policeCar, GetComponent<TerrainController>().listShowTerrain[0].transform) as GameObject;
            carPolice.transform.position = pointPoliceCarNew;
        }
        HeroController heroControl = Modules.mainCharacter.GetComponent<HeroController>();
        Modules.SetModelUseItem(Modules.mainCharacter.transform, heroControl.codeBody, heroControl.mySkis, "Skis");
        //tao cac nhan vat enemy va thiet lap ban dau
        Vector3 pointStartEnemyLeftNew = new Vector3(pointStartEnemyLeft.x, pointStartEnemyLeft.y + Modules.pointHeroTerrain, pointStartEnemyLeft.z);
        if (createEnemyLeft) Destroy(createEnemyLeft);
        createEnemyLeft = Instantiate(enemyLeft, pointStartEnemyLeftNew, Quaternion.identity) as GameObject;
        createEnemyLeft.transform.parent = Modules.containMainGame.transform;
        EnemyController enemyConLeft = createEnemyLeft.GetComponent<EnemyController>();
        enemyConLeft.CallAniMenu(enemyConLeft.aniIdleStart, 1f);
    }

    private bool mesAniStart = false;
    private float timeAniStart = 1f;
    private float runTimeAniStart = 0f;
    [HideInInspector]
    public GameObject createEnemyLeft, carPolice;
    private Vector3 pointHeroTarget = Vector3.zero;
    private Vector3 pointEnemyLeftTarget = Vector3.zero;
    void SetupCharacterStart()
    {
        Modules.PlayAudioClipFree(Modules.audioSurprise);
        //xoa van truot
        Modules.RemoveModelUseItem(Modules.mainCharacter.transform, "Skis");
        //setup main character
        pointHeroTarget = Modules.mainCharacter.transform.position;
        Vector3 pointCreate = Modules.mainCharacter.GetComponent<HeroController>().pointShowHero.transform.position;
        Vector3 pointCreateNew = new Vector3(pointCreate.x, Modules.pointHeroTerrain, pointCreate.z);
        Modules.mainCharacter.transform.position = pointCreateNew;
        HeroController heroNow = Modules.mainCharacter.GetComponent<HeroController>();
        //an cac object hide
        foreach (GameObject go in heroNow.listObjectHide) go.gameObject.SetActive(false);
        timeAniStart = heroNow.CallAniMenu(heroNow.aniOhNoMenu, 1f, false);
        runTimeAniStart = 0;
        mesAniStart = true;
        //setup enemy characters
        EnemyController enemyConLeft = createEnemyLeft.GetComponent<EnemyController>();
        enemyConLeft.CallAniMenu(enemyConLeft.aniRun, 1f);
        pointEnemyLeftTarget = new Vector3(enemyConLeft.pointX, enemyConLeft.transform.position.y, enemyConLeft.pointZNear);
        //hen gio de an xe canh sat
        Invoke("HideCarPolice", 5f);
    }

    void HideCarPolice()
    {
        carPolice.SetActive(false);
    }

    void SetupCharacterPlay()
    {
        //thiet lap nhan vat khi choi
        HeroController heroNow = Modules.mainCharacter.GetComponent<HeroController>();
        heroNow.SetupShowMenu(0);
        heroNow.ReStart();
        //thiet lap enemy duoi theo
        EnemyController enemyConLeft = createEnemyLeft.GetComponent<EnemyController>();
        enemyConLeft.ReStart();
    }

    void CreateButtonItemBuys()//tao cac nut item mua truoc do
    {
        float pointX = 0;
        float heightHeadStart = 0;
        float heightScoreBooster = 120f;
        if (Modules.totalHeadStart <= 0)
        {
            if (Modules.totalScoreBooster > 0)
            {
                heightScoreBooster = 0;
                heightHeadStart = 120f;
            }
        }
        if (Modules.totalHeadStart > 0)//neu co item headStart
            CallButtonItemBuy(pointX, heightHeadStart, Modules.iconHeadStart, Modules.totalHeadStart, "headStart");
        if (Modules.totalScoreBooster > 0)//neu co item scoreBooster
            CallButtonItemBuy(pointX, heightScoreBooster, Modules.iconScoreBooster, Modules.totalScoreBooster, "scoreBooster");
        Modules.SetAllowHoverbike(false);
    }

    public void ReCreateButtonItemBuys()
    {
        foreach (Transform tran in Modules.containButtonBuy.transform) Destroy(tran.gameObject);
        CreateButtonItemBuys();
    }

    void CallButtonItemBuy(float pointX, float pointY, Sprite iconButton, int totalTime, string codeItem)
    {
        GameObject button = Instantiate(panelShowBuyItems, new Vector3(pointX, pointY, 0), Quaternion.identity) as GameObject;
        button.transform.SetParent(Modules.containButtonBuy.transform, false);
        ButtonItemBuy buttonClick = button.GetComponent<ButtonItemBuy>();
        buttonClick.codeItem = codeItem;
        Transform icon = button.transform.Find("Icon");
        Transform number = button.transform.Find("Number");
        Image imgIcon = icon.GetComponent<Image>();
        Text txtNumber = number.GetComponent<Text>();
        imgIcon.sprite = iconButton;
        txtNumber.text = totalTime.ToString();
        Animator aniPanel = button.GetComponent<Animator>();
        aniPanel.SetTrigger("TriOpen");
    }

    public void ButtonPauseGameClick()
    {
        if (Modules.startViewOnline) return;
        Modules.PlayAudioClipFree(Modules.audioButton);
        mesPauseGame.SetActive(true);
        mesPauseGame.GetComponent<Animator>().SetTrigger("TriOpen");
        MessagePauseGame mesPause = mesPauseGame.GetComponent<MessagePauseGame>();
        mesPause.ShowMessageBox();
    }

    public void ButtonRankingClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.containMainGame.SetActive(false);
        Modules.containAchievement.SetActive(true);
        Modules.containAchievement.transform.Find("MainCamera").GetComponent<PageAchievement>().CallStart();
    }

    public void ButtonHeroClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.containMainGame.SetActive(false);
        Modules.containHeroConstruct.SetActive(true);
        Modules.containHeroConstruct.transform.Find("MainCamera").GetComponent<PageConstructHero>().CallStart();
    }

    public void ButtonShopClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.containMainGame.SetActive(false);
        Modules.containShopItem.SetActive(true);
        Modules.containShopItem.transform.Find("MainCamera").GetComponent<PageShopItems>().CallStart();
    }

    public void ButtonOnlineClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        //networkBox.SetActive(true);
        //networkBox.GetComponent<MessageNetwork>().CallStart();
        //networkBox.GetComponent<Animator>().SetTrigger("TriOpen");
        findOpponentsBox.SetActive(true);
        findOpponentsBox.GetComponent<MessageFindOpponent>().CallStart();
        findOpponentsBox.GetComponent<Animator>().SetTrigger("TriOpen");
    }

    public void ButtonSettingClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        mesSetting.SetActive(true);
        mesSetting.GetComponent<MessageSetting>().StartShowMessage();
        mesSetting.GetComponent<Animator>().SetTrigger("TriOpen");
    }

    public void ButtonSkisMenuClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        getSkisBox.GetComponent<CooldownSkis>().ShowMessageBox();
        getSkisBox.SetActive(true);
        getSkisBox.GetComponent<Animator>().SetTrigger("TriOpen");
    }

    public void ButtonKeyMenuClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        getKeysBox.GetComponent<CooldownKey>().ShowMessageBox();
        getKeysBox.SetActive(true);
        getKeysBox.GetComponent<Animator>().SetTrigger("TriOpen");
    }

    public void ButtonMissionsClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        transform.GetComponent<MissionsController>().UpdateLanguage();
        missionsBox.SetActive(true);
        missionsBox.GetComponent<Animator>().SetTrigger("TriOpen");
    }

    public void ButtonChallengeClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        transform.GetComponent<ChallengeController>().UpdateLanguage();
        challengeBox.SetActive(true);
        challengeBox.GetComponent<Animator>().SetTrigger("TriOpen");
    }

    public void ButtonRateClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        rateBox.GetComponent<MessageRate>().StartShowMessage();
        rateBox.SetActive(true);
        rateBox.GetComponent<Animator>().SetTrigger("TriOpen");
    }

    public void ButtonShareFacebook()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        if (Modules.CheckReceivedFB())
        {
            Modules.ClickShareFB();
        }
        else
        {
            shareFBBox.GetComponent<MessageShareFB>().StartShowMessage();
            shareFBBox.SetActive(true);
            shareFBBox.GetComponent<Animator>().SetTrigger("TriOpen");
        }
    }

    public void ButtonInviteFacebook()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        if (Modules.coinMaxInvite <= 0)//neu da moi nhan thuong roi thi cho moi luon, khong thuong
        {
            Facebook.Unity.FB.AppRequest(
                AllLanguages.menuMessageInvite[Modules.indexLanguage],
                null, null, null, 100, string.Empty, AllLanguages.menuTitleInvite[Modules.indexLanguage], null);
        }
        else
        {
            inviteFBBox.GetComponent<MessageInviteFB>().StartShowMessage();
            inviteFBBox.SetActive(true);
            inviteFBBox.GetComponent<Animator>().SetTrigger("TriOpen");
        }
    }

    public void UpdateKeys()
    {
        textTotalKeySave.text = Mathf.RoundToInt(Modules.totalKey).ToString();
    }

    public void UpdateNeedKeys(int total)
    {
        textNeedKeys.text = AllLanguages.playKeyNeed[Modules.indexLanguage] + " " + Mathf.RoundToInt(total).ToString();
    }

    public Image imgRecordIcon;
    public Color colorRecordStop;
    public Color colorRecordStart;
    private int statusRecord = 0;//0 stop, 1 start, 2 pause
    public void ButtonRecord()
    {
        //if (statusRecord == 0)//dang stop
        //{
        //    if (Recorder.Instance.StartRecord())
        //    {
        //        statusRecord = 1;
        //        imgRecordIcon.color = colorRecordStart;
        //    }
        //}
        //else if (statusRecord == 1)//dang start
        //{
        //    if (Recorder.Instance.PauseRecord())
        //    {
        //        statusRecord = 2;
        //        imgRecordIcon.color = colorRecordStop;
        //    }
        //}
        //else//dang pause
        //{
        //    if (Recorder.Instance.ResumeRecord())
        //    {
        //        statusRecord = 1;
        //        imgRecordIcon.color = colorRecordStart;
        //    }
        //}
    }

    public void ButtonStopRecord()
    {
        //if (Recorder.Instance.StopRecord())
        //{
        //    statusRecord = 0;
        //    imgRecordIcon.color = colorRecordStop;
        //}
    }

    public void ButtonUploadVideo(GameObject myButton)
    {
        //if (!Recorder.Instance.isAvailableVideo)
        //{
        //    myButton.GetComponent<ButtonStatus>().Disable();
        //    return;
        //}
        //if (Recorder.Instance.ShareRecord())
        //{
        //    statusRecord = 0;
        //    imgRecordIcon.color = colorRecordStop;
        //}
    }

    //xu ly ngon ngu
    public Text textTapToPlay;
    public Text textButMission, textButChallenge;
    public Text textButTopRun, textButHero, textButShop, textButOnline;
    public Text textButApply, textButCancel;
    public Text textButUpVideo, textFindOpponents;
    public Text textTitleBonusFirst, textContentBonusFirst, textButtonBonusFirst;
    public Text textNoteGetFree;

    public void ChangeAllLanguage()
    {
        int iLang = Modules.indexLanguage;
        textTapToPlay.font = AllLanguages.listFontLangA[iLang];
        textTapToPlay.text = AllLanguages.menuTapToPlay[iLang];
        textButMission.font = AllLanguages.listFontLangA[iLang];
        textButMission.text = AllLanguages.menuMissions[iLang];
        textButChallenge.font = AllLanguages.listFontLangA[iLang];
        textButChallenge.text = AllLanguages.menuChallenge[iLang];
        textButUpVideo.font = AllLanguages.listFontLangA[iLang];
        textButUpVideo.text = AllLanguages.menuUpVideo[iLang];
        textButTopRun.font = AllLanguages.listFontLangA[iLang];
        textButTopRun.text = AllLanguages.menuTopRun[iLang];
        textButHero.font = AllLanguages.listFontLangA[iLang];
        textButHero.text = AllLanguages.menuHero[iLang];
        textButShop.font = AllLanguages.listFontLangA[iLang];
        textButShop.text = AllLanguages.menuShop[iLang];
        textButOnline.font = AllLanguages.listFontLangA[iLang];
        textButOnline.text = AllLanguages.menuOnline[iLang];
        textButApply.font = AllLanguages.listFontLangA[iLang];
        textButApply.text = AllLanguages.menuApply[iLang];
        textButCancel.font = AllLanguages.listFontLangA[iLang];
        textButCancel.text = AllLanguages.menuCancel[iLang];
        textFindOpponents.font = AllLanguages.listFontLangA[iLang];
        textFindOpponents.text = AllLanguages.menuFindOpponents[iLang];
        textTitleBonusFirst.font = AllLanguages.listFontLangA[iLang];
        textTitleBonusFirst.text = AllLanguages.playBonusTitle[iLang];
        textContentBonusFirst.font = AllLanguages.listFontLangB[iLang];
        textContentBonusFirst.text = AllLanguages.playBonusContent[iLang];
        textButtonBonusFirst.font = AllLanguages.listFontLangA[iLang];
        textButtonBonusFirst.text = AllLanguages.playBonusButton[iLang];
        textNoteGetFree.font = AllLanguages.listFontLangB[iLang];
        textNoteGetFree.text = AllLanguages.menuGetFree[iLang];
        //update font text khac
        textNeedKeys.font = AllLanguages.listFontLangA[Modules.indexLanguage];
        Transform textGuide = Modules.panelGameGuide.transform.Find("TextGuide");
        textGuide.GetComponent<Text>().font = AllLanguages.listFontLangA[iLang];
        //Transform tranName = Modules.panelHighScoreNow.transform.Find("TextHighScore");
        //tranName.GetComponent<Text>().font = AllLanguages.listFontLangA[iLang];
    }
}
