using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class LoadListEnemy : MonoBehaviour {

    private FacebookController fbController;
    private bool statusPost = false;
    public void CallStart()
    {
        statusPost = false;
        Modules.fbAvatarEnemy = new List<string>();
        Modules.fbNameEnemy = new List<string>();
        Modules.fbHighScore = new List<float>();
        if (Modules.countryEnemy == 1)
        {
            StartCoroutine(PostScore());
            Invoke("PostScoreWorld", 0f);
        }
        else
        {
#if (UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
            if (fbController == null) fbController = Modules.facebookController.GetComponent<FacebookController>();
            fbController.isPostDone = false;
            fbController.isGetDone = false;
            fbController.getEnemy = true;
            Invoke("PostScoreFBNew", 0f);
            Invoke("GetScoreFBNew", 0f);
#endif
        }
    }

    void PostScoreFBNew()
    {
        if (FB.IsLoggedIn)
        {
            fbController.PostScore(true);
        }
        else Invoke("PostScoreFBNew", 1f);
    }

    void GetScoreFBNew()
    {
        if (fbController.isPostDone)
        {
            fbController.GetScores();
            fbController.isPostDone = false;
        }
        else Invoke("GetScoreFBNew", 1f);
    }

    void PostScoreWorld()
    {
        if (statusPost)
        {
            StartCoroutine(GetScoreCountry());
        }
        else Invoke("PostScoreWorld", 1f);
    }

    IEnumerator PostScore()
    {
#if UNITY_WEBGL
        string idDevice = Modules.fbID;
#else
        string idDevice = SystemInfo.deviceUniqueIdentifier;
#endif
        if (idDevice == "Null")
        {
            statusPost = true;
            yield break;
        }
        //string nameDevice = SystemInfo.deviceName;
        string dataCountry = SaveLoadData.LoadData("CodeCountry", true);
        if (dataCountry == "") dataCountry = "Null";
        WWWForm form = new WWWForm();
        form.AddField("table", "useBusSubway");
        form.AddField("idUser", idDevice);
        form.AddField("name", Modules.fbName);
        form.AddField("avatar", Modules.fbLinkAvatar == "" ? "Null" : Modules.fbLinkAvatar);
        form.AddField("score", Mathf.RoundToInt(Modules.totalScore));
        form.AddField("country", dataCountry);
        form.AddField("win", Modules.winNow);
        form.AddField("lose", Modules.loseNow);
        form.AddField("fail", Modules.failNow);
        WWW _resuilt = new WWW(Modules.linkPost, form);
        float runTime = 0f;
        while (!_resuilt.isDone && runTime < Modules.maxTime)
        {
            runTime += Modules.requestTime;
            yield return new WaitForSeconds(Modules.requestTime);
        }
        yield return _resuilt;
        if (_resuilt.text == "Done")
        { //hoan thanh
            statusPost = true;
            Modules.winNow = 0;
            Modules.loseNow = 0;
            Modules.failNow = 0;
        }
        else
        { //qua lau, khong mang, cau lenh loi
            statusPost = false;
        }
        yield break;
    }

    IEnumerator GetScoreCountry()
    {
        string dataCountry = SaveLoadData.LoadData("CodeCountry", true);
        if (dataCountry == "") dataCountry = "Null";
        WWWForm form = new WWWForm();
        form.AddField("table", "useBusSubway");
        form.AddField("limit", "30");
        form.AddField("country", dataCountry);
        WWW _resuilt = new WWW(Modules.linkGetCountry, form);
        float runTime = 0f;
        while (!_resuilt.isDone && runTime < Modules.maxTime)
        {
            runTime += Modules.requestTime;
            yield return new WaitForSeconds(Modules.requestTime);
        }
        yield return _resuilt;
        if (_resuilt.text != "null" && _resuilt.text != "")
        { //hoan thanh
            //print(_resuilt.text);
            string[] dataLine = _resuilt.text.Split('\n');
            int dRun = 0;
            for (int i = 0; i < dataLine.Length; i++)
            {
                if (dataLine[i] == "") continue;
                string[] data = dataLine[i].Split(';');
                int scoreNow = int.Parse(data[2]);
                if (scoreNow >= Modules.totalScore)
                {
                    Modules.fbNameEnemy.Add(data[0]);
                    Modules.fbHighScore.Add(scoreNow);
                    Modules.fbAvatarEnemy.Add(data[1]);
                    //print(data[0] + "=>" + scoreNow.ToString() + ";" + data[1]);
                    dRun++;
                }
            }
            //statusGet = true;
        }
        else
        { //qua lau, khong mang, cau lenh loi
            //statusGet = false;
        }
        yield break;
    }
}
