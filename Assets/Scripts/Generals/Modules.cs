using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Modules : MonoBehaviour {

    //public static bool versionExtra = true;//neu true thi lam cho phien ban mo rong (tro choi moi)
    public static int versionCode = 2;//danh dau phien ban code de tranh bi loi save map
    //cac bien luu tru trong game
    public static int totalKey = 10;//de hoi sinh (co duoc trong luc choi hoac mua)
    public static int totalCoin = 0;//de mua do, nang cap do (co duoc trong luc choi hoac mua)
    public static float totalScore = 100;//diem ky luc cua nguoi choi
	public static float totalScoreNow = 0;//xu ly diem ky luc cua ban be
    public static int totalSkis = 5;//de lam van truot trong game
    public static int totalScoreBooster = 0;//de thuc cong diem nhan man choi chu dong
    public static int totalHeadStart = 0;//de thuc hien bay rocket chu dong
    public static int totalMysteryBox = 0;//tong hop trong game
    //khai bao list nhan vat
    public static List<GameObject> listHeroUse = new List<GameObject>();
    public static List<GameObject> listSkisUse = new List<GameObject>();
    public static GameObject facebookController;
    //cac bien xu ly trong man choi
    public static float speedGame = 0.78f;//thay doi toan bo speed game, animation
    public static float maxSpeedGame = 1.5f;//toc do toi da cho phep
    public static float moreSpeedAni = 0;//toc do them luc ban dau cho cac animation chay
    public static float maxSpeedAni = 1.75f;//toc do toi da cua cac animation run
    public static int timePlayer = 0;
    public static int coinPlayer = 0;//so coin trong man choi
    public static float scorePlayer = 0;//so diem trong man choi
    public static int xPointPlayer = 1;//so diem nhan trong man choi
    public static int numberXpoint = 1;//so da nhan de khi het thoi gian thi tru di
    public static int timeSaveMe = 0;//so lan hoi sinh de tinh key = 2^time
    //de thuc hien mo ra cac mon qua nhu tien, cac do de ghep
    public static bool showScorePlay = false;//de xu ly show diem ky luc hay diem man choi
    public static bool autoTapPlay = false;//xu ly tu dong chay khi load scene play
    public static string nextPageOpenBox = "ShowAchievement";
    //danh dau cac item dang su dung
    public static bool useSkis = false;//neu trang bi van truot
    public static bool usePower = false;//neu trang bi nhay cao
    public static bool useMagnet = false;//neu trang bi nam cham
    public static bool useRocket = false;//neu trang bi ten lua
    public static bool useJumper = false;//neu trang bi jumper
    public static bool useXPoint = false;//neu trang bi xPoint
    public static bool useCable = false;//neu trang bi cap treo
    public static bool useBonus = false;//neu dang su dung bonus road
    //thoi gian ton tai cac item dac biet
    public static Vector2 timeUseSkis = new Vector2(10, 12);
    public static Vector2 timeUsePower = new Vector2(5, 10);
    public static Vector2 timeUseMagnet = new Vector2(5, 10);
    public static Vector2 timeUseRocket = new Vector2(5, 10);
    public static Vector2 timeUseXPoint = new Vector2(5, 10);
    public static Vector2 timeUseCable = new Vector2(5, 10);
    public static int levelUpgradePower = 0, levelUpgradeMagnet = 0, levelUpgradeRocket = 0, levelUpgradeXPoint = 0, levelUpgradeCable = 0, levelUpgradeSkis = 0;
    public static int maxLevelItem = 10;
    public static float timeAddPerLevel = 3f;
    public static bool allowUseHoverbike = true;//dung de bat/tat chuc nang su dung hoverbike
    public static int maxHoverboard = 9999;
    public static int maxHeadstart = 10;
    public static int maxScorebooster = 7;
    //cac doi tuong luan chuyen trong game
    public static GameObject mainCharacter;
    public static GameObject itemSkis, itemShoeLeft, itemShoeRight, itemMagnet, itemRocket, itemCable;
    public static Text textCoinPlay, textScorePlay, textXPointPlay, textHighScore;
    public static GameObject panelShowEatItemsLeft, panelShowEatItemsRight, panelEffectAddPoint, panelHighScoreNow, panelViewEnemy, panelGameGuide;
    public static GameObject panelMissions, panelChallenge, panelBonus, panelCrackGlass;
    public static GameObject containMesItems, containButtonBuy, containEffectAddPoint;
    public static GameObject mesSaveMeBox, mesNotEnoughKey, bonusFirstBox, resultOnlineBox;
    public static GameObject panelBGEffectBonus, panelTextEffectBonus, panelTextEffectWinLose;
    public static GameObject parentResultOnline, panelFakeCity;
    //cac bien su dung trong man choi chinh (chay)
    public static StatusGame statusGame = StatusGame.menu;
    public static bool startBonusRoad = false;//neu bang true moi bat dau chay tren bonus, neu khong thi danh cho hieu ung chay nguoc map
    public static string codeHeroUse = "001";//mac dinh la loai nhan vat nay
    public static string codeSkisUse = "001";//mac dinh la loai skis nay
    public static int distanceEnemy = 1;//2 khuat, 1 nhin thay, 0 bi bat
    public static int timeFarEnemy = 5;//cu chay deu lau hon thoi gian nay thi enemy bi xa ra
    public static float speedSlowCollider = 0f;//chi so toc do bi cham lai khi va cham
    public static float speedAddUseRocket = 2f;//chi so toc do tang len khi su dung rocket, cap treo
    public static float speedAddMoreUse = 1f;//speed dang duoc su dung lam cham, nhanh them tu cac item
    public static List<string> listHeroUnlock = new List<string>() { "001" };
    public static List<string> listSkisUnlock = new List<string>() { "001" };
    public static float rangeReborn = 100f;//ban kinh pha huy cac vat can, items
    public static float rangeRunObj = 100f;//khoang cach chay xe bus, train...
    public static float rangeShowCoin = 50f;
    public static float rangeTakeOff = 30f;//khoang cach tao cac item, barrier khi ha canh boolone hoac road bonus
    public static float rangeHireBack = 10f;//khang cach them khi vat can va item ve phia sau thi an di cho nhe game
    public static float timeShowChest = 1.5f;
    public static bool runAffterDownBonus = false;//chi co tac dung phan chay ngay lap tuc sau khi roi tu bonus xuong
    public static bool runGameOverEffect = false;
    public static bool getPointFirst = false;
    //cac bien luu tru setting
    public static int indexLanguage = 0;
    public static int volumeBackground = 1;//1 true, 0 false
    public static int volumeAction = 1;//1 true, 0 false
    public static int reducedEffect = 0;//0 high, 1 medium, 2 low
    public static int skyEffect = 0;//1 true, 0 false
    public static int valueSensitivity = 60;//0=>100
    //public static int valueSpeedJumpUp = 14;//0=>40
    public static int countryEnemy = 1;//1 country, 0 friend
    public static int curvedWorld = 1;//1 true, 0 false
    public static int clickRate = 0;//1 la roi, 0 la chua
    public static int totalUseGame = 0;
    public static int autoCheckLang = 1;//0 thi khong check nua
    public static int indexTypeTerrain = 0;//vi tri the loai dia hinh dang chay
    public static int indexRunTerrain = 0;//vi tri dia hinh dang chay
    public static float pointRunTerrain = 0;//vi tri z hinh dang chay
    public static float pointHeroTerrain = 0;//vi tri cua hero so voi dia hinh (tren, giua, duoi)
    public static string listCodeTerrain = "";//ma terrain dang chay duoc luu lai
    //xu ly facebook sdk
    public static string fbID = "Null";//lay id facebook
	public static string fbName = "Null";
	public static string fbLinkAvatar = "Null";
	public static Sprite fbMyAvatar, fbEnemyAvatar;
    public static List<string> fbAvatarEnemy = new List<string>();//danh sach link avatar doi thu co diem so cao hon
    public static List<string> fbNameEnemy = new List<string>();//danh sach name doi thu co diem so cao hon
	public static List<float> fbHighScore = new List<float>();//danh sach diem so cua doi thu co diem so cao hon
    public static string bonusFacebook = "No";//neu bang 1 thi da thuong roi
    public static Color colorListLine = new Color(235f / 255f, 235f / 255f, 235f / 255f, 1f);
    public static int coinBonusInvite = 300;//so coin thuong sau moi lan moi 1 ban be
    public static int coinMaxInvite = 5000;//so coin thuong toi da, ke ca co moi nhieu hon nua
    //cac bien rank
    public static string linkPost = "http://baimathuat.ga/PostScoreBS.php";
    public static string linkGet = "http://baimathuat.ga/GetScoreBS.php";
    public static string linkGetCountry = "http://baimathuat.ga/GetScoreCountryBS.php";
    public static string linkGetDataMultiplayer = "http://baimathuat.ga/GetDataMultiplayerBS.php";
    public static string linkGetMissions = "http://baimathuat.ga/GetMissions.php";
    public static string linkGetChallenge = "http://baimathuat.ga/GetChallenge.php";
    public static float requestTime = 0.1f;
    public static float maxTime = 10f;//cho phep thuc hien toi da 10 giay
    //xu ly inApp purcharse va quang cao
    public static List<string> listProductID = new List<string>() { "ProductCoinA", "ProductKeyA" };
    public static string itemBonusViewAds = "Skis";
    public static Vector2 numberSkisBonus = new Vector2(1, 3);
    public static Vector2 numberKeyBonus = new Vector2(1, 2);
    public static int timeShowInterstitial = 1;//sau 5 lan chet moi hien thi quang cao
    public static int runTimeShowInterstitial = 0;
    //khai bao cac list icon
    public static List<Sprite> listIconItem = new List<Sprite>();//0 skis, 1 giay, 2 magnet, 3 rocket, 4 2X, 5 cable
    public static List<Sprite> listIconBonus = new List<Sprite>();//0 coin, 1 keys, 2 skis, 3 headStart, 4 scoreBooster
    public static List<ListMissionsClass> listMissions = new List<ListMissionsClass>();//chua cac icon, model de suu tap item
    public static List<ListChallengeClass> listChallenge = new List<ListChallengeClass>();//chua cac gia tri, model de suu tap chu
    public static Sprite iconHeadStart, iconScoreBooster, iconAvatarNull;
    //xu ly effect sang toi
    public static Vector2 KTCScenes = new Vector2(480, 800);
    //xu ly missions va challenge
    public static bool newMissions = false;
    public static bool newChallenge = false;
    public static string dataMissionsUse = "";//xu ly van de nhiem vu dang nhan
    public static string dataChallengeUse = "";//xu ly van de thu thach dang nhan
    public static string dataMissionsNew = "";//xu ly van de nhiem vu dang co san
    public static string dataChallengeNew = "";//xu ly van de thu thach dang co san
    public static string dataMissionsOld = "";//xu ly van de check nhiem vu da lam truoc do
    public static string dataChallengeOld = "";//xu ly van de check thu thach da lam truoc do
    public static int indexItemMissions = 0;
    public static int totalItemMissions = 0;
    public static int runItemMissions = 0;
    public static int indexBonusMissions = 0;
    public static int totalBonusMissions = 0;
    public static List<string> listTextRequire = new List<string>();
    public static List<string> listTextColect = new List<string>();
    public static int indexBonusChallenge = 0;
    public static int totalBonusChallenge = 0;
    public static bool autoGetMissions = true;//tu dong nhan neu chua co nhiem vu
    public static bool autoGetChallenge = true;//tu dong nhan neu chua co thu thach
    //xu ly gop cac scene
    public static GameObject containMainGame, containHeroConstruct, containAchievement, containShopItem, containHighScore, containOpenBox;
    public static GameObject listResources;//doi tuong chua cac component resources
    public static GameObject poolTerrains;//chua cac dia hinh
    public static GameObject poolOthers;//chua cac items, paticles
    //xu ly phan huong dan choi
    public static string gameGuide = "YES";
    public static int stepGuide = 0;//0 left, 1 right, 2 jump, 3 down, 4 double tap
    //them cac audios
    public static List<AudioSource> audioSource;
    public static AudioClip audioBackgrond, audioRoadBonus;
    public static AudioClip audioButton, audioTapPlay, audioOpenBox, audioSwipeMove, audioSwipeUp, audioSwipeDown, audioUpSkis;
    public static AudioClip audioCollider, audioColliderDie, audioSurprise, audioShowMessage, audioRocket, audioCable, audioTrapoline, audioBrokenGlass;
    public static AudioClip audioParReborn, audioParColSkis, audioBonusText, audioEatBonusBox;
    public static AudioClip audioPoStart, audioPoNear, audioPoFar;
    public static float maxPitchCoin = 0.95f;
    public static float minPitchCoin = 0.75f;
    public static float pitchCoin = 0.95f;
    public static float numChangePith = 0.04f;
    public static float oldTime = 0;
    //them cac particles
    public static GameObject parReborn, parSkisCollider, parSpeedFly;
    public static Text textDebug;
    //xu ly online mode
    public static string namePlayOnline = "";
    public static string nameRoomOnline = "";
    public static List<string> listNamePlayer = new List<string>();
    public static bool startViewOnline = false;
    public static GameObject networkManager;
    public static int winNow = 0, loseNow = 0, failNow = 0;
    //xu ly cac thuong them
    public static int bonusFirst = 0;//0 la chua, 1 la thuong roi
    public static int bonusWin = 0;//0 la chua, 1 la lan 1, 2 la lan 2, 3 la lan 3
    public static int totalWin = 0;//tong so lan win de tinh toan thuong

    public static void ResetGame()
    {
        statusGame = StatusGame.menu;
        startBonusRoad = false;
        speedGame = 0.78f;
        speedAddMoreUse = 1f;
        timePlayer = 0;
        coinPlayer = 0;
        scorePlayer = 0;
        xPointPlayer = 1;
        numberXpoint = 1;
        useSkis = false;
        usePower = false;
        useMagnet = false;
        useRocket = false;
        useJumper = false;
        useXPoint = false;
        useCable = false;
        useBonus = false;
        allowUseHoverbike = true;
        distanceEnemy = 1;
        timeSaveMe = 0;
        totalMysteryBox = 0;
        pitchCoin = 0.95f;
        oldTime = 0;
        showScorePlay = false;
        runGameOverEffect = false;
        getPointFirst = false;
        ResetMissions();
        ResetChallenge();
        if (panelCrackGlass) panelCrackGlass.gameObject.SetActive(false);
        listResources.GetComponent<LoadListEnemy>().CallStart();
        panelHighScoreNow.SetActive(false);
        panelViewEnemy.SetActive(false);
        panelBonus.SetActive(false);
        panelBGEffectBonus.SetActive(false);
        panelTextEffectBonus.SetActive(false);
        panelTextEffectWinLose.SetActive(false);
        parentResultOnline.SetActive(false);
        if (gameGuide == "YES")
        {
            stepGuide = 0;
            foreach (Transform tran in panelGameGuide.transform) tran.gameObject.SetActive(true);
            Transform textGuide = panelGameGuide.transform.Find("TextGuide");
            textGuide.GetComponent<Text>().text = "Swipe left";
            Transform arrowGuide = panelGameGuide.transform.Find("ArrowGuide");
            arrowGuide.transform.eulerAngles = new Vector3(0, 0, 0);
            Transform iconItemBuy = panelGameGuide.transform.Find("IconItemBuy");
            iconItemBuy.GetComponent<Image>().enabled = false;
        }
        foreach (Transform tran in containButtonBuy.transform) Destroy(tran.gameObject);
        foreach (Transform tran in containMesItems.transform) Destroy(tran.gameObject);
        textXPointPlay.text = "x" + xPointPlayer;
        poolOthers.GetComponent<HighItemsController>().ResetAllItems();
        Modules.startViewOnline = false;
    }

    public static void SetAllowHoverbike(bool valueSet)
    {
        if (valueSet == allowUseHoverbike) return;
        allowUseHoverbike = valueSet;
        foreach (Transform tran in containButtonBuy.transform)
        {
            ButtonItemBuy buttonClick = tran.GetComponent<ButtonItemBuy>();
            if (buttonClick.codeItem == "headStart")
            {
                Transform lockTran = tran.Find("IconLock");
                lockTran.GetComponent<Image>().enabled = !allowUseHoverbike;
                foreach (Transform tranChild in lockTran) tranChild.gameObject.SetActive(!allowUseHoverbike);
            }
        }
    }

    public static void ResetMissions()
    {
        if (dataMissionsUse == "") return;
        string[] data = dataMissionsUse.Split(';');
        indexItemMissions = IntParseFast(data[2]);
        totalItemMissions = IntParseFast(data[3]);
        indexBonusMissions = IntParseFast(data[4]);
        totalBonusMissions = IntParseFast(data[5]);
        runItemMissions = 0;
    }

    public static void ResetChallenge()
    {
        if (dataChallengeUse == "") return;
        listTextRequire = new List<string>();
        listTextColect = new List<string>();
        string[] data = dataChallengeUse.Split(';');
        string dataText = data[2];
        for (int i = 0; i < dataText.Length; i++)
            listTextRequire.Add(dataText.Substring(i, 1));
        indexBonusChallenge = IntParseFast(data[3]);
        string[] dataValue = data[4].Split(',');
        totalBonusChallenge = UnityEngine.Random.Range(IntParseFast(dataValue[0]), IntParseFast(dataValue[1]));
    }

    public static void RebornGame()
    {
        statusGame = StatusGame.play;
        speedAddMoreUse = 1f;
        useSkis = false;
        usePower = false;
        useMagnet = false;
        useRocket = false;
        useJumper = false;
        useXPoint = false;
        useCable = false;
        useBonus = false;
        distanceEnemy = 2;
        timeSaveMe++;
        if (panelCrackGlass) panelCrackGlass.gameObject.SetActive(false);
        runGameOverEffect = false;
        getPointFirst = false;
    }

    public static Vector2 GetTimeItemUse(TypeItems codeItem)
    {
        if (codeItem == TypeItems.sneaker)//power
            return new Vector2(timeUsePower.x + timeAddPerLevel * levelUpgradePower, timeUsePower.y + timeAddPerLevel * levelUpgradePower);
        else if (codeItem == TypeItems.magnet)//magnet
            return new Vector2(timeUseMagnet.x + timeAddPerLevel * levelUpgradeMagnet, timeUseMagnet.y + timeAddPerLevel * levelUpgradeMagnet);
        else if (codeItem == TypeItems.jetpack)//rocket
            return new Vector2(timeUseRocket.x + timeAddPerLevel * levelUpgradeRocket, timeUseRocket.y + timeAddPerLevel * levelUpgradeRocket);
        else if (codeItem == TypeItems.xpoint)//x point
            return new Vector2(timeUseXPoint.x + timeAddPerLevel * levelUpgradeXPoint, timeUseXPoint.y + timeAddPerLevel * levelUpgradeXPoint);
        else if (codeItem == TypeItems.hoverbike)//cable
            return new Vector2(timeUseCable.x + timeAddPerLevel * levelUpgradeCable, timeUseCable.y + timeAddPerLevel * levelUpgradeCable);
        else
            return new Vector2(timeUseSkis.x + timeAddPerLevel * levelUpgradeSkis, timeUseSkis.y + timeAddPerLevel * levelUpgradeSkis);
    }

    public static int SecondsToTimePerFrame(int seconds)
    {
        return Mathf.CeilToInt(seconds / Time.fixedDeltaTime);
    }

    public static int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }

    public static GameObject GetChildGameObject(GameObject obj, string name)
    {
        GameObject ketQua = null;
        foreach (Transform go in obj.transform)
        {
            if (go.name == name)
            {
                ketQua = go.gameObject;
                break;
            }
            else if (go.childCount > 0)
            {
                ketQua = GetChildGameObject(go.gameObject, name);
                if (ketQua != null)
                    break;
            }
        }
        return ketQua;
    }

    public static GameObject SetModelUseItem(Transform tran, string codeBody, GameObject itemAdd, string nameItem, bool autoScale = true)
    {
        GameObject itemUse = null;
        ItemAddHero itemHero = itemAdd.GetComponent<ItemAddHero>();
        for (int i = 0; i < itemHero.setupItem.Count; i++)
        {
            if (itemHero.setupItem[i].codeBody == codeBody)
            {
                GameObject goAddItem = Modules.GetChildGameObject(tran.gameObject, itemHero.setupItem[i].nameBoneAdd);
                if (goAddItem != null)
                {
                    itemUse = Instantiate(itemAdd, goAddItem.transform.position, Quaternion.identity) as GameObject;
                    itemUse.name = nameItem;
                    itemUse.transform.parent = goAddItem.transform;
                    itemUse.transform.localPosition = itemHero.setupItem[i].localPoint;
                    Vector3 rota = itemHero.setupItem[i].localAngle;
                    itemUse.transform.localRotation = Quaternion.Euler(rota.x, rota.y, rota.z);
                    if (autoScale)
                    {
                        itemUse.transform.localScale = new Vector3(
                            itemUse.transform.localScale.x * itemHero.setupItem[i].localScale.x,
                            itemUse.transform.localScale.y * itemHero.setupItem[i].localScale.y,
                            itemUse.transform.localScale.y * itemHero.setupItem[i].localScale.y);
                    }
                }
                break;
            }
        }
        return itemUse;
    }

    public static void RemoveModelUseItem(Transform tran, string nameItem)
    {
        GameObject goAddItem = Modules.GetChildGameObject(tran.gameObject, nameItem);
        if (goAddItem != null)
            Destroy(goAddItem);
    }

    public static GameObject HideModelUseItem(Transform tran, string nameItem)
    {
        GameObject goAddItem = Modules.GetChildGameObject(tran.gameObject, nameItem);
        if (goAddItem != null)
            goAddItem.SetActive(false);
        return goAddItem;
    }

    public static void SetAllRenderer(Transform tran, List<string> nameStop, bool status)
    {
        if (tran.GetComponent<Renderer>() != null)
        {
            tran.GetComponent<Renderer>().enabled = status;
        }
        foreach (Transform child in tran)
        {
            if (!nameStop.Contains(child.name))
                SetAllRenderer(child, nameStop, status);
        }
    }

    public static void ShowPanelEffectAddPoint(int pointAdd, Vector3 location, GameObject parent, float timeDestroy)
    {
        GameObject panelShow = Instantiate(panelEffectAddPoint, location, Quaternion.identity) as GameObject;
        panelShow.transform.SetParent(parent.transform, false);
        Transform number = panelShow.transform.Find("TextXPoint");
        Text txtNumber = number.GetComponent<Text>();
        txtNumber.text = "+" + pointAdd.ToString();
        Destroy(panelShow, timeDestroy);
    }

    public static void SetPanelShowItem(TypeItems codeItem, Sprite icon)
    {
        float heightMes = 60;//chieu cao cua mes
        float pointYMes = 0;//vi tri ban dau cua mes
        //check exit item
        foreach (Transform tran in containMesItems.transform)
        {
            if (tran.GetComponent<PanelShowUseItem>().codeItemNow == codeItem)
            {
                //update lai item dang dung neu dang chay ra hoac dang hien thi
                Animator aniNow = tran.GetComponent<Animator>();
                aniNow.ResetTrigger("TriOpen");
                aniNow.ResetTrigger("TriClose");
                aniNow.SetTrigger("TriOpen");
                tran.GetComponent<PanelShowUseItem>().ResetTime(Modules.GetTimeItemUse(codeItem));
                return;
            }
        }
        int indexSlot = 0;
        if (totalHeadStart != 0 || totalScoreBooster != 0) indexSlot = 1;
        List<int> listIndexUse = new List<int>();
        foreach (Transform tran in containMesItems.transform)
            listIndexUse.Add(tran.GetComponent<PanelShowUseItem>().numberSlot);
        if (listIndexUse.Count > 0)
        {
            listIndexUse.Sort();
            for (int i = 0; i <= listIndexUse[listIndexUse.Count - 1] + 2; i++)
            {
                bool checkOk = false;
                if (totalHeadStart == 0 && totalScoreBooster == 0)
                    checkOk = !listIndexUse.Contains(i);
                else
                    checkOk = (!listIndexUse.Contains(i) && (i % 2 != 0));
                if (checkOk)
                {
                    indexSlot = i;
                    break;
                }
            }
        }
        //tao moi item use
        GameObject panelInstan = panelShowEatItemsRight;
        float pointX = 250;
        if (indexSlot % 2 == 0)
        {
            panelInstan = panelShowEatItemsLeft;
            pointX = -490;
        }
        int heightNow = indexSlot / 2;
        GameObject mesShowItem = Instantiate(panelInstan, new Vector3(pointX, pointYMes + heightMes * heightNow, 0), Quaternion.identity) as GameObject;
        mesShowItem.transform.SetParent(containMesItems.transform, false);
        PanelShowUseItem inforShowItem = mesShowItem.AddComponent<PanelShowUseItem>();
        inforShowItem.numberSlot = indexSlot;
        inforShowItem.codeItemNow = codeItem;
        inforShowItem.timeUseItem = Modules.GetTimeItemUse(codeItem);
        Transform avatar = mesShowItem.transform.Find("Icon");
        Transform progressBar = mesShowItem.transform.Find("RunTime");
        Transform number = mesShowItem.transform.Find("Number");
        Image imgAvatar = avatar.GetComponent<Image>();
        Image imgProgressBar = progressBar.GetComponent<Image>();
        Text txtNumber = number.GetComponent<Text>();
        imgAvatar.sprite = icon;
        imgProgressBar.fillAmount = 1f;
        txtNumber.text = "";
        if (codeItem == TypeItems.hoverboard) txtNumber.text = (totalSkis - 1).ToString();
        Animator aniPanel = mesShowItem.GetComponent<Animator>();
        aniPanel.SetTrigger("TriOpen");
    }

    public static void LoadDataSave()
    {
#if UNITY_WEBGL
        SaveLoadData.khoaBiMat = "magic";
#else
        SaveLoadData.khoaBiMat = SystemInfo.deviceUniqueIdentifier.Substring(0, 5);
#endif
        string dataScore = SaveLoadData.LoadData("SaveScore", true);
        if (dataScore == "") dataScore = totalScore.ToString();
        totalScore = IntParseFast(dataScore);
        string dataCoin = SaveLoadData.LoadData("SaveCoin", true);
        if (dataCoin == "") dataCoin = "0";
        totalCoin = IntParseFast(dataCoin);
        string dataKey = SaveLoadData.LoadData("SaveKey", true);
        if (dataKey == "") dataKey = "0";
        totalKey = IntParseFast(dataKey);
        string dataSkis = SaveLoadData.LoadData("SaveSkis", true);
        if (dataSkis == "") dataSkis = "5";
        totalSkis = IntParseFast(dataSkis);
        string dataHeadStart = SaveLoadData.LoadData("SaveHeadStart", true);
        if (dataHeadStart == "") dataHeadStart = "3";
        totalHeadStart = IntParseFast(dataHeadStart);
        string dataScoreBooster = SaveLoadData.LoadData("SaveScoreBooster", true);
        if (dataScoreBooster == "") dataScoreBooster = "0";
        totalScoreBooster = IntParseFast(dataScoreBooster);
        string dataBodyHero = SaveLoadData.LoadData("SaveBodyHero", true);
        if (dataBodyHero == "") dataBodyHero = "001";
        codeHeroUse = dataBodyHero;
        string dataSkisHero = SaveLoadData.LoadData("SaveSkisHero", true);
        if (dataSkisHero == "") dataSkisHero = "001";
        codeSkisUse = dataSkisHero;
        string dataLevelRocket = SaveLoadData.LoadData("SaveLevelRocket", true);
        if (dataLevelRocket == "") dataLevelRocket = "0";
        levelUpgradeRocket = IntParseFast(dataLevelRocket);
        string dataLevelPower = SaveLoadData.LoadData("SaveLevelPower", true);
        if (dataLevelPower == "") dataLevelPower = "0";
        levelUpgradePower = IntParseFast(dataLevelPower);
        string dataLevelMagnet = SaveLoadData.LoadData("SaveLevelMagnet", true);
        if (dataLevelMagnet == "") dataLevelMagnet = "0";
        levelUpgradeMagnet = IntParseFast(dataLevelMagnet);
        string dataLevelXPoint = SaveLoadData.LoadData("SaveLevelXPoint", true);
        if (dataLevelXPoint == "") dataLevelXPoint = "0";
        levelUpgradeXPoint = IntParseFast(dataLevelXPoint);
        string dataLevelCable = SaveLoadData.LoadData("SaveLevelCable", true);
        if (dataLevelCable == "") dataLevelCable = "0";
        levelUpgradeCable = IntParseFast(dataLevelCable);
        string dataLevelSkis = SaveLoadData.LoadData("SaveLevelSkis", true);
        if (dataLevelSkis == "") dataLevelSkis = "0";
        levelUpgradeSkis = IntParseFast(dataLevelSkis);
        //lay du lieu hero unlock
        string dataHeroUnlock = SaveLoadData.LoadData("SaveHeroUnlock", true);
        if (dataHeroUnlock == "") dataHeroUnlock = "001";
        string[] dataHeroU = dataHeroUnlock.Split(';');
        listHeroUnlock = new List<string>();
        for (int i = 0; i < dataHeroU.Length; i++) listHeroUnlock.Add(dataHeroU[i]);
        //lay du lieu skis unlock
        string dataSkisUnlock = SaveLoadData.LoadData("SaveSkisUnlock", true);
        if (dataSkisUnlock == "") dataSkisUnlock = "001";
        string[] dataSkisU = dataSkisUnlock.Split(';');
        listSkisUnlock = new List<string>();
        for (int i = 0; i < dataSkisU.Length; i++) listSkisUnlock.Add(dataSkisU[i]);
        string dataBonusFacebook = SaveLoadData.LoadData("SaveBonusFacebook", true);
        if (dataBonusFacebook == "") dataBonusFacebook = "No";
        bonusFacebook = dataBonusFacebook;
        //load save setting
        string dataCheckLanguage = SaveLoadData.LoadData("SaveCheckLanguage", true);
        if (dataCheckLanguage == "") dataCheckLanguage = "1";
        autoCheckLang = IntParseFast(dataCheckLanguage);
        bool loadLang = true;
        if (autoCheckLang == 1)
        {
            string codeLang = GetCountryPlayer.ToCountryCode(Application.systemLanguage);
            if (AllLanguages.listLangShort.Contains(codeLang))
            {
                int indexLangTemp = AllLanguages.listLangShort.IndexOf(codeLang);
                if (AllLanguages.listSupport[indexLangTemp])
                {
                    indexLanguage = indexLangTemp;
                    autoCheckLang = 0;
                    SaveCheckLanguage();
                    SaveSettingValue();
                    loadLang = false;
                }
            }
        }
        if (loadLang)
        {
            string dataSaveLanguage = SaveLoadData.LoadData("SaveSetLanguage", true);
            if (dataSaveLanguage == "") dataSaveLanguage = "EN";
            int indexLangTemp = 0;
            if (AllLanguages.listLangShort.Contains(dataSaveLanguage))
                indexLangTemp = AllLanguages.listLangShort.IndexOf(dataSaveLanguage);
            if (indexLangTemp >= 0 && indexLangTemp < AllLanguages.listSupport.Count && AllLanguages.listSupport[indexLangTemp])
                indexLanguage = indexLangTemp;
        }
        if (indexLanguage < 0) indexLanguage = 0;
        if (fbName == "Null") fbName = AllLanguages.topNoName[indexLanguage];
        string dataSaveVolumeBG = SaveLoadData.LoadData("SaveSetVolumeBG", true);
        if (dataSaveVolumeBG == "") dataSaveVolumeBG = "1";
        volumeBackground = IntParseFast(dataSaveVolumeBG);
        string dataSaveVolumeAT = SaveLoadData.LoadData("SaveSetVolumeAT", true);
        if (dataSaveVolumeAT == "") dataSaveVolumeAT = "1";
        volumeAction = IntParseFast(dataSaveVolumeAT);
        string dataReducedEffect = SaveLoadData.LoadData("SaveReducedEffect", true);
        if (dataReducedEffect == "") dataReducedEffect = "0";
        reducedEffect = IntParseFast(dataReducedEffect);
        //string dataSkyEffect = SaveLoadData.LoadData("SaveSkyEffect", true);
        //if (dataSkyEffect == "") dataSkyEffect = "0";
        //skyEffect = IntParseFast(dataSkyEffect);
        string dataSensitivity = SaveLoadData.LoadData("SaveSensitivity", true);
        if (dataSensitivity == "") dataSensitivity = "60";
        valueSensitivity = IntParseFast(dataSensitivity);
        //string dataSpeedJumpUp = SaveLoadData.LoadData("SaveSpeedJumpUp", true);
        //if (dataSpeedJumpUp == "") dataSpeedJumpUp = "14";
        //valueSpeedJumpUp = IntParseFast(dataSpeedJumpUp);
        string dataCountryEnemy = SaveLoadData.LoadData("SaveCountryEnemy", true);
        if (dataCountryEnemy == "") dataCountryEnemy = "1";
        countryEnemy = IntParseFast(dataCountryEnemy);
        string dataCurvedWorld = SaveLoadData.LoadData("SaveCurvedWorld", true);
        if (dataCurvedWorld == "") dataCurvedWorld = "1";
        curvedWorld = IntParseFast(dataCurvedWorld);
        string dataRateGame = SaveLoadData.LoadData("SaveRateGame", true);
        if (dataRateGame == "") dataRateGame = "0";
        clickRate = IntParseFast(dataRateGame);
        string dataCheckInviteFB = SaveLoadData.LoadData("SaveCheckInviteFB", true);
        if (dataCheckInviteFB == "") dataCheckInviteFB = "5000";
        coinMaxInvite = IntParseFast(dataCheckInviteFB);
        //xu ly du lieu ban do hien tai
        int dataOldVersion = PlayerPrefs.GetInt("SaveVersionCode", 0);
        if (dataOldVersion != versionCode)
        {
            PlayerPrefs.SetInt("SaveVersionCode", versionCode);
            SaveLoadData.DeleteVar("SaveIndexTypeTerrain");
            SaveLoadData.DeleteVar("SaveIndexRunTerrain");
            SaveLoadData.DeleteVar("SavePointRunTerrain");
            SaveLoadData.DeleteVar("SaveCodeRunTerrain");
        }
        string dataIndexTypeTerrain = SaveLoadData.LoadData("SaveIndexTypeTerrain", true);
        if (dataIndexTypeTerrain == "") dataIndexTypeTerrain = "0";
        indexTypeTerrain = IntParseFast(dataIndexTypeTerrain);
        string dataIndexRunTerrain = SaveLoadData.LoadData("SaveIndexRunTerrain", true);
        if (dataIndexRunTerrain == "") dataIndexRunTerrain = "0";
        indexRunTerrain = IntParseFast(dataIndexRunTerrain);
        string dataPointRunTerrain = SaveLoadData.LoadData("SavePointRunTerrain", true);
        if (dataPointRunTerrain == "") dataPointRunTerrain = "0";
        bool minusA = false;
        if (dataPointRunTerrain[0] == '-')
        {
            minusA = true;
            dataPointRunTerrain = dataPointRunTerrain.Replace("-", "");
        }
        pointRunTerrain = (float)IntParseFast(dataPointRunTerrain) / 1000f;
        if (minusA) pointRunTerrain *= -1;
        string dataPointHeroTerrain = SaveLoadData.LoadData("SavePointHeroTerrain", true);
        if (dataPointHeroTerrain == "") dataPointHeroTerrain = "0";
        bool minusB = false;
        if (dataPointHeroTerrain[0] == '-')
        {
            minusB = true;
            dataPointHeroTerrain = dataPointHeroTerrain.Replace("-", "");
        }
        pointHeroTerrain = (float)IntParseFast(dataPointHeroTerrain) / 1000f;
        if (minusB) pointHeroTerrain *= -1;
        string dataCodeRunTerrain = SaveLoadData.LoadData("SaveCodeRunTerrain", true);
        listCodeTerrain = dataCodeRunTerrain;
        //load huong dan
        gameGuide = SaveLoadData.LoadData("SaveGameGuide", true);
        if (gameGuide == "") gameGuide = "YES";
        //xu ly missions va challenge
        string oldDay = SaveLoadData.LoadData("SaveOldDay", true);
        if (oldDay == "") oldDay = "1;1;2000";
        string[] dataDay = oldDay.Split(';');
        int day = IntParseFast(dataDay[0]);
        int month = IntParseFast(dataDay[1]);
        int year = IntParseFast(dataDay[02]);
        int dayNow = DateTime.Now.Day;
        int monthNow = DateTime.Now.Month;
        int yearNow = DateTime.Now.Year;
        if (dayNow != day || monthNow != month || yearNow != year)
        {
            newMissions = true;
            newChallenge = true;
            SaveLoadData.SaveData("SaveOldDay",
                dayNow.ToString() + ";" + monthNow.ToString() + ";" + yearNow.ToString(), true);
            coinMaxInvite = 5000;
            SaveCheckInviteFB();
        }
        else
        {
            newMissions = false;
            newChallenge = false;
        }
        //load save data missions challenge
        dataMissionsUse = SaveLoadData.LoadData("SaveDataMissions", true);
        dataChallengeUse = SaveLoadData.LoadData("SaveDataChallenge", true);
        dataMissionsOld = SaveLoadData.LoadData("SaveDataMissionsOld", true);
        dataChallengeOld = SaveLoadData.LoadData("SaveDataChallengeOld", true);
        //xu ly cac bien online
        namePlayOnline = SaveLoadData.LoadData("NetworkNamePlay", true).Replace(" ", "");
        if (namePlayOnline == "")
            namePlayOnline = "Play" + UnityEngine.Random.Range(0, 1000);
        nameRoomOnline = SaveLoadData.LoadData("NetworkNameRoom", true).Replace(" ", "");
        if (nameRoomOnline == "")
            nameRoomOnline = "Room" + UnityEngine.Random.Range(0, 1000);
        //xu ly bonus firt and win
        string dataBonus = SaveLoadData.LoadData("SaveBonusWinFirst", true);
        if (dataBonus == "") dataBonus = "0;0;0";
        string[] dataSplit = dataBonus.Split(';');
        bonusFirst = IntParseFast(dataSplit[0]);
        bonusWin = IntParseFast(dataSplit[1]);
        totalWin = IntParseFast(dataSplit[2]);
    }

    public static void SaveNetworkNamePlay()
    {
        SaveLoadData.SaveData("NetworkNamePlay", namePlayOnline, true);
    }

    public static void SaveBonusWinFirst()
    {
        SaveLoadData.SaveData("SaveBonusWinFirst", bonusFirst.ToString() + ";" + bonusWin.ToString() + ";" + totalWin.ToString(), true);
    }

    public static void SaveNetworkNameRoom()
    {
        SaveLoadData.SaveData("NetworkNameRoom", nameRoomOnline, true);
    }

    public static void SaveBonusFacebook()
    {
        SaveLoadData.SaveData("SaveBonusFacebook", bonusFacebook, true);
    }

    public static void SaveScore()
    {
        SaveLoadData.SaveData("SaveScore", Mathf.RoundToInt(totalScore).ToString(), true);
    }

    public static void SaveCoin()
    {
        SaveLoadData.SaveData("SaveCoin", Mathf.RoundToInt(totalCoin).ToString(), true);
    }

    public static void SaveKey()
    {
        SaveLoadData.SaveData("SaveKey", Mathf.RoundToInt(totalKey).ToString(), true);
    }

    public static void SaveSkis()
    {
        SaveLoadData.SaveData("SaveSkis", Mathf.RoundToInt(totalSkis).ToString(), true);
    }

    public static void SaveHeadStart()
    {
        SaveLoadData.SaveData("SaveHeadStart", Mathf.RoundToInt(totalHeadStart).ToString(), true);
    }

    public static void SaveScoreBooster()
    {
        SaveLoadData.SaveData("SaveScoreBooster", Mathf.RoundToInt(totalScoreBooster).ToString(), true);
    }

    public static void SaveBodyHero()
    {
        SaveLoadData.SaveData("SaveBodyHero", codeHeroUse, true);
    }

    public static void SaveSkisHero()
    {
        SaveLoadData.SaveData("SaveSkisHero", codeSkisUse, true);
    }

    public static void SaveLevelRocket()
    {
        SaveLoadData.SaveData("SaveLevelRocket", levelUpgradeRocket.ToString(), true);
    }

    public static void SaveLevelPower()
    {
        SaveLoadData.SaveData("SaveLevelPower", levelUpgradePower.ToString(), true);
    }

    public static void SaveLevelMagnet()
    {
        SaveLoadData.SaveData("SaveLevelMagnet", levelUpgradeMagnet.ToString(), true);
    }

    public static void SaveLevelXPoint()
    {
        SaveLoadData.SaveData("SaveLevelXPoint", levelUpgradeXPoint.ToString(), true);
    }

    public static void SaveLevelCable()
    {
        SaveLoadData.SaveData("SaveLevelCable", levelUpgradeCable.ToString(), true);
    }

    public static void SaveLevelSkis()
    {
        SaveLoadData.SaveData("SaveLevelSkis", levelUpgradeSkis.ToString(), true);
    }

    public static void SaveSettingValue()
    {
        SaveLoadData.SaveData("SaveSetLanguage", AllLanguages.listLangShort[indexLanguage].ToString(), true);
        SaveLoadData.SaveData("SaveSetVolumeBG", volumeBackground.ToString(), true);
        SaveLoadData.SaveData("SaveSetVolumeAT", volumeAction.ToString(), true);
        SaveLoadData.SaveData("SaveReducedEffect", reducedEffect.ToString(), true);
        SaveLoadData.SaveData("SaveSkyEffect", skyEffect.ToString(), true);
        SaveLoadData.SaveData("SaveSensitivity", valueSensitivity.ToString(), true);
        //SaveLoadData.SaveData("SaveSpeedJumpUp", valueSpeedJumpUp.ToString(), true);
        SaveLoadData.SaveData("SaveCountryEnemy", countryEnemy.ToString(), true);
        SaveLoadData.SaveData("SaveCurvedWorld", curvedWorld.ToString(), true);
    }

    public static void UpdateValueSensitivity()
    {
        int value = Mathf.RoundToInt(((50 - valueSensitivity) / 10f) * 2);
        PageMainGame pageMain = Camera.main.GetComponent<PageMainGame>();
        pageMain.minSwipeDistX = pageMain.originSwipeDistX + value;
        pageMain.minSwipeDistY = pageMain.originSwipeDistY + value;
    }

    public static void SaveListHeroUnlock()
    {
        string data = "";
        for (int i = 0; i < listHeroUnlock.Count; i++)
        {
            data += listHeroUnlock[i];
            if (i < listHeroUnlock.Count - 1) data += ";";
        }
        SaveLoadData.SaveData("SaveHeroUnlock", data, true);
    }

    public static void SaveListSkisUnlock()
    {
        string data = "";
        for (int i = 0; i < listSkisUnlock.Count; i++)
        {
            data += listSkisUnlock[i];
            if (i < listSkisUnlock.Count - 1) data += ";";
        }
        SaveLoadData.SaveData("SaveSkisUnlock", data, true);
    }

    public static void SaveGameGuide()
    {
        SaveLoadData.SaveData("SaveGameGuide", gameGuide, true);
    }

    public static void SaveDataMissions()
    {
        SaveLoadData.SaveData("SaveDataMissions", dataMissionsUse, true);
    }

    public static void SaveDataChallenge()
    {
        SaveLoadData.SaveData("SaveDataChallenge", dataChallengeUse, true);
    }

    public static void SaveDataMissionsOld()
    {
        SaveLoadData.SaveData("SaveDataMissionsOld", dataMissionsOld, true);
    }

    public static void SaveDataChallengeOld()
    {
        SaveLoadData.SaveData("SaveDataChallengeOld", dataChallengeOld, true);
    }

    public static void SaveRateGame()
    {
        SaveLoadData.SaveData("SaveRateGame", clickRate.ToString(), true);
    }

    public static void SaveCheckLanguage()
    {
        SaveLoadData.SaveData("SaveCheckLanguage", autoCheckLang.ToString(), true);
    }

    public static void SaveCheckInviteFB()
    {
        SaveLoadData.SaveData("SaveCheckInviteFB", coinMaxInvite.ToString(), true);
    }

    public static void SaveIndexTypeTerrain()
    {
        SaveLoadData.SaveData("SaveIndexTypeTerrain", indexTypeTerrain.ToString(), true);
    }

    public static void SaveIndexRunTerrain()
    {
        SaveLoadData.SaveData("SaveIndexRunTerrain", indexRunTerrain.ToString(), true);
    }

    public static void SavePointRunTerrain()
    {
        SaveLoadData.SaveData("SavePointRunTerrain", Mathf.RoundToInt(pointRunTerrain * 1000).ToString(), true);
    }

    public static void SavePointHeroTerrain()
    {
        SaveLoadData.SaveData("SavePointHeroTerrain", Mathf.RoundToInt(pointHeroTerrain * 1000).ToString(), true);
    }

    public static void SaveCodeRunTerrain()
    {
        SaveLoadData.SaveData("SaveCodeRunTerrain", listCodeTerrain, true);
    }

    public static void UpdateValueMissions()
    {
        Transform tranIcon = panelMissions.transform.Find("IconMissions");
        Transform tranValueA = panelMissions.transform.Find("TextMissions");
        Transform tranValue = tranValueA.transform.Find("TextMissions");
        Image imgIcon = tranIcon.GetComponent<Image>();
        Text txtValue = tranValue.GetComponent<Text>();
        imgIcon.sprite = listMissions[indexItemMissions].icon;
        txtValue.text = runItemMissions + "/" + totalItemMissions;
    }

    public static void UpdateValueChallenge()
    {
        Transform tranContent = panelChallenge.transform.Find("Content");
        foreach (Transform tran in tranContent) if (tran.name != "Text") Destroy(tran.gameObject);
        Transform tranText = tranContent.Find("Text");
        for (int i = 0; i < listTextRequire.Count; i++)
        {
            GameObject textNew = tranText.gameObject;
            if (i > 0)
                textNew = Instantiate(tranText.gameObject, tranContent) as GameObject;
            Text txtValue = textNew.GetComponent<Text>();
            txtValue.text = listTextRequire[i];
            Color setColor = new Color(0, 40f / 255f, 70f / 255f, 1f);
            if (i < listTextColect.Count)
                setColor = Color.yellow;
            txtValue.color = setColor;
        }
    }

    private static int oldTypeMessage = 2;
    public static void ShowMessageBonusRoad(int typeMessage)
    {
        if (panelTextEffectBonus.activeSelf && (oldTypeMessage == 0 || oldTypeMessage == 1)) return;
        panelTextEffectBonus.SetActive(true);
        Transform tranTextTop = panelTextEffectBonus.transform.GetChild(1).GetChild(0);
        Transform tranTextBottom = panelTextEffectBonus.transform.GetChild(1).GetChild(1);
        Text txtTop = tranTextTop.GetComponent<Text>();
        Text txtDown = tranTextBottom.GetComponent<Text>();
        if (typeMessage == 0)//you win
        {
            tranTextTop.GetComponent<Gradient>().EndColor = new Color(0, 1, 0, 1);
            txtTop.text = AllLanguages.playWinTop[indexLanguage];
            tranTextBottom.GetComponent<Gradient>().EndColor = new Color(0, 1, 0, 1);
            txtDown.text = AllLanguages.playWinDown[indexLanguage];
        }
        else if (typeMessage == 1)//you lose
        {
            tranTextTop.GetComponent<Gradient>().EndColor = new Color(1, 0, 0, 1);
            txtTop.text = AllLanguages.playLoseTop[indexLanguage];
            tranTextBottom.GetComponent<Gradient>().EndColor = new Color(1, 0, 0, 1);
            txtDown.text = AllLanguages.playLoseDown[indexLanguage];
        }
        else//bonus road
        {
            tranTextTop.GetComponent<Gradient>().EndColor = new Color(1, 0.59f, 0, 1);
            txtTop.text = AllLanguages.playBonus[indexLanguage];
            tranTextBottom.GetComponent<Gradient>().EndColor = new Color(1, 0.59f, 0, 1);
            txtDown.text = AllLanguages.playRoad[indexLanguage];
        }
        txtTop.font = AllLanguages.listFontLangA[indexLanguage];
        txtDown.font = AllLanguages.listFontLangA[indexLanguage];
        panelTextEffectBonus.GetComponent<Animator>().Play("TextEffectBonusRoadRun");
        oldTypeMessage = typeMessage;
    }

    public static void ShowMessageWinLose(int typeMessage, string textTitle = "", int valueBonus = 0)
    {
        panelTextEffectWinLose.SetActive(true);
        Transform tranTextMessage = panelTextEffectWinLose.transform.GetChild(1).GetChild(0);
        Text txtMessage = tranTextMessage.GetComponent<Text>();
        if (typeMessage == 0)//you win
        {
            tranTextMessage.GetComponent<Gradient>().EndColor = new Color(0, 1, 0, 1);
            txtMessage.text = AllLanguages.playYouWin[indexLanguage];
        }
        else if (typeMessage == 1)//you lose
        {
            tranTextMessage.GetComponent<Gradient>().EndColor = new Color(1, 0, 0, 1);
            txtMessage.text = AllLanguages.playYouLose[indexLanguage];
        }
        txtMessage.font = AllLanguages.listFontLangA[indexLanguage];
        Transform tranTextTitle = panelTextEffectWinLose.transform.GetChild(1).GetChild(1);
        Transform tranTextValue = panelTextEffectWinLose.transform.GetChild(1).GetChild(2);
        if (textTitle != "" && valueBonus > 0)
        {
            tranTextTitle.gameObject.SetActive(true);
            tranTextValue.gameObject.SetActive(true);
            Text txtTitle = tranTextTitle.GetComponent<Text>();
            Text txtValue = tranTextValue.GetComponent<Text>();
            txtTitle.text = textTitle;
            txtTitle.font = AllLanguages.listFontLangA[indexLanguage];
            txtValue.text = "+" + valueBonus.ToString() + " " + AllLanguages.playCoins[indexLanguage];
            txtValue.font = AllLanguages.listFontLangA[indexLanguage];
            totalCoin += valueBonus;
            SaveCoin();
        }
        else
        {
            tranTextTitle.gameObject.SetActive(false);
            tranTextValue.gameObject.SetActive(false);
        }
        panelTextEffectWinLose.GetComponent<Animator>().Play("TextEffectWinLoseRun");
    }

    public static void BonusMissionsChallenge(int indexBonus, string title, int valueBonus, Vector3 pointShow)
    {
        panelBonus.SetActive(true);
        Transform tranIcon = panelBonus.transform.GetChild(1).GetChild(0);
        Transform tranValue = panelBonus.transform.GetChild(1).GetChild(1);
        Transform tranTitle = panelBonus.transform.GetChild(1).GetChild(2);
        if (title == "")
            tranTitle.gameObject.SetActive(false);
        else
        {
            tranTitle.GetComponent<Text>().text = title;
            tranTitle.GetComponent<Text>().font = AllLanguages.listFontLangA[indexLanguage];
        }
        Image imgIcon = tranIcon.GetComponent<Image>();
        Text txtValue = tranValue.GetComponent<Text>();
        if (indexBonus == 0)//neu la coin
        {
            if (valueBonus < 100) valueBonus = 100;
            valueBonus = Mathf.RoundToInt(valueBonus / 100f) * 100;
            totalCoin += valueBonus;
            SaveCoin();
        }
        else if (indexBonus == 1)//neu la key
        {
            totalKey += valueBonus;
            SaveKey();
        }
        else if (indexBonus == 2)//neu la skis
        {
            totalSkis += valueBonus;
            if (totalSkis > maxHoverboard)
                totalSkis = maxHoverboard;
            SaveSkis();
        }
        else if (indexBonus == 3)//neu la headStart
        {
            totalHeadStart += valueBonus;
            if (totalHeadStart > maxHeadstart) 
                totalHeadStart = maxHeadstart;
            SaveHeadStart();
            Camera.main.GetComponent<PageMainGame>().ReCreateButtonItemBuys();
        }
        else if (indexBonus == 4)//neu la scoreBooster
        {
            totalScoreBooster += valueBonus;
            if (totalScoreBooster > maxScorebooster) 
                totalHeadStart = maxScorebooster;
            SaveScoreBooster();
            Camera.main.GetComponent<PageMainGame>().ReCreateButtonItemBuys();
        }
        imgIcon.sprite = listIconBonus[indexBonus];
        txtValue.text = "x" + valueBonus.ToString();
    }

    public static bool HandleReborn()
    {
        bool result = false;
        int requireKey = (int)Mathf.Pow(2, timeSaveMe);
        if (totalKey >= requireKey)//neu du key thi thuc hien hoi sinh
        {
            result = true;
            totalKey -= requireKey;
            SaveKey();
            mainCharacter.GetComponent<HeroController>().ResetItemUse();
            RebornGame();
            mainCharacter.GetComponent<HeroController>().ReStart();
            parReborn.GetComponent<ParticleSystem>().Play();
            PlayAudioClipFree(Modules.audioParReborn);
            //thuc hien xoa tat ca cac vat can xung quanh khu vuc nay
            Collider[] hitColliders = Physics.OverlapSphere(mainCharacter.transform.position, rangeReborn * speedGame);
            for (var i = 0; i < hitColliders.Length; i++)
            {
                BarrierInformation barrier = hitColliders[i].gameObject.GetComponent<BarrierInformation>();
                if (barrier != null && barrier.parentBarrier != null && !barrier.neverDestroy)
                {
                    barrier.parentBarrier.GetComponent<BarrierController>().ResetBarrier();
                    barrier.parentBarrier.SetActive(false);
                }
                ItemInformation itemSub = hitColliders[i].gameObject.GetComponent<ItemInformation>();
                if (itemSub != null && itemSub.typeItem != TypeItems.nextGate && itemSub.typeItem != TypeItems.startTunner && itemSub.typeItem != TypeItems.endTunner)
                {
                    itemSub.ResetItem();
                    itemSub.gameObject.SetActive(false);
                }
            }
            Camera.main.GetComponent<PageMainGame>().createEnemyLeft.GetComponent<EnemyController>().ReStart();
            mainCharacter.GetComponent<HeroController>().statusHero = StatusHero.run;
        }
        return result;
    }

    private static string codeSkisReward = "002";
    public static void ShowBonusFirst()
    {
        //xu ly tang thuong lan choi dau tien
        totalCoin += 5000;
        SaveCoin();
        //mo khoa hoverboard 2
        if (!listSkisUnlock.Contains(codeSkisReward))
        {
            listSkisUnlock.Add(codeSkisReward);
            SaveListSkisUnlock();
        }
        bonusFirst = 1;
        SaveBonusWinFirst();
        bonusFirstBox.SetActive(true);
        bonusFirstBox.transform.GetComponent<Animator>().SetTrigger("TriOpen");
    }

    public static void CheckShowAds()
    {
#if (UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
        //ADSController.Instance.DestroyBanner();
        if (runTimeShowInterstitial >= timeShowInterstitial - 1)
        {
            ADSController.Instance.ShowInterstitial();
            runTimeShowInterstitial = 0;
        }
        else runTimeShowInterstitial++;
#endif
    }

    public static void HandleGameOver()
    {
        CheckShowAds();
        UpdateIndexRunTerrain();
        showScorePlay = true;
        totalCoin += coinPlayer;
        SaveCoin();
        if (scorePlayer > totalScore)
        {
            totalScore = scorePlayer;
            SaveScore();
            containHighScore.SetActive(true);
            containMainGame.SetActive(false);
            containHighScore.transform.Find("MainCamera").GetComponent<PageHighScore>().CallStart();
        }
        else
        {
            if (totalMysteryBox > 0)
            {
                nextPageOpenBox = "ShowAchievement";
                containOpenBox.SetActive(true);
                containMainGame.SetActive(false);
                containOpenBox.transform.Find("MainCamera").GetComponent<PageOpenMysteryBox>().CallStart();
            }
            else
            {
                containAchievement.SetActive(true);
                containMainGame.SetActive(false);
                containAchievement.transform.Find("MainCamera").GetComponent<PageAchievement>().CallStart();
            }
        }
    }

    public static void UpdateIndexRunTerrain()
    {
        Vector4 point = Camera.main.GetComponent<TerrainController>().GetPointRunTerrain();
        int tempIndex = Mathf.RoundToInt(point.y);
        if (tempIndex < 0) return;
        indexTypeTerrain = Mathf.RoundToInt(point.x);
        indexRunTerrain = tempIndex;
        pointRunTerrain = point.z;
        pointHeroTerrain = point.w;
        listCodeTerrain = Camera.main.GetComponent<TerrainController>().listCodeTerrain;
        SaveIndexTypeTerrain();
        SaveIndexRunTerrain();
        SavePointRunTerrain();
        SavePointHeroTerrain();
        SaveCodeRunTerrain();
    }

    public static DateTime LoadOldDateTime(string firstSave)
    {
        DateTime dateTimeNow = DateTime.Now;
        int oldYear = PlayerPrefs.GetInt(firstSave + "Year", dateTimeNow.Year);
        int oldMonth = PlayerPrefs.GetInt(firstSave + "Month", dateTimeNow.Month);
        int oldDay = PlayerPrefs.GetInt(firstSave + "Day", dateTimeNow.Day);
        int oldHour = PlayerPrefs.GetInt(firstSave + "Hour", dateTimeNow.Hour);
        int oldMinute = PlayerPrefs.GetInt(firstSave + "Minute", dateTimeNow.Minute);
        int oldSecond = PlayerPrefs.GetInt(firstSave + "Second", dateTimeNow.Second);
        return new DateTime(oldYear, oldMonth, oldDay, oldHour, oldMinute, oldSecond);
    }

    public static void SaveNewDateTime(string firstSave)
    {
        DateTime dateTimeNow = DateTime.Now;
        PlayerPrefs.SetInt(firstSave + "Year", dateTimeNow.Year);
        PlayerPrefs.SetInt(firstSave + "Month", dateTimeNow.Month);
        PlayerPrefs.SetInt(firstSave + "Day", dateTimeNow.Day);
        PlayerPrefs.SetInt(firstSave + "Hour", dateTimeNow.Hour);
        PlayerPrefs.SetInt(firstSave + "Minute", dateTimeNow.Minute);
        PlayerPrefs.SetInt(firstSave + "Second", dateTimeNow.Second);
        PlayerPrefs.Save();
    }

    private static float oldTimePlayCoin = 0;//xu ly dan cach am thanh, khong cho phep chay am lien tuc day rit
    public static void PlayAudioClipFree(AudioClip audio, bool typeCoin = false)
    {
        if (audio != null && volumeAction > 0)
        {
            if (typeCoin)
            {
                if (Time.time - oldTimePlayCoin < 0.1f) return;
                if (oldTime == 0)
                {
                    oldTime = Time.time;
                    pitchCoin = minPitchCoin;
                }
                else
                {
                    if (Time.time - oldTime < 0.5f)
                    {
                        oldTime = Time.time;
                        pitchCoin += numChangePith;
                        if (pitchCoin > maxPitchCoin) pitchCoin = maxPitchCoin;
                    }
                    else
                    {
                        oldTime = Time.time;
                        pitchCoin = minPitchCoin;
                    }
                }
            }
            //AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position, volumeAction);
            int indexPlay = 0;
            for (int i = 0; i < audioSource.Count; i++)
            {
                if (!audioSource[i].isPlaying) { indexPlay = i; break; }
            }
            if (indexPlay < audioSource.Count)
            {
                audioSource[indexPlay].clip = audio;
                if (!typeCoin)
                    audioSource[indexPlay].pitch = 1f;
                else
                {
                    audioSource[indexPlay].pitch = pitchCoin;
                    oldTimePlayCoin = Time.time;
                }
                audioSource[indexPlay].Play();
            }
        }
    }

    public static void PlayAudioClipLoop(AudioClip audio, Transform tran)
    {
        if (tran.GetComponent<AudioSource>() == null)
            tran.gameObject.AddComponent<AudioSource>();
        tran.GetComponent<AudioSource>().clip = audio;
        tran.GetComponent<AudioSource>().volume = volumeBackground;
        tran.GetComponent<AudioSource>().loop = true;
        tran.GetComponent<AudioSource>().Play();
    }

    public static void SetLayer(GameObject parent, string layerName, bool includeChildren = true)
    {
        parent.layer = LayerMask.NameToLayer(layerName);
        if (includeChildren)
            foreach (Transform trans in parent.transform)
                SetLayer(trans.gameObject, layerName, includeChildren);
    }

#if UNITY_IOS
    public static string linkStoreGame = "?index=1";
#else
    public static string linkStoreGame = "?index=0";
#endif
    public static string linkIconGame = "http://52.220.93.168/Others/Icons/IconFruntastic.png";
    public static string linkChange = "http://52.220.93.168/Others/ChangeLink.php";
    public static string linkShortFB = "https://fb.me/303070490147052";

    public static void ClickShareFB()
    {
        string Info = AllLanguages.menuInforStart[indexLanguage] + " " + totalScore + " " + AllLanguages.menuInforEnd[indexLanguage];
        string Title = "Fruntastic Squad Run";
        string Description = AllLanguages.menuInforDetail[indexLanguage] + ". " + Info;
        Facebook.Unity.FB.ShareLink(new Uri(linkChange + linkStoreGame), Title, Description, new Uri(linkIconGame), RewardShareFB);
    }

    private static string codeHeroReward = "002";
    private static void RewardShareFB(Facebook.Unity.IShareResult result)
    {
        //mo khoa Stela
        if (!listHeroUnlock.Contains(codeHeroReward))
        {
            listHeroUnlock.Add(codeHeroReward);
            SaveListHeroUnlock();
        }
    }

    public static bool CheckReceivedFB()
    {
        return listHeroUnlock.Contains(codeHeroReward);
    }

    public static void ClickShareFBOld()
    {
        string AppID = "284354778685290";
        string Info = AllLanguages.menuInforStart[indexLanguage] + " " + totalScore + " " + AllLanguages.menuInforEnd[indexLanguage];
        string Title = "Fruntastic Squad Run";
        string Description = AllLanguages.menuInforDetail[indexLanguage] + ". " + Info;
        Application.OpenURL("https://www.facebook.com/dialog/feed?" +
            "app_id=" + AppID +
            "&link=" + linkChange + linkStoreGame +
            "&picture=" + linkIconGame +
            "&name=" + Info.Replace(" ", "%20") +
            "&caption=" + Title.Replace(" ", "%20") +
            "&description=" + Description.Replace(" ", "%20") +
            "&redirect_uri=https://facebook.com/");
    }

    public static void SetStatusButShareVideo(GameObject objButton)
    {
        //if (!Recorder.Instance.isAvailableVideo)
        //    objButton.GetComponent<ButtonStatus>().Disable();
        //else
        //    objButton.GetComponent<ButtonStatus>().Enable();
    }

    public static void RewardKeysSkis()
    {
        if (itemBonusViewAds == "Skis")
        {
            int totalBonus = UnityEngine.Random.Range(Mathf.RoundToInt(numberSkisBonus.x), Mathf.RoundToInt(numberSkisBonus.y));
            totalSkis += totalBonus;
            if (totalSkis > maxHoverboard)
                totalSkis = maxHoverboard;
            SaveSkis();
        }
        else if (itemBonusViewAds == "Key")
        {
            int totalBonus = UnityEngine.Random.Range(Mathf.RoundToInt(numberKeyBonus.x), Mathf.RoundToInt(numberKeyBonus.y));
            totalKey += totalBonus;
            SaveKey();
        }
    }
}
[System.Serializable]//de show ra phan input cua unity editor
public class SetupItemBody
{
    public string codeBody;
    public string nameBoneAdd;
    public Vector3 localPoint;
    public Vector3 localAngle;
    public Vector3 localScale;
    public SetupItemBody(string codeBodyInput, string nameBoneAddInput, Vector3 localPointInput, Vector3 localAngleInput, Vector3 localScaleInput)
    {
        this.codeBody = codeBodyInput;
        this.nameBoneAdd = nameBoneAddInput;
        this.localPoint = localPointInput;
        this.localAngle = localAngleInput;
        this.localScale = localScaleInput;
    }
}

[System.Serializable]//de show ra phan input cua unity editor
public class ListCodeTerrain
{
    public List<string> listTerrain = new List<string>();
    public ListCodeTerrain(List<string> listTerrainInput)
    {
        this.listTerrain = listTerrainInput;
    }
}

[System.Serializable]//de show ra phan input cua unity editor
public class ListUseTerrain
{
    public List<GameObject> listTerrain = new List<GameObject>();
    public ListUseTerrain(List<GameObject> listTerrainInput)
    {
        this.listTerrain = listTerrainInput;
    }
}

[System.Serializable]//de show ra phan input cua unity editor
public class ListMissionsClass
{
    public Sprite icon;
    public GameObject model;
    public ListMissionsClass(Sprite iconInput, GameObject modelInput)
    {
        this.icon = iconInput;
        this.model = modelInput;
    }
}

[System.Serializable]//de show ra phan input cua unity editor
public class ListChallengeClass
{
    public string value;
    public GameObject model;
    public ListChallengeClass(string valueInput, GameObject modelInput)
    {
        this.value = valueInput;
        this.model = modelInput;
    }
}
public enum StatusGame
{
    menu,
    start,
    play,
    pause,
    flyScene,
    over,
    bonusEffect,
    stop
}

//CAC DIEU CAN CHU Y VE CAC LAYER
/*
MCG-Terrain => Danh cho cac Box khong lam nhan vat chet, xu ly va cham nhu dia hinh
MCG-Barrier => Danh cho cac Box co cac mat chet (front)
*/