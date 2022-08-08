using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighItemsController : MonoBehaviour {

    public float heighItems = 27f;
    public float farMaxItems = 50f;
    public Vector2 rangeChange = new Vector2(15, 30);//range change lane coins
    [Range(0, 100)]
    public int percentItems = 5;
    public List<GameObject> listCoins;
    public List<GameObject> listItems;
    public List<GameObject> listParCoins;
    public List<GameObject> listParEatCoins;
    public List<GameObject> listParEatItems;
    private bool lockAllItem = true;

    void Awake()
    {
        DontDestroyOnLoad(transform);
    }

    int oldIndexCoins = 0;
    int runCoinLane = 0;
    int totalCoinLane = 15;
    int indexLane = 0;//-1,0,1 <=> left middle right
    int oldLane = 0;
    float distance = 5f;

    public void ResetAllItems()
    {
        foreach (GameObject go in listCoins) go.SetActive(false);
        foreach (GameObject go in listItems) go.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Modules.statusGame != StatusGame.play) return;
        if (!Modules.useJumper && !Modules.useRocket && !Modules.useCable)
        {
            if (!lockAllItem)
            {
                ResetAllItems();
                lockAllItem = true;
            }
            return;
        }
        lockAllItem = false;
        if (listCoins[oldIndexCoins].activeSelf)
        {
            if (listCoins[oldIndexCoins].transform.position.z <= farMaxItems - distance)
            {
                for (int i = 0; i < listCoins.Count; i++)
                {
                    if (!listCoins[i].activeSelf)
                    {
                        listCoins[i].SetActive(true);
                        float laneNow = indexLane * 5;
                        if (oldLane != indexLane)
                        {
                            if (indexLane != 0) laneNow = indexLane * 2.5f;
                            else laneNow = oldLane * 2.5f;
                            oldLane = indexLane;
                        }
                        listCoins[i].transform.position = new Vector3(laneNow, heighItems, farMaxItems);
                        listCoins[i].GetComponent<ItemInformation>().CallStart();
                        TerrainController terrain = Camera.main.GetComponent<TerrainController>();
                        listCoins[i].transform.parent = terrain.listShowTerrain[terrain.listShowTerrain.Count - 1].transform;
                        oldIndexCoins = i;
                        runCoinLane++;
                        //xu ly ngau nhien items
                        if (runCoinLane == Mathf.RoundToInt(rangeChange.x / 2f))
                        {
                            int ran = Random.Range(0, 100);
                            if (ran < percentItems || Modules.useJumper)//neu dung ty le hoac dang an jumper thi xuat hien
                            {
                                List<int> indexItems = new List<int>();
                                for (int j = 0; j < listItems.Count; j++)
                                    if (!listItems[j].activeSelf)
                                    {
                                        if (!Modules.useJumper)
                                        {
                                            if (listItems[j].GetComponent<ItemInformation>().typeItem != TypeItems.jetpack
                                                && listItems[j].GetComponent<ItemInformation>().typeItem != TypeItems.hoverbike)
                                                indexItems.Add(j);
                                        }
                                        else indexItems.Add(j);
                                    }
                                List<int> laneShow = new List<int>();
                                int ranNum = Random.Range(0, 2);
                                if (Modules.useJumper) ranNum = 1;
                                if (ranNum == 0 && indexItems.Count > 0)//chi co 1 item
                                {
                                    if (indexLane == 0)//neu o giua
                                    {
                                        int ranSide = Random.Range(0, 2);
                                        if (ranSide == 0)//-1
                                            laneShow.Add(-1);
                                        else//1
                                            laneShow.Add(1);
                                    }
                                    else laneShow.Add(0);
                                }
                                else if (ranNum == 1 && indexItems.Count > 1)//co 2 items
                                {
                                    if (indexLane == -1)//neu ben trai
                                    {
                                        laneShow.Add(0);
                                        laneShow.Add(1);
                                    }
                                    else if (indexLane == 1)
                                    {
                                        laneShow.Add(0);
                                        laneShow.Add(-1);
                                    }
                                    else
                                    {
                                        laneShow.Add(-1);
                                        laneShow.Add(1);
                                    }
                                }
                                //hien thi items o day
                                for (int j = 0; j < laneShow.Count; j++)
                                {
                                    int indexItemNow = indexItems[Random.Range(0, indexItems.Count)];
                                    listItems[indexItemNow].SetActive(true);
                                    listItems[indexItemNow].transform.position = new Vector3(laneShow[j] * 5, heighItems, farMaxItems);
                                    listItems[indexItemNow].GetComponent<ItemInformation>().CallStart();
                                    listItems[indexItemNow].transform.parent = terrain.listShowTerrain[terrain.listShowTerrain.Count - 1].transform;
                                    indexItems.Remove(indexItemNow);
                                }
                            }
                        }
                        //xu ly chuyen lane
                        if (runCoinLane >= totalCoinLane)//change lane
                        {
                            oldLane = indexLane;
                            if (indexLane == 0)//neu dang o giua
                            {
                                int ran = Random.Range(0, 2);
                                if (ran == 0) indexLane = -1;
                                else indexLane = 1;
                            }
                            else indexLane = 0;
                            totalCoinLane = Random.Range(Mathf.RoundToInt(rangeChange.x), Mathf.RoundToInt(rangeChange.y));
                            runCoinLane = 1;
                        }
                        break;
                    }
                }
            }
        }
        else
        {
            listCoins[oldIndexCoins].SetActive(true);
            listCoins[oldIndexCoins].transform.position = new Vector3(indexLane*5, heighItems, farMaxItems);
            listCoins[oldIndexCoins].GetComponent<ItemInformation>().CallStart();
            TerrainController terrain = Camera.main.GetComponent<TerrainController>();
            listCoins[oldIndexCoins].transform.parent = terrain.listShowTerrain[terrain.listShowTerrain.Count - 1].transform;
            //xu ly doi lane
            totalCoinLane = Random.Range(Mathf.RoundToInt(rangeChange.x), Mathf.RoundToInt(rangeChange.y));
            runCoinLane = 1;
        }
    }

    public void PlayEffectCoin(GameObject parent)
    {
        foreach (GameObject go in listParCoins)
        {
            if (!go.GetComponent<ParticleSystem>().isPlaying)
            {
                go.transform.parent = parent.transform;
                go.transform.localPosition = Vector3.zero;
                go.GetComponent<ParticleSystem>().Play();
                break;
            }
        }
    }

    public void PlayEffectEatCoins(Vector3 pointShow)
    {
        foreach (GameObject go in listParEatCoins)
        {
            if (!go.GetComponent<PlayParticle>().IsPlay())
            {
                go.transform.position = pointShow;
                go.GetComponent<PlayParticle>().Play(true);
                break;
            }
        }
    }

    public void PlayEffectEatItems(Vector3 pointShow)
    {
        foreach (GameObject go in listParEatItems)
        {
            if (!go.GetComponent<PlayParticle>().IsPlay())
            {
                go.transform.position = pointShow;
                go.GetComponent<PlayParticle>().Play();
                break;
            }
        }
    }
}
