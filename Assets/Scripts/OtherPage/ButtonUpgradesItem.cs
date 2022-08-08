using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonUpgradesItem : MonoBehaviour {

    public int codeItem = 0;//0 rocket, 1 power, 2 magnet, 3 2x
    public GameObject progressBox;
    public Text textCost, textNote, textCoin;
    public Color colorError = Color.red;
    private Color originColorNote = Color.white;
	void Start () {
        originColorNote = textNote.color;
	}

    void ShowError(string errorData)
    {
        progressBox.gameObject.SetActive(false);
        textNote.gameObject.SetActive(true);
        textNote.text = errorData;
        textNote.color = colorError;
        Invoke("ReturnValueTotal", 1f);
    }

    void ReturnValueTotal()
    {
        progressBox.gameObject.SetActive(true);
        textNote.gameObject.SetActive(false);
        textNote.text = "";
        textNote.color = originColorNote;
    }

    public void ButtonClick()
    {
        textNote.font = AllLanguages.listFontLangB[Modules.indexLanguage];
        Modules.PlayAudioClipFree(Modules.audioButton);
        if (codeItem == 0)//neu la rocket
        {
            int cost = Modules.IntParseFast(textCost.text);
            if (Modules.totalCoin >= cost)//neu du tien
            {
                if (Modules.levelUpgradeRocket < Modules.maxLevelItem)
                {
                    Modules.levelUpgradeRocket++;
                    Modules.totalCoin -= cost;
                    Modules.SaveCoin();
                    textCoin.text = Modules.totalCoin.ToString();
                    Modules.SaveLevelRocket();
                    transform.parent.parent.gameObject.GetComponent<UpgradesController>().LoadDataNow();
                }
                else
                    ShowError(AllLanguages.shopMaxLevel[Modules.indexLanguage]);
            }
            else//neu khong du tien
                ShowError(AllLanguages.shopNotEnough[Modules.indexLanguage]);
        }
        else if (codeItem == 1)//neu la power
        {
            int cost = Modules.IntParseFast(textCost.text);
            if (Modules.totalCoin >= cost)//neu du tien
            {
                if (Modules.levelUpgradePower < Modules.maxLevelItem)
                {
                    Modules.levelUpgradePower++;
                    Modules.totalCoin -= cost;
                    Modules.SaveCoin();
                    textCoin.text = Modules.totalCoin.ToString();
                    Modules.SaveLevelPower();
                    transform.parent.parent.gameObject.GetComponent<UpgradesController>().LoadDataNow();
                }
                else
                    ShowError(AllLanguages.shopMaxLevel[Modules.indexLanguage]);
            }
            else//neu khong du tien
                ShowError(AllLanguages.shopNotEnough[Modules.indexLanguage]);
        }
        else if (codeItem == 2)//neu la magnet
        {
            int cost = Modules.IntParseFast(textCost.text);
            if (Modules.totalCoin >= cost)//neu du tien
            {
                if (Modules.levelUpgradeMagnet < Modules.maxLevelItem)
                {
                    Modules.levelUpgradeMagnet++;
                    Modules.totalCoin -= cost;
                    Modules.SaveCoin();
                    textCoin.text = Modules.totalCoin.ToString();
                    Modules.SaveLevelMagnet();
                    transform.parent.parent.gameObject.GetComponent<UpgradesController>().LoadDataNow();
                }
                else
                    ShowError(AllLanguages.shopMaxLevel[Modules.indexLanguage]);
            }
            else//neu khong du tien
                ShowError(AllLanguages.shopNotEnough[Modules.indexLanguage]);
        }
        else if (codeItem == 3)//neu la 2X
        {
            int cost = Modules.IntParseFast(textCost.text);
            if (Modules.totalCoin >= cost)//neu du tien
            {
                if (Modules.levelUpgradeXPoint < Modules.maxLevelItem)
                {
                    Modules.levelUpgradeXPoint++;
                    Modules.totalCoin -= cost;
                    Modules.SaveCoin();
                    textCoin.text = Modules.totalCoin.ToString();
                    Modules.SaveLevelXPoint();
                    transform.parent.parent.gameObject.GetComponent<UpgradesController>().LoadDataNow();
                }
                else
                    ShowError(AllLanguages.shopMaxLevel[Modules.indexLanguage]);
            }
            else//neu khong du tien
                ShowError(AllLanguages.shopNotEnough[Modules.indexLanguage]);
        }
        else if (codeItem == 4)//neu la cable
        {
            int cost = Modules.IntParseFast(textCost.text);
            if (Modules.totalCoin >= cost)//neu du tien
            {
                if (Modules.levelUpgradeCable < Modules.maxLevelItem)
                {
                    Modules.levelUpgradeCable++;
                    Modules.totalCoin -= cost;
                    Modules.SaveCoin();
                    textCoin.text = Modules.totalCoin.ToString();
                    Modules.SaveLevelCable();
                    transform.parent.parent.gameObject.GetComponent<UpgradesController>().LoadDataNow();
                }
                else
                    ShowError(AllLanguages.shopMaxLevel[Modules.indexLanguage]);
            }
            else//neu khong du tien
                ShowError(AllLanguages.shopNotEnough[Modules.indexLanguage]);
        }
        else if (codeItem == 5)//neu la skis
        {
            int cost = Modules.IntParseFast(textCost.text);
            if (Modules.totalCoin >= cost)//neu du tien
            {
                if (Modules.levelUpgradeSkis < Modules.maxLevelItem)
                {
                    Modules.levelUpgradeSkis++;
                    Modules.totalCoin -= cost;
                    Modules.SaveCoin();
                    textCoin.text = Modules.totalCoin.ToString();
                    Modules.SaveLevelSkis();
                    transform.parent.parent.gameObject.GetComponent<UpgradesController>().LoadDataNow();
                }
                else
                    ShowError(AllLanguages.shopMaxLevel[Modules.indexLanguage]);
            }
            else//neu khong du tien
                ShowError(AllLanguages.shopNotEnough[Modules.indexLanguage]);
        }
    }
}
