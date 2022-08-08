using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageSetting : MonoBehaviour
{
    public Text valueLanguage, valueVolumeBG, valueVolumeAT, valueReducedEffect, valueSkyEffect, valueCountryEnemy, valueCurvedWorld;
    public Image iconFlag;
    public Slider sliderSensitivity; //sliderSpeedJumpUp;
    public GameObject listLanguages;
    public GameObject curvedWorld;
    //xu ly ngon ngu
    public Text textTitle, textLanguage, textBGMusic, textAGMusic, textLevelEffect, textSkyEffect, textSensitivity, textCompetitor, textCurvedWorld, textButtonPrivacyPolicy, textButtonQuitGame;//textSpeedJumpUp

    public void StartShowMessage()
    {
        int lang = Modules.indexLanguage;
        //xu ly ngon ngu
        iconFlag.sprite = AllLanguages.listIconLang[lang];
        textTitle.font = AllLanguages.listFontLangA[lang];
        textTitle.text = AllLanguages.menuSetting[lang];
        textLanguage.font = AllLanguages.listFontLangA[lang];
        textLanguage.text = AllLanguages.menuLanguage[lang];
        textBGMusic.font = AllLanguages.listFontLangA[lang];
        textBGMusic.text = AllLanguages.menuBGMusic[lang];
        textAGMusic.font = AllLanguages.listFontLangA[lang];
        textAGMusic.text = AllLanguages.menuActionSound[lang];
        textLevelEffect.font = AllLanguages.listFontLangA[lang];
        textLevelEffect.text = AllLanguages.menuLevelEffect[lang];
        textSkyEffect.font = AllLanguages.listFontLangA[lang];
        textSkyEffect.text = AllLanguages.menuSkyEffect[lang];
        textSensitivity.font = AllLanguages.listFontLangA[lang];
        textSensitivity.text = AllLanguages.menuSensitivity[lang];
        //textSpeedJumpUp.font = AllLanguages.listFontLangA[lang];
        //textSpeedJumpUp.text = AllLanguages.menuSpeedJump[lang];
        textCompetitor.font = AllLanguages.listFontLangA[lang];
        textCompetitor.text = AllLanguages.menuCompititor[lang];
        textCurvedWorld.font = AllLanguages.listFontLangA[lang];
        textCurvedWorld.text = AllLanguages.menuCurvedWorld[lang];
        textButtonPrivacyPolicy.font = AllLanguages.listFontLangA[lang];
        textButtonPrivacyPolicy.text = AllLanguages.menuPrivacyPolicy[lang];
        textButtonQuitGame.font = AllLanguages.listFontLangA[lang];
        textButtonQuitGame.text = AllLanguages.menuQuitGame[lang];
        //xu ly khac
        valueLanguage.font = AllLanguages.listFontLangA[lang];
        valueLanguage.text = AllLanguages.listLangShort[lang];
        valueVolumeBG.font = AllLanguages.listFontLangA[lang];
        if (Modules.volumeBackground == 1) valueVolumeBG.text = AllLanguages.menuOn[lang];
        else valueVolumeBG.text = AllLanguages.menuOff[lang];
        valueVolumeAT.font = AllLanguages.listFontLangA[lang];
        if (Modules.volumeAction == 1) valueVolumeAT.text = AllLanguages.menuOn[lang];
        else valueVolumeAT.text = AllLanguages.menuOff[lang];
        valueReducedEffect.font = AllLanguages.listFontLangA[lang];
        if (Modules.reducedEffect == 2) valueReducedEffect.text = AllLanguages.menuLow[lang];
        else if (Modules.reducedEffect == 1) valueReducedEffect.text = AllLanguages.menuMedium[lang];
        else valueReducedEffect.text = AllLanguages.menuHigh[lang];
        valueSkyEffect.font = AllLanguages.listFontLangA[lang];
        if (Modules.skyEffect == 1) valueSkyEffect.text = AllLanguages.menuOn[lang];
        else valueSkyEffect.text = AllLanguages.menuOff[lang];
        valueCountryEnemy.font = AllLanguages.listFontLangA[lang];
        if (Modules.countryEnemy == 1) valueCountryEnemy.text = AllLanguages.menuCountry[lang];
        else valueCountryEnemy.text = AllLanguages.menuFriend[lang];
        valueCurvedWorld.font = AllLanguages.listFontLangA[lang];
        if (Modules.curvedWorld == 1) valueCurvedWorld.text = AllLanguages.menuOn[lang];
        else valueCurvedWorld.text = AllLanguages.menuOff[lang];
        sliderSensitivity.value = (float)(Modules.valueSensitivity) / 100f;
        //sliderSpeedJumpUp.value = 1 - (float)(Modules.valueSpeedJumpUp) / 40f;
    }

    public void ButtonLanguageClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        listLanguages.SetActive(true);
        listLanguages.GetComponent<Animator>().SetTrigger("TriOpen");
        listLanguages.GetComponent<MessageListLanguage>().StartShowMessage();
    }

    public void ButtonVolumeBGClick()
    {
        int lang = Modules.indexLanguage;
        if (Modules.volumeBackground == 1)
        {
            Modules.volumeBackground = 0;
            valueVolumeBG.text = AllLanguages.menuOff[lang];
        }
        else
        {
            Modules.volumeBackground = 1;
            valueVolumeBG.text = AllLanguages.menuOn[lang];
        }
        Modules.PlayAudioClipLoop(Modules.audioBackgrond, Camera.main.transform);
        Modules.SaveSettingValue();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonVolumeATClick()
    {
        int lang = Modules.indexLanguage;
        if (Modules.volumeAction == 1)
        {
            Modules.volumeAction = 0;
            valueVolumeAT.text = AllLanguages.menuOff[lang];
        }
        else
        {
            Modules.volumeAction = 1;
            valueVolumeAT.text = AllLanguages.menuOn[lang];
        }
        Modules.SaveSettingValue();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonReducedEffects()
    {
        int lang = Modules.indexLanguage;
        if (Modules.reducedEffect == 2)
        {
            Modules.reducedEffect = 1;
            valueReducedEffect.text = AllLanguages.menuMedium[lang];
        }
        else if (Modules.reducedEffect == 1)
        {
            Modules.reducedEffect = 0;
            valueReducedEffect.text = AllLanguages.menuHigh[lang];
        }
        else
        {
            Modules.reducedEffect = 2;
            valueReducedEffect.text = AllLanguages.menuLow[lang];
        }
        Modules.SaveSettingValue();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    //public void ButtonSkyEffect()
    //{
    //    int lang = Modules.indexLanguage;
    //    if (Modules.skyEffect == 0)
    //    {
    //        Modules.skyEffect = 1;
    //        valueSkyEffect.text = AllLanguages.menuOn[lang];
    //        skyController.GetComponent<SkyController>().CallStart();
    //    }
    //    else
    //    {
    //        Modules.skyEffect = 0;
    //        valueSkyEffect.text = AllLanguages.menuOff[lang];
    //        skyController.GetComponent<SkyController>().CallStart();
    //    }
    //    Modules.SaveSettingValue();
    //    Modules.PlayAudioClipFree(Modules.audioButton);
    //}

    public void SliderSensitivity()
    {
        Modules.valueSensitivity = Mathf.RoundToInt(sliderSensitivity.value * 100);
        Modules.UpdateValueSensitivity();
        Modules.SaveSettingValue();
    }

    //public void SliderSpeedJumpUp()
    //{
    //    Modules.valueSpeedJumpUp = Mathf.RoundToInt(40 - sliderSpeedJumpUp.value * 40);
    //    Modules.SaveSettingValue();
    //}

    public void ButtonCountryEnemy()
    {
        int lang = Modules.indexLanguage;
        if (Modules.countryEnemy == 0)
        {
            Modules.countryEnemy = 1;
            valueCountryEnemy.text = AllLanguages.menuCountry[lang];
        }
        else
        {
            Modules.countryEnemy = 0;
            valueCountryEnemy.text = AllLanguages.menuFriend[lang];
        }
        Modules.SaveSettingValue();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonCurvedWorld()
    {
        int lang = Modules.indexLanguage;
        if (Modules.curvedWorld == 1)
        {
            Modules.curvedWorld = 0;
            valueCurvedWorld.text = AllLanguages.menuOff[lang];
            curvedWorld.SetActive(false);
        }
        else
        {
            Modules.curvedWorld = 1;
            valueCurvedWorld.text = AllLanguages.menuOn[lang];
            curvedWorld.SetActive(true);
        }
        Modules.SaveSettingValue();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonCloseMessage()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        transform.gameObject.GetComponent<Animator>().SetTrigger("TriClose");
    }

    public void ButtonPrivacyPolicy()
    {
        Application.OpenURL("http://shooterboy.com/Bus-Subway-Multiplayer-Runner-Privacy-PolicyEULA/");
    }

    public void ButtonQuitGame()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (transform.gameObject.activeSelf)
                ButtonCloseMessage();
        }
    }
}
