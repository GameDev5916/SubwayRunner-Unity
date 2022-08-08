using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UpgradesController : MonoBehaviour {

    public int codeItem = 0;//0 rocket, 1 power, 2 magnet, 3 2x, 4 cable
    public Image progressBox;
    public Text textCost, textNote;
    private float maxCost = 256000;

	void Start () {
        LoadDataNow();
        textNote.gameObject.SetActive(false);
	}

    public void LoadDataNow()
    {
        if (codeItem == 0)//neu la rocket
        {
            float money = 250 * Mathf.Pow(2, (Modules.levelUpgradeRocket + 1));
            if (money > maxCost) money = maxCost;
            textCost.text = money.ToString();
            progressBox.fillAmount = (float)Modules.levelUpgradeRocket / (float)Modules.maxLevelItem;
        }
        else if (codeItem == 1)//neu la power
        {
            float money = 250 * Mathf.Pow(2, (Modules.levelUpgradePower + 1));
            if (money > maxCost) money = maxCost;
            textCost.text = money.ToString();
            progressBox.fillAmount = (float)Modules.levelUpgradePower / (float)Modules.maxLevelItem;
        }
        else if (codeItem == 2)//neu la magnet
        {
            float money = 250 * Mathf.Pow(2, (Modules.levelUpgradeMagnet + 1));
            if (money > maxCost) money = maxCost;
            textCost.text = money.ToString();
            progressBox.fillAmount = (float)Modules.levelUpgradeMagnet / (float)Modules.maxLevelItem;
        }
        else if (codeItem == 3)//neu la 2X
        {
            float money = 250 * Mathf.Pow(2, (Modules.levelUpgradeXPoint + 1));
            if (money > maxCost) money = maxCost;
            textCost.text = money.ToString();
            progressBox.fillAmount = (float)Modules.levelUpgradeXPoint / (float)Modules.maxLevelItem;
        }
        else if (codeItem == 4)//neu la cable
        {
            float money = 250 * Mathf.Pow(2, (Modules.levelUpgradeCable + 1));
            if (money > maxCost) money = maxCost;
            textCost.text = money.ToString();
            progressBox.fillAmount = (float)Modules.levelUpgradeCable / (float)Modules.maxLevelItem;
        }
        else if (codeItem == 5)//neu la skis
        {
            float money = 250 * Mathf.Pow(2, (Modules.levelUpgradeSkis + 1));
            if (money > maxCost) money = maxCost;
            textCost.text = money.ToString();
            progressBox.fillAmount = (float)Modules.levelUpgradeSkis / (float)Modules.maxLevelItem;
        }
    }
}
