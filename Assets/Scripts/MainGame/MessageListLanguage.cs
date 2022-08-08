using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageListLanguage : MonoBehaviour {

    public GameObject settingBox;
    public GameObject pauseBox;
    public GameObject contentList;
    public Text textTitle;

    public void StartShowMessage()
    {
        int iLang = Modules.indexLanguage;
        textTitle.font = AllLanguages.listFontLangA[iLang];
        textTitle.text = AllLanguages.menuLanguage[iLang];
        foreach (Transform tran in contentList.transform)
            if (tran.gameObject.name != "Item") Destroy(tran.gameObject);
        Transform panelItem = contentList.transform.Find("Item");
        for (int i = 0; i < AllLanguages.listLanguage.Count; i++)
        {
            if (!AllLanguages.listSupport[i]) continue;
            GameObject newItem = panelItem.gameObject;
            if (i > 0)
            {
                newItem = Instantiate(panelItem.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
                newItem.transform.SetParent(contentList.transform, false);
            }
            Transform tranAvatar = newItem.transform.Find("Avatar");
            Transform tranName = newItem.transform.Find("Name");
            Image langAvatar = tranAvatar.GetComponent<Image>();
            Text langName = tranName.GetComponent<Text>();
            if (AllLanguages.listIconLang.Count > i) langAvatar.sprite = AllLanguages.listIconLang[i];
            langName.font = AllLanguages.listFontLangA[i];
            langName.text = AllLanguages.listLanguage[i];
            newItem.GetComponent<ButtonLanguage>().indexLang = i;
            newItem.GetComponent<ButtonLanguage>().listLanguageBox = transform.gameObject;
        }
    }

    public void ChangeLanguage(int indexLang)
    {
        Modules.indexLanguage = indexLang;
        Modules.SaveSettingValue();  
        settingBox.GetComponent<MessageSetting>().StartShowMessage();
        pauseBox.GetComponent<MessagePauseGame>().UpdateLanguages();
        Camera.main.GetComponent<PageMainGame>().ChangeAllLanguage();
        CloseListLanguage();
    }

    public void CloseListLanguage()
    {
        transform.GetComponent<Animator>().SetTrigger("TriClose");
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (transform.gameObject.activeSelf)
                CloseListLanguage();
        }
    }
}
