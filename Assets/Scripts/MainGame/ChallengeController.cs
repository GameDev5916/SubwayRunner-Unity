using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengeController : MonoBehaviour {

    public GameObject messageBox, parentContent;
    public Text textValueText;
    public Image imageButApplyCancel;
    public Color colorApply, colorCancel;
    public float requestTime = 0.1f;
    public float maxTime = 10f;//cho phep thuc hien toi da 10 giay
    //xu ly ngon ngu
    public Text textTitle, textInfo, textNote, textNoContent, textApplyCancel;

    public void StartShowMessage(bool updateLanguages)
    {
        if (updateLanguages) UpdateLanguage();
        //xu ly khac
        HandleFailLoad();
        if (Modules.dataChallengeUse != "")//neu da co thu thach tu truoc thi load lai
        {
            Modules.autoGetChallenge = false;
            textNoContent.gameObject.SetActive(false);
            parentContent.SetActive(true);
            SetValueContent(Modules.dataChallengeUse);
            textApplyCancel.text = AllLanguages.menuCancel[Modules.indexLanguage];
            imageButApplyCancel.color = colorCancel;
            Modules.ResetChallenge();
        }
        else//neu khong co thi kiem tra xem co duoc phep load nhiem vu moi hay khong (ngay moi)
        {
            StartCoroutine(GetValueChallenge());
        }
    }

    public void UpdateLanguage()
    {
        //xu ly ngon ngu
        int iLang = Modules.indexLanguage;
        textTitle.font = AllLanguages.listFontLangA[iLang];
        textTitle.text = AllLanguages.menuChallenge[iLang];
        textInfo.font = AllLanguages.listFontLangB[iLang];
        textInfo.text = AllLanguages.menuTitleChallenge[iLang];
        textNote.font = AllLanguages.listFontLangB[iLang];
        textNote.text = AllLanguages.menuNoteChallenge[iLang];
        textNoContent.font = AllLanguages.listFontLangA[iLang];
        textNoContent.text = AllLanguages.menuNoChallenge[iLang];
        textApplyCancel.font = AllLanguages.listFontLangA[iLang];
        if (Modules.dataChallengeUse != "") textApplyCancel.text = AllLanguages.menuCancel[iLang];
        else textApplyCancel.text = AllLanguages.menuApply[iLang];
    }

    public void ButtonCloseClick()
    {
        Modules.SaveDataChallenge();
        Modules.PlayAudioClipFree(Modules.audioButton);
        messageBox.GetComponent<Animator>().SetTrigger("TriClose");
    }

    public void ButtonApplyCancelClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        if (AllLanguages.menuCancel.Contains(textApplyCancel.text))//neu dang apply
        {
            textApplyCancel.text = AllLanguages.menuApply[Modules.indexLanguage];
            imageButApplyCancel.color = colorApply;
            Modules.dataChallengeUse = "";
            Modules.SaveDataChallenge();
            //kiem tra load xem co thu thach moi hay khong
            StartCoroutine(GetValueChallenge());
        }
        else//neu dang cancel
        {
            if (textNoContent.gameObject.activeSelf)
                ButtonCloseClick();
            else 
                GetNewChallenge();
        }
    }

    void GetNewChallenge()
    {
        textApplyCancel.text = AllLanguages.menuCancel[Modules.indexLanguage];
        imageButApplyCancel.color = colorCancel;
        Modules.dataChallengeUse = Modules.dataChallengeNew;
        Modules.SaveDataChallenge();
        Modules.ResetChallenge();
        Modules.autoGetChallenge = false;
    }

    IEnumerator GetValueChallenge()
    {
        WWWForm form = new WWWForm();
        form.AddField("table", "ChallengeiRun");
        WWW _resuilt = new WWW(Modules.linkGetChallenge, form);
        float runTime = 0f;
        while (!_resuilt.isDone && runTime < maxTime)
        {
            runTime += requestTime;
            yield return new WaitForSeconds(requestTime);
        }
        yield return _resuilt;
        if (_resuilt.text != "null" && _resuilt.text != "")
        { //hoan thanh
            string[] data = _resuilt.text.Split(';');
            if (data.Length == 5)
            {
                if (Modules.newChallenge || Modules.dataChallengeOld != _resuilt.text)
                {
                    Modules.dataChallengeNew = _resuilt.text;
                    textNoContent.gameObject.SetActive(false);
                    parentContent.SetActive(true);
                    SetValueContent(_resuilt.text);
                    textApplyCancel.text = AllLanguages.menuApply[Modules.indexLanguage];
                    imageButApplyCancel.color = colorApply;
                    if (Modules.autoGetChallenge) GetNewChallenge();
                    //print("getChallengeDone");
                }
                else
                {
                    HandleFailLoad();
                }
            }
            else
            {
                HandleFailLoad();
            }
        }
        else
        { //qua lau, khong mang, cau lenh loi
            HandleFailLoad();
        }
        yield break;
    }

    void HandleFailLoad()
    {
        textApplyCancel.text = AllLanguages.menuApply[Modules.indexLanguage];
        imageButApplyCancel.color = colorApply;
        textNoContent.gameObject.SetActive(true);
        parentContent.SetActive(false);
        //print("getChallengeFail");
    }

    void SetValueContent(string dataString)
    {
        string[] data = dataString.Split(';');
        textValueText.text = data[2];
    }
}
