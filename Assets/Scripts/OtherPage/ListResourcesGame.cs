using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ListResourcesGame : MonoBehaviour {

    public List<Sprite> listIconLang = new List<Sprite>();
    public List<Font> listFontLangA = new List<Font>();
    public List<Font> listFontLangB = new List<Font>();
    public List<Sprite> listIconItem = new List<Sprite>();//0 skis, 1 giay, 2 magnet, 3 rocket, 4 2X, 5 cable
    public List<Sprite> listIconBonus = new List<Sprite>();//0 coin, 1 keys, 2 skis, 3 headStart, 4 scoreBooster
    public List<ListMissionsClass> listMissions = new List<ListMissionsClass>();//chua cac icon, model de suu tap item
    public List<ListChallengeClass> listChallenge = new List<ListChallengeClass>();//chua cac gia tri, model de suu tap chu
    public Sprite iconHeadStart, iconScoreBooster, iconAvatarNull;
    public Text textWaitAMoment;
    //them cac audios
    public List<AudioSource> audioSource;
    public AudioClip audioBackgrond, audioRoadBonus;
    public AudioClip audioButton, audioTapPlay, audioOpenBox, audioSwipeMove, audioSwipeUp, audioSwipeDown, audioUpSkis;
    public AudioClip audioCollider, audioColliderDie, audioSurprise, audioShowMessage, audioRocket, audioCable, audioTrapoline, audioBrokenGlass;
    public AudioClip audioParReborn, audioParColSkis, audioBonusText, audioEatBonusBox;
    public AudioClip audioPoStart, audioPoNear, audioPoFar;

    void Start()
    {
        AllLanguages.listIconLang = listIconLang;
        AllLanguages.listFontLangA = listFontLangA;
        AllLanguages.listFontLangB = listFontLangB;
        Modules.listIconItem = listIconItem;
        Modules.listIconBonus = listIconBonus;
        Modules.listMissions = listMissions;
        Modules.listChallenge = listChallenge;
        Modules.iconHeadStart = iconHeadStart;
        Modules.iconScoreBooster = iconScoreBooster;
        Modules.iconAvatarNull = iconAvatarNull;
        Modules.audioSource = audioSource;
        Modules.audioBackgrond = audioBackgrond;
        Modules.audioRoadBonus = audioRoadBonus;
        Modules.audioButton = audioButton;
        Modules.audioTapPlay = audioTapPlay;
        Modules.audioOpenBox = audioOpenBox;
        Modules.audioSwipeMove = audioSwipeMove;
        Modules.audioSwipeUp = audioSwipeUp;
        Modules.audioSwipeDown = audioSwipeDown;
        Modules.audioUpSkis = audioUpSkis;
        Modules.audioCollider = audioCollider;
        Modules.audioSurprise = audioSurprise;
        Modules.audioColliderDie = audioColliderDie;
        Modules.audioShowMessage = audioShowMessage;
        Modules.audioRocket = audioRocket;
        Modules.audioCable = audioCable;
        Modules.audioTrapoline = audioTrapoline;
        Modules.audioBrokenGlass = audioBrokenGlass;
        Modules.audioParReborn = audioParReborn;
        Modules.audioParColSkis = audioParColSkis;
        Modules.audioBonusText = audioBonusText;
        Modules.audioEatBonusBox = audioEatBonusBox;
        Modules.audioPoStart = audioPoStart;
        Modules.audioPoNear = audioPoNear;
        Modules.audioPoFar = audioPoFar;
        textWaitAMoment.font = AllLanguages.listFontLangB[Modules.indexLanguage];
        textWaitAMoment.text = AllLanguages.menuWaitMoment[Modules.indexLanguage];
        //AudioSource[] audioSource = transform.GetComponents<AudioSource>();
        //foreach (AudioSource audio in audioSource)
        //    Modules.audioSource.Add(audio);
        //InvokeRepeating("FreeUpRam", 20f, 20f);
#if !UNITY_EDITOR
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
#endif
    }

    //void FreeUpRam()
    //{
    //    System.GC.Collect();
    //}

    //void Update()
    //{
    //    if (Time.frameCount % 300 == 0)
    //        System.GC.Collect();
    //}
}
