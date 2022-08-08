using UnityEngine;
using System.Collections;

public class ButtonLanguage : MonoBehaviour {

    public int indexLang = 0;
    public GameObject listLanguageBox;

    public void ClickButton()
    {
        listLanguageBox.SendMessage("ChangeLanguage", indexLang);
    }
}
