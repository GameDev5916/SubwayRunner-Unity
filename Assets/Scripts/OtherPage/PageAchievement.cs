using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.Unity;

public class PageAchievement : MonoBehaviour
{

    public Text textKey, textCoin, textHighScore, textHighCoin;
    public Vector3 pointShowHero, scaleShowHero;
    public Image fbAvatar;
    public Text fbName, fbScore, fbIndex;
    public Button doubleCoin;
    public GameObject listTempFriend, listTempCountry, listTempWorld, listTempMultiplayer;//prefab mau list player score
    public GameObject parentListFriend, parentListCountry, parentListWorld, parentListMultiplayer;
    public GameObject panelFBLogin;
    public GameObject panelLoadingA, panelLoadingB, panelLoadingC, panelLoadingD;
    public Vector3 pointListFriend = Vector3.zero;
    public Vector3 pointListCountry = Vector3.zero;
    public Vector3 pointListWorld = Vector3.zero;
    public Vector3 pointListMultiplayer = Vector3.zero;
    private FacebookController fbController;
    private GameObject panelTopCountry;//list hien thi xep hang dat nuoc
    private GameObject panelTopWorld;//list hien thi xep hang the gioi
    private GameObject panelTopMultiplayer;//list hien thi xep hang Multiplayer
    private GameObject mainCharacter;

	public void CallStart () {
        ChangeAllLanguage();
        float scoreNow = Modules.totalScore;
		float coinNow = Modules.totalCoin;
        CancelInvoke("BlinkDoubleCoin");
        if(Modules.showScorePlay)
        {
            scoreNow = Modules.scorePlayer;
			coinNow = Modules.coinPlayer;
            doubleCoin.interactable = true;
            Invoke("BlinkDoubleCoin", 0f);
        }
        else
        {
            doubleCoin.transform.GetComponent<Image>().color = colorBlinkCoinA;
            doubleCoin.transform.GetComponentInChildren<Text>().color = colorBlinkTextA;
            doubleCoin.interactable = false;
        }
        textKey.text = Mathf.RoundToInt(Modules.totalKey).ToString();
        textCoin.text = Mathf.RoundToInt(Modules.totalCoin).ToString();
        textHighScore.GetComponent<EffectUpScore>().StartEffect(scoreNow);
        textHighCoin.GetComponent<EffectUpScore>().StartEffect(coinNow);
        fbScore.text = Mathf.RoundToInt(Modules.totalScore).ToString();
        if (mainCharacter != null) Destroy(mainCharacter);
        foreach (GameObject go in Modules.listHeroUse)
        {
            HeroController heroCon = go.GetComponent<HeroController>();
            if (heroCon.idHero == Modules.codeHeroUse)
            {
                if (Modules.showScorePlay) mainCharacter = Instantiate(heroCon.modelIdelStun, pointShowHero, Quaternion.identity) as GameObject;
                else mainCharacter = Instantiate(go, pointShowHero, Quaternion.identity) as GameObject;
                mainCharacter.transform.parent = Modules.containAchievement.transform;
                mainCharacter.transform.localScale = scaleShowHero;
                mainCharacter.transform.eulerAngles = new Vector3(0, 180, 0);
                if (!Modules.showScorePlay)
                {
                    HeroController heroNow = mainCharacter.GetComponent<HeroController>();
                    heroNow.SetupShowMenu(2);
                    heroNow.CallAniMenu(heroNow.aniIdleMenu, 1f);
                }
                break;
            }
        }
        Modules.showScorePlay = false;
        if (panelTopCountry == null)
        {
            panelLoadingC.SetActive(true);
            panelLoadingC.transform.GetComponent<TextLoading>().CallStart();
        }
        else panelLoadingC.SetActive(false);
        if (panelTopWorld == null)
        {
            panelLoadingB.SetActive(true);
            panelLoadingB.transform.GetComponent<TextLoading>().CallStart();
        }
        else panelLoadingB.SetActive(false);
        if (panelTopMultiplayer == null)
        {
            panelLoadingD.SetActive(true);
            panelLoadingD.transform.GetComponent<TextLoading>().CallStart();
        }
        else panelLoadingD.SetActive(false);
#if !(UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
        ButtonTopCountryClick();
        panelFBLogin.SetActive(true);
#else
        //xu ly xep hang facebook
        if (fbController == null) fbController = Modules.facebookController.GetComponent<FacebookController>();
		fbController.isPostDone = false;
		fbController.isGetDone = false;
        fbController.getEnemy = false;
		if (fbController.panelGetInfo != null)
			Destroy(fbController.panelGetInfo);
        if (FB.IsLoggedIn)
        {
            panelFBLogin.SetActive(false);
            panelLoadingA.SetActive(true);
            panelLoadingA.transform.GetComponent<TextLoading>().CallStart();
        }
        else
        {
            panelFBLogin.SetActive(true);
            panelLoadingA.SetActive(false);
        }
        Invoke("PostScoreFacebook", 0f);
        Invoke("GetScoreFacebook", 0f);
        InvokeRepeating("GetBoardScoreFacebook", 0f, 1f);
#endif
        //xu ly xep hang quoc gia va the gioi
        if (Modules.containAchievement.activeSelf) StartCoroutine(PostScore());
        Invoke("PostScoreWorld", 0f);
	}

    void OnDisable()
    {
        CancelInvoke();
    }

	public void DoubleCoins () {
		Modules.itemBonusViewAds = "DoubleCoins";
#if (UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
        ADSController.Instance.RequestRewardBasedVideo(true, CallReward);
#endif
	}

    private void CallReward(bool obj)
    {
        if (!Modules.containAchievement.activeSelf) return;
        if (obj)
        {
            float coinNow = Modules.coinPlayer * 2;
            if (Modules.showScorePlay)
                coinNow = Modules.coinPlayer * 2;
            Modules.totalCoin += Modules.coinPlayer;
            textCoin.text = Mathf.RoundToInt(Modules.totalCoin).ToString();
            textHighCoin.GetComponent<EffectUpScore>().StartEffect(coinNow);

            doubleCoin.transform.GetComponent<Image>().color = colorBlinkCoinA;
            doubleCoin.transform.GetComponentInChildren<Text>().color = colorBlinkTextA;
            doubleCoin.interactable = false;
            CancelInvoke("BlinkDoubleCoin");
        }
    }

    public Color colorBlinkCoinA = Color.white;
    public Color colorBlinkCoinB = Color.green;
    public Color colorBlinkTextA = Color.black;
    public Color colorBlinkTextB = Color.white;
    void BlinkDoubleCoin()
    {
        Image img = doubleCoin.transform.GetComponent<Image>();
        Text txt = doubleCoin.transform.GetComponentInChildren<Text>();
        if (img.color == colorBlinkCoinA)
        {
            img.color = colorBlinkCoinB;
            txt.color = colorBlinkTextB;
        }
        else
        {
            img.color = colorBlinkCoinA;
            txt.color = colorBlinkTextA;
        }
        Invoke("BlinkDoubleCoin", 0.5f);
    }

    void PostScoreFacebook()
    {
        if (FB.IsLoggedIn)
        {
			fbAvatar.sprite = Modules.fbMyAvatar;
            fbName.text = Modules.fbName;
            fbScore.text = Mathf.RoundToInt(Modules.totalScore).ToString();
            fbIndex.text = "1";
            fbController.PostScore(true);
        }
        else Invoke("PostScoreFacebook", 1f);
    }

    void GetScoreFacebook()
    {
        if (fbController.isPostDone)
        {
            fbController.GetScores();
            fbController.isPostDone = false;
        }
        else Invoke("GetScoreFacebook", 1f);
    }

    void GetBoardScoreFacebook()
    {
        if (fbController.panelGetInfo == null || !fbController.isGetDone) return;
        GameObject listFriend = fbController.panelGetInfo;
        listFriend.transform.position = pointListFriend;
        listFriend.transform.SetParent(parentListFriend.transform, false);
        panelFBLogin.SetActive(false);
        panelLoadingA.SetActive(false);
        fbController.isGetDone = false;
    }

    void PostScoreWorld()
    {
        if (statusPost)
        {
            if (Modules.containAchievement.activeSelf)
            {
                StartCoroutine(GetScoreCountry());
                StartCoroutine(GetDataMultiplayer());
            }
        }
        else Invoke("PostScoreWorld", 1f);
    }

    //XU LY GET POST DIEM LEN SERVER
    List<Texture2D> listAvatarCountry = new List<Texture2D>();
    List<Texture2D> listAvatarWorld = new List<Texture2D>();
    List<Texture2D> listAvatarMultiPlayer = new List<Texture2D>();
    IEnumerator LoadImageCountry(string url, int index, Image avatar)
    {
        WWW www = new WWW(url);
        while (!www.isDone && string.IsNullOrEmpty(www.error))
            yield return new WaitForSeconds(0.1f);
        if (string.IsNullOrEmpty(www.error) && url != "Null" && www.texture != null && avatar != null)
        {
            listAvatarCountry[index] = www.texture;
            int width = listAvatarCountry[index].width;
            int height = listAvatarCountry[index].height;
            if (width > 128) width = 128;
            if (height > 128) height = 128;
            avatar.sprite = Sprite.Create(listAvatarCountry[index], new Rect(0, 0, width, height), new Vector2(0, 0));
        }
        www.Dispose();
        //yield return Resources.UnloadUnusedAssets();
        yield break;
    }

    IEnumerator LoadImageWorld(string url, int index, Image avatar)
    {
        WWW www = new WWW(url);
        while (!www.isDone && string.IsNullOrEmpty(www.error))
            yield return new WaitForSeconds(0.1f);
        if (string.IsNullOrEmpty(www.error) && url != "Null" && www.texture != null && avatar != null)
        {
            listAvatarWorld[index] = www.texture;
            int width = listAvatarWorld[index].width;
            int height = listAvatarWorld[index].height;
            if (width > 128) width = 128;
            if (height > 128) height = 128;
            avatar.sprite = Sprite.Create(listAvatarWorld[index], new Rect(0, 0, width, height), new Vector2(0, 0));
        }
        www.Dispose();
        //yield return Resources.UnloadUnusedAssets();
        yield break;
    }

    IEnumerator LoadImageMultiplayer(string url, int index, Image avatar)
    {
        WWW www = new WWW(url);
        while (!www.isDone && string.IsNullOrEmpty(www.error))
            yield return new WaitForSeconds(0.1f);
        if (string.IsNullOrEmpty(www.error) && url != "Null" && www.texture != null && avatar != null)
        {
            listAvatarMultiPlayer[index] = www.texture;
            int width = listAvatarMultiPlayer[index].width;
            int height = listAvatarMultiPlayer[index].height;
            if (width > 128) width = 128;
            if (height > 128) height = 128;
            avatar.sprite = Sprite.Create(listAvatarMultiPlayer[index], new Rect(0, 0, width, height), new Vector2(0, 0));
        }
        www.Dispose();
        //yield return Resources.UnloadUnusedAssets();
        yield break;
    }

    private bool statusPost = false;
    //private bool statusGet = false; 
    IEnumerator PostScore()
    {
#if UNITY_WEBGL
        string idDevice = Modules.fbID;
#else
        string idDevice = SystemInfo.deviceUniqueIdentifier;
#endif
        if (idDevice == "Null")
        {
            statusPost = true;
            yield break;
        }
        //string nameDevice = SystemInfo.deviceName;
        string dataCountry = SaveLoadData.LoadData("CodeCountry", true);
        if (dataCountry == "") dataCountry = "Null";
        WWWForm form = new WWWForm();
        form.AddField("table", "useBusSubway");
        form.AddField("idUser", idDevice);
        form.AddField("name", Modules.fbName);
        form.AddField("avatar", Modules.fbLinkAvatar == "" ? "Null" : Modules.fbLinkAvatar);
        form.AddField("score", Mathf.RoundToInt(Modules.totalScore));
        form.AddField("country", dataCountry);
        form.AddField("win", Modules.winNow);
        form.AddField("lose", Modules.loseNow);
        form.AddField("fail", Modules.failNow);
        WWW _resuilt = new WWW(Modules.linkPost, form);
        float runTime = 0f;
        while (!_resuilt.isDone && runTime < Modules. maxTime)
        {
            runTime += Modules.requestTime;
            yield return new WaitForSeconds(Modules.requestTime);
        }
        yield return _resuilt;
        if (_resuilt.text == "Done")
        { //hoan thanh
            statusPost = true;
            Modules.winNow = 0;
            Modules.loseNow = 0;
            Modules.failNow = 0;
        }
        else
        { //qua lau, khong mang, cau lenh loi
            statusPost = false;
        }
        yield break;
    }

    IEnumerator GetScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("table", "useBusSubway");
        form.AddField("limit", "30");
        WWW _resuilt = new WWW(Modules.linkGet, form);
        float runTime = 0f;
        while (!_resuilt.isDone && runTime < Modules.maxTime)
        {
            runTime += Modules.requestTime;
            yield return new WaitForSeconds(Modules.requestTime);
        }
        yield return _resuilt;
        if (_resuilt.text != "null" && _resuilt.text != "")
        { //hoan thanh
            //print(_resuilt.text);
            listAvatarWorld = new List<Texture2D>();
            string[] dataLine = _resuilt.text.Split('\n');
            if (panelTopWorld != null) Destroy(panelTopWorld);
            panelTopWorld = Instantiate(listTempWorld, Vector3.zero, Quaternion.identity) as GameObject;
            Transform panelContent = panelTopWorld.transform.Find("Content");
            Transform panelItem = panelContent.transform.Find("Item");
            for (int i = 0; i < dataLine.Length; i++)
            {
                if (dataLine[i] == "") continue;
                GameObject newItem = panelItem.gameObject;
                if (i > 0)
                {
                    newItem = Instantiate(panelItem.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
                    newItem.transform.SetParent(panelContent, false);
                }
                if (i % 2 != 0) newItem.GetComponent<Image>().color = Modules.colorListLine;
                Transform tranAvatar = newItem.transform.Find("Avatar");
                Transform tranName = newItem.transform.Find("Name");
                Transform tranScore = newItem.transform.Find("Score");
                Transform tranIndex = newItem.transform.Find("Index");

                Image fbAvatar = tranAvatar.GetComponent<Image>();
                Text fbName = tranName.GetComponent<Text>();
                Text fbScore = tranScore.GetComponent<Text>();
                Text fbIndex = tranIndex.GetComponent<Text>();

                string[] data = dataLine[i].Split(';');
                fbName.text = data[0];
                listAvatarWorld.Add(null);
                if (Modules.containAchievement.activeSelf) StartCoroutine(LoadImageWorld(data[1], i, fbAvatar));
                fbScore.text = data[2];
                fbIndex.text = (i + 1).ToString();
            }
            panelTopWorld.transform.position = pointListWorld;
            panelTopWorld.transform.SetParent(parentListWorld.transform, false);
            panelLoadingB.SetActive(false);
            //statusGet = true;
        }
        else
        { //qua lau, khong mang, cau lenh loi
            //statusGet = false;
            panelLoadingB.SetActive(true);
            panelLoadingB.transform.GetComponent<TextLoading>().CallStart();
        }
        yield break;
    }

    IEnumerator GetScoreCountry()
    {
        string dataCountry = SaveLoadData.LoadData("CodeCountry", true);
        if (dataCountry == "") dataCountry = "Null";
        WWWForm form = new WWWForm();
        form.AddField("table", "useBusSubway");
        form.AddField("limit", "30");
        form.AddField("country", dataCountry);
        WWW _resuilt = new WWW(Modules.linkGetCountry, form);
        float runTime = 0f;
        while (!_resuilt.isDone && runTime < Modules.maxTime)
        {
            runTime += Modules.requestTime;
            yield return new WaitForSeconds(Modules.requestTime);
        }
        yield return _resuilt;
        if (_resuilt.text != "null" && _resuilt.text != "")
        { //hoan thanh
            //print(_resuilt.text);
            listAvatarCountry = new List<Texture2D>();
            string[] dataLine = _resuilt.text.Split('\n');
            if (panelTopCountry != null) Destroy(panelTopCountry);
            panelTopCountry = Instantiate(listTempCountry, Vector3.zero, Quaternion.identity) as GameObject;
            Transform panelContent = panelTopCountry.transform.Find("Content");
            Transform panelItem = panelContent.transform.Find("Item");
            for (int i = 0; i < dataLine.Length; i++)
            {
                if (dataLine[i] == "") continue;
                GameObject newItem = panelItem.gameObject;
                if (i > 0)
                {
                    newItem = Instantiate(panelItem.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
                    newItem.transform.SetParent(panelContent, false);
                }
                if (i % 2 != 0) newItem.GetComponent<Image>().color = Modules.colorListLine;
                Transform tranAvatar = newItem.transform.Find("Avatar");
                Transform tranName = newItem.transform.Find("Name");
                Transform tranScore = newItem.transform.Find("Score");
                Transform tranIndex = newItem.transform.Find("Index");

                Image fbAvatar = tranAvatar.GetComponent<Image>();
                Text fbName = tranName.GetComponent<Text>();
                Text fbScore = tranScore.GetComponent<Text>();
                Text fbIndex = tranIndex.GetComponent<Text>();

                string[] data = dataLine[i].Split(';');
                fbName.text = data[0];
                listAvatarCountry.Add(null);
                if (Modules.containAchievement.activeSelf) StartCoroutine(LoadImageCountry(data[1], i, fbAvatar));
                fbScore.text = data[2];
                fbIndex.text = (i + 1).ToString();
            }
            panelTopCountry.transform.position = pointListCountry;
            panelTopCountry.transform.SetParent(parentListCountry.transform, false);
            panelLoadingC.SetActive(false);
            //statusGet = true;
        }
        else
        { //qua lau, khong mang, cau lenh loi
            //statusGet = false;
            panelLoadingC.SetActive(true);
            panelLoadingC.transform.GetComponent<TextLoading>().CallStart();
        }
        yield break;
    }

    IEnumerator GetDataMultiplayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("table", "useBusSubway");
        form.AddField("limit", "30");
        WWW _resuilt = new WWW(Modules.linkGetDataMultiplayer, form);
        float runTime = 0f;
        while (!_resuilt.isDone && runTime < Modules.maxTime)
        {
            runTime += Modules.requestTime;
            yield return new WaitForSeconds(Modules.requestTime);
        }
        yield return _resuilt;
        if (_resuilt.text != "null" && _resuilt.text != "")
        { //hoan thanh
            //print(_resuilt.text);
            listAvatarMultiPlayer = new List<Texture2D>();
            string[] dataLine = _resuilt.text.Split('\n');
            if (panelTopMultiplayer != null) Destroy(panelTopMultiplayer);
            panelTopMultiplayer = Instantiate(listTempMultiplayer, Vector3.zero, Quaternion.identity) as GameObject;
            Transform panelContent = panelTopMultiplayer.transform.Find("Content");
            Transform panelItem = panelContent.transform.Find("Item");
            for (int i = 0; i < dataLine.Length; i++)
            {
                if (dataLine[i] == "") continue;
                GameObject newItem = panelItem.gameObject;
                if (i > 0)
                {
                    newItem = Instantiate(panelItem.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
                    newItem.transform.SetParent(panelContent, false);
                }
                if (i % 2 != 0) newItem.GetComponent<Image>().color = Modules.colorListLine;
                Transform tranAvatar = newItem.transform.Find("Avatar");
                Transform tranName = newItem.transform.Find("Name");
                Transform tranScore = newItem.transform.Find("Score");
                Transform tranIndex = newItem.transform.Find("Index");

                Image fbAvatar = tranAvatar.GetComponent<Image>();
                Text fbName = tranName.GetComponent<Text>();
                Text fbScore = tranScore.GetComponent<Text>();
                Text fbIndex = tranIndex.GetComponent<Text>();

                string[] data = dataLine[i].Split(';');
                fbName.text = data[0];
                listAvatarMultiPlayer.Add(null);
                if (Modules.containAchievement.activeSelf) StartCoroutine(LoadImageMultiplayer(data[1], i, fbAvatar));
                fbScore.text = data[2];
                fbIndex.text = (i + 1).ToString();
            }
            panelTopMultiplayer.transform.position = pointListMultiplayer;
            panelTopMultiplayer.transform.SetParent(parentListMultiplayer.transform, false);
            panelLoadingD.SetActive(false);
            //statusGet = true;
        }
        else
        { //qua lau, khong mang, cau lenh loi
            //statusGet = false;
        }
        yield break;
    }

    public void ButtonLoginFacebook()
    {
#if !(UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
        return;
#endif
        Modules.PlayAudioClipFree(Modules.audioButton);
        fbController.FBlogin();
    }

    public void ButtonTopFacebookClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        parentListFriend.SetActive(true);
        parentListCountry.SetActive(false);
        //parentListWorld.SetActive(false);
        parentListMultiplayer.SetActive(false);
    }

    public void ButtonTopCountryClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        parentListFriend.SetActive(false);
        parentListCountry.SetActive(true);
        //parentListWorld.SetActive(false);
        parentListMultiplayer.SetActive(false);
    }

    public void ButtonTopWorldClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        parentListFriend.SetActive(false);
        parentListCountry.SetActive(false);
        parentListWorld.SetActive(true);
    }

    public void ButtonTopMultiplayerClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        parentListFriend.SetActive(false);
        parentListCountry.SetActive(false);
        parentListMultiplayer.SetActive(true);
    }

    public void ButtonPlayClick()
    {
        Modules.PlayAudioClipFree(Modules.audioTapPlay);
        Modules.autoTapPlay = true;
        Modules.containMainGame.SetActive(true);
        Modules.containAchievement.SetActive(false);
        if (Modules.statusGame == StatusGame.over)
            Modules.containMainGame.transform.Find("CamContent").GetComponentInChildren<PageMainGame>().ResetGame();
        else
            Modules.containMainGame.transform.Find("CamContent").GetComponentInChildren<PageMainGame>().CheckTapNow();
    }

    public void ButtonGoHomeClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.containMainGame.SetActive(true);
        Modules.containAchievement.SetActive(false);
        if (Modules.statusGame == StatusGame.over)
            Modules.containMainGame.transform.Find("CamContent").GetComponentInChildren<PageMainGame>().ResetGame();
    }

    public void ButtonShopClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Modules.containShopItem.SetActive(true);
        Modules.containAchievement.SetActive(false);
        Modules.containShopItem.transform.Find("MainCamera").GetComponent<PageShopItems>().CallStart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonGoHomeClick();
        }
    }

    //xu ly ngon ngu
    public Text textPlay, textScore, textDoubleCoin, textNoName;
    public Text textTopFriendA, textTopFriendB, textTopFriendC;
    public Text textTopCountryA, textTopCountryB, textTopCountryC;
    public Text textTopWorldA, textTopWorldB, textTopWorldC;
    public Text textTopMultiplayerA, textTopMultiplayerB, textTopMultiplayerC;
    public Text textLoginFB, textNoteBonus, textLoadingA, textLoadingB, textLoadingC, textLoadingD;
    public void ChangeAllLanguage()
    {
        int iLang = Modules.indexLanguage;
        textPlay.font = AllLanguages.listFontLangA[iLang];
        textPlay.text = AllLanguages.menuPlay[iLang];
        textScore.font = AllLanguages.listFontLangA[iLang];
        textScore.text = AllLanguages.topScore[iLang];
        textDoubleCoin.font = AllLanguages.listFontLangA[iLang];
        textDoubleCoin.text = AllLanguages.topDoubleCoin[iLang];
        textNoName.font = AllLanguages.listFontLangA[iLang];
        textNoName.text = AllLanguages.topNoName[iLang];
        textTopFriendA.font = AllLanguages.listFontLangA[iLang];
        textTopFriendA.text = AllLanguages.topTopFriend[iLang];
        textTopCountryA.font = AllLanguages.listFontLangA[iLang];
        textTopCountryA.text = AllLanguages.topTopCountry[iLang];
        textTopWorldA.font = AllLanguages.listFontLangA[iLang];
        textTopWorldA.text = AllLanguages.topTopWorld[iLang];
        textTopMultiplayerA.font = AllLanguages.listFontLangA[iLang];
        textTopMultiplayerA.text = AllLanguages.topTopMultiplayer[iLang];
        textTopFriendB.font = AllLanguages.listFontLangA[iLang];
        textTopFriendB.text = AllLanguages.topTopFriend[iLang];
        textTopCountryB.font = AllLanguages.listFontLangA[iLang];
        textTopCountryB.text = AllLanguages.topTopCountry[iLang];
        textTopWorldB.font = AllLanguages.listFontLangA[iLang];
        textTopWorldB.text = AllLanguages.topTopWorld[iLang];
        textTopMultiplayerB.font = AllLanguages.listFontLangA[iLang];
        textTopMultiplayerB.text = AllLanguages.topTopMultiplayer[iLang];
        textTopFriendC.font = AllLanguages.listFontLangA[iLang];
        textTopFriendC.text = AllLanguages.topTopFriend[iLang];
        textTopCountryC.font = AllLanguages.listFontLangA[iLang];
        textTopCountryC.text = AllLanguages.topTopCountry[iLang];
        textTopWorldC.font = AllLanguages.listFontLangA[iLang];
        textTopWorldC.text = AllLanguages.topTopWorld[iLang];
        textTopMultiplayerC.font = AllLanguages.listFontLangA[iLang];
        textTopMultiplayerC.text = AllLanguages.topTopMultiplayer[iLang];
        textLoadingA.font = AllLanguages.listFontLangA[iLang];
        textLoadingA.text = AllLanguages.topLoading[iLang];
        textLoadingB.font = AllLanguages.listFontLangA[iLang];
        textLoadingB.text = AllLanguages.topLoading[iLang];
        textLoadingC.font = AllLanguages.listFontLangA[iLang];
        textLoadingC.text = AllLanguages.topLoading[iLang];
        textLoadingD.font = AllLanguages.listFontLangA[iLang];
        textLoadingD.text = AllLanguages.topLoading[iLang];
        textLoginFB.font = AllLanguages.listFontLangA[iLang];
        textLoginFB.text = AllLanguages.topLoginFacebook[iLang];
        textNoteBonus.font = AllLanguages.listFontLangA[iLang];
        textNoteBonus.text = AllLanguages.topNoteGetStart[iLang] + " 10000 " + AllLanguages.topNoteGetEnd[iLang];
    }
}
