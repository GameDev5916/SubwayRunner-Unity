using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageFindOpponent : MonoBehaviour {

    public Image myAvatar, enemyAvatar;
    public GameObject iconLoading;
    public GameObject numLoading;
    private bool randomRoom = false;
    private int countTime = 3;
    [HideInInspector]
    public bool isClose = false;

    public void CallStart()
    {
        PlayerPrefs.DeleteKey("PUNCloudBestRegion");
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings("v1.0");
        Modules.fbEnemyAvatar = null;
        myAvatar.sprite = Modules.fbMyAvatar;
        if (myAvatar.sprite == null) myAvatar.sprite = Modules.iconAvatarNull;
        UpdateAvatarEnemy();
        iconLoading.SetActive(true);
        numLoading.SetActive(false);
        randomRoom = false;
        isClose = false;
        countTime = 3;
        CheckRooms();
	}

    void CheckRooms()
    {
        if (PhotonNetwork.inRoom)
        {
            if (PhotonNetwork.room.PlayerCount == 2)//neu du nguoi thi vao choi
            {
                Modules.networkManager.GetComponent<NetworkMagnager>().SyncAvatar();
                iconLoading.SetActive(false);
                numLoading.SetActive(true);
                numLoading.GetComponent<Text>().text = countTime.ToString();
                Invoke("RunCountTime", 1);
            }
            else Invoke("CheckRooms", 1);
        }
        else
        {
            if (PhotonNetwork.insideLobby)
            {
                if (!randomRoom)
                {
                    PhotonNetwork.JoinRandomRoom();
                    randomRoom = true;
                }
            }
            Invoke("CheckRooms", 1);
        }
    }

    void OnPhotonRandomJoinFailed()
    {
        if (isClose) return;
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    void RunCountTime()
    {
        countTime--;
        numLoading.GetComponent<Text>().text = countTime.ToString();
        if (countTime > 0)
            Invoke("RunCountTime", 1);
        else
        {
            CancelInvoke();
            Modules.networkManager.GetComponent<NetworkMagnager>().StartGame();
        }
    }

    public void UpdateAvatarEnemy()
    {
        enemyAvatar.sprite = Modules.fbEnemyAvatar;
        if (enemyAvatar.sprite == null) enemyAvatar.sprite = Modules.iconAvatarNull;
    }

    public void ButtonCloseBox()
    {
        CancelInvoke();
        transform.GetComponent<Animator>().SetTrigger("TriClose");
        if (PhotonNetwork.inRoom) Modules.networkManager.GetComponent<NetworkMagnager>().CancelRoom();
        Modules.PlayAudioClipFree(Modules.audioButton);
        isClose = true;
    }
}
