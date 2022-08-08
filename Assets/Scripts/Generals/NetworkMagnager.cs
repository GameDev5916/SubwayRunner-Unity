using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NetworkMagnager : MonoBehaviour {

    //public GameObject messageNetworkBox;
    //public GameObject messageRoomBox;
    public GameObject messageFindBox;
    public Transform tranSyncServer, tranSyncClient;
    public Image imageShowLive;
    public Camera cameraRender;
    private GameObject syncWrite, syncRead;

	void Awake()
    {
        Modules.networkManager = transform.gameObject;
    }

    void Start()
    {
        if (transform.GetComponent<PhotonView>() == null)
        {
            PhotonView photonView = transform.gameObject.AddComponent<PhotonView>();
            photonView.viewID = 1;
            photonView.instantiationId = 1;
        }
        receiveImage = new Texture2D(128, 128, TextureFormat.RGB24, false);
    }

    public void KickPlayer()
    {
        transform.GetComponent<PhotonView>().RPC("SendKickPlayer", PhotonTargets.Others, Modules.listNamePlayer[1]);
    }

    //void CheckoutMyRoom()
    //{
    //    if (PhotonNetwork.otherPlayers.Length == 0)
    //        PhotonNetwork.LeaveRoom();
    //    else Invoke("CheckoutMyRoom", 0.1f);
    //}

    public void DisconectNetwork()
    {
        PhotonNetwork.Disconnect();
    }

    public void CancelRoom()
    {
        if (PhotonNetwork.inRoom) PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        if (!PhotonNetwork.isMasterClient) return;
        PhotonNetwork.room.IsOpen = false;
        syncWrite = PhotonNetwork.Instantiate(tranSyncServer.name, Vector3.zero, Quaternion.identity, 0);
        syncWrite.GetComponent<SendViewEnemyNew>().myImage = imageShowLive;
        syncWrite.GetComponent<SendViewEnemyNew>().isWriting = true;
        syncWrite.GetComponent<SendViewEnemyNew>().CallStart();
        transform.GetComponent<PhotonView>().RPC("SendSyncServer", PhotonTargets.Others);
    }

    public void SyncAvatar()
    {
        if (Modules.fbMyAvatar != null)
            transform.GetComponent<PhotonView>().RPC("SendSyncAvatar", PhotonTargets.Others, Modules.fbMyAvatar.texture.EncodeToJPG());
        else transform.GetComponent<PhotonView>().RPC("SendNullAvatar", PhotonTargets.Others);
    }

    void OnCreatedRoom()//tao room
    {
        if (messageFindBox.GetComponent<MessageFindOpponent>().isClose)
        {
            CancelRoom();
            return;
        }
        Modules.listNamePlayer = new List<string>();
        Modules.listNamePlayer.Add(Modules.namePlayOnline);
        //messageNetworkBox.GetComponent<MessageNetwork>().ButtonCloseBox();
        //messageRoomBox.SetActive(true);
        //messageRoomBox.GetComponent<Animator>().SetTrigger("TriOpen");
        //messageRoomBox.GetComponent<MessageRoom>().CallStart();
        print("cretate");
    }

    void OnJoinedRoom()//vao room
    {
        if (PhotonNetwork.isMasterClient) return;
        transform.GetComponent<PhotonView>().RPC("AskJoinRoom", PhotonTargets.MasterClient, Modules.namePlayOnline);
        print("join room");
    }

    void OnLeftRoom()//neu chinh minh tu thoat ra
    {
        ResetRoom();
        print("left room");
    }

    public void ResetRoom()
    {
        if (syncWrite != null) Destroy(syncWrite);
        if (syncRead != null) Destroy(syncRead);
        cameraRender.gameObject.SetActive(false);
        Modules.listNamePlayer = new List<string>();
        //if (messageRoomBox.activeSelf)
        //    messageRoomBox.GetComponent<Animator>().SetTrigger("TriClose");
        if (Modules.startViewOnline && Modules.panelViewEnemy.activeSelf)
            Modules.panelViewEnemy.GetComponent<RunEffectViewEnemy>().StartView(false);
        Camera.main.GetComponent<PageMainGame>().buttonPause.GetComponent<ButtonStatus>().Enable();
        messageFindBox.SetActive(false);
    }

    void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)//xu ly ca 2 phia server va client
    {
        if (Modules.startViewOnline)
            Modules.panelViewEnemy.GetComponent<RunEffectViewEnemy>().StartView(true);
        CancelRoom();
        print("dis room");
    }

    void OnDisconnectedFromPhoton()//neu bi dis mang
    {
        if (Modules.startViewOnline)
            Modules.failNow++;
        ResetRoom();
        print("dis photon");
    }

    string ImportDataArray(List<string> listInput, string charSplit)
    {
        string result = "";
        for (int i = 0; i < listInput.Count; i++)
        {
            result += listInput[i];
            if (i < listInput.Count - 1) result += charSplit;
        }
        return result;
    }

    List<string> ExportDataArray(string dataInput, params char[] charSplit)
    {
        List<string> result = new List<string>();
        string[] data = dataInput.Split(charSplit);
        foreach (string str in data) result.Add(str);
        return result;
    }

    //KHU VUC PunRPC
    [PunRPC]
    void AskJoinRoom(string namePlayer)//xu ly khi client gui du lieu dang ky join len server
    {
        Modules.listNamePlayer.Add(namePlayer);
        //messageRoomBox.GetComponent<MessageRoom>().UpdateStatus();
        transform.GetComponent<PhotonView>().RPC("SendJoinRoom", PhotonTargets.Others, ImportDataArray(Modules.listNamePlayer, ";"));
    }

    [PunRPC]
    void SendJoinRoom(string dataServer)//server gui tra du lieu ve phia client
    {
        Modules.listNamePlayer = ExportDataArray(dataServer, ';');
        //messageNetworkBox.GetComponent<MessageNetwork>().ButtonCloseBox();
        //messageRoomBox.SetActive(true);
        //messageRoomBox.GetComponent<Animator>().SetTrigger("TriOpen");
        //messageRoomBox.GetComponent<MessageRoom>().CallStart();
        //messageRoomBox.GetComponent<MessageRoom>().UpdateStatus();
    }

    [PunRPC]
    void SendKickPlayer(string namePlayKick)//server gui thong diep kick player toi client
    {
        if (Modules.namePlayOnline != namePlayKick) return;
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    void SendSyncServer()//gan bien vao doi tuong view control o client
    {
        syncRead = GameObject.Find("PreSyncServer(Clone)");
        if (syncRead != null)
        {
            syncRead.GetComponent<SendViewEnemyNew>().myImage = imageShowLive;
            syncRead.GetComponent<SendViewEnemyNew>().isWriting = false;
            syncRead.GetComponent<SendViewEnemyNew>().CallStart();
        }
        syncWrite = PhotonNetwork.Instantiate(tranSyncClient.name, Vector3.zero, Quaternion.identity, 0);
        syncWrite.GetComponent<SendViewEnemyNew>().myImage = imageShowLive;
        syncWrite.GetComponent<SendViewEnemyNew>().isWriting = true;
        syncWrite.GetComponent<SendViewEnemyNew>().CallStart();
        transform.GetComponent<PhotonView>().RPC("SendSyncClient", PhotonTargets.MasterClient);
    }

    [PunRPC]
    void SendSyncClient()//gan bien vao doi tuong view control o server
    {
        syncRead = GameObject.Find("PreSyncClient(Clone)");
        if (syncRead != null)
        {
            syncRead.GetComponent<SendViewEnemyNew>().myImage = imageShowLive;
            syncRead.GetComponent<SendViewEnemyNew>().isWriting = false;
            syncRead.GetComponent<SendViewEnemyNew>().CallStart();
        }
        transform.GetComponent<PhotonView>().RPC("SendStartGame", PhotonTargets.All);
    }

    [PunRPC]
    void SendStartGame()//thuc hien show view va cac object o tat ca
    {
        cameraRender.gameObject.SetActive(true);
        //messageRoomBox.SetActive(false);
        Modules.startViewOnline = true;
        Camera.main.GetComponent<PageMainGame>().ClickPlayGame(false);
        messageFindBox.SetActive(false);
    }

    private Texture2D receiveImage;
    [PunRPC]
    void SendSyncAvatar(byte[] imageCode)//thuc hien dong bo avatar
    {
        receiveImage.LoadImage(imageCode);
        Rect rect = new Rect(0, 0, receiveImage.width, receiveImage.height);
        Modules.fbEnemyAvatar = Sprite.Create(receiveImage, rect, new Vector2(0.5f, 0.5f), 100);
        messageFindBox.GetComponent<MessageFindOpponent>().UpdateAvatarEnemy();
    }

    [PunRPC]
    void SendNullAvatar()//thuc hien dong bo avatar khi null
    {
        messageFindBox.GetComponent<MessageFindOpponent>().UpdateAvatarEnemy();
    }
}
