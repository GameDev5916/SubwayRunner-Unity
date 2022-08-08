using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageRoom : MonoBehaviour {

    public Text textTitleMessage;
    public Text nameCompetitor;
    public GameObject butKick, butStart;

    public void CallStart()
    {
        ResetDefault();
    }

    private void ResetDefault()
    {
        string textTitle = PhotonNetwork.room.Name;
        textTitleMessage.text = textTitle + " (1/2)";
        nameCompetitor.text = "COMPETITOR";
        butKick.GetComponent<ButtonStatus>().Disable();
        butStart.GetComponent<ButtonStatus>().Disable();
    }

    public void UpdateStatus()
    {
        if (Modules.listNamePlayer.Count < 2)
        {
            ResetDefault();
            return;
        }
        if (PhotonNetwork.isMasterClient)
        {
            textTitleMessage.text = PhotonNetwork.room.Name + " (2/2)";
            nameCompetitor.text = Modules.listNamePlayer[1];
            butKick.GetComponent<ButtonStatus>().Enable();
            butStart.GetComponent<ButtonStatus>().Enable();
        }
        else
        {
            textTitleMessage.text = PhotonNetwork.room.Name + " (2/2)";
            nameCompetitor.text = Modules.listNamePlayer[0];
        }
    }

    public void ButtonKick()
    {
        Modules.networkManager.GetComponent<NetworkMagnager>().KickPlayer();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }

    public void ButtonStart()
    {
        Modules.networkManager.GetComponent<NetworkMagnager>().StartGame();
    }

    public void ButtonClose()
    {
        Modules.networkManager.GetComponent<NetworkMagnager>().CancelRoom();
        Modules.PlayAudioClipFree(Modules.audioButton);
    }
}
