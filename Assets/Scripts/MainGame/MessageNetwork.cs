using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MessageNetwork : MonoBehaviour {

    public GameObject contentFullA, contentFullB;
    public GameObject loadingTextA, loadingTextB;
    public InputField inputNamePlay;
    public InputField inputNameRoom;
    public GameObject contentListRoom;
    public GameObject quickMessageBox;
    public Text textContentMessage;
    private Color originColorPlay;
    private Color originColorRoom;
    //xu ly ranking
    public GameObject panelNetwork, panelRanking;

    public void CallStart()
    {
        inputNamePlay.text = Modules.namePlayOnline;
        inputNameRoom.text = Modules.nameRoomOnline;
        originColorPlay = inputNamePlay.placeholder.color;
        originColorRoom = inputNameRoom.placeholder.color;
        contentFullA.SetActive(false);
        loadingTextA.SetActive(true);
        loadingTextA.GetComponent<TextLoading>().CallStart();
        InvokeRepeating("UpdateRooms", 0, 3);
    }

    void UpdateRooms()
    {
        if (!PhotonNetwork.connected) return;
        if (loadingTextA.activeSelf)
        {
            contentFullA.SetActive(true);
            loadingTextA.SetActive(false);
        }
        if (PhotonNetwork.inRoom) return;
        Transform itemTemp = null;
        foreach (Transform tran in contentListRoom.transform)
        {
            if (tran.name != "Item") Destroy(tran.gameObject);
            else { itemTemp = tran; itemTemp.gameObject.SetActive(true); }
        }
        int maxRoom = PhotonNetwork.GetRoomList().Length;
        if (maxRoom > 10) maxRoom = 10;
        for (int i = 0; i < maxRoom; i++)
        {
            Transform itemNow = Instantiate(itemTemp, contentListRoom.transform) as Transform;
            Transform tranMember = itemNow.transform.Find("Member");
            Transform tranName = itemNow.transform.Find("Name");
            Text textMember = tranMember.GetComponent<Text>();
            Text textName = tranName.GetComponent<Text>();
            textMember.text = PhotonNetwork.GetRoomList()[i].PlayerCount + "/" + PhotonNetwork.GetRoomList()[i].MaxPlayers;
            textName.text = PhotonNetwork.GetRoomList()[i].Name;
            ButtonRoom butRoom = itemNow.GetComponent<ButtonRoom>();
            butRoom.nameRoom = textName.text;
        }
        itemTemp.gameObject.SetActive(false);
    }

    void ReturnColorPlay()
    {
        inputNamePlay.placeholder.color = originColorPlay;
    }

    void ReturnColorRoom()
    {
        inputNameRoom.placeholder.color = originColorRoom;
    }

    void HideQuickMessage()
    {
        quickMessageBox.SetActive(false);
    }

    void ShowQuickMessage(string messageInput, float timeHide)
    {
        CancelInvoke("HideQuickMessage");
        quickMessageBox.SetActive(true);
        textContentMessage.text = messageInput;
        Invoke("HideQuickMessage", timeHide);
    }

    bool CheckAndSavePlay()
    {
        string nameTemp = inputNamePlay.text.Trim();
        if (nameTemp == "")
        {
            inputNamePlay.placeholder.color = Color.red;
            Invoke("ReturnColorPlay", 1f);
            return false;
        }
        Modules.namePlayOnline = nameTemp;
        Modules.SaveNetworkNamePlay();
        return true;
    }

    bool CheckAndSaveRoom()
    {
        string nameTemp = inputNameRoom.text.Trim();
        if (nameTemp == "")
        {
            inputNameRoom.placeholder.color = Color.red;
            Invoke("ReturnColorRoom", 1f);
            return false;
        }
        Modules.nameRoomOnline = nameTemp;
        Modules.SaveNetworkNameRoom();
        return true;
    }

    public void ButtonCreateRoom()
    {
        if (!CheckAndSavePlay()) return;
        if (!CheckAndSaveRoom()) return;
        if (!PhotonNetwork.CreateRoom(inputNameRoom.text, new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default))
            ShowQuickMessage("Room name already exists, please enter another name!", 1f);
    }

    public void ButtonJoinRandom()
    {
        if (!CheckAndSavePlay()) return;
        if (PhotonNetwork.JoinRandomRoom())
            ShowQuickMessage("No room is found at this time, please create a new room!", 1f);
    }

    public void ButtonJoinSelect(string nameRoom)
    {
        if (!CheckAndSavePlay()) return;
        if (PhotonNetwork.JoinRoom(nameRoom))
            ShowQuickMessage("The race was started in this room, please choose another room!", 1f);
    }

    public void ButtonCloseBox()
    {
        CancelInvoke("UpdateRooms");
        CancelInvoke("HideQuickMessage");
        quickMessageBox.SetActive(false);
        transform.GetComponent<Animator>().SetTrigger("TriClose");
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonNetwork()
    {
        panelNetwork.SetActive(true);
        panelRanking.SetActive(false);
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonRanking()
    {
        panelNetwork.SetActive(false);
        panelRanking.SetActive(true);
        contentFullB.SetActive(false);
        loadingTextB.SetActive(true);
        loadingTextB.GetComponent<TextLoading>().CallStart();
        StartLoadRanking();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    //XU LY POST/GET DU LIEU XEP HANG
    public GameObject listTempMultiPlayer;//prefab mau list player score
    public Vector3 pointListMultiPlayer = Vector3.zero;
    private List<Texture2D> listAvatarMultiPlayer = new List<Texture2D>();
    private GameObject panelTopMultiPlayer;//list hien thi xep hang MultiPlayer
    private bool statusPost = false;
    private void StartLoadRanking()
    {
        statusPost = false;
        StartCoroutine(PostScore());
        Invoke("PostDataMultiplayer", 0f);
    }

    void PostDataMultiplayer()
    {
        if (statusPost)
        {
            StartCoroutine(GetDataMultiplayer());
        }
        else Invoke("PostDataMultiplayer", 1f);
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

    IEnumerator GetDataMultiplayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("table", "useBusSubway");
        form.AddField("limit", "30");
        WWW _resuilt = new WWW(Modules.linkGetDataMultiplayer, form);
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
            listAvatarMultiPlayer = new List<Texture2D>();
            string[] dataLine = _resuilt.text.Split('\n');
            if (panelTopMultiPlayer != null) Destroy(panelTopMultiPlayer);
            panelTopMultiPlayer = Instantiate(listTempMultiPlayer, Vector3.zero, Quaternion.identity) as GameObject;
            Transform panelContent = panelTopMultiPlayer.transform.Find("Content");
            Transform panelItem = panelContent.transform.Find("Item");
            for (int i = 0; i < dataLine.Length; i++)
            {
                if (dataLine[i] == "") continue;
                GameObject newItem = panelItem.gameObject;
                if (i > 0)
                {
                    newItem = Instantiate(panelItem.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
                    newItem.transform.SetParent(panelContent, false);
                }
                if (i % 2 != 0) newItem.GetComponent<Image>().color = Modules.colorListLine;
                Transform tranAvatar = newItem.transform.Find("Avatar");
                Transform tranName = newItem.transform.Find("Name");
                Transform tranScore = newItem.transform.Find("Score");
                Transform tranIndex = newItem.transform.Find("Index");

                Image fbAvatar = tranAvatar.GetComponent<Image>();
                Text fbName = tranName.GetComponent<Text>();
                Text fbScore = tranScore.GetComponent<Text>();
                Text fbIndex = tranIndex.GetComponent<Text>();

                string[] data = dataLine[i].Split(';');
                fbName.text = data[0];
                listAvatarMultiPlayer.Add(null);
                if (Modules.containAchievement.activeSelf) StartCoroutine(LoadImageMultiplayer(data[1], i, fbAvatar));
                fbScore.text = data[2];
                fbIndex.text = (i + 1).ToString();
            }
            panelTopMultiPlayer.transform.position = pointListMultiPlayer;
            panelTopMultiPlayer.transform.SetParent(contentFullB.transform, false);
            loadingTextB.SetActive(false);
            contentFullB.SetActive(true);
            //statusGet = true;
        }
        else
        { //qua lau, khong mang, cau lenh loi
            //statusGet = false;
        }
        yield break;
    }

    IEnumerator LoadImageMultiplayer(string url, int index, Image avatar)
    {
        WWW www = new WWW(url);
        while (!www.isDone && string.IsNullOrEmpty(www.error))
            yield return new WaitForSeconds(0.1f);
        if (string.IsNullOrEmpty(www.error) && url != "Null" && www.texture != null && avatar != null)
        {
            listAvatarMultiPlayer[index] = www.texture;
            int width = listAvatarMultiPlayer[index].width;
            int height = listAvatarMultiPlayer[index].height;
            if (width > 128) width = 128;
            if (height > 128) height = 128;
            avatar.sprite = Sprite.Create(listAvatarMultiPlayer[index], new Rect(0, 0, width, height), new Vector2(0, 0));
        }
        www.Dispose();
        //yield return Resources.UnloadUnusedAssets();
        yield break;
    }
}
