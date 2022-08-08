using UnityEngine;
using System.Collections;

public class BarrierController : MonoBehaviour {

    public float lengthBarrier = 0;//chieu dai cua vat can
    [Range(0, 100)]
    public int percentShow = 100;//phan tram ty le xuat hien
    public float speedMove = 0;
    public GameObject objectOther;//doi tuong thay the neu doi tuong nay khong xuat hien
    public float farMoreHide = 0;//danh cho doi tuong nao noi them phia sau dai ra (vi du noi them duoi coins)
    //xu ly hieu ung den va coi car and train
    public GameObject listLights;
    public AudioClip audioHorn;
    [Range(0, 100)]
    public int percentPlay = 100;
    private float distancePlay = 70;
    private bool statusPlay = false;
    //private float numEdit = 1.5f;
    private bool startNow = false;

    public void ResetBarrier()
    {
        startNow = false;
    }

    public void CallStart()
    {
        if (Modules.gameGuide == "YES") { gameObject.SetActive(false); return; }
        if (Modules.statusGame != StatusGame.play)
        {
            if (Modules.statusGame == StatusGame.flyScene || Modules.statusGame == StatusGame.menu || Modules.statusGame == StatusGame.start)
            {
                if (transform.position.z - lengthBarrier / 2f <= Modules.rangeTakeOff)
                { gameObject.SetActive(false); return; }
            }
            else if (Modules.statusGame == StatusGame.bonusEffect)
            {
                if (!Modules.useBonus)
                {
                    if (transform.position.z - lengthBarrier / 2f <= Modules.rangeTakeOff)
                    { gameObject.SetActive(false); return; }
                }
            }
            else { gameObject.SetActive(false); return; }
        }
        if ((Modules.useRocket || Modules.useCable) && lengthBarrier <= 3) { gameObject.SetActive(false); return; }
        //int percentNew = Mathf.RoundToInt((Modules.speedGame / Modules.maxSpeedMove * numEdit) * percentShow);
        int ran = Random.Range(0, 100);
        if (ran >= percentShow /*percentNew*/)
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
        if (listLights != null) listLights.SetActive(false);
        statusPlay = false;
        startNow = true;
    }

    void FixedUpdate()
    {
        if (!startNow || Modules.statusGame != StatusGame.play) return;
        float point = transform.position.z - lengthBarrier / 2f;
        if (point <= Modules.rangeRunObj)//move object
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speedMove * Modules.speedGame);
        if (point <= -lengthBarrier - Modules.rangeHireBack - farMoreHide) { gameObject.SetActive(false); return; }
        //xu ly light horn
        if (!statusPlay && transform.position.z <= distancePlay)
        {
            statusPlay = true;
            if (Mathf.Abs(Modules.mainCharacter.transform.position.y - transform.position.y) < 7 &&//neu khong bay qua cao hoac qua thap
                Mathf.Abs(Modules.mainCharacter.transform.position.x - transform.position.x) < 5)//neu khong cach trai/phai qua xa
            {
                int ran = Random.Range(0, 100);
                if (ran < percentPlay)
                {
                    if (audioHorn != null) Modules.PlayAudioClipFree(audioHorn);
                    if (listLights != null) listLights.SetActive(true);
                }
            }
        }
    }
}
