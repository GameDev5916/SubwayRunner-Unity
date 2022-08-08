using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonItemBuy : MonoBehaviour {

    public string codeItem = "";
    public void ClickButton()
    {
        if (Modules.statusGame != StatusGame.play)
            return;
        if (codeItem == "headStart" && Modules.allowUseHoverbike)
        {
            //neu la item head start thi thuc hien an luon rocket
            if (Modules.gameGuide == "YES" && Modules.stepGuide != 5) return; //check xem co su dung trong luc huong dan
            if (Modules.gameGuide == "YES" && Modules.stepGuide == 5)
            {
                Modules.stepGuide++;
                Transform textGuide = Modules.panelGameGuide.transform.Find("TextGuide");
                textGuide.GetComponent<Text>().text = AllLanguages.playBeginMove[Modules.indexLanguage];
                Transform iconItemBuy = Modules.panelGameGuide.transform.Find("IconItemBuy");
                iconItemBuy.GetComponent<Image>().enabled = false;
                Modules.gameGuide = "NO";
                Modules.SaveGameGuide();
                Invoke("RemoveGuide", 1f);
            }
            if (Modules.totalHeadStart <= 0)
                return;
            Modules.totalHeadStart--;
            Modules.SaveHeadStart();
            Transform number = transform.Find("Number");
            Text txtNumber = number.GetComponent<Text>();
            txtNumber.text = Modules.totalHeadStart.ToString();
            //thuc hien chuc nang button
            Modules.mainCharacter.GetComponent<HeroController>().RunFunctionItem(TypeItems.hoverbike, Modules.mainCharacter.transform.position.x, true);
            //thuc hien dong button
            if (Modules.totalHeadStart <= 0)
            {
                Modules.totalHeadStart = 0;
                Animator aniPanel = transform.GetComponent<Animator>();
                aniPanel.SetTrigger("TriClose");
            }
        }
        else if (codeItem == "scoreBooster" && Modules.gameGuide != "YES")
        {
            //neu la item scoreBooster thi thuc hien cong diem nhan score
            if (Modules.totalScoreBooster <= 0)
                return;
            Modules.totalScoreBooster--;
            Modules.SaveScoreBooster();
            Transform number = transform.Find("Number");
            Text txtNumber = number.GetComponent<Text>();
            txtNumber.text = Modules.totalScoreBooster.ToString();
            //thuc hien chuc nang button
            Modules.mainCharacter.GetComponent<HeroController>().RunFunctionItem(TypeItems.scoreBooster, Modules.mainCharacter.transform.position.x, true);
            //thuc hien dong button
            if (Modules.totalScoreBooster <= 0)
            {
                Modules.totalScoreBooster = 0;
                Animator aniPanel = transform.GetComponent<Animator>();
                aniPanel.SetTrigger("TriClose");
            }
        }
    }

    public void AddForeDown()
    {
        foreach (Transform tran in Modules.containButtonBuy.transform)
        {
            if (tran.GetComponent<ButtonItemBuy>().codeItem != codeItem)
            {
                tran.GetComponent<Rigidbody2D>().AddForce(new Vector3(1, -7000, 1));
            }
        }
    }

    void RemoveGuide()
    {
        Modules.panelGameGuide.SetActive(false);
        Camera.main.GetComponent<PageMainGame>().ShowMissionsChallenge();
    }
}
