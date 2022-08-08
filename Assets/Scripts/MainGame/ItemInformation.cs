using UnityEngine;
using System.Collections;

public class ItemInformation : MonoBehaviour {

    public TypeItems typeItem = TypeItems.coin;
    [Range(0, 100)]
    public int percentShow = 100;//phan tram ty le xuat hien
    public GameObject objectOther;//doi tuong thay the neu doi tuong nay khong xuat hien
    //public GameObject effectDestroy;
    public GameObject pointFllow;//chi danh cho next gate
    public GameObject meshShow;
    public float speedRotate = 50f;
    public bool heightItem = false;//neu la true thi se check hien thi khi su dung jumper, rocket, cable
    //danh cho challenge
    [HideInInspector]
    public string valueText = "";
    [HideInInspector]
    public int indexText = -1;
    private bool checkShowItems = false;
    private bool startNow = false;

    public void ResetItem()
    {
        startNow = false;
        if (meshShow) meshShow.transform.eulerAngles = Vector3.zero;
    }

    public void CallStart()
    {
        if (Modules.gameGuide == "YES") { gameObject.SetActive(false); return; }
        if (Modules.statusGame != StatusGame.play)
        {
            if (Modules.statusGame == StatusGame.flyScene || Modules.statusGame == StatusGame.menu || Modules.statusGame == StatusGame.start)
            {
                if (transform.position.z <= Modules.rangeTakeOff)
                { gameObject.SetActive(false); return; }
            }
            else if (Modules.statusGame == StatusGame.bonusEffect)
            {
                if (!Modules.useBonus)
                {
                    if (transform.position.z <= Modules.rangeTakeOff)
                    { gameObject.SetActive(false); return; }
                }
            }
            else { gameObject.SetActive(false); return; }
        }
        if ((Modules.useRocket || Modules.useCable) && !heightItem) { gameObject.SetActive(false); return; }
        if (typeItem == TypeItems.missions && Modules.dataMissionsUse == "") { gameObject.SetActive(false); return; }
        else if (typeItem == TypeItems.challenge && Modules.dataChallengeUse == "") { gameObject.SetActive(false); return; }
        int ran = Random.Range(0, 100);
        if (ran >= percentShow)
        {
            if (objectOther != null)
            {
                objectOther.SetActive(true);
                if (objectOther.GetComponent<BarrierController>()) objectOther.GetComponent<BarrierController>().CallStart();
                else if (objectOther.GetComponent<ItemInformation>()) objectOther.GetComponent<ItemInformation>().CallStart();
                else//neu khong thi la list coin
                {
                    foreach (Transform tran in objectOther.transform)
                    {
                        tran.gameObject.SetActive(true);
                        tran.GetComponent<ItemInformation>().CallStart();
                    }
                }
            }
            gameObject.SetActive(false); return;
        }
        if (typeItem == TypeItems.coin || heightItem)//neu la coin hoac item tren cao
        {
            foreach (Transform tran in transform) tran.gameObject.SetActive(false);
            //GetComponent<BoxCollider>().enabled = false;
            checkShowItems = true;
        }
        //xu ly missions va challenge
        if (typeItem == TypeItems.missions)//neu la missions
        {
            if (meshShow) Destroy(meshShow);
            meshShow = Instantiate(Modules.listMissions[Modules.indexItemMissions].model, transform) as GameObject;
            meshShow.transform.localPosition = Vector3.zero;
        }
        else if (typeItem == TypeItems.challenge)//neu la challenge
        {
            valueText = Modules.listTextRequire[Modules.listTextColect.Count];
            indexText = Modules.listTextColect.Count;
            GameObject textModel = null;
            for (int i = 0; i < Modules.listChallenge.Count; i++)
            {
                if (Modules.listChallenge[i].value == valueText)
                {
                    textModel = Modules.listChallenge[i].model;
                    break;
                }
            }
            if (textModel != null)
            {
                if (meshShow) Destroy(meshShow);
                meshShow = Instantiate(textModel, transform) as GameObject;
                meshShow.transform.localPosition = Vector3.zero;
            }
            else gameObject.SetActive(false);
        }
        startNow = true;
    }

    void FixedUpdate()
    {
        if (!startNow) return;
        if (meshShow != null && meshShow.activeSelf)
            meshShow.transform.Rotate(0, speedRotate * Time.fixedDeltaTime, 0);
        if (Modules.statusGame == StatusGame.play && transform.position.z <= -Modules.rangeHireBack) { gameObject.SetActive(false); return; }
        if (typeItem == TypeItems.coin || heightItem) UpdateShowItems();
    }

    void UpdateShowItems()
    {
        if (!startNow || !checkShowItems || GetComponent<MoveToMagnet>()) return;
        if (heightItem)
        {
            if (!Modules.useJumper && !Modules.useRocket && !Modules.useCable)
                return;
        }
        else
        {
            if (Modules.useRocket || Modules.useCable)
                return;
        }
        if (transform.position.z > Modules.rangeShowCoin
            || transform.position.z <= Modules.mainCharacter.transform.position.z) return;
        if (Modules.reducedEffect < 2 && Modules.statusGame == StatusGame.play)
            Modules.poolOthers.GetComponent<HighItemsController>().PlayEffectCoin(transform.gameObject);
        foreach (Transform tran in transform) tran.gameObject.SetActive(true);
        //GetComponent<BoxCollider>().enabled = true;
        checkShowItems = false;
    }
}
public enum TypeItems
{
    coin,
    key,
    sneaker,
    magnet,
    jetpack,
    trampoline,
    xpoint,
    hoverboard,
    scoreBooster,
    headStart,
    mysteryBox,
    hoverbike,
    balloon,
    missions,
    challenge,
    nextGate,
    roadBonus,
    boxBonus,
    startTunner,
    endTunner
}