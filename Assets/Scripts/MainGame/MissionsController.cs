using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionsController : MonoBehaviour {

    public GameObject messageBox, parentContent;
    public Image iconRequire, iconResult;
    public Text numRequire, numResult;
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
        if (Modules.dataMissionsUse != "")//neu da co nhiem vu tu truoc thi load lai
        {
            Modules.autoGetMissions = false;
            textNoContent.gameObject.SetActive(false);
            parentContent.SetActive(true);
            SetValueContent(Modules.dataMissionsUse);
            textApplyCancel.text = AllLanguages.menuCancel[Modules.indexLanguage];
            imageButApplyCancel.color = colorCancel;
            Modules.ResetMissions();
        }
        else//neu khong co thi kiem tra xem co duoc phep load nhiem vu moi hay khong (ngay moi)
        {
            StartCoroutine(GetValueMissions());
        }
    }

    public void UpdateLanguage()
    {
        //xu ly ngon ngu
        int iLang = Modules.indexLanguage;
        textTitle.font = AllLanguages.listFontLangA[iLang];
        textTitle.text = AllLanguages.menuMissions[iLang];
        textInfo.font = AllLanguages.listFontLangB[iLang];
        textInfo.text = AllLanguages.menuTitleMissions[iLang];
        textNote.font = AllLanguages.listFontLangB[iLang];
        textNote.text = AllLanguages.menuNoteMissions[iLang];
        textNoContent.font = AllLanguages.listFontLangA[iLang];
        textNoContent.text = AllLanguages.menuNoMissions[iLang];
        textApplyCancel.font = AllLanguages.listFontLangA[iLang];
        if (Modules.dataMissionsUse != "") textApplyCancel.text = AllLanguages.menuCancel[iLang];
        else textApplyCancel.text = AllLanguages.menuApply[iLang];
    }

    public void ButtonCloseClick()
    {
        Modules.SaveDataMissions();
        Modules.PlayAudioClipFree(Modules.audioButton);
        messageBox.GetComponent<Animator>().SetTrigger("TriClose");
    }

    public void ButtonApplyCancelClick()
    {
        Modules.PlayAudioClipFree(Modules.audioButton);
        if(AllLanguages.menuCancel.Contains(textApplyCancel.text))//neu dang apply
        {
            textApplyCancel.text = AllLanguages.menuApply[Modules.indexLanguage];
            imageButApplyCancel.color = colorApply;
            Modules.dataMissionsUse = "";
            Modules.SaveDataMissions();
            //kiem tra load xem co nhiem vu moi hay khong
            StartCoroutine(GetValueMissions());
        }
        else//neu dang cancel
        {
            if (textNoContent.gameObject.activeSelf)
                ButtonCloseClick();
            else
                GetNewMissions();
        }
    }

    void GetNewMissions()
    {
        textApplyCancel.text = AllLanguages.menuCancel[Modules.indexLanguage];
        imageButApplyCancel.color = colorCancel;
        Modules.dataMissionsUse = Modules.dataMissionsNew;
        Modules.SaveDataMissions();
        Modules.ResetMissions();
        Modules.autoGetMissions = false;
    }

    IEnumerator GetValueMissions()
    {
        WWWForm form = new WWWForm();
        form.AddField("table", "MissionsiRun");
        WWW _resuilt = new WWW(Modules.linkGetMissions, form);
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
            if (data.Length == 6)
            {
                if (Modules.newMissions || Modules.dataMissionsOld != _resuilt.text)
                {
                    Modules.dataMissionsNew = _resuilt.text;
                    textNoContent.gameObject.SetActive(false);
                    parentContent.SetActive(true);
                    SetValueContent(_resuilt.text);
                    textApplyCancel.text = AllLanguages.menuApply[Modules.indexLanguage];
                    imageButApplyCancel.color = colorApply;
                    if (Modules.autoGetMissions) GetNewMissions();
                    //print("getMissionDone");
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
        //print("getMissionFail");
    }

    void SetValueContent(string dataString)
    {
        string[] data = dataString.Split(';');
        iconRequire.sprite = Modules.listMissions[Modules.IntParseFast(data[2])].icon;
        numRequire.text = "x" + data[3];
        iconResult.sprite = Modules.listIconBonus[Modules.IntParseFast(data[4])];
        numResult.text = "x" + data[5];
    }
}
