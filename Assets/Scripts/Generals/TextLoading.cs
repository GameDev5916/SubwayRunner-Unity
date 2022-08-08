using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextLoading : MonoBehaviour {

    public Text textLoading;
    public string originText = "Loading";
    private int numDots = 0;

    public void CallStart()
    {
        originText = AllLanguages.topLoading[Modules.indexLanguage];
        CancelInvoke("UpdateText");
        numDots = 0;
        Invoke("UpdateText", 0.25f);
    }

    void UpdateText()
    {
        numDots++;
        if (numDots > 3) numDots = 0;
        string dot = "";
        for (int i = 0; i < numDots; i++) dot += ".";
        textLoading.text = originText + dot;
        Invoke("UpdateText", 1);
    }
}
