using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PageOpenMysteryBox : MonoBehaviour {

    [Header("0 Coin, 1 Key, 2 Skis, 3 HeadStart, 4 ScoreBosster")]
    public List<GameObject> listItemBonus = new List<GameObject>();
    public GameObject containItemBonus, mysteryBox;
    public GameObject effectExplosion, effectAura;
    public Text textKey, textCoin, textBox, textStatus, textTitleItem, textNoteItem;
    public float timeCreateItem = 0.5f;//tinh bang giay
    private bool openDone = false;
    private bool allowTap = true;
    private GameObject mysteryBoxUse, effectAuraUse, itemBonus;

	public void CallStart () {
        openDone = false;
        allowTap = true;
        textKey.text = Mathf.RoundToInt(Modules.totalKey).ToString();
        textCoin.text = Mathf.RoundToInt(Modules.totalCoin).ToString();
        textBox.text = Mathf.RoundToInt(Modules.totalMysteryBox).ToString();
        if (mysteryBoxUse != null) Destroy(mysteryBoxUse);
        mysteryBoxUse = Instantiate(mysteryBox, Modules.containOpenBox.transform) as GameObject;
        if (effectAuraUse != null) Destroy(effectAuraUse);
        effectAuraUse = Instantiate(effectAura, Modules.containOpenBox.transform) as GameObject;
        if (itemBonus != null) Destroy(itemBonus);
        textTitleItem.gameObject.SetActive(false);
        textNoteItem.gameObject.SetActive(false);
        textStatus.font = AllLanguages.listFontLangA[Modules.indexLanguage];
        textStatus.text = AllLanguages.otherTapOpen[Modules.indexLanguage];
	}

    public void TapScene()
    {
        if (!allowTap) return;
        if (openDone)//neu hoan thanh thi chuyen trang
        {
            Modules.PlayAudioClipFree(Modules.audioButton);
            if (Modules.totalMysteryBox > 0)
            {
                CallStart();
            }
            else
            {
                if (Modules.nextPageOpenBox == "ShowAchievement")
                {
                    Modules.containAchievement.SetActive(true);
                    Modules.containAchievement.transform.Find("MainCamera").GetComponent<PageAchievement>().CallStart();
                }
                else if (Modules.nextPageOpenBox == "ShopItems")
                {
                    Modules.containShopItem.SetActive(true);
                    Modules.containShopItem.transform.Find("MainCamera").GetComponent<PageShopItems>().CallStart();
                }
                Modules.containOpenBox.SetActive(false);
            }
        }
        else//neu khong thi mo box
        {
            allowTap = false;
            Modules.PlayAudioClipFree(Modules.audioOpenBox);
            mysteryBoxUse.GetComponent<Animator>().SetTrigger("TriOpen");
            Destroy(mysteryBoxUse, 0.4f);
            if (effectExplosion != null) Instantiate(effectExplosion, Vector3.zero, Quaternion.identity);
            Invoke("CreateItemBonus", timeCreateItem);
            Invoke("AllowClickTap", 1f);
        }
    }

    void AllowClickTap()
    {
        allowTap = true;
    }

    void CreateItemBonus()
    {
        effectAuraUse.GetComponent<Animator>().ResetTrigger("TriOpen");
        effectAuraUse.GetComponent<Animator>().SetTrigger("TriOpen");
        int numberRan = Random.Range(0, 100);
        int indexRan = 0;
        if (numberRan < 40) indexRan = 0;
        else if (numberRan < 60) indexRan = 1;
        else if (numberRan < 75) indexRan = 2;
        else if (numberRan < 90) indexRan = 3;
        else if (numberRan < 100) indexRan = 4;
        itemBonus = Instantiate(listItemBonus[indexRan], containItemBonus.transform) as GameObject;
        itemBonus.transform.localPosition = Vector3.zero;
        itemBonus.transform.localEulerAngles = Vector3.zero;
        containItemBonus.GetComponent<Animator>().SetTrigger("TriOpen");
        //xu ly cong vao du lieu nguoi choi
        ItemBonusInformation itemBonusInfo = itemBonus.GetComponent<ItemBonusInformation>();
        int valueRan = Mathf.RoundToInt(Random.Range(itemBonusInfo.valueItem.x, itemBonusInfo.valueItem.y));
        string textValue = "";
        if (indexRan == 0)//neu la coin
        {
            if (valueRan < 100) valueRan = 100;
            valueRan = Mathf.RoundToInt(valueRan / 100f) * 100;
            Modules.totalCoin += valueRan;
            textCoin.text = Mathf.RoundToInt(Modules.totalCoin).ToString();
            Modules.SaveCoin();
        }
        else if (indexRan == 1)//neu la key
        {
            Modules.totalKey += valueRan;
            textKey.text = Mathf.RoundToInt(Modules.totalKey).ToString();
            Modules.SaveKey();
        }
        else if (indexRan == 2)//neu la skis
        {
            Modules.totalSkis += valueRan;
            if (Modules.totalSkis > Modules.maxHoverboard)
                Modules.totalSkis = Modules.maxHoverboard;
            textValue = "x";
            Modules.SaveSkis();
        }
        else if (indexRan == 3)//neu la headStart
        {
            Modules.totalHeadStart += valueRan;
            if (Modules.totalHeadStart > Modules.maxHeadstart)
                Modules.totalHeadStart = Modules.maxHeadstart;
            textValue = "x";
            Modules.SaveHeadStart();
        }
        else if (indexRan == 4)//neu la scoreBooster
        {
            Modules.totalScoreBooster += valueRan;
            if (Modules.totalScoreBooster > Modules.maxScorebooster)
                Modules.totalScoreBooster = Modules.maxScorebooster;
            textValue = "x";
            Modules.SaveScoreBooster();
        }
        textTitleItem.gameObject.SetActive(true);
        textNoteItem.gameObject.SetActive(true);
        textTitleItem.font = AllLanguages.listFontLangA[Modules.indexLanguage];
        textTitleItem.text = valueRan.ToString() + textValue + " " + AllLanguages.otherNameItemBonus[itemBonusInfo.indexInfo][Modules.indexLanguage];
        textNoteItem.font = AllLanguages.listFontLangB[Modules.indexLanguage];
        textNoteItem.text = AllLanguages.otherInfoItemBonus[itemBonusInfo.indexInfo][Modules.indexLanguage];
        Modules.totalMysteryBox--;
        if (Modules.totalMysteryBox < 0) Modules.totalMysteryBox = 0;
        textStatus.text = AllLanguages.otherTapContinue[Modules.indexLanguage]; ;
        openDone = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TapScene();
        }
    }
}
