using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatePoolTerrains : MonoBehaviour {

    public List<GameObject> listTypeTerrain = new List<GameObject>();
    public List<ListCodeTerrain> listSceneTerrain = new List<ListCodeTerrain>();
    public List<ListUseTerrain> listUseTerrain = new List<ListUseTerrain>();
    private int totalObject = 0;
    private int runTime = 0;
    private bool mesStart = false;
    private int indexI = 0, indexJ = 0;
    private List<GameObject> listTerrainNow = new List<GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(transform);
    }

    public void CallStart()
    {
        indexI = 0;
        indexJ = 0;
        runTime = 0;
        totalObject = 0;
        listTerrainNow = new List<GameObject>();
        for (int i = 0; i < listSceneTerrain.Count; i++)
            totalObject += listSceneTerrain[i].listTerrain.Count;
        mesStart = true;
    }

    public float GetPercent()
    {
        return (float)runTime / (float)totalObject;
    }

    void FixedUpdate()
    {
        if (!mesStart || runTime >= totalObject) return;
        GameObject terrainNow = null;
        for (int i = 0; i < listTypeTerrain.Count; i++)
        {
            if (listTypeTerrain[i].GetComponent<TerrainInformation>().codeTerrain == listSceneTerrain[indexI].listTerrain[indexJ])
            {
                terrainNow = Instantiate(listTypeTerrain[i], transform) as GameObject;
                terrainNow.GetComponent<TerrainInformation>().GetStart();
                terrainNow.SetActive(false);
                break;
            }
        }
        listTerrainNow.Add(terrainNow);
        runTime++;
        indexJ++;
        if (indexJ >= listSceneTerrain[indexI].listTerrain.Count)
        {
            listUseTerrain.Add(new ListUseTerrain(listTerrainNow));
            indexJ = 0;
            indexI++;
            listTerrainNow = new List<GameObject>();
        }
    }
}
