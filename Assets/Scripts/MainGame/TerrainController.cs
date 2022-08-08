using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TerrainController : MonoBehaviour
{
    public int indexScenesNow = 0;
    public List<int> listIndexScenesNormal = new List<int>();
    public List<int> listIndexScenesBonus = new List<int>();
    public int maxShowTerrainFront = 4;//ap dung chay xuoi map
    public int maxShowTerrainBack = 4;//ap dung chay lui map
    private int originMaxShowFront = 4, originMaxShowBack =4;
    private int runFront = 1;//neu -1 la run back
    [HideInInspector]
    public List<GameObject> listShowTerrain = new List<GameObject>();
    private int runCodeTerrain = 0;//thay dia hinh trong canh
    private float pointCodeTerrain = 0;//toa do z luc bat dau tao dia hinh
    [HideInInspector]
    public string listCodeTerrain = "";//luu lai ma cua cac terrain
    public float numEditSpeed = 0.75f;//so lieu dieu chinh toc do speed
    public float pointBySpeed = 1f;//nhan theo toc do chay cua map
    public float timeChangeSpeed = 3;//so giay de thay doi speed
    public float stepSpeedMove = 0.001f;//toc do gia tang speed
    public float destanShowMore = 15f;//khoang cach them de destroy terrain da di qua
    //xu ly hieu ung avatar
    public GameObject effectAvatar;
    public GameObject containEffectAvatar;
    private CreatePoolTerrains poolTerrains;

    public void Start()
    {
        poolTerrains = Modules.poolTerrains.GetComponent<CreatePoolTerrains>();
        originMaxShowFront = maxShowTerrainFront;
        originMaxShowBack = maxShowTerrainBack;
    }

    //khi xep map nho chu y khong nen su dung cac map khong ho tro Menu de lam random (thang khac goi den hoac goi den thang khac)
    public Vector4 GetPointRunTerrain()
    {
        Vector4 result = Vector4.zero;
        if (listIndexScenesNormal.Contains(indexScenesNow))
        {
            result.x = indexScenesNow;
            bool terSupport = true;
            result.y = runCodeTerrain - maxShowTerrainFront;
            result.z = 0;
            result.w = Modules.mainCharacter.GetComponent<HeroController>().GetGroundNear();
            listCodeTerrain = "";
            if (result.y < 0)
            {
                if (Modules.gameGuide == "YES")//neu dang huong dan
                    result.y = maxShowTerrainFront + result.y;
                else
                    result.y = poolTerrains.listUseTerrain[indexScenesNow].listTerrain.Count + result.y;
            }
            if (listShowTerrain.Count > 1)
            {
                int indexTemp = Mathf.RoundToInt(result.y);
                if (!listShowTerrain[0].GetComponent<TerrainInformation>().supportMenu
                    || (!listShowTerrain[1].GetComponent<TerrainInformation>().supportMenu
                    && listShowTerrain[1].transform.position.z < listShowTerrain[1].GetComponent<TerrainInformation>().lengthTerrain / 2f + destanShowMore))
                {
                    result.y = -1;
                    result.w = 0;
                    terSupport = false;
                    if (listShowTerrain[0].GetComponent<TerrainInformation>().supportMenu)
                    {
                        result.y = indexTemp;
                        GetListCodeTerrainNow();
                    }
                    else
                    {
                        for (int i = indexTemp - 1; i >= 0; i--)
                        {
                            if (poolTerrains.listUseTerrain[indexScenesNow].listTerrain[i].GetComponent<TerrainInformation>().supportMenu)
                            {
                                result.y = i;
                                break;
                            }
                        }
                    }
                }
            }
            if (terSupport && listShowTerrain.Count > 0)
            {
                result.z = listShowTerrain[0].transform.position.z;
                GetListCodeTerrainNow();
            }
        }
        return result;
    }

    void GetListCodeTerrainNow()
    {
        for (int i = 0; i < listShowTerrain.Count; i++)
        {
            listCodeTerrain += listShowTerrain[i].GetComponent<TerrainInformation>().codeTerrain;
            if (i < listShowTerrain.Count - 1) listCodeTerrain += ";";
        }
    }

    public void ResetTerrain()
    {
        CancelInvoke("UpdateSpeedBack");
        CancelInvoke("UpdateSpeedMap");
        oldIndexScenesNow = -1;
        Modules.panelFakeCity.SetActive(true);
        maxShowTerrainFront = originMaxShowFront;
        maxShowTerrainBack = originMaxShowBack;
        indexScenesNow = Modules.indexTypeTerrain;
        runCodeTerrain = Modules.indexRunTerrain;
        pointCodeTerrain = Modules.pointRunTerrain;
        listCodeTerrain = Modules.listCodeTerrain;
        runFront = 1;
        foreach (GameObject go in listShowTerrain)
            go.SetActive(false);
        listShowTerrain = new List<GameObject>();
        if (listShowTerrain.Count < maxShowTerrainFront)
        {
            for (int i = listShowTerrain.Count; i < maxShowTerrainFront; i++)
                AddNewTerrain(i == 0 ? false : true);
        }
        Invoke("UpdateSpeedMap", timeChangeSpeed);
    }

    public void SetNewScene(int indexScene)//neu nho hoac lon hon range thi random
    {
        Modules.panelFakeCity.SetActive(true);
        maxShowTerrainFront = originMaxShowFront;
        maxShowTerrainBack = originMaxShowBack;
        runFront = 1;
        //xoa het cac terrain cu
        foreach (GameObject go in listShowTerrain)
            go.SetActive(false);
        listShowTerrain = new List<GameObject>();
        int oldIndexScene = indexScenesNow;
        if (oldIndexScenesNow != -1)
        {
            indexScenesNow = oldIndexScenesNow;
            oldIndexScenesNow = -1;
        }
        else indexScenesNow = indexScene;
        if (indexScenesNow < 0)
        {
            int newMap = oldIndexScene;
            while (newMap == oldIndexScene)
                newMap = listIndexScenesNormal[Random.Range(0, listIndexScenesNormal.Count)];
            indexScenesNow = newMap;
        }
        pointCodeTerrain = 0;
        listCodeTerrain = "";
        //thuc hien create terrain moi
        for (int i = 0; i < maxShowTerrainFront; i++)
            AddNewTerrain(true);
    }

    private float oldSpeedGame = 1f;//luu lai speed game
    private int oldIndexScenesNow = -1;
    public void SetBonusScene()
    {
        Modules.panelFakeCity.SetActive(false);
        maxShowTerrainFront = originMaxShowFront + 1;
        maxShowTerrainBack = originMaxShowBack + 1;
        oldSpeedGame = Modules.speedGame;
        Modules.speedGame = 0;
        runFront = -1;
        //xoa het cac terrain cu
        foreach (GameObject go in listShowTerrain)
            go.SetActive(false);
        listShowTerrain = new List<GameObject>();
        oldIndexScenesNow = indexScenesNow;
        int indexRan = Random.Range(0, listIndexScenesBonus.Count);
        indexScenesNow = listIndexScenesBonus[indexRan];
        runCodeTerrain = poolTerrains.listSceneTerrain[indexScenesNow].listTerrain.Count - 1;
        pointCodeTerrain = 0;
        listCodeTerrain = "";
        //thuc hien create terrain moi
        for (int i = 0; i < maxShowTerrainBack; i++)
            AddNewTerrain(true);
        //tang toc do speed chay ve dau
        Invoke("UpdateSpeedBack", Modules.timeShowChest);
    }

    void UpdateSpeedBack()
    {
        Modules.speedGame += 0.75f;
        if (Modules.speedGame < 10f)
            Invoke("UpdateSpeedBack", 0.25f);
    }

    void UpdateSpeedMap()
    {
        if (Modules.statusGame != StatusGame.play)
        {
            Invoke("UpdateSpeedMap", timeChangeSpeed);
            return;
        }
        //xu ly gia tang speed
        if (Modules.speedGame > Modules.maxSpeedGame)
            Modules.speedGame = Modules.maxSpeedGame;
        else
        {
            Modules.speedGame += stepSpeedMove;
            Invoke("UpdateSpeedMap", timeChangeSpeed);
        }
    }

    IEnumerator LoadImage(string url)
    {
        if (Time.time - oldTimeLoad < 0.2f) yield break;
        oldTimeLoad = Time.time;
        WWW www = new WWW(url);
        while (!www.isDone && string.IsNullOrEmpty(www.error))
            yield return new WaitForSeconds(0.1f);
        if (string.IsNullOrEmpty(www.error) && url != "Null" && www.texture != null)
        {
            int width = www.texture.width;
            int height = www.texture.height;
            if (width > 128) width = 128;
            if (height > 128) height = 128;
            Transform tranAvatar = Modules.panelHighScoreNow.transform.Find("Avatar");
            Image fbAvatar = tranAvatar.GetComponent<Image>();
            fbAvatar.sprite = Sprite.Create(www.texture, new Rect(0, 0, width, height), new Vector2(0, 0));
        }
        www.Dispose();
        //yield return Resources.UnloadUnusedAssets();
        yield break;
    }

    //private IEnumerator coroutine;
    //void OnDisable()
    //{
    //    if (coroutine != null)
    //        StopCoroutine(coroutine);
    //}

    private float pointFirst = 0;
    private float distanEffect = 3f;
    private float speedEffect = 0.25f;

    void LateUpdate()
    {
        if (Modules.statusGame == StatusGame.over && !Modules.runGameOverEffect)
        {
            if (!Modules.getPointFirst)
            {
                Modules.getPointFirst = true;
                pointFirst = listShowTerrain[0].transform.position.z;
            }
            foreach (GameObject go in listShowTerrain)
                go.transform.Translate(Vector3.forward * speedEffect);
            if (listShowTerrain[0].transform.position.z > pointFirst + distanEffect)//ket thuc hieu ung
                Modules.runGameOverEffect = true;
        }
        if (Modules.statusGame != StatusGame.play && Modules.statusGame != StatusGame.bonusEffect) return;
        if (Modules.panelBGEffectBonus.activeSelf && !Modules.runAffterDownBonus) return;
        if (listShowTerrain.Count <= 0) return;
        //xu ly terrain
        //float deltaTime = Time.deltaTime;
        //if (deltaTime > Time.fixedDeltaTime) deltaTime = Time.fixedDeltaTime;
        float speedNow = numEditSpeed * Modules.speedGame * Modules.speedAddMoreUse * Time.deltaTime;// deltaTime;
        foreach (GameObject go in listShowTerrain)
            go.transform.Translate(Vector3.back * runFront * speedNow);
        bool checkRun = false;
        if (runFront > 0)
        {
            if (listShowTerrain[0].transform.position.z <= -listShowTerrain[0].GetComponent<TerrainInformation>().lengthTerrain / 2f - destanShowMore)
                checkRun = true;
        }
        else
        {
            if (listShowTerrain[listShowTerrain.Count - 1].transform.position.z >= -destanShowMore)
                checkRun = true;
        }
        if (checkRun)
        {
            listShowTerrain[0].SetActive(false);
            listShowTerrain.RemoveAt(0);
            AddNewTerrain(true);
        }
        //xu ly cong diem theo toc do chay cua map
        if (Modules.gameGuide == "YES" || runFront < 0) return;//neu dang huong dan hoac dang chay lui thi khong cong diem ky luc
        Modules.scorePlayer += pointBySpeed * Modules.xPointPlayer * speedNow;
        string scoreShow = Mathf.RoundToInt(Modules.scorePlayer).ToString();
        string numberZero = "";
        for (int i = scoreShow.Length; i < 7; i++) numberZero += "0";
        Modules.textScorePlay.text = numberZero + scoreShow;
        //xu ly chay lui diem ky luc hien tai
        if (!Modules.panelHighScoreNow.activeSelf) return;
        if (Modules.fbHighScore.Count > 0)
        {
            if (Modules.scorePlayer >= Modules.totalScoreNow)
            {
                Transform tranAvatar = Modules.panelHighScoreNow.transform.Find("Avatar");
                Transform tranName = Modules.panelHighScoreNow.transform.Find("TextHighScore");
                Image fbAvatar = tranAvatar.GetComponent<Image>();
                Text fbName = tranName.GetComponent<Text>();
                CreateEffectAvatar(fbAvatar.sprite, fbName.text);
                if (Modules.fbHighScore.Count > 1)
                {
                    fbAvatar.sprite = Modules.iconAvatarNull;
                    StartCoroutine(LoadImage(Modules.fbAvatarEnemy[Modules.fbAvatarEnemy.Count - 2]));
                    fbName.text = Modules.fbNameEnemy[Modules.fbNameEnemy.Count - 2].ToUpper();
                    Modules.totalScoreNow = Modules.fbHighScore[Modules.fbHighScore.Count - 2];
                }
                else
                {
                    Modules.panelHighScoreNow.SetActive(false);
                }
                Modules.fbAvatarEnemy.RemoveAt(Modules.fbAvatarEnemy.Count - 1);
                Modules.fbNameEnemy.RemoveAt(Modules.fbNameEnemy.Count - 1);
                Modules.fbHighScore.RemoveAt(Modules.fbHighScore.Count - 1);
            }
            else
            {
                Modules.textHighScore.text = Mathf.RoundToInt(Modules.totalScoreNow - Modules.scorePlayer).ToString();
            }
        }
        else
        {
            if (Modules.scorePlayer >= Modules.totalScoreNow)
            {
                Transform tranAvatar = Modules.panelHighScoreNow.transform.Find("Avatar");
                Image fbAvatar = tranAvatar.GetComponent<Image>();
                CreateEffectAvatar(fbAvatar.sprite, AllLanguages.playHighScore[Modules.indexLanguage]);
                Modules.panelHighScoreNow.SetActive(false);
            }
            else
            {
                Modules.textHighScore.text = Mathf.RoundToInt(Modules.totalScoreNow - Modules.scorePlayer).ToString();
            }
        }
    }

    private float oldTimeEffect = 0;
    private float oldTimeLoad = 0;
    void CreateEffectAvatar(Sprite newSprite, string newName)
    {
        if (Time.time - oldTimeEffect < 0.2f) return;
        oldTimeEffect = Time.time;
        GameObject effectA = Instantiate(effectAvatar, Vector3.zero, Quaternion.identity) as GameObject;
        effectA.transform.SetParent(containEffectAvatar.transform, false);
        Transform tranAE = effectA.transform.Find("Avatar");
        Transform tranNE = effectA.transform.Find("TextHighScore");
        Image fbAE = tranAE.GetComponent<Image>();
        Text fbNE = tranNE.GetComponent<Text>();
        fbAE.sprite = newSprite;
        fbNE.text = newName;
        Destroy(effectA, 1f);
    }

    void AddNewTerrain(bool handleSafe)
    {
        if (runCodeTerrain >= poolTerrains.listSceneTerrain[indexScenesNow].listTerrain.Count)
        {
            if (Modules.useBonus) return;
            else runCodeTerrain = 0;
        }
        else if (runCodeTerrain < 0)
        {
            runCodeTerrain = listShowTerrain.Count;
            listShowTerrain.Reverse();
            CancelInvoke("UpdateSpeedBack");
            Modules.speedGame = oldSpeedGame;
            Modules.startBonusRoad = true;
            //xu ly hieu ung UI va toc do o day
            runFront = 1;
            AddNewTerrain(handleSafe);
            Modules.ShowMessageBonusRoad(2);
            return;
        }
        if (Modules.gameGuide == "YES")//neu dang huong dan
            if (runCodeTerrain >= maxShowTerrainFront) runCodeTerrain = 0;
        //thuc hien them moi terrain
        float pointZCreate = pointCodeTerrain;
        int indexType = indexScenesNow;
        int indexTerr = runCodeTerrain;
        if (runFront > 0)//chi thuc hien random terrain khi chay xuoi duong
        {
            int index = -1;
            List<string> codeTerRan = poolTerrains.listUseTerrain[indexType].listTerrain[indexTerr].GetComponent<TerrainInformation>().listTerrainRan;
            if (listCodeTerrain != "")//chi dinh index neu nhu co list code save
            {
                string[] codeTer = listCodeTerrain.Split(';');
                if (codeTerRan != null && codeTerRan.Count > 0)
                {
                    int indexTemp = codeTerRan.IndexOf(codeTer[0]);
                    if (indexTemp >= 0 && indexTemp < codeTerRan.Count) index = indexTemp;
                }
                listCodeTerrain = "";
                for (int i = 1; i < codeTer.Length; i++)
                {
                    listCodeTerrain += codeTer[i];
                    if (i < codeTer.Length - 1) listCodeTerrain += ";";
                }
            }
            else//thuc hien ngau nhien dia hinh
            {
                int ran = Random.Range(0, 100);
                if (ran < poolTerrains.listUseTerrain[indexType].listTerrain[indexTerr].GetComponent<TerrainInformation>().percentRan)
                {
                    if (codeTerRan != null && codeTerRan.Count > 0)
                        index = Random.Range(0, codeTerRan.Count);
                }
            }
            if (codeTerRan != null && codeTerRan.Count > 0 && index > -1)
            {
                bool findStart = false;
                for (int i = 0; i < indexTerr; i++)
                {
                    if (poolTerrains.listUseTerrain[indexType].listTerrain[i].GetComponent<TerrainInformation>().codeTerrain == codeTerRan[index])
                    {
                        if (!poolTerrains.listUseTerrain[indexType].listTerrain[i].activeSelf)
                        {
                            indexTerr = i;
                            findStart = true;
                            break;
                        }
                    }
                }
                if (!findStart)
                {
                    for (int i = indexTerr + maxShowTerrainFront; i < poolTerrains.listUseTerrain[indexType].listTerrain.Count; i++)
                    {
                        if (poolTerrains.listUseTerrain[indexType].listTerrain[i].GetComponent<TerrainInformation>().codeTerrain == codeTerRan[index])
                        {
                            if (!poolTerrains.listUseTerrain[indexType].listTerrain[i].activeSelf)
                            {
                                indexTerr = i;
                                break;
                            }
                        }
                    }
                }
            }
        }
        GameObject terrainNew = poolTerrains.listUseTerrain[indexType].listTerrain[indexTerr];
        if (listShowTerrain.Count > 0)
        {
            pointZCreate = listShowTerrain[listShowTerrain.Count - 1].transform.position.z
               + runFront * 0.5f * listShowTerrain[listShowTerrain.Count - 1].GetComponent<TerrainInformation>().lengthTerrain
               + runFront * 0.5f * terrainNew.GetComponent<TerrainInformation>().lengthTerrain;
        }
        terrainNew.transform.position = new Vector3(0, 0, pointZCreate);
        terrainNew.SetActive(true);
        terrainNew.GetComponent<TerrainInformation>().Restart(handleSafe);
        listShowTerrain.Add(terrainNew);
        if (runFront > 0) runCodeTerrain++;
        else runCodeTerrain--;
    }
}
