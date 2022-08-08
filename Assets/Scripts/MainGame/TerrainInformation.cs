using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainInformation : MonoBehaviour {

    public string codeTerrain = "A00";
    public float lengthTerrain = 5f;
    public List<string> listTerrainRan;//dia chi cua terrain co the thay the, dia chi nay can cach xa no hon chi so max terrain
    [Range(0, 100)]
    public int percentRan = 0;//kha nang bi thay doi
    public bool supportMenu = true;//co ho tro canh o menu game hay khong
    public GameObject objectTwoSide;
    public GameObject containDisplays, containInsteads, coinDisplays, coinInsteads;//doi tuong hien thi ban dau va an ban dau
    public List<GameObject> listObjectChilds = new List<GameObject>();//danh sach cac doi tuong le te ben trong vat can
    private List<GameObject> listDisplays = new List<GameObject>();//mang co san
    private List<GameObject> listInsteads = new List<GameObject>();//mang thay the
    private List<Vector3> listOriginPointDisplays = new List<Vector3>(), listOriginPointInsteads = new List<Vector3>();

    public void GetStart()
    {
        //tu dong lay cac doi tuong ben trong
        if (containDisplays) foreach (Transform tran in containDisplays.transform) listDisplays.Add(tran.gameObject);
        if (containInsteads) foreach (Transform tran in containInsteads.transform) listInsteads.Add(tran.gameObject);
        foreach (Transform tran in coinDisplays.transform)
            foreach (Transform tranChild in tran) listDisplays.Add(tranChild.gameObject);
        foreach (Transform tran in coinInsteads.transform)
            foreach (Transform tranChild in tran) listInsteads.Add(tranChild.gameObject);
        foreach (GameObject go in listObjectChilds)
        {
            if (go.GetComponent<BarrierController>() != null || go.GetComponent<ItemInformation>() != null)
                listDisplays.Add(go);
            else
                foreach (Transform tranChild in go.transform) listDisplays.Add(tranChild.gameObject);
        }
        //sap xep lai cac doi tuong theo chieu uu tien vi tri va di chuyen
        //SortListDisplays();
        //SortListInsteads();
        //luu lai vi tri cac doi tuong
        foreach (GameObject go in listDisplays) listOriginPointDisplays.Add(go.transform.localPosition);
        foreach (GameObject go in listInsteads) listOriginPointInsteads.Add(go.transform.localPosition);
    }

    void SortListDisplays()
    {
        for (int i = 0; i < listDisplays.Count - 1; i++)
        {
            for (int j = i; j < listDisplays.Count; j++)
            {
                if (CheckMoveObject(listDisplays[i]))
                {
                    if (CheckMoveObject(listDisplays[j]))
                    {
                        if (GetPointObject(listDisplays[j]) < GetPointObject(listDisplays[i]))
                        {
                            GameObject objectTemp = listDisplays[i];
                            listDisplays[i] = listDisplays[j];
                            listDisplays[j] = objectTemp;
                        }
                    }
                }
                else
                {
                    if (CheckMoveObject(listDisplays[j]))
                    {
                        GameObject objectTemp = listDisplays[i];
                        listDisplays[i] = listDisplays[j];
                        listDisplays[j] = objectTemp;
                    }
                    else
                    {
                        if (GetPointObject(listDisplays[j]) < GetPointObject(listDisplays[i]))
                        {
                            GameObject objectTemp = listDisplays[i];
                            listDisplays[i] = listDisplays[j];
                            listDisplays[j] = objectTemp;
                        }
                    }
                }
            }
        }
    }

    void SortListInsteads()
    {
        for (int i = 0; i < listInsteads.Count - 1; i++)
        {
            for (int j = i; j < listInsteads.Count; j++)
            {
                if (CheckMoveObject(listInsteads[i]))
                {
                    if (CheckMoveObject(listInsteads[j]))
                    {
                        if (GetPointObject(listInsteads[j]) < GetPointObject(listInsteads[i]))
                        {
                            GameObject objectTemp = listInsteads[i];
                            listInsteads[i] = listInsteads[j];
                            listInsteads[j] = objectTemp;
                        }
                    }
                }
                else
                {
                    if (CheckMoveObject(listInsteads[j]))
                    {
                        GameObject objectTemp = listInsteads[i];
                        listInsteads[i] = listInsteads[j];
                        listInsteads[j] = objectTemp;
                    }
                    else
                    {
                        if (GetPointObject(listInsteads[j]) < GetPointObject(listInsteads[i]))
                        {
                            GameObject objectTemp = listInsteads[i];
                            listInsteads[i] = listInsteads[j];
                            listInsteads[j] = objectTemp;
                        }
                    }
                }
            }
        }
    }

    bool CheckMoveObject(GameObject go)
    {
        bool result = false;
        if (go.GetComponent<BarrierController>() != null && go.GetComponent<BarrierController>().speedMove < 0) result = true;
        return result;
    }

    float GetPointObject(GameObject go)
    {
        float result = go.transform.position.z;
        if (go.GetComponent<BarrierController>() != null) result = result - go.GetComponent<BarrierController>().lengthBarrier / 2f;
        return result;
    }

    public void Restart(bool handleSafe = true)
    {
        //GetComponent<LODSwitcher>().SetCustomCamera(Camera.main);
        if (objectTwoSide != null)
        {
            if (Modules.reducedEffect == 2) objectTwoSide.SetActive(false);
            else objectTwoSide.SetActive(true);
        }
        if (handleSafe)
        {
            runReset = 0;
            typeReset = 0;
            startReset = true;
        }
        else
        {
            for (int i = 0; i < listInsteads.Count; i++)
            {
                listInsteads[i].transform.localPosition = listOriginPointInsteads[i];
                listInsteads[i].SetActive(false);
            }
            for (int i = 0; i < listDisplays.Count; i++)
            {
                listDisplays[i].transform.localPosition = listOriginPointDisplays[i];
                listDisplays[i].SetActive(true);
                BarrierController barrierControl = listDisplays[i].GetComponent<BarrierController>();
                if (barrierControl)
                {
                    barrierControl.CallStart();
                }
                else
                {
                    ItemInformation itemInfo = listDisplays[i].GetComponent<ItemInformation>();
                    itemInfo.CallStart();
                }
            }
        }
    }

    private int runReset = 0;//bien chay
    private int typeReset = 0;//reset displays hay insteads
    private bool startReset = false;
    private int handelPerTime = 10;//moi lan update xu ly 10 doi tuong
    void Update()
    {
        if (!startReset) return;
        for (int i = 0; i < handelPerTime; i++)
        {
            if (typeReset == 0)//load insteads
            {
                if (runReset < listInsteads.Count)
                {
                    listInsteads[runReset].SetActive(false);
                    listInsteads[runReset].transform.localPosition = listOriginPointInsteads[runReset];
                    runReset++;
                }
                else
                {
                    typeReset = 1;
                    runReset = 0;
                }
            }
            else if (typeReset == 1)//load displays
            {
                if (runReset < listDisplays.Count)
                {
                    listDisplays[runReset].SetActive(true);
                    listDisplays[runReset].transform.localPosition = listOriginPointDisplays[runReset];
                    BarrierController barrierControl = listDisplays[runReset].GetComponent<BarrierController>();
                    if (barrierControl)
                    {
                        barrierControl.CallStart();
                    }
                    else
                    {
                        ItemInformation itemInfo = listDisplays[runReset].GetComponent<ItemInformation>();
                        itemInfo.CallStart();
                    }
                    runReset++;
                }
                else
                {
                    typeReset = 0;
                    runReset = 0;
                    startReset = false;
                    break;
                }
            }
        }
    }
}