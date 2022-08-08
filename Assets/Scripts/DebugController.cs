using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugController : MonoBehaviour {

    public Text textDebug;
    public bool checkFPS = true;
	void Awake () {
        Modules.textDebug = textDebug;
	}

    void Start()
    {
        if (checkFPS) InvokeRepeating("ShowFPS", 1f, 1f);
        InvokeRepeating("ClearText", 3f, 3f);
    }

    void ShowFPS()
    {
        textDebug.text += "\nFPS:" + (1f / Time.smoothDeltaTime).ToString();
    }

    void ClearText()
    {
        string[] dataLines = textDebug.text.Split('\n');
        if (dataLines.Length > 30)
        {
            textDebug.text = "";
            for (int i = dataLines.Length - 30; i <= dataLines.Length - 1; i++)
            {
                textDebug.text += dataLines[i];
                if (i <= dataLines.Length - 2) textDebug.text += "\n";
            }
        }
    }
}
