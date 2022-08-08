using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageTimeCount : MonoBehaviour {

    public GameObject effectTimeShow;
    public Vector3 pointShow = new Vector3(0, -35, 0);
    public int numberBegin = 3;//so ban dau dem lui ve 0
    public Text textStartIn;

    public void StartCount()
    {
        int iLang = Modules.indexLanguage;
        textStartIn.font = AllLanguages.listFontLangA[iLang];
        textStartIn.text = AllLanguages.playStartIn[iLang];
        GameObject numberCount = Instantiate(effectTimeShow, pointShow, Quaternion.identity) as GameObject;
        numberCount.transform.SetParent(transform, false);
        UpdateCountTime updateCount = numberCount.GetComponent<UpdateCountTime>();
        updateCount.parentMe = transform.gameObject;
        updateCount.timeBegin = numberBegin;
    }
}
