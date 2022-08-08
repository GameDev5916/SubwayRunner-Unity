using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelShowUseItem : MonoBehaviour {

    public int numberSlot = 0;
    public TypeItems codeItemNow = TypeItems.coin;
    public Vector2 timeUseItem = Vector2.zero;
    private int totalTime = 0, runTimeCool = 0;
    private Image imgProgressBar;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(1, -7000, 1));
        ResetTime(timeUseItem);
        imgProgressBar = transform.Find("RunTime").GetComponent<Image>();
    }

    void FixedUpdate()
    {
        if (Modules.statusGame == StatusGame.over)
        {
            Destroy(gameObject);
            return;
        }
        if (runTimeCool >= totalTime)
        {
            imgProgressBar.fillAmount = 0;
            SetClosePanel();
        }
        else
        {
            if (codeItemNow != TypeItems.jetpack && codeItemNow != TypeItems.hoverbike)//neu la cac item khac rocket va cable thi set them dieu kien
            {
                if (!Modules.useRocket && !Modules.useJumper && !Modules.useCable && Modules.statusGame == StatusGame.play)
                    runTimeCool++;
            }
            else runTimeCool++;
            float percent = (float)(totalTime - runTimeCool) / (float)totalTime;
            imgProgressBar.fillAmount = percent;
        }
    }

    void DestroyPanel()
    {
        if (handleContent)
        {
            //het thoi gian su dung item
            if (codeItemNow == TypeItems.hoverboard)//van truot
                Modules.mainCharacter.GetComponent<HeroController>().RemoveSkisItem();
            else if (codeItemNow == TypeItems.sneaker)//giay
                Modules.mainCharacter.GetComponent<HeroController>().RemovePowerItem();
            else if (codeItemNow == TypeItems.magnet)//nam cham
                Modules.mainCharacter.GetComponent<HeroController>().RemoveMagnetItem();
            else if (codeItemNow == TypeItems.xpoint)//X point
                Modules.mainCharacter.GetComponent<HeroController>().RemoveXPointItem();
        }
        foreach (Transform tran in Modules.containMesItems.transform)
        {
            if (numberSlot % 2 == 0)
            {
                if (tran.GetComponent<PanelShowUseItem>().numberSlot % 2 == 0 && tran.GetComponent<PanelShowUseItem>().numberSlot > numberSlot)
                {
                    tran.GetComponent<PanelShowUseItem>().numberSlot -= 2;
                    tran.GetComponent<Rigidbody2D>().AddForce(new Vector3(1, -7000, 1));
                }
            }
            else
            {
                if (tran.GetComponent<PanelShowUseItem>().numberSlot % 2 != 0 && tran.GetComponent<PanelShowUseItem>().numberSlot > numberSlot)
                {
                    tran.GetComponent<PanelShowUseItem>().numberSlot -= 2;
                    tran.GetComponent<Rigidbody2D>().AddForce(new Vector3(1, -7000, 1));
                }
            }
        }
        Destroy(gameObject);
    }

    public void ResetTime(Vector2 newTime)
    {
        timeUseItem = newTime;
        int timeUse = Mathf.RoundToInt(Random.Range(timeUseItem.x, timeUseItem.y));
        totalTime = Modules.SecondsToTimePerFrame(timeUse);
        runTimeCool = 0;
    }

    private bool handleContent = true;
    public void RemovePanel(bool handleContentInput = true)
    {
        handleContent = handleContentInput;
        runTimeCool = totalTime;
    }

    private void SetClosePanel()
    {
        Animator aniPanel = transform.GetComponent<Animator>();
        aniPanel.SetTrigger("TriClose");
        Invoke("DestroyPanel", 0.3f);
    }
}
