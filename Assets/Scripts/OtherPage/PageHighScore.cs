using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PageHighScore : MonoBehaviour {

    public Text textHighScore;
    public Vector3 pointBeginHero, pointShowHero;
    public Vector3 angleBeginHero;
    private float speedMove = 3f;
    private GameObject myHero;

    public void CallStart()
    {
        ChangeAllLanguage();
        textHighScore.GetComponent<EffectUpScore>().StartEffect(Modules.totalScore);
        if (myHero != null)
        {
            myHero.GetComponent<ShadowFixed>().RemoveShadow();
            Destroy(myHero); 
        }
        foreach (GameObject go in Modules.listHeroUse)
        {
            HeroController heroCon = go.GetComponent<HeroController>();
            if (heroCon.idHero == Modules.codeHeroUse)
            {
                myHero = Instantiate(go, pointBeginHero, Quaternion.Euler(angleBeginHero)) as GameObject;
                myHero.transform.parent = Modules.containHighScore.transform;
                HeroController heroNow = myHero.GetComponent<HeroController>();
                heroNow.SetupShowMenu(1);
                heroNow.CallAniMenu(heroNow.aniRunNormal, 1f);
                break;
            }
        }
    }

    void Update()
    {
        float step = speedMove * Time.deltaTime;
        myHero.transform.position = Vector3.MoveTowards(myHero.transform.position, pointShowHero, step);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TapToContinue();
        }
    }

    private void MovePageOver()
    {
        if (Modules.totalMysteryBox > 0)
        {
            Modules.nextPageOpenBox = "ShowAchievement";
            Modules.containOpenBox.SetActive(true);
            Modules.containHighScore.SetActive(false);
            Modules.containOpenBox.transform.Find("MainCamera").GetComponent<PageOpenMysteryBox>().CallStart();
        }
        else
        {
            Modules.containAchievement.SetActive(true);
            Modules.containHighScore.SetActive(false);
            Modules.containAchievement.transform.Find("MainCamera").GetComponent<PageAchievement>().CallStart();
        }
    }

    public void TapToContinue()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        MovePageOver();
    }

    //xu ly ngon ngu
    public Text textTitleHighScore, textTapToContinue;
    public void ChangeAllLanguage()
    {
        int iLang = Modules.indexLanguage;
        textTitleHighScore.font = AllLanguages.listFontLangA[iLang];
        textTitleHighScore.text = AllLanguages.otherNewHighScore[iLang];
        textTapToContinue.font = AllLanguages.listFontLangA[iLang];
        textTapToContinue.text = AllLanguages.otherTapContinue[iLang];
    }
}
