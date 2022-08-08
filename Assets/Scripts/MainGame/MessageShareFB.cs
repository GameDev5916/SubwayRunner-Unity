using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageShareFB : MonoBehaviour {

    //xu ly ngon ngu
    public Text textTitle, textNote, textButton;

    public void StartShowMessage()
    {
        //xu ly ngon ngu
        int iLang = Modules.indexLanguage;
        textTitle.font = AllLanguages.listFontLangA[iLang];
        textTitle.text = AllLanguages.menuShare[iLang];
        textNote.font = AllLanguages.listFontLangB[iLang];
        textNote.text = AllLanguages.menuNoteShare[iLang];
        textButton.font = AllLanguages.listFontLangA[iLang];
        textButton.text = AllLanguages.menuButtonShare[iLang];
    }

    public void ButtonCloseClick()
    {
        transform.gameObject.GetComponent<Animator>().SetTrigger("TriClose");
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonShareClick()
    {
        Modules.ClickShareFB();
        ButtonCloseClick();
    }
}
