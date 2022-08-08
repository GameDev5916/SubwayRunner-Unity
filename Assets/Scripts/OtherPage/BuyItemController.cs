using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuyItemController : MonoBehaviour {

    public int codeItem = 0;//0 coin, 1 key, 2 skis, 3 mysteryBox, 4 headStart, 5 scoreBooster
    public Text textTotal;

    void Start()
    {
        LoadDataNow();
    }

    public void LoadDataNow()
    {
        int iLang = Modules.indexLanguage;
        textTotal.font = AllLanguages.listFontLangB[iLang];
        if (codeItem == 0)//neu la coin
        {
            textTotal.text = AllLanguages.shopTotal[iLang] + " " + Modules.totalCoin;
        }
        else if (codeItem == 1)//neu la key
        {
            textTotal.text = AllLanguages.shopTotal[iLang] + " " + Modules.totalKey;
        }
        else if (codeItem == 2)//neu la skis
        {
            textTotal.text = AllLanguages.shopTotal[iLang] + " " + Modules.totalSkis;
        }
        else if (codeItem == 3)//neu la mysteryBox
        {
            textTotal.text = AllLanguages.shopUseRight[iLang];
        }
        else if (codeItem == 4)//neu la headStart
        {
            textTotal.text = AllLanguages.shopTotal[iLang] + " " + Modules.totalHeadStart;
        }
        else if (codeItem == 5)//neu la scoreBooster
        {
            textTotal.text = AllLanguages.shopTotal[iLang] + " " + Modules.totalScoreBooster;
        }
    }
}
