using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using Facebook.MiniJSON;

public class FacebookController : MonoBehaviour {

    public GameObject panelListFriend;//mau panel nhap vao
    [HideInInspector]
    public GameObject panelGetInfo;//panel duoc lay thong tin facebook
    [HideInInspector]
    public bool isPostDone = false;
    [HideInInspector]
    public bool isGetDone = false;
    [HideInInspector]
    public bool getEnemy = true;

    void Awake()
    {
#if !(UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
        return;
#endif
        DontDestroyOnLoad(gameObject);
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            SetInit();
        }
        else
        {
            //Handle FB.Init
            FB.Init(() =>
           {
               FB.ActivateApp();
               SetInit();
           });
        }
        //FB.Init(SetInit, OnHideUnity);
        Modules.facebookController = transform.gameObject;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() =>
                {
                    FB.ActivateApp();
                });
            }
        }
    }

    void SetInit()
    {
        DealWithFBMenus(FB.IsLoggedIn);
    }

    //void OnHideUnity(bool isGameShown)
    //{
    //    if (!isGameShown)
    //    {
    //        Time.timeScale = 0;
    //    }
    //    else
    //    {
    //        Time.timeScale = 1;
    //    }
    //}

    public void FBlogin()
    {
        List<string> perms = new List<string>() { "user_friends", "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, LoginReadCallback);
    }

    void LoginReadCallback(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB is logged in");
                List<string> perms = new List<string>() { "publish_actions" };
                FB.LogInWithPublishPermissions(perms, LoginPublishCallback);
                if (Modules.bonusFacebook == "No")//thuc hien thuong
                {
                    Modules.totalCoin += 10000;
                    Modules.SaveCoin();
                    if (Modules.containAchievement.activeSelf)
                        Camera.main.GetComponent<PageAchievement>().textCoin.text = Modules.totalCoin.ToString();
                    Modules.bonusFacebook = "Yes";
                    Modules.SaveBonusFacebook();
                }
            }
            else
            {
                Debug.Log("FB is not logged in");
            }
            DealWithFBMenus(FB.IsLoggedIn);
        }
    }

    void LoginPublishCallback(ILoginResult result)
    {
        //You also granted the asked publish_actions permission.
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)//dang nhap thanh cong
        {
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
            Modules.fbID = AccessToken.CurrentAccessToken.UserId;
        }
        else
        {
            //dang nhap that bai
        }
    }

    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
           Modules.fbName = result.ResultDictionary["first_name"].ToString();
           //print(Modules.fbName);
           //Modules.textDebug.text += "\nName[Okay]: " + Modules.fbName;
        }
        else
        {
            Debug.Log(result.Error);
            //Modules.textDebug.text += "\nName[Error]: " + Modules.fbName;
        }
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            Modules.fbMyAvatar = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
            Modules.fbLinkAvatar = "https" + "://graph.facebook.com/" + AccessToken.CurrentAccessToken.UserId.ToString() + "/picture?g&width=128&height=128";
            print(Modules.fbLinkAvatar);
        }
    }

    public void ShareWithFriends()
    {
        FB.FeedShare("", null, "Name", "CAP", "DES", null, "", null);
    }

    public void InviteFriends()
    {
        FB.AppRequest(
            message: "This game is awesome, join me. now.",
            title: "Invite your friends to join you"
            );
    }

    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

        var width = Screen.width;
        var height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        byte[] screenshot = tex.EncodeToPNG();

        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", screenshot, "Screenshot.png");

        FB.API("me/photos", HttpMethod.POST, OnPostScreenShot, wwwForm);
    }

    private void OnPostScreenShot(IGraphResult result)
    {
        print("Post image OK");
    }

    private bool checkScore = true;
    private int oldScore = 0;
    private bool postNewScore = true;
    public void PostScore(bool check)
    {
        checkScore = check;
        if (checkScore)
        {
            GetScores();
        }
        else
        {

            var scoreData = new Dictionary<string, string>();
            int scorePost = oldScore;
            if (postNewScore) scorePost = Mathf.RoundToInt(Modules.totalScore);
            scoreData["score"] = scorePost.ToString();
            FB.API("/me/scores", HttpMethod.POST, OnPostScore, scoreData);
        }
    }

    private void OnPostScore(IGraphResult result)
    {
        //Modules.textDebug.text += "\nPOST: " + result.RawResult;
        print("Post score " + result.RawResult);
        isPostDone = true;
    }

    public void GetScores()
    {
        string strGetScore = "/app/scores?fields=score,user.limit(30)";
        if (checkScore) strGetScore = "/" + AccessToken.CurrentAccessToken.UserId.ToString() + "/scores";
        FB.API(strGetScore, HttpMethod.GET, OnGetScore);
    }

    private List<object> scoresList = null;
    private void OnGetScore(IGraphResult result)
    {
        //khoi tao lai list doi thu score
        if (getEnemy && Modules.countryEnemy != 1 && !checkScore)
        {
            Modules.fbAvatarEnemy = new List<string>();
            Modules.fbNameEnemy = new List<string>();
            Modules.fbHighScore = new List<float>();
        }
        //thuc hien khac
        scoresList = DeserializeScores(result.RawResult);
        if (panelGetInfo != null) Destroy(panelGetInfo);
        panelGetInfo = Instantiate(panelListFriend, Vector3.zero, Quaternion.identity) as GameObject;
        Transform panelContent = panelGetInfo.transform.Find("Content");
        Transform panelItem = panelContent.transform.Find("Item");
        int index = 0;
        //Modules.textDebug.text += "\nGET: " + result.RawResult;
        //Modules.textDebug.text += "\nGET: ScoresList: " + scoresList.Count;
        foreach (object score in scoresList)
        {
            var entry = (Dictionary<string, object>)score;
            var user = (Dictionary<string, object>)entry["user"];
            GameObject newItem = panelItem.gameObject;
            if (index > 0)
            {
                newItem = Instantiate(panelItem.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
                newItem.transform.SetParent(panelContent, false);
            }
            if (index % 2 != 0) newItem.GetComponent<Image>().color = Modules.colorListLine;
            Transform tranAvatar = newItem.transform.Find("Avatar");
            Transform tranName = newItem.transform.Find("Name");
            Transform tranScore = newItem.transform.Find("Score");
            Transform tranIndex = newItem.transform.Find("Index");

            Image fbAvatar = tranAvatar.GetComponent<Image>();
            Text fbName = tranName.GetComponent<Text>();
            Text fbScore = tranScore.GetComponent<Text>();
            Text fbIndex = tranIndex.GetComponent<Text>();

            fbName.text = user["name"].ToString().Split(' ')[0];
            int scoreNow = Modules.IntParseFast(entry["score"].ToString());
            if (getEnemy && Modules.countryEnemy != 1 && !checkScore && scoreNow >= Modules.totalScore)
            {
                Modules.fbNameEnemy.Add(fbName.text);
                Modules.fbHighScore.Add(scoreNow);
                Modules.fbAvatarEnemy.Add("https" + "://graph.facebook.com/" + user["id"] + "/picture?g&width=128&height=128");
                //Modules.textDebug.text += "\nADD: " + fbName.text;
            }
            fbScore.text = scoreNow.ToString();
            if (checkScore) oldScore = scoreNow;
            fbIndex.text = (index + 1).ToString();

            FB.API(GetPictureURL(user["id"].ToString(), 128, 128), HttpMethod.GET, delegate(IGraphResult pictureResult)
            {
                if (pictureResult.Error != null) // if there was an error
                {
                    Debug.Log(pictureResult.Error);
                }
                else // if everything was fine
                {
                    if (fbAvatar) fbAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect(0, 0, 128, 128), new Vector2(0, 0));
                }
            });
            index++;
        }
        if (checkScore)
        {
            if (oldScore >= Modules.totalScore)
                postNewScore = false;
            else postNewScore = true;
            PostScore(false);
        }
        else
        {
            isGetDone = true;
            getEnemy = false;
        }
    }

    public static List<object> DeserializeScores(string response)
    {

        var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
        object scoresh;
        var scores = new List<object>();
        if (responseObject.TryGetValue("data", out scoresh))
        {
            scores = (List<object>)scoresh;
        }

        return scores;
    }

    public static string GetPictureURL(string facebookID, int? width = null, int? height = null, string type = null)
    {
        string url = string.Format("/{0}/picture", facebookID);
        string query = width != null ? "&width=" + width.ToString() : "";
        query += height != null ? "&height=" + height.ToString() : "";
        query += type != null ? "&type=" + type : "";
        if (query != "") url += ("?g" + query);
        return url;
    }
}
